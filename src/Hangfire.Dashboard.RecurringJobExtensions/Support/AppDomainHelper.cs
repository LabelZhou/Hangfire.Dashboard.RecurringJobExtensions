using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hangfire.Dashboard.RecurringJobExtensions.Support
{

        public static class AppDomainHelper
        {
            public static Assembly GetAssemblyByName(string assemblyName)
            {
            var listOfAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                return listOfAssemblies.FirstOrDefault(a => a.GetName().Name == assemblyName);
            }
        }
    
}
