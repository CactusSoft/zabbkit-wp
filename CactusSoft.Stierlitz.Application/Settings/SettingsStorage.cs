using System;
using System.Linq.Expressions;
using System.IO.IsolatedStorage;
using CactusSoft.Stierlitz.Common;

namespace CactusSoft.Stierlitz.Application.Settings
{
    public class SettingsStorage : ISettingsStorage
    {
        private readonly IsolatedStorageSettings _appSettings = IsolatedStorageSettings.ApplicationSettings;

        public T GetValue<T>(Expression<Func<T>> property, T defaultValue)
        {
            string key = ((MemberExpression)property.Body).Member.Name;

            if (_appSettings.Contains(key))
            {
                return (T)_appSettings[key];    
            }

            return defaultValue;
        }

        public void SetValue<T>(Expression<Func<T>> property, T value)
        {
            //IsolatedStorageSettings will be saved automatically on app closed on deactivated
            string key = ((MemberExpression)property.Body).Member.Name;
            _appSettings[key] = value;

            #if DEBUG
            _appSettings.Save();
            #endif
        }

        public bool Contains<T>(Expression<Func<T>> property)
        {
            string key = ((MemberExpression)property.Body).Member.Name;
            return _appSettings.Contains(key);
        }
    }
}