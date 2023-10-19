using Microsoft.UI.Xaml;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace rick.tools.alarm.Helpers
{
    public static class WindowHelper
    {
        public static void ApplySystemTheme(Window window, ElementTheme theme)
        {
            var frame = window.Content as FrameworkElement;
            frame.RequestedTheme = theme;

            //Color color = frame.ActualTheme == ElementTheme.Dark ? Colors.White : Colors.Black;
            //window.AppWindow.TitleBar.ButtonForegroundColor = color;
        }
    }
}
