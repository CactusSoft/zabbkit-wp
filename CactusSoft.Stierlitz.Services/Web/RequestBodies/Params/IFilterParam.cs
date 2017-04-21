namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    interface IFilterParam<T>
    {
        T Filter
        {
            get;
        }
    }
}
