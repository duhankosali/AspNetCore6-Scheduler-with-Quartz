using AspNetCore_QuartzExample.Models;
using Quartz;
using Quartz.Spi;

namespace AspNetCore_QuartzExample.Service
{
    public class SingletonJobFactory : IJobFactory
    {
        // Dependency Injection
        private readonly IServiceProvider _serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        // interface methods
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            //throw new NotImplementedException();

            Common.Logs($"NewJob at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), "NewJob" + DateTime.Now.ToString("hhmmss"));

            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            //throw new NotImplementedException();
        }
    }
}
