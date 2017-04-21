using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ZabbixTriggerProxyServer : ZabbixProxyServerBase, ITriggerProxyServer
    {
        public ZabbixTriggerProxyServer(IWebConfiguration webConfiguration, IWebChannel webChannel,
                                        IRequestBodyBuilder requestBodyBuilder) :
            base(webConfiguration, webChannel, requestBodyBuilder)
        {
        }

        public async Task<IEnumerable<Trigger>> GetTriggers(string hostId, uint? limit, TriggersSortField[] sortFields, Select selectHosts, IList<string> triggerIds = null)
        {
            ParamsRequestBody<GetTriggersParams> requestBody =
                RequestBodyBuilder.Build(new GetTriggersParams()
        {
                                                 TriggerIds = triggerIds,
                                                 HostId = hostId,
                                                 Limit = limit,
                                                 ActiveOnly = true,
                                                 SelectHosts = selectHosts == Select.None ? Query.None : Query.Extend,
                                                 SortBy = sortFields != null ? sortFields.Select(field => field.ToDescriptionString()).ToArray() : null,
                                                 ExpandDescription = true,
                                                 Output = new Query { Params = new List<string> { "description", "priority", "value" } }
                                             });

            ResultResponseBody<IEnumerable<GetTriggersResult>> responceBody = await WebChannel.GetResponseAsync
                    <ParamsRequestBody<GetTriggersParams>, ResultResponseBody<IEnumerable<GetTriggersResult>>>(requestBody);

            if (responceBody.Error != null)
            {
                throw new WebServiceException(responceBody.Error.Code,
                                              responceBody.Error.Message);
            }

            return responceBody.Result.Select(t => t.ToTrigger()).Where(t => t != null).ToList();
        }
    }
}