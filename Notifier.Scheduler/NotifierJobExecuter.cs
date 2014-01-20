using Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notifier.Scheduler
{
    public interface INotifierJobExecuter
    {
        void ScheduleIt<TJob>(string CronExpression, 
                              string jobKey = null,
                              string jobGroupKey = null,
                              string triggerKey = null,
                              string triggerGroupKey = null);    
        void StartJob();
        void StopJob();
    }
        
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
            this.scheduler.Start();           
        }

        public virtual void StopJob()
        {
            this.StopJob();
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

    public class NotifierJobExecuter<TJob>
        : BaseNotifierJobExecuter<TJob>
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void ScheduleIt(string CronExpression,
                                        string jobKey = null,
                                        string jobGroupKey = null,
                                        string triggerKey = null,
                                        string triggerGroupKey = null)
        {
            
            base.CronExpression = CronExpression;
            base.jobKey = String.IsNullOrEmpty(jobKey) ? Guid.NewGuid().ToString() : jobKey;
            base.jobGroupKey = String.IsNullOrEmpty(jobGroupKey) ? Guid.NewGuid().ToString() : jobGroupKey;
            base.triggerKey = String.IsNullOrEmpty(triggerKey) ? Guid.NewGuid().ToString() : triggerKey;
            base.triggerGroupKey = String.IsNullOrEmpty(triggerGroupKey) ? Guid.NewGuid().ToString() : triggerGroupKey;

            if (base.jobAction.Equals(null))
            {
                log.Error("");
                throw new ApplicationException("");
            }
            if (String.IsNullOrEmpty(base.CronExpression))
            {
                log.Error("");
                throw new ApplicationException("");
            }

            // Job
            IJobDetail job = JobBuilder.Create<TJob>()
                             .WithIdentity(jobKey, jobGroupKey)
                             .Build();

            // Trigger
            // http://www.quartz-scheduler.org/documentation/quartz-2.2.x/tutorials/crontrigger to CronSchedule
            // http://www.cronmaker.com
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                                .WithIdentity(triggerKey, triggerGroupKey)
                                                                .WithCronSchedule(CronExpression)
                                                                .Build();
            try
            {
                scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception)
            {
                log.Error("");
                throw new ApplicationException("");
            }            
        }
    }

    public class NotifierJob
       : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("OK");
        }
    }
}
