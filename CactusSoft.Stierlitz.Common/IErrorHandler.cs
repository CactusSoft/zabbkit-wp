using System;

namespace CactusSoft.Stierlitz.Common
{
    public interface IErrorHandler
    {
        void Handle(Exception exception);
    }
}
