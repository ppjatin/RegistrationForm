using RegistrationForm.Data;
using RegistrationForm.Repositories;
using RegistrationForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Services.Description;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Integration.Mvc;


namespace RegistrationForm
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            // Register your MVC controllers (depends on Autofac.Mvc5)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register your ApplicationDbContext
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();

            // Register your repositories and services
            builder.RegisterType<UserRegistrationRepository>().As<IUserRegistrationRepository>().InstancePerRequest();
            builder.RegisterType<UserRegistrationService>().AsSelf().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
       
    }
}
