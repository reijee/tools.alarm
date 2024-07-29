// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Controls;
using rick.tools.alarm.Data.Entitys;
using rick.tools.alarm.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace rick.tools.alarm.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public ObservableCollection<AlarmDto> AlarmDtoList = new ObservableCollection<AlarmDto>();

        public HomePage()
        {
            this.InitializeComponent();

            SetDatetime();
            QueryAlarmData();
            App.TimerSchedule.TimerTick += (sender, e) =>
            {
                SetDatetime();
                QueryAlarmData();
            };
        }

        private void SetDatetime()
        {
            TimeZoneInfo timeZone = TimeZoneInfo.Local;
            txtTimeZone.Text = timeZone.DisplayName;

            DateTime nowTime = DateTime.Now;
            txtTime.Text = nowTime.ToString("T");
            txtDay.Text = nowTime.ToString("D");
        }

        private void QueryAlarmData()
        {
            var list = new ObservableCollection<AlarmDto>() {
                new AlarmDto(){ AlarmId = Guid.NewGuid(), AlarmTime = new TimeOnly(08, 30), IsActive = true, IsRepeat = true, Title = "һ��Ĺ���Ҫ��ʼ��" },
                new AlarmDto(){ AlarmId = Guid.NewGuid(), AlarmTime = new TimeOnly(13, 30), IsActive = true, IsRepeat = true, Title = "���ݽ����ˣ���ʼ������" },
                new AlarmDto(){ AlarmId = Guid.NewGuid(), AlarmTime = new TimeOnly(18, 00), IsActive = true, IsRepeat = true, Title = "�°�ؼ���" },
            };
            AlarmDtoList = new ObservableCollection<AlarmDto>(list);
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            foreach (var item in AlarmDtoList)
            {
                var msg = item.IsRepeat ? "ÿ��" : "ֻ��һ��";
                if (item.IsActive)
                {
                    string leftTimeStr = string.Empty;
                    TimeSpan timeSpan = item.NextActiveTime - DateTime.Now;
                    if (timeSpan.Hours > 0) leftTimeStr = $"{timeSpan.Hours}Сʱ";
                    leftTimeStr += $"{timeSpan.Minutes}����{timeSpan.Seconds}��";
                    msg = $"{msg} | {leftTimeStr}������";
                }

                item.AlarmMessage = msg;
            }
        }
    }
}
