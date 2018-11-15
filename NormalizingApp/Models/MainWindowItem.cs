using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace NormalizingApp.Models
{
    /// <summary>
    /// 界面上菜单选项的ListView控件后台数据的实体类
    /// </summary>
    public class MainWindowItem : MVVM.NotifyObject
    {
        private string _name;
        private object _content;
        private Thickness _marginRequirement = new Thickness(16);

        public MainWindowItem(string name, object content)
        {
            _name = name;
            Content = content;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged("Content");
            }
        }
        
    }
}
