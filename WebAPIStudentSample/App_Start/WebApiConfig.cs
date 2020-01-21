using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;
using WebAPIStudentSample.Custom;

namespace WebAPIStudentSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Below line of code for versioning using query string. Created a new customcontrollerselector class
            //which i am going to refer here.      

           config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelector(config));


            //This is the default route created when you create webapi project.
            config.Routes.MapHttpRoute(
                name: "DefaultConfig",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ); ;

            //// versioning using convention based routing
            ////version1
            //config.Routes.MapHttpRoute(
            //    name: "Version1Config",
            //    routeTemplate: "api/v1/products/{id}",
            //    defaults: new { id = RouteParameter.Optional, Controller = "ProductsV1" }
            //); ;
            ////version2
            //config.Routes.MapHttpRoute(
            //    name: "Version2Config",
            //    routeTemplate: "api/v2/products/{id}",
            //    defaults: new { id = RouteParameter.Optional, Controller = "ProductsV2" }
            //); ;


            //adding a custom formmatter to support in web api.

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.pragimtech.products.v1+json"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.pragimtech.products.v2+json"));

            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.pragimtech.products.v1+xml"));
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.pragimtech.products.v2+xml"));
        }
    }
}
