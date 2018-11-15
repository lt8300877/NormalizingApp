using System;
using System.Threading;
using System.Threading.Tasks;
using HslCommunication;
using System.Windows.Media;
using NormalizingApp.MVVM;
using NormalizingApp.Messagebox;
using System.Windows.Input;

namespace NormalizingApp.ViewModels

{
    public class ManualPageViewModel:NotifyObject
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
        #region 手动加热控制框颜色数据
        private Brush powerBrush;
        public Brush PowerBrush
        {
            get { return powerBrush; }
            set
            {
                powerBrush = value;
                RaisePropertyChanged("PowerBrush");
            }
        }
        #endregion
        #region 喷风控制框颜色数据
        private Brush windBrush;
        public Brush WindBrush
        {
            get { return windBrush; }
            set
            {
                windBrush = value;
                RaisePropertyChanged("WindBrush");
            }
        }
        #endregion
        #region Y轴位移气缸控制框颜色数据
        private Brush yCylinderBrush;
        public Brush YCylinderBrush
        {
            get { return yCylinderBrush; }
            set
            {
                yCylinderBrush = value;
                RaisePropertyChanged("YCylinderBrush");
            }
        }
        #endregion
        #region Z轴位移气缸控制框颜色数据
        private Brush zCylinderBrush;
        public Brush ZCylinderBrush
        {
            get { return zCylinderBrush; }
            set
            {
                zCylinderBrush = value;
                RaisePropertyChanged("ZCylinderBrush");
            }
        }
        #endregion
        #region 1#顶料气缸控制框颜色数据
        private Brush dL1CylinderBrush;
        public Brush DL1CylinderBrush
        {
            get { return dL1CylinderBrush; }
            set
            {
                dL1CylinderBrush = value;
                RaisePropertyChanged("DL1CylinderBrush");
            }
        }
        #endregion
        #region 2#顶料气缸控制框颜色数据
        private Brush dL2CylinderBrush;
        public Brush DL2CylinderBrush
        {
            get { return dL2CylinderBrush; }
            set
            {
                dL2CylinderBrush = value;
                RaisePropertyChanged("DL2CylinderBrush");
            }
        }
        #endregion
        #region 3#顶料气缸控制框颜色数据
        private Brush dL3CylinderBrush;
        public Brush DL3CylinderBrush
        {
            get { return dL3CylinderBrush; }
            set
            {
                dL3CylinderBrush = value;
                RaisePropertyChanged("DL3CylinderBrush");
            }
        }
        #endregion
        #region 4#顶料气缸控制框颜色数据
        private Brush dL4CylinderBrush;
        public Brush DL4CylinderBrush
        {
            get { return dL4CylinderBrush; }
            set
            {
                dL4CylinderBrush = value;
                RaisePropertyChanged("DL4CylinderBrush");
            }
        }
        #endregion

        #region 功率设定数据
        private string powerSet;
        public string PowerSet
        {
            get { return powerSet; }
            set
            {
                powerSet = value;
                RaisePropertyChanged("PowerSet");
            }
        }
        #endregion
        #region 功率设定数据读取
        private float actPowerSet;
        public float ActPowerSet
        {
            get { return actPowerSet; }
            set
            {
                actPowerSet = value;
                RaisePropertyChanged("ActPowerSet");
            }
        }
        #endregion



        #region 手动加热时间设定数据
        private string powerTimeSet;
        public string PowerTimeSet
        {
            get { return powerTimeSet; }
            set
            {
                powerTimeSet = value;
                RaisePropertyChanged("PowerTimeSet");
            }
        }
        #endregion
        #region 手动加热时间设定数据读取
        private float actPowerTimeSet;
        public float ActPowerTimeSet
        {
            get { return actPowerTimeSet; }
            set
            {
                actPowerTimeSet = value;
                RaisePropertyChanged("ActPowerTimeSet");
            }
        }
        #endregion

        #region 电源启动按钮事件
        private MyCommand powerStartButtonClick;
        public MyCommand PowerStartButtonClick
        {
            get
            {
                if (powerStartButtonClick == null)
                    powerStartButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                Lib.S71KConnect.siemensS7Net.Write("DB7.66.0", true);
                                Lib.S71KConnect.siemensS7Net.Write("DB7.66.0", false);
                            }));
                return powerStartButtonClick;
            }
        }
        #endregion
        #region 电源停止按钮事件
        private MyCommand powerStopButtonClick;
        public MyCommand PowerStopButtonClick
        {
            get
            {
                if (powerStopButtonClick == null)
                    powerStopButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.66.1", true);
                                if( ret.IsSuccess)
                                Lib.S71KConnect.siemensS7Net.Write("DB7.66.1", false);
                            }));
                return powerStopButtonClick;
            }
        }
        #endregion
        #region 喷风启动按钮事件
        private MyCommand windStartButtonClick;
        public MyCommand WindStartButtonClick
        {
            get
            {
                if (windStartButtonClick == null)
                    windStartButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                Lib.S71KConnect.siemensS7Net.Write("DB7.72.0", true);
                                Lib.S71KConnect.siemensS7Net.Write("DB7.72.0", false);
                            }));
                return windStartButtonClick;
            }
        }
        #endregion
        #region 喷风停止按钮事件
        private MyCommand windStopButtonClick;
        public MyCommand WindStopButtonClick
        {
            get
            {
                if (windStopButtonClick == null)
                    windStopButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.72.1", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.72.1", false);
                            }));
                return windStopButtonClick;
            }
        }
        #endregion
        #region Y轴位移气缸前进按钮事件
        private MyCommand yCylinderStartButtonClick;
        public MyCommand YCylinderStartButtonClick
        {
            get
            {
                if (yCylinderStartButtonClick == null)
                    yCylinderStartButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.74.0", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.74.0", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return yCylinderStartButtonClick;
            }
        }
        #endregion
        #region Y轴位移气缸后退按钮事件
        private MyCommand yCylinderStopButtonClick;
        public MyCommand YCylinderStopButtonClick
        {
            get
            {
                if (yCylinderStopButtonClick == null)
                    yCylinderStopButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.74.1", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.74.1", false);
                                else
                                    CMessageBox.Show("写入失败！","提示");
                            }));
                return yCylinderStopButtonClick;
            }
        }
        #endregion
        #region Z轴位移气缸前进按钮事件
        private MyCommand zCylinderStartButtonClick;
        public MyCommand ZCylinderStartButtonClick
        {
            get
            {
                if (zCylinderStartButtonClick == null)
                    zCylinderStartButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.74.2", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.74.2", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return zCylinderStartButtonClick;
            }
        }
        #endregion
        #region Z轴位移气缸后退按钮事件
        private MyCommand zCylinderStopButtonClick;
        public MyCommand ZCylinderStopButtonClick
        {
            get
            {
                if (zCylinderStopButtonClick == null)
                    zCylinderStopButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.74.3", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.74.3", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return zCylinderStopButtonClick;
            }
        }
        #endregion
        #region 1#顶料气缸上升按钮事件
        private MyCommand dL1CylinderStartButtonClick;
        public MyCommand DL1CylinderStartButtonClick
        {
            get
            {
                if (dL1CylinderStartButtonClick == null)
                    dL1CylinderStartButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.76.0", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.76.0", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return dL1CylinderStartButtonClick;
            }
        }
        #endregion
        #region 1#顶料气缸下降按钮事件
        private MyCommand dL1CylinderStopButtonClick;
        public MyCommand DL1CylinderStopButtonClick
        {
            get
            {
                if (dL1CylinderStopButtonClick == null)
                    dL1CylinderStopButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.76.1", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.76.1", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return dL1CylinderStopButtonClick;
            }
        }
        #endregion
        #region 2#顶料气缸上升按钮事件
        private MyCommand dL2CylinderStartButtonClick;
        public MyCommand DL2CylinderStartButtonClick
        {
            get
            {
                if (dL2CylinderStartButtonClick == null)
                    dL2CylinderStartButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.76.2", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.76.2", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return dL2CylinderStartButtonClick;
            }
        }
        #endregion
        #region 2#顶料气缸下降按钮事件
        private MyCommand dL2CylinderStopButtonClick;
        public MyCommand DL2CylinderStopButtonClick
        {
            get
            {
                if (dL2CylinderStopButtonClick == null)
                    dL2CylinderStopButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.76.3", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.76.3", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return dL2CylinderStopButtonClick;
            }
        }
        #endregion
        #region 3#顶料气缸上升按钮事件
        private MyCommand dL3CylinderStartButtonClick;
        public MyCommand DL3CylinderStartButtonClick
        {
            get
            {
                if (dL3CylinderStartButtonClick == null)
                    dL3CylinderStartButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.76.4", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.76.4", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return dL3CylinderStartButtonClick;
            }
        }
        #endregion
        #region 3#顶料气缸下降按钮事件
        private MyCommand dL3CylinderStopButtonClick;
        public MyCommand DL3CylinderStopButtonClick
        {
            get
            {
                if (dL3CylinderStopButtonClick == null)
                    dL3CylinderStopButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.76.5", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.76.5", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return dL3CylinderStopButtonClick;
            }
        }
        #endregion
        #region 4#顶料气缸上升按钮事件
        private MyCommand dL4CylinderStartButtonClick;
        public MyCommand DL4CylinderStartButtonClick
        {
            get
            {
                if (dL4CylinderStartButtonClick == null)
                    dL4CylinderStartButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.76.6", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.76.6", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return dL4CylinderStartButtonClick;
            }
        }
        #endregion
        #region 4#顶料气缸下降按钮事件
        private MyCommand dL4CylinderStopButtonClick;
        public MyCommand DL4CylinderStopButtonClick
        {
            get
            {
                if (dL4CylinderStopButtonClick == null)
                    dL4CylinderStopButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.76.7", true);
                                if (ret.IsSuccess)
                                    Lib.S71KConnect.siemensS7Net.Write("DB7.76.7", false);
                                else
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return dL4CylinderStopButtonClick;
            }
        }
        #endregion
        #region 功率设定输入事件绑定
        private MyCommand<KeyEventArgs> powerSetTextChanged;
        public MyCommand<KeyEventArgs> PowerSetTextChanged
        {
            get
            {
                if (powerSetTextChanged == null)
                    powerSetTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.80", float.Parse(PowerSet));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch(Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                        
                                }
                            }));
                return powerSetTextChanged;
            }
        }
        #endregion
        #region 手动加热时间设定输入事件绑定
        private MyCommand<KeyEventArgs> powerTimeSetTextChanged;
        public MyCommand<KeyEventArgs> PowerTimeSetTextChanged
        {
            get
            {
                if (powerTimeSetTextChanged == null)
                    powerTimeSetTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB7.68", float.Parse(PowerTimeSet));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch(Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                    

                                }
                            }));
                return powerTimeSetTextChanged;
            }
        }
        #endregion
        Thread thread;

        public ManualPageViewModel()
        {
            thread = new Thread(new ThreadStart(UpData));
            thread.IsBackground = true;
            thread.Start();
        }

        private void UpData()
        {
            while (true)
            {
                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
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
                PowerBrush = Brushes.Transparent;
                WindBrush = Brushes.Transparent;
                YCylinderBrush = Brushes.Transparent;
                ZCylinderBrush = Brushes.Transparent;
                DL1CylinderBrush = Brushes.Transparent;
                DL2CylinderBrush = Brushes.Transparent;
                DL3CylinderBrush = Brushes.Transparent;
                DL4CylinderBrush = Brushes.Transparent;
                if (Lib.S71KConnect.userItem.DY_ZT)
                    PowerBrush = Brushes.GreenYellow;
                if(Lib.S71KConnect.userItem.PQ_ZT)
                    WindBrush = Brushes.GreenYellow;
                if (Lib.S71KConnect.userItem.Y_CDJ_QGQW)
                    YCylinderBrush = Brushes.GreenYellow;
                if (Lib.S71KConnect.userItem.Z_CDJ_QGQW)
                    ZCylinderBrush = Brushes.GreenYellow;

                if (Lib.S71KConnect.userItem.JLG_1_SW)
                    DL1CylinderBrush = Brushes.GreenYellow;
                if (Lib.S71KConnect.userItem.JLG_2_SW)
                    DL2CylinderBrush = Brushes.GreenYellow;
                if (Lib.S71KConnect.userItem.JLG_3_SW)
                    DL3CylinderBrush = Brushes.GreenYellow;
                if (Lib.S71KConnect.userItem.JLG_4_SW)
                    DL4CylinderBrush = Brushes.GreenYellow;

                ActPowerSet = Lib.S71KConnect.siemensS7Net.ReadFloat("DB7.80").Content;
                ActPowerTimeSet = Lib.S71KConnect.siemensS7Net.ReadFloat("DB7.68").Content;

                Thread.Sleep(100);

            }

        }
        
    }
}
