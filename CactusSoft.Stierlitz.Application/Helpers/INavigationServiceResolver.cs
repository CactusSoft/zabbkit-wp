using System;
using System.Collections.Generic;

namespace CactusSoft.Stierlitz.Application.Helpers
{
    public interface INavigationServiceResolver
    {

        bool TryResolve();
        void Preserve();
        void TryRestore();
    }
}
