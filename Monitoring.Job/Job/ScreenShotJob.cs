using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Monitoring.Core;
using Monitoring.Core.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Monitoring.Job.Job
{
    [DisallowConcurrentExecution]
    public class ScreenShotJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IWebDriver driver;

        public ScreenShotJob(IServiceProvider provider)
        {
            _provider = provider;
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                driver.Manage().Window.Maximize();
                // Create a new scope
                using (var scope = _provider.CreateScope())
                {
                    // Resolve the Scoped service
                    var _context = scope.ServiceProvider.GetService<MonitoringContext>();

                    var dashbord = _context.Set<DepartmentDashboard>().Where(x => DateTime.Now.AddSeconds(-x.RefreshTime) > x.UpdateDate && x.RenderMode == Core.Enums.RenderMode.ScreenShot).ToList();

                    if (dashbord != null && dashbord.Count > 0)
                    {
                        foreach (var item in dashbord)
                        {
                            driver.Navigate().GoToUrl(item.Url);
                            var vcs = new VerticalCombineDecorator(new ScreenshotMaker());
                            var screenshot = driver.TakeScreenshot(vcs);
                            item.Image = screenshot;
                            item.UpdateDate = DateTime.Now;
                        }

                        _context.SaveChanges();
                    }

                }

                driver.Close();
                driver.Quit();
                driver.Dispose();
                return Task.CompletedTask;

            }
            catch (Exception e)
            {
                //if (e.Message.Contains("chrome not reachable"))
                //{
                //    driver.Quit();
                //    driver.Dispose();
                //    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                //    driver.Manage().Window.Maximize();

                //}

                return Task.CompletedTask;

            }

        }
    }
}
