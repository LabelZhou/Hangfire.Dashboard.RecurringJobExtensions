using Hangfire.Annotations;
using Hangfire.Dashboard.RecurringJobExtensions.Dashboard;
using Hangfire.Dashboard.RecurringJobExtensions.Server;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hangfire.Dashboard.RecurringJobExtensions
{
    public static class GlobalConfigurationExtension
    {

        [PublicAPI]
        public static IGlobalConfiguration UseDashboardRecurringJobExtensions(this IGlobalConfiguration config, RecurringJobExtensionOptions options = null)
        {
            options = options ?? new RecurringJobExtensionOptions();

            //处理http请求
            DashboardRoutes.Routes.Add(options.ApiPath, new AddRecurringJobDispatcher(new RecurringJobRegistry()));

            // register additional dispatchers for CSS and JS
            var assembly = typeof(AddRecurringJobDispatcher).GetTypeInfo().Assembly;

            var jsPath = DashboardRoutes.Routes.Contains("/js[0-9]+") ? "/js[0-9]+" : "/js[0-9]{3}";
            DashboardRoutes.Routes.Append(jsPath, new EmbeddedResourceDispatcher(assembly, "Hangfire.Dashboard.RecurringJobExtensions.Resource.jsoneditor.js"));
            DashboardRoutes.Routes.Append(jsPath, new DynamicJsDispatcher(options));
            DashboardRoutes.Routes.Append(jsPath, new EmbeddedResourceDispatcher(assembly, "Hangfire.Dashboard.RecurringJobExtensions.Resource.recurringjobextensions.js"));


            var cssPath = DashboardRoutes.Routes.Contains("/css[0-9]+") ? "/css[0-9]+" : "/css[0-9]{3}";
            DashboardRoutes.Routes.Append(cssPath, new EmbeddedResourceDispatcher(assembly, "Hangfire.Dashboard.RecurringJobExtensions.Resource.jsoneditor.css"));

            return config;
        }
    }
}
