using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Web.Exceptions;
using CactusSoft.Stierlitz.Services.Web.RequestBodies;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results;
using CactusSoft.Stierlitz.Services.Web.WebChannel;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public class ZabbixGraphsProxyServer : ZabbixProxyServerBase, IGraphsProxyServer
    {
        public static readonly TimeSpan MinPeriod = TimeSpan.FromHours(1);
        private readonly IServiceConfiguration _serviceConfiguration;

        public ZabbixGraphsProxyServer(IWebConfiguration webConfiguration, IWebChannel webChannel, IRequestBodyBuilder requestBodyBuilder, IServiceConfiguration serviceConfiguration) 
            : base(webConfiguration, webChannel, requestBodyBuilder)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public async Task<IList<Graph>> GetGraphsAsync(string groupId, string hostId)
        {
            ParamsRequestBody<GetGraphsParams> requestBody =
                RequestBodyBuilder.Build(new GetGraphsParams
                                             {
                                                 GroupId = groupId,
                                                 HostId = hostId,
                                                 Output = new Query { Params = new List<string>{"name"}}
                                             });

            ResultResponseBody<IList<GetGraphsResult>> responceBody = await WebChannel.GetResponseAsync
                    <ParamsRequestBody<GetGraphsParams>, ResultResponseBody<IList<GetGraphsResult>>>(requestBody);

            if (responceBody.Error != null)
            {
                throw new WebServiceException(responceBody.Error.Code,
                                              responceBody.Error.Message);
            }

            return responceBody.Result.Select(t => t.ToGraph()).Where(g => g != null).ToList();
        }

        public async Task<byte[]> GetGraphImageAsync(string graphId, uint height, uint width, DateTime stime)
        {
            return await GetGraphImageAsync(graphId, height, width, stime, MinPeriod);
        }

        public async Task<byte[]> GetGraphImageAsync(string graphId, uint height, uint width, DateTime stime, TimeSpan period)
        {
            if (period < MinPeriod)
            {
                period = MinPeriod;
            }
            var uriFormat = string.Concat(WebConfiguration.ServerUri, _serviceConfiguration.GraphPathFormat);
            var uri = string.Format(uriFormat, graphId, width, height, stime.ToUnixTicks(), (int)period.TotalSeconds);

            return await WebChannel.Download(uri);
        }
    }
}
