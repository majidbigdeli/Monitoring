using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Monitoring.Core;
using Monitoring.Core.Models;
using Monitoring.Job.Job;
using Monitoring.Job.Job.Settings;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Monitoring.Job
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            await new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                var sp = services.BuildServiceProvider();

                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                });

                services.AddSingleton<IJobFactory, SingletonJobFactory>();
                services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                services.AddSingleton<ScreenShotJob>();


                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

                 var con = configuration.GetConnectionString("SqlConnectionString");
                services.AddDbContext<MonitoringContext>(options => options.UseSqlServer(con));



                var logger = sp.GetService<ILogger<Program>>();
                services.AddSingleton(typeof(ILogger), logger);

                services.AddSingleton(new JobSchedule(
                    jobType: typeof(ScreenShotJob),
                    cronExpression: "0 * * ? * *"));
                services.AddHostedService<QuartzHostedService>();
            }).RunConsoleAsync();

        }

    }
}
