using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email.DataProvider;

namespace rick.tools.alarm.Workers
{
    public class TimerScheduledHost
    {
        private DispatcherTimer _timer;
        private List<TimerWorkerItem> _timerWorkerItems;
        public event EventHandler<object> Scheduled;

        private TimerScheduledHost() 
        {
            _timerWorkerItems = new List<TimerWorkerItem>();
            _timer = new DispatcherTimer() { 
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += TimerScheduled;
        }

        private void TimerScheduled(object sender, object e)
        {
            foreach (var worker in _timerWorkerItems)
            {
                if (worker.NextRunTime <= DateTime.Now)
                {
                    ExecWorker(worker);
                }
            }
        }

        public static TimerScheduledHost CreateHost()
        {
            var host = new TimerScheduledHost();

            return host;
        }

        public TimerScheduledHost Register(Func<bool> worker, string title, TimeSpan period)
        {
            _timerWorkerItems.Add(new TimerWorkerItem() { Period = period, Title = title, WorkerFun = worker, NextRunTime = DateTime.Now.Add(period) });
            return this;
        }

        public TimerScheduledHost Start()
        {
            _timer.Start();
            return this;
        }

        public void Dispose()
        {
            if(_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }
        }

        private void ExecWorker(TimerWorkerItem worker)
        {
            //Task.Run(() =>
            //{
                try
                {
                    worker.WorkerFun();
                }
                finally { 
                    worker.NextRunTime = DateTime.Now.Add(worker.Period);
                }
            //});
        }

        private class TimerWorkerItem
        {
            public string Title { get; set; }

            public TimeSpan Period { get; set; }

            public Func<bool> WorkerFun { get; set; }

            public DateTime NextRunTime { get; set; }
        }
    }
}
