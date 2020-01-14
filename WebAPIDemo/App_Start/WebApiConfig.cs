using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using WebApiContrib.Formatting.Jsonp;
using System.Web.Http.Cors;

namespace WebAPIDemo
{
    //creating a class for customformatter
    //public class CustomJsonFormatter : JsonMediaTypeFormatter
    //{
    //    public CustomJsonFormatter()
    //    {
    //        this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
    //    }

    //    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
    //    {
    //        base.SetDefaultContentHeaders(type, headers, mediaType);
    //        headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    }
    //}
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //Registering a class "RequireHttpsAttribute.cs" class which will be used as a filter. 
            // This will be used to auto redirect a http request to https request.

            config.Filters.Add(new RequireHttpsAttribute());

            //Registering a class which provides basic authentication.
            config.Filters.Add(new BasicAuthenticationAttribute());



            //Registering new custom formatter which created above

            // config.Formatters.Add(new CustomJsonFormatter());

            //If i want response in json when request from browser so below lines need to add.
            //Even after adding below line when request from fiddler or client application it will give response as the type mention in application header. Only browser request will be responded as json

            // config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            /*--above line of code fullfill our requirement to get json response when request from browser but when we
            investigate the response from fiddler we will see in header that Content-Type is "text/html" which is 
            contradictory as we are getting response in json so it should be "application/json". To achieve this create custom formatter and register it. 
            Refer above class "CustomJsonFormatter" codes. --*/

            //This line is to get the response always in json formatter irrespective of the type mentioned in Accept header while request
            //Here we have removed the xmlformatter so it will always return in json format.

            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //This line is to get the response always in xml formatter irrespective of the type mentioned in Accept header while request
            //Here we have removed the jsonformatter so it will always return in json format.

            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //Below two line is for formatting and serialized the response into indented manner and in camelcasing

            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCaseProprtyNamesContarctResolver();


            // Below line of codes are for adding jsonp formatter in config object so that we can use cross domain call using jsonp

            //var jsonpformatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpformatter);


            // Below line of code when we want to call service from cross domain by enabling CORS.
            //Either this or above written lines of code using JSONP can be used for cross domain call.

            //first param is origins like http://localhost:12345 or https://facebook.com we can mention as many site we want by comma seperated.
            //we can use "*" if we want all origin

            //second param is headers. we can mention as many headers we want by comma seperated.
            //we can use "*" if we want all headers

            //Third param is methods like GET,POST,PUT. we can mention as many methods we want by comma seperated.
            //we can use "*" if we want all methods

            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:12345, https://facebook.com", "*", "GET,PUT,POST");
            EnableCorsAttribute cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);
            //Above enable cors enables the cors globally for all controller and methods.
            //This can be done (enable/disable) for specific controller or method using below code in controller
            // [EnableCorsAttribute("*","*","*")]
            // [DisableCors]

        }
    }
}
