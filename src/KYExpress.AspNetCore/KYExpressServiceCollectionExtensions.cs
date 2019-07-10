using Autofac;
using Autofac.Extensions.DependencyInjection;
using KYExpress.AspNetCore.Web.Filters;
using KYExpress.Core.Domain;
using KYExpress.Core.Domain.Query;
using KYExpress.Core.Domain.Uow;
using KYExpress.Core.EventBus;
using KYExpress.Core.EventBus.ServiceBus.Mediator;
using KYExpress.Dapper;
using KYExpress.Dapper.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KYExpressServiceCollectionExtensions
    {
  
        public static IServiceProvider AddKYExpress(this IServiceCollection services, Action<KYExprressOptions> optionsAction)
        {
            var options = new KYExprressOptions();
            optionsAction?.Invoke(options);

            services.AddDapper(options.ConnectionString);
            services.AddDefaultEventBus();
            services.AddFilters();

            var container = new ContainerBuilder();
            container.Populate(services);
            container.InitializeModules(options.Modules);
            return new AutofacServiceProvider(container.Build());
        }

        private static void AddFilters(this IServiceCollection services)
        {
            services.AddTransient<KYExpressResultFilter>();
            services.AddTransient<KYExpressExceptionFilter>();
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.Filters.AddService(typeof(KYExpressResultFilter));
                mvcOptions.Filters.AddService(typeof(KYExpressExceptionFilter));
            });
        }

        public static void InitializeModules(this ContainerBuilder container, Assembly[] modules)
        {
            if (modules == null || modules.Length == 0) return;
           
            foreach (var module in modules)
            {
                InitializeEventHandler(container, module);
            }

        }

        public static void InitializeEventHandler(this ContainerBuilder container, Assembly module)
        {
            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                container.RegisterAssemblyTypes(module)
               .AsClosedTypesOf(mediatrOpenType)
               .AsImplementedInterfaces();
            }
        }

        private static void AddDapper(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(typeof(IRepository<>), typeof(DapperRepository<>));
            services.AddScoped<IUnitOfWork, DapperUnitOfWork>();
            services.AddTransient<IQueryService>(provider => {
                var dapperQueryService = new DapperQueryService(provider.GetRequiredService<IDbConnectionFactory>(), connectionString);
                return dapperQueryService; });
            services.AddSingleton<IDbConnectionFactory, DapperDbConnectionFactory<SqlConnection>>();
            services.AddTransient<IDbConnection>(provider => { return new SqlConnection(connectionString); });

            DapperConfiguration.Initialize();
        }

        private static void AddDefaultEventBus(this IServiceCollection services)
        {
            services.AddScoped<ServiceFactory>(p => p.GetService);
            services.AddScoped<IEventBus, DefaultEventBus>();
        }
    }

    public class KYExprressOptions
    {
        public string ConnectionString { get; set; }

        public Assembly[] Modules { get; set; }
    }
}
