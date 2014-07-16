// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The web api config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Web
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Newtonsoft.Json.Serialization;

    /// <summary>
    ///     The web api config.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// The HTTP Route registration.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });

            // Force JSON Output
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}