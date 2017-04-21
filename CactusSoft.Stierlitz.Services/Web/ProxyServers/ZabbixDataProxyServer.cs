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
    public class ZabbixDataProxyServer : ZabbixProxyServerBase, IDataProxyServer
    {
        public ZabbixDataProxyServer(IWebConfiguration webConfiguration, 
            IWebChannel webChannel, 
            IRequestBodyBuilder requestBodyBuilder) 
            : base(webConfiguration, webChannel, requestBodyBuilder)
        {
        }

        public async Task<IList<Item>> GetItemsAsync(string groupId, string hostId)
        {
            var requestBody = RequestBodyBuilder.Build(new GetDataParams
                {
                    GroupId = groupId,
                    HostId = hostId,
                    Output = Query.Extend,
                    ExpandDescription = true,
                    Monitored = true,
                    SortField = "description"
                });

            var responceBody = await WebChannel.GetResponseAsync
                    <ParamsRequestBody<GetDataParams>, ResultResponseBody<IList<GetDataResult>>>(requestBody);

            if (responceBody.Error != null)
            {
                throw new WebServiceException(responceBody.Error.Code,
                                              responceBody.Error.Message);
            }

            return responceBody.Result.Select(t => t.ToItem()).Where(g => g != null).ToList();
        }
    }
}
