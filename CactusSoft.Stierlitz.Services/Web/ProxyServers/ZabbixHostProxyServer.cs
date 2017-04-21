using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Web.Exceptions;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using CactusSoft.Stierlitz.Services.Web.RequestBodies;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results;
using CactusSoft.Stierlitz.Services.Web.WebChannel;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public class ZabbixHostProxyServer : ZabbixProxyServerBase, IHostProxyServer
    {
        public ZabbixHostProxyServer(IWebConfiguration webConfiguration, IWebChannel webChannel, IRequestBodyBuilder requestBodyBuilder) 
            : base(webConfiguration, webChannel, requestBodyBuilder)
        {
        }

        public async Task<IEnumerable<Host>> GetHostsAsync(string hostGroupId, HostSortField[] sortFields, string[] hostIds = null)
        {
            ParamsRequestBody<GetHostsParams> requestBody =
                RequestBodyBuilder.Build(new GetHostsParams()
                                             {
                                                 GroupId = hostGroupId,
                                                 SortBy = sortFields != null ? sortFields.Select(sortField => sortField.ToDescriptionString()).ToArray() : null,
                                                 Output = new Query{Params = new List<string>{"name"}},
                                                 HostIds = hostIds
                                             });

            ResultResponseBody<IEnumerable<GetHostsResult>> responceBody = await WebChannel.GetResponseAsync
                    <ParamsRequestBody<GetHostsParams>, ResultResponseBody<IEnumerable<GetHostsResult>>>(requestBody);

            if (responceBody.Error != null)
            {
                throw new WebServiceException(responceBody.Error.Code,
                                              responceBody.Error.Message);
            }

            return responceBody.Result.Select(t => t.ToHost()).Where(h => h != null).ToList();
        }
    }
}
