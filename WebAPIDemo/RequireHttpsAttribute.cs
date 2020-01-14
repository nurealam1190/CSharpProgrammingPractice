using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Text;

namespace WebAPIDemo
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p>USE HTTPS INSTEAD OF HTTP</p>");
                //overload version of above line of code to get content type as "text/html"
                //actionContext.Response.Content = new StringContent("<p>USE HTTPS INSTEAD OF HTTP</p>", Encoding.UTF8, "text/html");

                UriBuilder uribuilder = new UriBuilder(actionContext.Request.RequestUri);

                uribuilder.Scheme = Uri.UriSchemeHttps;
                uribuilder.Port = 44397;

                actionContext.Response.Headers.Location = uribuilder.Uri;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
            
        }
    }
}