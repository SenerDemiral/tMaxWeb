using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace tMaxWebServiceSend
{
    public partial class tMaxWebServiceSend : ServiceBase
    {
        private int eventId = 1;
        private bool sending = false;

        public tMaxWebServiceSend()
        {
            InitializeComponent();

            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("tMaxWebServiceSend"))
            {
                EventLog.CreateEventSource("tMaxWebServiceSend", "tMaxWebServiceSendLog");
            }
            eventLog1.Source = "tMaxWebServiceSend";
            eventLog1.Log = "tMaxWebServiceSendLog";
        }

        protected override void OnStart(string[] args)
        {
            // Set up a timer that triggers every 15minute.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 15 * 60 * 1000;    // 15dak
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            eventLog1.WriteEntry("Service Start.");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Service Stop.");
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            if (!sending)
            {
                eventLog1.WriteEntry("Sending..", EventLogEntryType.Information, eventId++);

                sending = true;

                RestClient.SendWithGrpc.FrtSend("M");   // Send modified

                sending = false;
            }
        }
    }
}
