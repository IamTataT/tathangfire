using Hangfire;
using Hangfire.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using tathangfire.Config;

namespace tathangfire
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.IoC.Container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            WindsorConfigurator.Configure();
            JobActivator.Current = new WindsorJobActivator(IoC.IoC.Container.Kernel);
        }
    }
}
