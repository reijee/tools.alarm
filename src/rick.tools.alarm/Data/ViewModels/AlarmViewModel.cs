using rick.tools.alarm.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace rick.tools.alarm.Data.ViewModels
{
    public class AlarmViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<LeeAlarm> _alarmList;

        public ObservableCollection<LeeAlarm> AlarmList
        {
            get
            {
                return this._alarmList;
            }
            set
            {
                if (this._alarmList != value)
                {
                    this._alarmList = value;
                    OnPropertyChanged("AlarmList");
                }
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
