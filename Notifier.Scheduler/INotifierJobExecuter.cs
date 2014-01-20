using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifier.Scheduler
{
    public interface INotifierJobExecuter
    {
        void ScheduleIt(string CronExpression,
                              string jobKey = null,
                              string jobGroupKey = null,
                              string triggerKey = null,
                              string triggerGroupKey = null);
        void StartJob();
        void StopJob();
    }
}
