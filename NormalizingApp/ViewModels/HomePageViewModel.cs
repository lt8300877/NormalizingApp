using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Media;
using NormalizingApp.MVVM;
namespace NormalizingApp.ViewModels
{
    public class HomePageViewModel:NotifyObject
    {
        #region X轴显示数据
        private float xPos;
        public float XPos
        {
            get { return xPos; }
            set
            {
                xPos = value;
                RaisePropertyChanged("XPos");
            }
        }
        private float xSpeed;
        public float XSpeed
        {
            get { return xSpeed; }
            set
            {
                xSpeed = value;
                RaisePropertyChanged("XSpeed");
            }
        }
        private float xSetPos;
        public float XSetPos
        {
            get { return xSetPos; }
            set
            {
                xSetPos = value;
                RaisePropertyChanged("XSetPos");
            }
        }
        private float xSetSpeed;
        public float XSetSpeed
        {
            get { return xSetSpeed; }
            set
            {
                xSetSpeed = value;
                RaisePropertyChanged("XSetSpeed");
            }
        }
        private float xCurrent;
        public float XCurrent
        {
            get { return xCurrent; }
            set
            {
                xCurrent = value;
                RaisePropertyChanged("XCurrent");
            }
        }
        #endregion
        #region Y轴显示数据
        private float yPos;
        public float YPos
        {
            get { return yPos; }
            set
            {
                yPos = value;
                RaisePropertyChanged("YPos");
            }
        }
        private float ySpeed;
        public float YSpeed
        {
            get { return ySpeed; }
            set
            {
                ySpeed = value;
                RaisePropertyChanged("YSpeed");
            }
        }
        private float ySetPos;
        public float YSetPos
        {
            get { return ySetPos; }
            set
            {
                ySetPos = value;
                RaisePropertyChanged("YSetPos");
            }
        }
        private float ySetSpeed;
        public float YSetSpeed
        {
            get { return ySetSpeed; }
            set
            {
                ySetSpeed = value;
                RaisePropertyChanged("YSetSpeed");
            }
        }
        private float yCurrent;
        public float YCurrent
        {
            get { return yCurrent; }
            set
            {
                yCurrent = value;
                RaisePropertyChanged("YCurrent");
            }
        }
        #endregion
        #region Z轴显示数据
        private float zPos;
        public float ZPos
        {
            get { return zPos; }
            set
            {
                zPos = value;
                RaisePropertyChanged("ZPos");
            }
        }
        private float zSpeed;
        public float ZSpeed
        {
            get { return zSpeed; }
            set
            {
                zSpeed = value;
                RaisePropertyChanged("ZSpeed");
            }
        }
        private float zSetPos;
        public float ZSetPos
        {
            get { return zSetPos; }
            set
            {
                zSetPos = value;
                RaisePropertyChanged("ZSetPos");
            }
        }
        private float zSetSpeed;
        public float ZSetSpeed
        {
            get { return zSetSpeed; }
            set
            {
                zSetSpeed = value;
                RaisePropertyChanged("ZSetSpeed");
            }
        }
        private float zCurrent;
        public float ZCurrent
        {
            get { return zCurrent; }
            set
            {
                zCurrent = value;
                RaisePropertyChanged("ZCurrent");
            }
        }
        #endregion
        #region 温度数据显示
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
        #endregion
        #region 流量数据显示
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
        private float flow4;
        public float Flow4
        {
            get { return flow4; }
            set
            {
                flow4 = value;
                RaisePropertyChanged("Flow4");
            }
        }
        #endregion
        #region 长度计数据显示
        private float lenghtY;
        public float LenghtY
        {
            get { return lenghtY; }
            set
            {
                lenghtY = value;
                RaisePropertyChanged("LenghtY");
            }
        }
        private float lenghtZ;
        public float LenghtZ
        {
            get { return lenghtZ; }
            set
            {
                lenghtZ = value;
                RaisePropertyChanged("LenghtZ");
            }
        }
        #endregion
        #region 压力数据显示
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
        private float pressure1;
        public float Pressure1
        {
            get { return pressure1; }
            set
            {
                pressure1 = value;
                RaisePropertyChanged("Pressure1");
            }
        }
        #endregion
        #region LED指示灯
        private Brush led1;
        public Brush LED1
        {
            get { return led1; }
            set
            {
                led1 = value;
                RaisePropertyChanged("LED1");
            }
        }
        private Brush led2;
        public Brush LED2
        {
            get { return led2; }
            set
            {
                led2 = value;
                RaisePropertyChanged("LED2");
            }
        }
        private Brush led3;
        public Brush LED3
        {
            get { return led3; }
            set
            {
                led3 = value;
                RaisePropertyChanged("LED3");
            }
        }
        private Brush led4;
        public Brush LED4
        {
            get { return led4; }
            set
            {
                led4 = value;
                RaisePropertyChanged("LED4");
            }
        }
        private Brush led5;
        public Brush LED5
        {
            get { return led5; }
            set
            {
                led5 = value;
                RaisePropertyChanged("LED5");
            }
        }
        private Brush led6;
        public Brush LED6
        {
            get { return led6; }
            set
            {
                led6 = value;
                RaisePropertyChanged("LED6");
            }
        }
        private Brush led7;
        public Brush LED7
        {
            get { return led7; }
            set
            {
                led7 = value;
                RaisePropertyChanged("LED7");
            }
        }
        private Brush led8;
        public Brush LED8
        {
            get { return led8; }
            set
            {
                led8 = value;
                RaisePropertyChanged("LED8");
            }
        }
        #endregion
        #region 表盘数据显示
        private double gaugeV;
        public double GaugeV
        {
            get { return gaugeV; }
            set
            {
                gaugeV = value;
                RaisePropertyChanged("GaugeV");
            }
        }
        private double gaugeA;
        public double GaugeA
        {
            get { return gaugeA; }
            set
            {
                gaugeA = value;
                RaisePropertyChanged("GaugeA");
            }
        }
        private double gaugeKW;
        public double GaugeKW
        {
            get { return gaugeKW; }
            set
            {
                gaugeKW = value;
                RaisePropertyChanged("GaugeKW");
            }
        }
        private double gaugeKWS;
        public double GaugeKWS
        {
            get { return gaugeKWS; }
            set
            {
                gaugeKWS = value;
                RaisePropertyChanged("GaugeKWS");
            }
        }
        #endregion

        Thread thread;

        public HomePageViewModel()
        {
            thread = new Thread(new ThreadStart(UpData));
            thread.IsBackground = true;
            thread.Start();
        }
        private void UpData()
        {
            while(true)
            {
                //表盘数据
                GaugeV = Lib.S71KConnect.userItem.Voltage;
                GaugeA = Lib.S71KConnect.userItem.Current;
                GaugeKW = Lib.S71KConnect.userItem.Power;
                GaugeKWS = Lib.S71KConnect.userItem.Energy;
                //X轴数据
                XPos = Lib.S71KConnect.userItem.XActPos;
                XSpeed = Lib.S71KConnect.userItem.XActSpeed;
                XSetPos = Lib.S71KConnect.userItem.XSetPos;
                XSetSpeed = Lib.S71KConnect.userItem.XSetSpeed;
                XCurrent = Lib.S71KConnect.userItem.XActCurrent;
                //Y轴数据
                YPos = Lib.S71KConnect.userItem.YActPos;
                YSpeed = Lib.S71KConnect.userItem.YActSpeed;
                YSetPos = Lib.S71KConnect.userItem.YSetPos;
                YSetSpeed = Lib.S71KConnect.userItem.YSetSpeed;
                YCurrent = Lib.S71KConnect.userItem.YActCurrent;
                //Z轴数据
                ZPos = Lib.S71KConnect.userItem.ZActPos;
                ZSpeed = Lib.S71KConnect.userItem.ZActSpeed;
                ZSetPos = Lib.S71KConnect.userItem.ZSetPos;
                ZSetSpeed = Lib.S71KConnect.userItem.ZSetSpeed;
                ZCurrent = Lib.S71KConnect.userItem.ZActCurrent;
                //温度数据
                Temperature1 = Lib.S71KConnect.userItem.ActTemp1;
                Temperature2 = Lib.S71KConnect.userItem.ActTemp2;
                Temperature3 = Lib.S71KConnect.userItem.ActTemp3;
                Temperature4 = Lib.S71KConnect.userItem.ActTemp4;
                //流量数据
                Flow1 = Lib.S71KConnect.userItem.ActFlow1;
                Flow2 = Lib.S71KConnect.userItem.ActFlow2;
                Flow3 = Lib.S71KConnect.userItem.ActFlow3;
                //位移传感器
                LenghtY = Lib.S71KConnect.userItem.ActLenght1;
                LenghtZ = Lib.S71KConnect.userItem.ActLenght2;
                //压力
                Pressure = Lib.S71KConnect.userItem.ActPressure;
                //LED指示灯
                LED1 = Brushes.Red;
                LED2 = Brushes.Red;
                LED3 = Brushes.Red;
                LED4 = Brushes.Red;
                LED5 = Brushes.Red;
                LED6 = Brushes.Red;
                LED7 = Brushes.Red;
                LED8 = Brushes.Red;
                if (Lib.S71KConnect.userItem.DY_ZT)
                    LED1 = Brushes.Green;
                if (Lib.S71KConnect.userItem.Y_CDJ_QGQW)
                    LED2 = Brushes.Green;
                if (Lib.S71KConnect.userItem.Z_CDJ_QGQW)
                    LED3 = Brushes.Green;
                if (Lib.S71KConnect.userItem.JLG_1_XW  && Lib.S71KConnect.userItem.JLG_2_XW
                        && Lib.S71KConnect.userItem.JLG_3_XW && Lib.S71KConnect.userItem.JLG_4_XW)
                    LED4 = Brushes.Green;


                LED5 = Brushes.Green;
                LED6 = Brushes.Green;
                LED7 = Brushes.Green;
                LED8 = Brushes.Green;
                Thread.Sleep(100);

            }

        }
    }
}
