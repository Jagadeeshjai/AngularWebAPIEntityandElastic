using AngularAspNetAPIElasticsearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AngularAspNetAPIElasticsearch
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AutoMapperConfig.Initialize();
            Database.SetInitializer(new DatabaseInitializer());
            using (var context = new DatabaseContext())
            {
                context.Database.Initialize(force: true);
            }
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DatabaseInitializer());
        }
    }
}

 