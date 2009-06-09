using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuartzAdmin.web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{instanceName}/{groupName}/{itemName}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            /*
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{groupName}/{itemName}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
             * */
            routes.MapRoute(
                "Default2",                                              // Route name
                "{controller}/{action}/{instanceName}/{itemName}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

            routes.MapRoute(
                "Default3",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            /*
            routes.MapRoute(
                "Job",                                              // Route name
                "Job/Details/{groupName}/{jobName}",                           // URL with parameters
                new { controller = "Job", action = "Details", groupName = "", jobName="" }  // Parameter defaults
            );
            routes.MapRoute(
                "Trigger",                                              // Route name
                "Trigger/Details/{groupName}/{triggerName}",                           // URL with parameters
                new { controller = "Trigger", action = "Details", groupName = "", triggerName = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "FireTimes",                                              // Route name
                "Trigger/FireTimes/{groupName}/{triggerName}",                           // URL with parameters
                new { controller = "Trigger", action = "FireTimes", groupName = "", triggerName = "" }  // Parameter defaults
            );
             * */

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            log4net.ILog _log = log4net.LogManager.GetLogger("Quartz Admin Global Error");
            _log.Error(Server.GetLastError());
        }

    }
}