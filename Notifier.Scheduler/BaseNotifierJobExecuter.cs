using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifier.Scheduler
{
    public abstract class BaseNotifierJobExecuter<TJob>
       : INotifierJobExecuter
        where TJob : IJob
    {
        private ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
        public IScheduler scheduler = null;

        protected IJob jobAction = null;
        protected string CronExpression = String.Empty;
        protected string jobKey = String.Empty;
        protected string jobGroupKey = String.Empty;
        protected string triggerKey = String.Empty;
        protected string triggerGroupKey = String.Empty;

        public BaseNotifierJobExecuter()
        {
            this.scheduler = schedulerFactory.GetScheduler();
        }

        public virtual void StartJob()
        {
            //this.scheduler.Start();
            this.scheduler.WithTryCatch(scheduler.Start);
        }

        public virtual void StopJob()
        {
            //this.scheduler.Shutdown();
            this.scheduler.WithTryCatch(scheduler.Shutdown);
        }

        public abstract void ScheduleIt(string CronExpression,
                                              string jobKey = null,
                                              string jobGroupKey = null,
                                              string triggerKey = null,
                                              string triggerGroupKey = null);

        public virtual void Execute()
        {
            StartJob();

            ScheduleIt(this.CronExpression,
                       this.jobKey,
                       this.jobGroupKey,
                       this.triggerKey,
                       this.triggerGroupKey);

            StopJob();
        }

    }
}
