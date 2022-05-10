using AspNetCore_QuartzExample.Models;
using Quartz;

namespace AspNetCore_QuartzExample.Service
{
    // İş hatırlatıcı
    public class JobReminders : IJob
    {
        public JobReminders() // constructor
        {

        }

        // interface method
        public Task Execute(IJobExecutionContext context)
        {
            //throw new NotImplementedException();

            Common.Logs($"JobReminders at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), "JobReminders" + DateTime.Now.ToString("hhmmss"));

            return Task.CompletedTask;
        }
    }
}
