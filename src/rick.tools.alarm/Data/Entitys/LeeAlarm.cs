using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace rick.tools.alarm.Data.Entitys
{
    public class LeeAlarm
    {
        public Guid AlarmId { get; set; }

        public string Title { get; set; }

        public TimeOnly AlarmTime { get; set; }

        public bool IsRepeat { get; set; } = false;

        public string Voice { get; set; }

        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public DateTime NextActiveTime { get; set; }
    }
}
