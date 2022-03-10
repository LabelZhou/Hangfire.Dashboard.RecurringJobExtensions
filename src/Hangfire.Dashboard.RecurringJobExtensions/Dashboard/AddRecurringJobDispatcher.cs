using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire.Annotations;
using Hangfire.Dashboard.RecurringJobExtensions.Server;
using Hangfire.Dashboard.RecurringJobExtensions.Support;

namespace Hangfire.Dashboard.RecurringJobExtensions.Dashboard
{
    public class AddRecurringJobDispatcher : IDashboardDispatcher
    {
        private readonly IRecurringJobRegistry _recurringJobRegistry;

        public AddRecurringJobDispatcher(IRecurringJobRegistry recurringJobRegistry)
        {
            _recurringJobRegistry = recurringJobRegistry;
        }

        public async Task Dispatch([NotNull] DashboardContext context)
        {
            if (!"POST".Equals(context.Request.Method, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                return;
            }

            var options = await GetRecurringJobInfoAsync(context);

            if (options == null
                || string.IsNullOrWhiteSpace(options.Type)
                || string.IsNullOrWhiteSpace(options.Cron)
                || string.IsNullOrWhiteSpace(options.JobId)
                || string.IsNullOrWhiteSpace(options.MethodName)
                || string.IsNullOrWhiteSpace(options.Queue)
                || string.IsNullOrWhiteSpace(options.TimeZone))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Parameters are null or empty");
                return;
            }

            Type jobType = GetJobType(options);
            if (jobType == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync($"{options.Type} error");
                return;
            }

            var method = jobType.GetTypeInfo().GetDeclaredMethod(options.MethodName);

            if (method == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync($"{options.MethodName} error");
                return;
            }
            TimeZoneInfo timeZoneInfo = null;
            try
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(options.TimeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync($"{options.TimeZone} error");
                return;
            }

            _recurringJobRegistry.Register(options.JobId, method, options.Cron, timeZoneInfo, options.Queue);
            await context.Response.WriteAsync("Successfully Added/Updated");
        }

        private Type GetJobType(RecurringJobInfo options)
        {
            try
            {
                Type jobType = null;
                var assembly = AppDomainHelper.GetAssemblyByName(options.AssemblyName);
                if (assembly == null)
                {
                    jobType = Type.GetType(options.Type, false, true);
                }
                else
                {
                    var className = options.Type.Split('.')[0];
                    var methodName = options.Type.Split('.')[1];
                    foreach (var item in assembly.GetTypes())
                    {
                        if (item.IsClass && item.Name == className)
                        {
                            jobType = item;
                            break;
                        }
                    }
                }
                return jobType;
            }
            catch
            {
                return null;
            }
        }

        private async Task<RecurringJobInfo> GetRecurringJobInfoAsync(DashboardContext _context)
        {
            var parameterJson = await _context.Request.GetFormValuesAsync("json");
            if (parameterJson?.Count == 1)
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<RecurringJobInfo>(parameterJson[0]);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}
