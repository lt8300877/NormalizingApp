using System.Threading;
using System.Threading.Tasks;
using NormalizingApp.Lib;
using NormalizingApp.MVVM;

namespace NormalizingApp.ViewModels
{
    public class CurvePageViewModel:NotifyObject
    {
        #region 曲线数据数字显示
        private float temperature1;
        public float Temperature1
        {
            get { return temperature1; }
            set
            {
                temperature1 = value;
                RaisePropertyChanged("Temperature1");
            }
        }
        private float temperature2;
        public float Temperature2
        {
            get { return temperature2; }
            set
            {
                temperature2 = value;
                RaisePropertyChanged("Temperature2");
            }
        }
        private float temperature3;
        public float Temperature3
        {
            get { return temperature3; }
            set
            {
                temperature3 = value;
                RaisePropertyChanged("Temperature3");
            }
        }
        private float temperature4;
        public float Temperature4
        {
            get { return temperature4; }
            set
            {
                temperature4 = value;
                RaisePropertyChanged("Temperature4");
            }
        }
        private float pressure;
        public float Pressure
        {
            get { return pressure; }
            set
            {
                pressure = value;
                RaisePropertyChanged("Pressure");
            }
        }
        private float flow1;
        public float Flow1
        {
            get { return flow1; }
            set
            {
                flow1 = value;
                RaisePropertyChanged("Flow1");
            }
        }
        private float flow2;
        public float Flow2
        {
            get { return flow2; }
            set
            {
                flow2 = value;
                RaisePropertyChanged("Flow2");
            }
        }
        private float flow3;
        public float Flow3
        {
            get { return flow3; }
            set
            {
                flow3 = value;
                RaisePropertyChanged("Flow3");
            }
        }
        #endregion

        //构造函数
        public CurvePageViewModel()
        {
            UpDataAsync();
        }
        private async void UpDataAsync()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    //文本显示读取变量
                    Temperature1 = S71KConnect.userItem.ActTemp1;
                    Temperature2 = S71KConnect.userItem.ActTemp2;
                    Temperature3 = S71KConnect.userItem.ActTemp3;
                    Temperature4 = S71KConnect.userItem.ActTemp4;
                    Pressure = S71KConnect.userItem.ActPressure;
                    Flow1 = S71KConnect.userItem.ActFlow1;
                    Flow2 = S71KConnect.userItem.ActFlow2;
                    Flow3 = S71KConnect.userItem.ActFlow3;
                    Task.Delay(100);
                }
            });
        }
    }
}
