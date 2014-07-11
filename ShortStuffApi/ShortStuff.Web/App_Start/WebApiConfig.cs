// ShortStuff.Web
// WebApiConfig.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace ShortStuff.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new
            {
                id = RouteParameter.Optional
            });

            // Force JSON Output
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>()
                                      .First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
