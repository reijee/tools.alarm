using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace rick.tools.alarm.Data.ViewModels
{
    public class AlarmDto: INotifyPropertyChanged
    {
        private string _alarmMessage;

        public Guid AlarmId { get; set; }

        public string Title { get; set; }

        public TimeOnly AlarmTime { get; set; }

        public bool IsRepeat { get; set; } = false;

        public string Voice { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime NextActiveTime
        {
            get
            {
                var nextTime = DateTime.Today.Add(this.AlarmTime.ToTimeSpan());
                if (nextTime < DateTime.Now) nextTime = nextTime.AddDays(1);
                return nextTime;
            }
        }

        public string AlarmMessage
        {
            get
            {
                return _alarmMessage;
            }
            set
            {
                _alarmMessage = value;
                OnPropertyChanged("AlarmMessage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
