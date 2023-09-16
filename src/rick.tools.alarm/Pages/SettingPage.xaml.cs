// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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

            displayModePanel.SelectedItem = App.Settings.DisplayMode;
            themePanel.SelectedItem = App.Settings.Theme;
        }

        private void ThomeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ElementTheme selectedTheme = (ElementTheme)((RadioButton)sender).Tag;

            Grid content = App.MainLayout.Content as Grid;
            if (content is not null)
            {
                content.RequestedTheme = selectedTheme;
                App.Settings.Theme = selectedTheme;
            }
        }

        private void NavRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var positon = (NavigationViewPaneDisplayMode)((RadioButton)sender).Tag;
            (App.MainLayout as NavigationLayout).SetPaneDisplayMode(positon);
            App.Settings.DisplayMode = positon;
        }
    }
}
