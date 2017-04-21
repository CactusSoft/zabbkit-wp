namespace CactusSoft.Stierlitz.Services.Web.Configurations
{
    public interface IServiceConfiguration
    {
        string ApiPath { get; }
        string GraphPathFormat { get; }
        string Version { get; }
        string ResolveMethod<T>();
    }
}