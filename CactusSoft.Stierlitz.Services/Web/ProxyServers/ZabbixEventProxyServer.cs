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
    public class ZabbixEventProxyServer : ZabbixProxyServerBase, IEventProxyServer
    {
        public ZabbixEventProxyServer(IWebConfiguration webConfiguration, IWebChannel webChannel,
                                      IRequestBodyBuilder requestBodyBuilder) :
            base(webConfiguration, webChannel, requestBodyBuilder)
        {
        }

        public async Task<IOrderedEnumerable<Event>> GetEvents(string triggerId, uint? limit, EventSortField sortBy, Select selectHosts, Select selectTriggers)
        {
            ParamsRequestBody<GetEventsParams> requestBody =
                RequestBodyBuilder.Build(new GetEventsParams
                {
                    TriggerId = triggerId,
                    Limit = limit,
                    SortBy = new[]{ sortBy.ToDescriptionString() },
                    Order = SortOrderField.Desc.ToDescriptionString(),
                    Filter = new Filter { Value = new[] { 0, 1 }, Object = new[] { 0 }},
                    SelectHosts = selectHosts == Select.None ? Query.None : Query.Extend,
                    SelectTriggers = selectTriggers == Select.None ? Query.None : Query.Extend,
                    Output = new Query { Value = QueryValues.Extend}
                });

            ResultResponseBody<IEnumerable<GetEventsResult>> responceBody = await WebChannel.GetResponseAsync
                    <ParamsRequestBody<GetEventsParams>, ResultResponseBody<IEnumerable<GetEventsResult>>>(requestBody);

            if (responceBody.Error != null)
            {
                throw new WebServiceException(responceBody.Error.Code,
                                              responceBody.Error.Message);
            }

            // Where(t => t.Object == 0) use only triggers
            return responceBody.Result.Where(t => t.Object == 0).Select(t => t.ToEvent()).Where(e => e != null).OrderByDescending(e => e.Time);
        }


    }
}