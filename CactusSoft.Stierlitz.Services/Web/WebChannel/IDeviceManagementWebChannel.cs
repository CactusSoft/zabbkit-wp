using System.Threading.Tasks;
using CactusSoft.Stierlitz.Services.Web.RequestBodies;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies;

namespace CactusSoft.Stierlitz.Services.Web.WebChannel
{
    public interface IDeviceManagementWebChannel
    {
        Task<TOut> GetResponseAsync<TIn, TOut>(TIn request)
            where TIn : RequestBodyBase
            where TOut : ResponseBodyBase;

        Task<byte[]> Download(string url);
    }
}
