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

        public static void WithTryCatch(this Object theClass , Action theMethod) 
        {
            try
            {
                theMethod.Invoke();
            }
            catch (Exception ex)
            {
                string msg = String.Format("Class : {0} - Method : {1} - Error : {2} ", theClass.GetType().Name, theMethod.Method.Name, ex.Message);
                log.Error(msg);
                throw new ApplicationException("");
            }  
        }
    }
}
