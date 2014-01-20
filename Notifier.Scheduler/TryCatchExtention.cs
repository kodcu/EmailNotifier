using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notifier.Scheduler
{
    public static class TryCatchExtention
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void TryCatch(this Action action) 
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                log.Error("Error : {0} " + ex.Message);
                throw new ApplicationException("");
            }  
        }
    }
}
