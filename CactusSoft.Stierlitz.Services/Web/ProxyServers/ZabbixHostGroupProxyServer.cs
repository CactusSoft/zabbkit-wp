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
    public class ZabbixHostGroupProxyServer : ZabbixProxyServerBase, IHostGroupProxyServer
    {
        public ZabbixHostGroupProxyServer(IWebConfiguration webConfiguration, IWebChannel webChannel,
                                          IRequestBodyBuilder requestBodyBuilder) :
                                              base(webConfiguration, webChannel, requestBodyBuilder)
        {
        }

        public async Task<IEnumerable<HostGroup>> GetHostGroups(HostGroupsSortField[] sortFields, uint? limit)
        {
            ParamsRequestBody<GetHostGroupsParams> getHostGroupsRequestBody =
                RequestBodyBuilder.Build(new GetHostGroupsParams
        {
                                                 SortBy = sortFields != null ? sortFields.Select(sortField => sortField.ToDescriptionString()).ToArray() : null,
                                                 Limit = limit,
                                                 MonitoredHosts = true,
                                                 Output = new Query{Params = new List<string>{"name"}}
                                             });

            ResultResponseBody<IEnumerable<GetHostGroupsResult>> getHostGroupsResponseBody =
                await WebChannel.GetResponseAsync
                          <ParamsRequestBody<GetHostGroupsParams>, ResultResponseBody<IEnumerable<GetHostGroupsResult>>>
                          (getHostGroupsRequestBody);

            if (getHostGroupsResponseBody.Error != null)
            {
                throw new WebServiceException(getHostGroupsResponseBody.Error.Code,
                                              getHostGroupsResponseBody.Error.Message);
            }

            return getHostGroupsResponseBody.Result.Select(hostGroupResult => hostGroupResult.ToHostGroup()).Where(h => h != null).ToList();
        }

    }
}