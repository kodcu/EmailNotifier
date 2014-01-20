using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Notifier;
using System.Reflection;
using log4net;

namespace Notifier.Server.WebController
{
    /// <summary>
    /// Summary description for NotifierController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NotifierController 
        : System.Web.Services.WebService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [WebMethod]
        public void ScanRecords()
        {
            string infoMsg = String.Format("Date : {0} Time : {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongTimeString());
            log.Info(infoMsg + " REQUESTED");

            try
            {
                NotificationController controller = new NotificationController();
                controller.Check();
            }
            catch (Exception ex)
            {
                log.Error("" + ex.Message.ToString());
            }

            log.Info(infoMsg + " RESPONDED");
        }
    }
}
