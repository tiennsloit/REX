using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using REX.Core.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace REX.UnitTest
{
    [TestClass]
    public class TestBase
    {
        private static IUnityContainer _unityContainer = null;
        private static IServiceLocator _serviceLocator = null;

        private static IUnityContainer UnityContainer
        {
            get { return _unityContainer ?? (_unityContainer = UnityConfiger.GetUnityContainer()); }
        }

        private static IServiceLocator ServiceLocator
        {
            get { return _serviceLocator ?? (_serviceLocator = UnityContainer.Resolve<IServiceLocator>()); }
        }

        protected TService GetService<TService>()
        {
            return ServiceLocator.GetInstance<TService>();
        }
    }
}
