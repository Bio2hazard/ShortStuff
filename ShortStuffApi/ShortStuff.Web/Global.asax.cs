// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The Web Api global startup file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Web
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    ///     The Web Api global startup file.
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// The Wep Api application_start.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}