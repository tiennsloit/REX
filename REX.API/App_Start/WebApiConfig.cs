﻿using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using REX.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace REX.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Web API configuration and services

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var container = UnityConfig.RegisterComponents();

            //DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            // Web API routes

            //config.Routes.MapHttpRoute(
            //    name: "ContactApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { controller = "Contact", action = "Contact", id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DistrictApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { controller = "District", action = "District", id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
