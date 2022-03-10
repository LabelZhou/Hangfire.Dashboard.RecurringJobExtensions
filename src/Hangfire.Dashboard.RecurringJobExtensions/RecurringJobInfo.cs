using Hangfire.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.Dashboard.RecurringJobExtensions
{
    /// <summary>
    /// recurring job info 
    /// </summary>
    internal class RecurringJobInfo
    {
        /// <summary>
        /// Recurring job ID
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// e.g.: "namespace.clasName, assemblyName"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Execute method name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets cron expression
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// gets or sets time zone info id
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// gets or sets queue
        /// </summary>
        public string Queue { get; set; }

        public string AssemblyName { get; set; }
    }
}
