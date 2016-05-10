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
    public class NotifierJobExecuter<TJob>
        : BaseNotifierJobExecuter<TJob>
         where TJob : IJob
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
}
