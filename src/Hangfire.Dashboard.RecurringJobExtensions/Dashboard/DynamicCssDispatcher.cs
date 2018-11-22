using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Annotations;

namespace Hangfire.Dashboard.RecurringJobExtensions.Dashboard
{
    public class DynamicCssDispatcher : IDashboardDispatcher
    {
        public Task Dispatch([NotNull] DashboardContext context)
        {
            var builder = new StringBuilder();

            builder.AppendLine(".console, .console .line-buffer {")
                .Append("    color: ").Append("red").AppendLine(";")
                .AppendLine("}");

            return context.Response.WriteAsync(builder.ToString());
        }
    }
}
