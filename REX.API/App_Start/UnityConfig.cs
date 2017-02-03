using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using REX.Core.Services;
using System.Web.Http;
using REX.Core.Unity;

namespace REX.API
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            return UnityConfiger.GetUnityContainer();
        }
    }
}