using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using REX.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace REX.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(BuildUnityContainer()));

        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

           
            container.RegisterType<IBarcodeService, BarcodeService>();
            container.RegisterType<IProductTypeService, ProductTypeService>();


            return container;
        }
    }
}
