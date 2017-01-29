using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using REX.Core.Services;
using System.Web.Http;

namespace REX.API
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IDistrictService, DistrictService>();
           
            // return the container so it can be used for the dependencyresolver.  
            return container;
        }
    }
}