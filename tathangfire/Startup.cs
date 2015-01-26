using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Windsor;
using Hangfire.Dashboard;
using System.Collections.Generic;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(tathangfire.Startup))]
namespace tathangfire
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfire(config =>
            {
                
                config.UseSqlServerStorage(ConfigurationManager.AppSettings["hangfireConnectionString"]);
                config.UseServer();

                config.UseAuthorizationFilters(new MyRestrictiveAuthorizationFilter());
            });
        }

        public class MyRestrictiveAuthorizationFilter : IAuthorizationFilter
        {
            public bool Authorize(IDictionary<string, object> owinEnvironment)
            {
                // In case you need an OWIN context, use the next line.
                return true;
            }
        }
    }
}
