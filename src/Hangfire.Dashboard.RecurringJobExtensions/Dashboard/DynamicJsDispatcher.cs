using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Annotations;

namespace Hangfire.Dashboard.RecurringJobExtensions.Dashboard
{
    public class DynamicJsDispatcher : IDashboardDispatcher
    {
        private readonly RecurringJobExtensionOptions _options;

        public DynamicJsDispatcher(RecurringJobExtensionOptions options)
        {
            _options = options;
        }
        public Task Dispatch([NotNull] DashboardContext context)
        {
            var sb = new StringBuilder();

            sb.Append("(function(hangfire){")

                .Append("hangfire.recurringJobExtensionsConfiguration=hangfire.recurringJobExtensions || {};")
                .Append($"hangfire.recurringJobExtensionsConfiguration.DefaultTimeZone='{_options.DefaultTimeZone}';")
                .AppendFormat($"hangfire.recurringJobExtensionsConfiguration.AddRecurringJobUrl = '{context.Request.PathBase}{_options.ApiPath}';")
                .Append($"hangfire.recurringJobExtensionsConfiguration.DefaultCron='{_options.DefaultCron}';")
                .Append($@"hangfire.recurringJobExtensionsConfiguration.DefaultQueue='{_options.DefaultQueue}';")
                .Append($@"hangfire.recurringJobExtensionsConfiguration.AddRecurringJobButtonName='{_options.AddRecurringJobButtonName}';")
                .Append($@"hangfire.recurringJobExtensionsConfiguration.SumbitButtonName='{_options.SumbitButtonName}';")
                .Append($@"hangfire.recurringJobExtensionsConfiguration.CloseButtonName='{_options.CloseButtonName}';")
                .Append($@"hangfire.recurringJobExtensionsConfiguration.AssemblyName='{_options.AssemblyName}';")
                .AppendFormat("hangfire.recurringJobExtensionsConfiguration.NeedAddRecurringJobButton = location.href.substring(location.href.length-'{0}'.length)=='{0}';", _options.RecurringEndPath)
                .Append("})(window.Hangfire=window.Hangfire||{});")
            ;
            return context.Response.WriteAsync(sb.ToString());
        }
    }
}
