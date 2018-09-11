using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace NormalizingApp.Views.Model
{
    /// <summary>
    /// 界面上菜单选项的ListView控件后台数据的实体类
    /// </summary>
    public class MainWindowItem : INotifyPropertyChanged
    {
        private string _name;
        private object _content;
        //private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        //private ScrollBarVisibility _verticalScrollBarVisibilityRequirement;
        private Thickness _marginRequirement = new Thickness(16);

        public MainWindowItem(string name, object content)
        {
            _name = name;
            Content = content;
        }

        public string Name
        {
            get { return _name; }
            set { this.MutateVerbose(ref _name, value, RaisePropertyChanged()); }
        }

        public object Content
        {
            get { return _content; }
            set { this.MutateVerbose(ref _content, value, RaisePropertyChanged()); }
        }

        //public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        //{
        //    get { return _horizontalScrollBarVisibilityRequirement; }
        //    set { this.MutateVerbose(ref _horizontalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
        //}

        //public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        //{
        //    get { return _verticalScrollBarVisibilityRequirement; }
        //    set { this.MutateVerbose(ref _verticalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
        //}

        //public Thickness MarginRequirement
        //{
        //    get { return _marginRequirement; }
        //    set { this.MutateVerbose(ref _marginRequirement, value, RaisePropertyChanged()); }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
