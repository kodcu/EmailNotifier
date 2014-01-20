using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notifier.Scheduler
{
    public class SampleJob
        : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("OK");
        }
    }

    public  class SampleJobExecuter
    {

        public virtual void Run()
        {
            // Scheduler
            // First we must get a reference to a scheduler
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();

            // Job
            IJobDetail job = JobBuilder.Create<SampleJob>()
                             .WithIdentity("jobx", "groupx")
                             .Build();

            // Trigger
            // http://www.quartz-scheduler.org/documentation/quartz-2.2.x/tutorials/crontrigger to CronSchedule
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                                .WithIdentity("triggerx1", "groupx1")
                                                                .WithCronSchedule("0/10 * * * * ?")
                                                                .Build();



            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();

            try
            {
                // wait five minutes to show jobs
                Thread.Sleep(10000);
                // executing...
            }
            catch (ThreadInterruptedException)
            {
            }

            scheduler.Shutdown();
        }
    }
}
