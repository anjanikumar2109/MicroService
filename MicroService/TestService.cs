using System.IO;
using System.ServiceProcess;

namespace MicroService
{
    public partial class TestService : ServiceBase
    {
        System.Timers.Timer timeDelay;
        int count;
        public TestService()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer();
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);
        }

        public void WorkProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            string process = "Timer Tick " + count;
            LogService(process);
            count++;
        }

        protected override void OnStart(string[] args)
        {
            LogService("Service is Started");
            timeDelay.Enabled = true;
        }

        protected override void OnStop()
        {
            LogService("Service Stoped");
            timeDelay.Enabled = false;
        }

        private void LogService(string content)
        {
            // Do something for logging here
            var fs = new FileStream(@"c:\TestServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
