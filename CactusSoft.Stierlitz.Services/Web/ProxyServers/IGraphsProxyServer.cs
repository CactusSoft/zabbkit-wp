using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public interface IGraphsProxyServer
    {
        Task<IList<Graph>> GetGraphsAsync(string groupId, string hostId);
        Task<byte[]> GetGraphImageAsync(string graphId, uint height, uint width, DateTime stime);
        Task<byte[]> GetGraphImageAsync(string graphId, uint height, uint width, DateTime stime, TimeSpan period);
    }
}
