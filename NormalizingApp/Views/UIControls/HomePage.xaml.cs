using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NormalizingApp.Views.UIControls
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {   
        //表盘数据绑定实例化
        public Lib.MyGauge voltageGauge = new Lib.MyGauge();
        public Lib.MyGauge currentGauge = new Lib.MyGauge();
        public Lib.MyGauge powerGauge = new Lib.MyGauge();
        public Lib.MyGauge energyGauge = new Lib.MyGauge();
        //TextBlock数据绑定实例化
        //X轴数据
        public Lib.DataDisplay xActPos = new Lib.DataDisplay();
        public Lib.DataDisplay xActSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay xSetPos = new Lib.DataDisplay();
        public Lib.DataDisplay xSetSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay xActCurrent = new Lib.DataDisplay();
        //Y轴数据
        public Lib.DataDisplay yActPos = new Lib.DataDisplay();
        public Lib.DataDisplay yActSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay ySetPos = new Lib.DataDisplay();
        public Lib.DataDisplay ySetSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay yActCurrent = new Lib.DataDisplay();
        //Z轴数据
        public Lib.DataDisplay zActPos = new Lib.DataDisplay();
        public Lib.DataDisplay zActSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay zSetPos = new Lib.DataDisplay();
        public Lib.DataDisplay zSetSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay zActCurrent = new Lib.DataDisplay();
        //温度
        public Lib.DataDisplay actTemp1 = new Lib.DataDisplay();
        public Lib.DataDisplay actTemp2 = new Lib.DataDisplay();
        public Lib.DataDisplay actTemp3 = new Lib.DataDisplay();
        //流量
        public Lib.DataDisplay actFlow1 = new Lib.DataDisplay();
        public Lib.DataDisplay actFlow2 = new Lib.DataDisplay();
        public Lib.DataDisplay actFlow3 = new Lib.DataDisplay();
        //位移传感器
        public Lib.DataDisplay actLenght1 = new Lib.DataDisplay();
        public Lib.DataDisplay actLenght2 = new Lib.DataDisplay();
        //压力
        public Lib.DataDisplay actPressure = new Lib.DataDisplay();

        //后台线程定义
        private Thread m_thread = null;

        public delegate void UpdateLampdelegate();


        public HomePage()
        {
            InitializeComponent();
            InitDisplayData();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitDisplayData()
        {
            //表盘
            this.TGauge1.DataContext = voltageGauge;
            this.TGauge2.DataContext = currentGauge;
            this.TGauge3.DataContext = powerGauge;
            this.TGauge4.DataContext = energyGauge;
            //textblock
            //X轴数据
            this.xActPosTbk.DataContext = xActPos;
            this.xActSpeedTbk.DataContext = xActSpeed;
            this.xSetPosTbk.DataContext = xSetPos;
            this.xSetSpeedTbk.DataContext = xSetSpeed;
            this.xActCurrentTbk.DataContext = xActCurrent;
            //Y轴数据
            this.yActPosTbk.DataContext = yActPos;
            this.yActSpeedTbk.DataContext = yActSpeed;
            this.ySetPosTbk.DataContext = ySetPos;
            this.ySetSpeedTbk.DataContext = ySetSpeed;
            this.yActCurrentTbk.DataContext = yActCurrent;
            //Z轴数据
            this.zActPosTbk.DataContext = zActPos;
            this.zActSpeedTbk.DataContext = zActSpeed;
            this.zSetPosTbk.DataContext = zSetPos;
            this.zSetSpeedTbk.DataContext = zSetSpeed;
            this.zActCurrentTbk.DataContext = zActCurrent;
            //温度数据
            this.actTemp1Tbk.DataContext = actTemp1;
            this.actTemp2Tbk.DataContext = actTemp2;
            this.actTemp3Tbk.DataContext = actTemp3;
            //流量数据
            this.actFlow1Tbk.DataContext = actFlow1;
            this.actFlow2Tbk.DataContext = actFlow2;
            this.actFlow3Tbk.DataContext = actFlow3;
            //位移传感器
            this.actLenght1Tbk.DataContext = actLenght1;
            this.actLenght2Tbk.DataContext = actLenght2;
            //压力
            this.actPressureTbk.DataContext = actPressure;
            //线程启动
            //初始化线程
            m_thread = new Thread(ThreadFunc)
            {
                IsBackground = true
            };
            m_thread.Start();
        }
        /// <summary>
        /// 后台刷新线程
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void ThreadFunc()
        {
            UpdateLampdelegate upLampDelegate = new UpdateLampdelegate(Update);
            while (true)
            {
                //表盘
                voltageGauge.Score = Lib.S71KConnect.userType.Voltage;
                currentGauge.Score = Lib.S71KConnect.userType.Current;
                powerGauge.Score = Lib.S71KConnect.userType.Power;
                energyGauge.Score = Lib.S71KConnect.userType.Energy;
                //X轴数据
                xActPos.DataText = Lib.S71KConnect.userType.XActPos.ToString("f2");

                xActSpeed.DataText = Lib.S71KConnect.userType.XActSpeed.ToString("f2");
                xSetPos.DataText = Lib.S71KConnect.userType.XSetPos.ToString("f2");
                xSetSpeed.DataText = Lib.S71KConnect.userType.XSetSpeed.ToString("f2");
                xActCurrent.DataText = Lib.S71KConnect.userType.XActCurrent.ToString("f2");
                //Y轴数据
                yActPos.DataText = Lib.S71KConnect.userType.YActPos.ToString("f2");
                yActSpeed.DataText = Lib.S71KConnect.userType.YActSpeed.ToString("f2");
                ySetPos.DataText = Lib.S71KConnect.userType.YSetPos.ToString("f2");
                ySetSpeed.DataText = Lib.S71KConnect.userType.YSetSpeed.ToString("f2");
                yActCurrent.DataText = Lib.S71KConnect.userType.YActCurrent.ToString("f2");
                //Z轴数据
                zActPos.DataText = Lib.S71KConnect.userType.ZActPos.ToString("f2");
                zActSpeed.DataText = Lib.S71KConnect.userType.ZActSpeed.ToString("f2");
                zSetPos.DataText = Lib.S71KConnect.userType.ZSetPos.ToString("f2");
                zSetSpeed.DataText = Lib.S71KConnect.userType.ZSetSpeed.ToString("f2");
                zActCurrent.DataText = Lib.S71KConnect.userType.ZActCurrent.ToString("f2");
                //温度数据
                actTemp1.DataText = Lib.S71KConnect.userType.ActTemp1.ToString("f2");
                actTemp2.DataText = Lib.S71KConnect.userType.ActTemp2.ToString("f2");
                actTemp3.DataText = Lib.S71KConnect.userType.ActTemp3.ToString("f2");
                //流量数据
                actFlow1.DataText = Lib.S71KConnect.userType.ActFlow1.ToString("f2");
                actFlow2.DataText = Lib.S71KConnect.userType.ActFlow2.ToString("f2");
                actFlow3.DataText = Lib.S71KConnect.userType.ActFlow3.ToString("f2");
                //位移传感器
                actLenght1.DataText = Lib.S71KConnect.userType.ActLenght1.ToString("f2");
                actLenght2.DataText = Lib.S71KConnect.userType.ActLenght2.ToString("f2");
                //压力
                actPressure.DataText = Lib.S71KConnect.userType.ActPressure.ToString("f2");
                //指示灯委托方法
                A.Dispatcher.Invoke(upLampDelegate);

                Thread.Sleep(200);
            }
            

        }

        private void Update()
        {
            if (Lib.S71KConnect.userType.DY_ZT)
                A.Fill = new SolidColorBrush(Colors.LimeGreen);
            else
                A.Fill = new SolidColorBrush(Colors.Red);

            if (Lib.S71KConnect.userType.Y_CDJ_QGQW)
                B.Fill = new SolidColorBrush(Colors.LimeGreen);
            else
                B.Fill = new SolidColorBrush(Colors.Red);


            if (Lib.S71KConnect.userType.Z_CDJ_QGQW)
                C.Fill = new SolidColorBrush(Colors.LimeGreen);
            else
                C.Fill = new SolidColorBrush(Colors.Red);

            if (NormalizingApp.Lib.S71KConnect.userType.ActAlarm1 != 0 || NormalizingApp.Lib.S71KConnect.userType.ActAlarm2 != 0)
                D.Fill = new SolidColorBrush(Colors.Red);
            else
                D.Fill = new SolidColorBrush(Colors.LimeGreen);


            E.Fill = new SolidColorBrush(Colors.LimeGreen);
            F.Fill = new SolidColorBrush(Colors.LimeGreen);
            G.Fill = new SolidColorBrush(Colors.LimeGreen);
            H.Fill = new SolidColorBrush(Colors.LimeGreen);
        }
    }
}
