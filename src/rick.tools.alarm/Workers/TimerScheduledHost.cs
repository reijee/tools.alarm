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
    public class TimerScheduledHost: IDisposable
    {
        private DispatcherTimer _timer;
        public event EventHandler<TimerTickEventArgs> TimerTick;

        private TimerScheduledHost() 
        {
            _timer = new DispatcherTimer() { 
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += TimerScheduled;
        }

        private void TimerScheduled(object sender, object e)
        {
            if(TimerTick != null)
            {
                TimerTick?.Invoke(this, new TimerTickEventArgs());
            }
        }

        private TimerScheduledHost Start()
        {
            _timer.Start();
            return this;
        }

        public static TimerScheduledHost CreateHost()
        {
            var host = new TimerScheduledHost();
            return host.Start();
        }

        public void Dispose()
        {
            if(_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }
        }
    }
}
