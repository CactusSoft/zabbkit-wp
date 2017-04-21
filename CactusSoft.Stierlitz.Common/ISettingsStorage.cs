using System;
using System.Linq.Expressions;

namespace CactusSoft.Stierlitz.Common
{
    public interface ISettingsStorage
    {
        T GetValue<T>(Expression<Func<T>> property, T defaultValue);
        void SetValue<T>(Expression<Func<T>> property, T value);
        bool Contains<T>(Expression<Func<T>> property);
    }
}
