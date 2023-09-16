using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rick.tools.alarm.Data.Entitys
{
    public class GlobalSetting
    {
        private Repository<GlobalSetting> _repository;
        private ElementTheme _theme;
        private NavigationViewPaneDisplayMode _displayMode;

        public GlobalSetting() 
        {
            _repository = new Repository<GlobalSetting>();
        }

        public static GlobalSetting CrearteInstance()
        {
            var instance = new GlobalSetting();
            instance.LoadSetting();
            return instance;
        }

        /// <summary>
        /// 主题
        /// </summary>
        public ElementTheme Theme 
        { 
            get { return _theme; }
            set { 
                _theme = value;
                SaveSetting();
            }
        }

        /// <summary>
        /// 菜单样式
        /// </summary>
        public NavigationViewPaneDisplayMode DisplayMode 
        {
            get { return _displayMode; }
            set
            {
                _displayMode = value;
                SaveSetting();
            }
        }

        #region 辅助方法
        private void LoadSetting()
        {
            var list = _repository.Query();
            if (list != null)
            {
                var instance = list.FirstOrDefault();
                this.Theme = instance.Theme;
                this.DisplayMode = instance.DisplayMode;
            }
            else
            {
                this.Theme = ElementTheme.Default;
                this.DisplayMode = NavigationViewPaneDisplayMode.Left;
            }
        }

        private void SaveSetting()
        {
            _repository.Save(new List<GlobalSetting> { this });
        }
        #endregion
    }
}
