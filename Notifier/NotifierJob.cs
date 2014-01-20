using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace Notifier
{
    public class NotifierJob
       : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("OK");
        }
    }
}
