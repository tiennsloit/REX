using Microsoft.Practices.Unity;
using REX.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Unity
{
    public static class UnityConfiger
    {
        public static IUnityContainer GetUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IDistrictService, DistrictService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IContactService, ContactService>();

            // return the container so it can be used for the dependencyresolver.  
            return container;
        }
    }
}
