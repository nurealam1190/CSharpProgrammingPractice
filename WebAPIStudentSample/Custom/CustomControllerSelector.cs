using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Text.RegularExpressions;

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

            //Below commented section is to read the query string value from uri. If maintaining version by query string.

            /*  var queryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

               if(queryString["v"]!=null)
               {
                   versionNumber = queryString["v"];
               }

           */

            //Below line of codes are to read the custom header value, if managing versison using custom header

            /* string customHeader = "X-ProductsService-Version";
             if(request.Headers.Contains(customHeader))
             {
                 versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();

                 if(versionNumber.Contains(","))
                 {
                     versionNumber = versionNumber.Substring(0, versionNumber.IndexOf(","));
                 }

             }
             */

            //Below lines of codes is for versioning using accept-header value

            /*
                var acceptHeader = request.Headers.Accept.Where(p=>p.Parameters.Count(q=>q.Name.ToLower()=="version")>0);
                if(acceptHeader.Any())
                {
                    versionNumber = acceptHeader.First().Parameters.First(p => p.Name.ToLower() == "version").Value;
                }
            */

            //Below lines of code is to read the version number from regular expression for versioning using media type
            // Actual Accept header will be something like "Accept: application/vnd.redionstech.products.v2+xml"
            // Converted the similar regular expression to read the version

           // var regex = @"application\/vnd\.pragimtech\.([a-z]+)\.v([0-9]+)\+([a-z]+)";

            //here we can add group name as well by mentioning ?<groupname>
            var regex = @"application\/vnd\.pragimtech\.([a-z]+)\.v(?<version>[0-9]+)\+([a-z]+)";

            var acceptHeader = request.Headers.Accept.Where(a => Regex.IsMatch(a.MediaType, regex, RegexOptions.IgnoreCase));

            if(acceptHeader.Any())
            {
                var match = Regex.Match(acceptHeader.First().MediaType, regex, RegexOptions.IgnoreCase);

                //by index of group
               // versionNumber = match.Groups[2].Value;

                //by group name
                versionNumber = match.Groups["version"].Value;
            }

            if (versionNumber=="1")
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