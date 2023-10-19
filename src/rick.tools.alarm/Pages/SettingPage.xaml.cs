// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using rick.tools.alarm.Helpers;
using rick.tools.alarm.Layouts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace rick.tools.alarm.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();

            SetSettingValue();
        }

        private void SetSettingValue()
        {
            switch (App.Settings.Theme)
            {
                case ElementTheme.Light:
                    themeMode.SelectedIndex = 0;
                    break;
                case ElementTheme.Dark:
                    themeMode.SelectedIndex = 1;
                    break;
                case ElementTheme.Default:
                    themeMode.SelectedIndex = 2;
                    break;
            }

            if (App.Settings.DisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navigationLocation.SelectedIndex = 1;
            }
            else
            {
                navigationLocation.SelectedIndex = 0;
            }
        }

        private void themeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTheme = ((ComboBoxItem)themeMode.SelectedItem)?.Tag?.ToString();
            var theme = EnumHelper.GetEnum<ElementTheme>(selectedTheme);
            WindowHelper.ApplySystemTheme(App.MainLayout, theme);
            App.Settings.Theme = theme;
        }

        private void navigationLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var positon = NavigationViewPaneDisplayMode.Left;
            if (navigationLocation.SelectedIndex != 0)
            {
                positon = NavigationViewPaneDisplayMode.Top;
            }
            (App.MainLayout as NavigationLayout).SetPaneDisplayMode(positon);
            App.Settings.DisplayMode = positon;
        }

        public string Version
        {
            get
            {
                var version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
                return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }
    }
}
