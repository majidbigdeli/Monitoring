using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Job.Job.Settings
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void ReturnJob(IJob job) { }
    }
}
