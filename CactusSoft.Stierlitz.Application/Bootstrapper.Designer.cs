using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application
{
	partial class Bootstrapper : PhoneBootstrapper
	{
		private PhoneContainer _container;

		partial void AddCustomConventions();

		partial void RegisterCustomObjects(PhoneContainer container);

		partial void InitializeInstance(object instance);

		#region Overrides of PhoneBootstrapper

        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
            var frame = new RadPhoneApplicationFrame();
#if DEBUG
                        frame.Navigated += (sender, args) =>
                            {
                                if (args.NavigationMode == NavigationMode.New && args.Content != null)
                                {
                                    AliveChecker.Monitor(args.Content);
                                }
                            };
#endif
            return frame;
        } 

		protected override void Configure()
		{
			_container = new PhoneContainer(RootFrame);

			_container.RegisterPhoneServices();

			RegisterCustomObjects(_container);

			AddCustomConventions();
		}

		protected override object GetInstance(Type service, string key)
		{
			var instance = _container.GetInstance(service, key);

			InitializeInstance(instance);

			return instance;
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			var instances = _container.GetAllInstances(service).ToList();

			foreach (var instance in instances)
			{
				InitializeInstance(instance);
			}

			return instances;
		}

		protected override void BuildUp(object instance)
		{
			_container.BuildUp(instance);
		}

		#endregion

		private static void AddGenericItemsControlConvention<T>(
			Func<DependencyProperty> headerTemplateProperty,
			Func<DependencyProperty> selectedItemProperty
			) where T : ItemsControl
		{
			ConventionManager.AddElementConvention<T>(
				ItemsControl.ItemsSourceProperty,
				"SelectedItem",
				"SelectionChanged").ApplyBinding =
				(viewModelType, path, property, element, convention) =>
				{
					if (ConventionManager
						.GetElementConvention(typeof(ItemsControl))
						.ApplyBinding(viewModelType, path, property, element, convention))
					{
						ConventionManager.ConfigureSelectedItem(
							element, selectedItemProperty(), viewModelType, path);
						ConventionManager.ApplyHeaderTemplate(
							element, headerTemplateProperty(), null, viewModelType);
						return true;
					}

					return false;
				};
		}
	}
}
