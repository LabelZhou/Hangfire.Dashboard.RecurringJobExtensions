# Hangfire.Dashboard.RecurringJobExtensions

[![NuGet](https://img.shields.io/nuget/v/Hangfire.Dashboard.RecurringJobExtensions.svg)](https://www.nuget.org/packages/Hangfire.Dashboard.RecurringJobExtensions/)
![MIT License](https://img.shields.io/badge/license-MIT-orange.svg)



## Installation
NuGet Package Console
```
PM> Install-Package Hangfire.Dashboard.RecurringJobExtensions
```

In .NET Core's Startup.cs:
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddHangfire(config =>
    {
        config.UseSqlServerStorage("connectionSting");
        config.UseDashboardRecurringJobExtensions();
    });
}
```

Otherwise:
```c#
GlobalConfiguration.Configuration
    .UseSqlServerStorage("connectionSting")
    .UseConsole();
```

## Usage

![dashboard](dashboard.png)
e.g.:
```json
{
  "JobId": "Demo",
  "Type": "Namespace.classname, assemblyname",
  "MethodName": "methodName",
  "Cron": "* * * * *",
  "Queue": "default",
  "TimeZone": "Asia/Shanghai"
}
```

## License
MIT License

Copyright (c) 2018 LabelZhou

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
