using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Dashboard.RecurringJobExtensions
{
    public class RecurringJobExtensionOptions
    {
        public string ApiPath { get; set; } = "/addRecurringJob";
        public string RecurringEndPath { get; set; } = "/recurring";
        public string DefaultTimeZone { get; set; } = TimeZoneInfo.Local.Id;
        public string DefaultCron { get; set; } = Cron.Minutely();
        public string DefaultQueue { get; set; } = States.EnqueuedState.DefaultQueue;

        public string AddRecurringJobButtonName { get; set; } = "Add Recurring Job";
        public string CloseButtonName { get; set; } = "Close";
        public string SumbitButtonName { get; set; } = "Submit";
    }
}
