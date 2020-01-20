using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace WebAPIStudentSample.Custom
{
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        HttpConfiguration _config;
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();
            string controller="";

            if(routeData.Route.RouteTemplate != "")
            {
                  controller = routeData.Values["controller"].ToString();
            }           
            

            string versionNumber = "1";

            var queryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

            if(queryString["v"]!=null)
            {
                versionNumber = queryString["v"];
            }

            if(versionNumber=="1")
            {
                controller = controller + "V1";

            }
            else
            {
                controller = controller + "V2";
            }

            HttpControllerDescriptor controllerDescriptor;

            if(controllers.TryGetValue(controller, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            else
            {
                return null;
            }
            


        }
    }
    
}