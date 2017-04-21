using CactusSoft.Stierlitz.Application.ViewModels;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusSoft.Stierlitz.Test.ViewModels
{
    [TestClass]
    public class LoginPageViewModelTester
    {
        private LoginPageViewModel _loginPageViewModel;
        
        [TestInitialize]
        public void Initialize()
        {
            _loginPageViewModel = IoC.Get<LoginPageViewModel>();
            Assert.IsNotNull(_loginPageViewModel);
        }

        [TestMethod]
        public void CanLoginFlagTest()
        {
            

        }
    }
}
