using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalizingApp.Lib
{
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
    /// <summary>
    /// TextBlock数据绑定
    /// </summary>
    public class DataDisplay : INotifyPropertyChanged
    {
        private string _DataText;

        public string DataText
        {
            get { return _DataText; }
            set
            {
                if (value != _DataText)
                {
                    _DataText = value;
                    Notify("DataText");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public DataDisplay(string s)
        {
            this._DataText = s;
        }
        public DataDisplay() { }
    }
}
