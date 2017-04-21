using System;
using System.Collections.Generic;
using CactusSoft.Stierlitz.Application.ViewModels;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Test
{
    public class Bootstrapper : PhoneBootstrapper
    {
        private PhoneContainer _container; 

        protected override void Configure()
        {
            _container = new PhoneContainer(RootFrame);

            _container.RegisterPhoneServices();

            RegisterServices();
            RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            _container.PerRequest<LoginPageViewModel>();
        }

        private void RegisterServices()
        {
            
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }  
    }
}
