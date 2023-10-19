// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using rick.tools.alarm.Helpers;
using rick.tools.alarm.Pages;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace rick.tools.alarm.Layouts
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationLayout : Window
    {
        public NavigationLayout()
        {
            this.InitializeComponent();

            // �Զ��������
            //customTitleBar.InitTitleBar(this, "����");
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppBarDrag);

            // ���ñ���ɫ
            /*
             * appsdk 1.2 �汾�еķ���
            //SystemBackdropSetter setter = new SystemBackdropSetter(this);
            //setter.TrySetSystemBackdrop();
            */
            // appsdk1.3�汾�е��·���
            this.SystemBackdrop = new MicaBackdrop();

            // ��������
            WindowHelper.ApplySystemTheme(this, App.Settings.Theme);

            // ���õ���
            SetPaneDisplayMode(App.Settings.DisplayMode);
        }

        public void SetPaneDisplayMode(NavigationViewPaneDisplayMode mode)
        {
            NavView.PaneDisplayMode = mode;
        }

        private void NavView_Navigate(Type page)
        {
            if (ContentFrame.CurrentSourcePageType == page) return;

            ContentFrame.Navigate(page, null, new SuppressNavigationTransitionInfo());
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavView_Navigate(typeof(HomePage));
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavView_Navigate(typeof(SettingPage));
            }
            else if (args.InvokedItemContainer is not null)
            {
                var navItemTag = args.InvokedItemContainer.Tag?.ToString();
                switch(navItemTag)
                {
                    case "Home":
                        NavView_Navigate(typeof(HomePage));
                        break;
                    case "Alarm":
                        NavView_Navigate(typeof(AlarmPage));
                        break;
                }
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            this.BackButton.IsEnabled = ContentFrame.CanGoBack;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
            else
            {
                BackButton.IsEnabled = false;
            }
        }
    }
}
