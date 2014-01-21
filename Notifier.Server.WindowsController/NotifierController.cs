using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Notifier.Server.WindowsController
{
    partial class NotifierController 
        : ServiceBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private Timer timer = new Timer();
        private double servicePollInterval;

        public NotifierController()
        {
            InitializeComponent();
            servicePollInterval = 2000;
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            timer.Elapsed += timer_Elapsed;
            //providing the time in miliseconds 
            timer.Interval = servicePollInterval;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start(); 
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string infoMsg = String.Format("Date : {0} Time : {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongTimeString());
            log.Info(infoMsg + " REQUESTED BY WINDOWS SERVICE");

            try
            {
                NotificationController controller = new NotificationController();
                controller.Check();
            }
            catch (Exception ex)
            {
                log.Error("" + ex.Message.ToString());
            }

            log.Info(infoMsg + " RESPONDED BY WINDOWS SERVICE");
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            timer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            timer.Stop();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service. 
            timer.Stop();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            timer.Stop();
        } 
    }
}
