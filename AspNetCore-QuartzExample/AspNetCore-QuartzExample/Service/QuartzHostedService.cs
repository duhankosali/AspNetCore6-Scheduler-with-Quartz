using AspNetCore_QuartzExample.Models;
using Quartz;
using Quartz.Spi;

namespace AspNetCore_QuartzExample.Service
{
    public class QuartzHostedService : IHostedService
    {
        // Dependency Injection --> Bağımlılık kontrolü ve yönetimi için kullanılır.
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<MyJob> _myJobs;

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<MyJob> myJobs) // constructor
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _myJobs = myJobs;
        }


        // properties
        public IScheduler Scheduler { get; set; }


        // interface methods
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Common.Logs($"StartAsync at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), "StartAsync" + DateTime.Now.ToString("hhmmss"));

            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            foreach (var myJob in _myJobs)
            {
                var job = CreateJob(myJob);
                var trigger = CreateTrigger(myJob);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Common.Logs($"StopAsync at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), "StopAsync" + DateTime.Now.ToString("hhmmss"));

            await Scheduler?.Shutdown(cancellationToken);
        }


        // methods
        private static IJobDetail CreateJob(MyJob myJob) // iş oluştur
        {
            var type = myJob.Type;
            return JobBuilder.Create(type).WithIdentity(type.FullName).WithDescription(type.Name).Build();
        }
        private static ITrigger CreateTrigger(MyJob myJob) // tetikleme oluştur
        {
            var type = myJob.Type;
            return TriggerBuilder.Create().WithIdentity($"{myJob.Type.FullName}.trigger").WithCronSchedule(myJob.Expression).WithDescription(myJob.Expression).Build();
        }

    }
}
