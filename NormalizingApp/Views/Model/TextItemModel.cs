using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalizingApp.Views.Model
{
    /// <summary>
    /// 界面的所有Text框都可以用这个实体类进行后台数据绑定
    /// </summary>
    public class TextItemModel: INotifyPropertyChanged
    {
        private string _content;
        public string Content
        {
            get { return _content; }
            set { this.MutateVerbose(ref _content, value, RaisePropertyChanged()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }

    /// <summary>
    /// 仪表盘控件的事件绑定
    /// </summary>
    public class MyGauge : INotifyPropertyChanged //绑定对象  
    {
        private double _score;//显示
        public event PropertyChangedEventHandler PropertyChanged;

        public double Score
        {
            get { return this._score; }
            set
            {
                this._score = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Score"));
                }
            }
        }
        public MyGauge(double scr)
        {
            this.Score = scr;
        }
        public MyGauge() { }
    }
}
