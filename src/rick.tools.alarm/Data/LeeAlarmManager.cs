using rick.tools.alarm.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rick.tools.alarm.Data
{
    public class LeeAlarmManager: Repository<LeeAlarm>
    {
        private List<LeeAlarm> _alarmList;
        private Repository<LeeAlarm> _repository;

        public LeeAlarmManager() 
        {
            _repository = new Repository<LeeAlarm>();
            _alarmList = _repository.Query();
        }

        public List<LeeAlarm> AlarmList { get => _alarmList; set => _alarmList = value; }

        public void Add(LeeAlarm alarm)
        {
            _alarmList.Add(alarm);
            _repository.Save(_alarmList);
        }

        public void Remove(Guid alarmId)
        {
            var item = _alarmList.FirstOrDefault(t=>t.AlarmId == alarmId);
            if (item != null)
            {
                _alarmList.Remove(item);
                _repository.Save(_alarmList);
            }
        }

        public void Update(LeeAlarm alarm)
        {
            var enttiy = _alarmList.FirstOrDefault(t => t.AlarmId == alarm.AlarmId);
            if (enttiy != null)
            {
                enttiy = alarm;
                _repository.Save(_alarmList);
            }
        }
    }
}
