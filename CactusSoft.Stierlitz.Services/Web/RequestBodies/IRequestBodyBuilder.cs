namespace CactusSoft.Stierlitz.Services.Web.RequestBodies
{
    public interface IRequestBodyBuilder
    {
        ParamsRequestBody<T> Build<T>(T requestParams);
    }
}
