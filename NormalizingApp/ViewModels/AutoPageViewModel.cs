using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using HslCommunication;
using NormalizingApp.Messagebox;
using NormalizingApp.MVVM;
using NormalizingApp.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace NormalizingApp.ViewModels
{
    public class AutoPageViewModel : NotifyObject
    {
        private string fileName = @"..\..\Data\Recipe\DataRecipe.xml";//序列化文件路径


        #region Y,Z位移传感器显示
        private float yLenght;
        public float YLenght
        {
            get { return yLenght; }
            set
            {
                yLenght = value;
                RaisePropertyChanged("YLenght");
            }
        }
        private float zLenght;
        public float ZLenght
        {
            get { return zLenght; }
            set
            {
                zLenght = value;
                RaisePropertyChanged("ZLenght");
            }
        }
        #endregion

        #region X,Y,Z实际坐标显示
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
        #endregion

        #region 风压,流量,能量实际值显示
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
        private float folw;
        public float Folw
        {
            get { return folw; }
            set
            {
                folw = value;
                RaisePropertyChanged("Folw");
            }
        }
        private float powerKWS;
        public float PowerKWS
        {
            get { return powerKWS; }
            set
            {
                powerKWS = value;
                RaisePropertyChanged("PowerKWS");
            }
        }
        #endregion


        #region Y轴找焊缝零位和焊缝高度设定
        private string weldingHome;
        public string WeldingHome
        {
            get { return weldingHome; }
            set
            {
                weldingHome = value;
                RaisePropertyChanged("WeldingHome");
            }
        }
        private string weldingHeight;
        public string WeldingHeight
        {
            get { return weldingHeight; }
            set
            {
                weldingHeight = value;
                RaisePropertyChanged("WeldingHeight ");
            }
        }
        #endregion

        #region Y轴找焊缝零位和焊缝高度设定显示
        private float actWeldingHome;
        public float ActWeldingHome
        {
            get { return actWeldingHome; }
            set
            {
                actWeldingHome = value;
                RaisePropertyChanged("ActWeldingHome");
            }
        }
        private float actWeldingHeight;
        public float ActWeldingHeight
        {
            get { return actWeldingHeight; }
            set
            {
                actWeldingHeight = value;
                RaisePropertyChanged("ActWeldingHeight");
            }
        }
        #endregion

        #region Y轴补偿零位和Z轴补偿零位设定
        private string yCompensationHome;
        public string YCompensationHome
        {
            get { return yCompensationHome; }
            set
            {
                yCompensationHome = value;
                RaisePropertyChanged("YCompensationHome");
            }
        }
        private string zCompensationHome;
        public string ZCompensationHome
        {
            get { return zCompensationHome; }
            set
            {
                zCompensationHome = value;
                RaisePropertyChanged("ZCompensationHome");
            }
        }
        #endregion

        #region Y轴补偿零位和Z轴补偿零位设定显示
        private float actYCompensationHome;
        public float ActYCompensationHome
        {
            get { return actYCompensationHome; }
            set
            {
                actYCompensationHome = value;
                RaisePropertyChanged("ActYCompensationHome");
            }
        }
        private float actZCompensationHome;
        public float ActZCompensationHome
        {
            get { return actZCompensationHome; }
            set
            {
                actZCompensationHome = value;
                RaisePropertyChanged("ActZCompensationHome");
            }
        }
        #endregion

        #region X轴伺服位置和速度设定数据
        private string homePos;
        public string HomePos
        {
            get { return homePos; }
            set
            {
                homePos = value;
                RaisePropertyChanged("HomePos");
            }
        }
        private string homeSpeed;
        public string HomeSpeed
        {
            get { return homeSpeed; }
            set
            {
                homeSpeed = value;
                RaisePropertyChanged("HomeSpeed");
            }
        }
        private string maxPos;
        public string MaxPos
        {
            get { return maxPos; }
            set
            {
                maxPos = value;
                RaisePropertyChanged("MaxPos");
            }
        }
        private string maxSpeed;
        public string MaxSpeed
        {
            get { return maxSpeed; }
            set
            {
                maxSpeed = value;
                RaisePropertyChanged("MaxSpeed");
            }
        }
        private string heatPos;
        public string HeatPos
        {
            get { return heatPos; }
            set
            {
                heatPos = value;
                RaisePropertyChanged("HeatPos");
            }
        }
        private string heatSpeed;
        public string HeatSpeed
        {
            get { return heatSpeed; }
            set
            {
                heatSpeed = value;
                RaisePropertyChanged("HeatSpeed");
            }
        }
        private string windPos;
        public string WindPos
        {
            get { return windPos; }
            set
            {
                windPos = value;
                RaisePropertyChanged("WindPos");
            }
        }
        private string windSpeed;
        public string WindSpeed
        {
            get { return windSpeed; }
            set
            {
                windSpeed = value;
                RaisePropertyChanged("WindSpeed");
            }
        }
        #endregion

        #region X轴伺服位置和速度设定数据显示
        private float actHomePos;
        public float ActHomePos
        {
            get { return actHomePos; }
            set
            {
                actHomePos = value;
                RaisePropertyChanged("ActHomePos");
            }
        }
        private float actHomeSpeed;
        public float ActHomeSpeed
        {
            get { return actHomeSpeed; }
            set
            {
                actHomeSpeed = value;
                RaisePropertyChanged("ActHomeSpeed");
            }
        }
        private float actMaxPos;
        public float ActMaxPos
        {
            get { return actMaxPos; }
            set
            {
                actMaxPos = value;
                RaisePropertyChanged("ActMaxPos");
            }
        }
        private float actMaxSpeed;
        public float ActMaxSpeed
        {
            get { return actMaxSpeed; }
            set
            {
                actMaxSpeed = value;
                RaisePropertyChanged("ActMaxSpeed");
            }
        }
        private float actHeatPos;
        public float ActHeatPos
        {
            get { return actHeatPos; }
            set
            {
                actHeatPos = value;
                RaisePropertyChanged("ActHeatPos");
            }
        }
        private float actHeatSpeed;
        public float ActHeatSpeed
        {
            get { return actHeatSpeed; }
            set
            {
                actHeatSpeed = value;
                RaisePropertyChanged("ActHeatSpeed");
            }
        }
        private float actWindPos;
        public float ActWindPos
        {
            get { return actWindPos; }
            set
            {
                actWindPos = value;
                RaisePropertyChanged("ActWindPos");
            }
        }
        private float actWindSpeed;
        public float ActWindSpeed
        {
            get { return actWindSpeed; }
            set
            {
                actWindSpeed = value;
                RaisePropertyChanged("ActWindSpeed");
            }
        }
        #endregion

        #region 加热工艺参数设定
        private string haetTempSet1;
        public string HaetTempSet1
        {
            get { return haetTempSet1; }
            set
            {
                haetTempSet1 = value;
                RaisePropertyChanged("HaetTempSet1");
            }
        }
        private string haetTempSet2;
        public string HaetTempSet2
        {
            get { return haetTempSet2; }
            set
            {
                haetTempSet2 = value;
                RaisePropertyChanged("HaetTempSet2");
            }
        }
        private string haetTempSet3;
        public string HaetTempSet3
        {
            get { return haetTempSet3; }
            set
            {
                haetTempSet3 = value;
                RaisePropertyChanged("HaetTempSet3");
            }
        }
        private string powerSet1;
        public string PowerSet1
        {
            get { return powerSet1; }
            set
            {
                powerSet1 = value;
                RaisePropertyChanged("PowerSet1");
            }
        }
        private string powerSet2;
        public string PowerSet2
        {
            get { return powerSet2; }
            set
            {
                powerSet2 = value;
                RaisePropertyChanged("PowerSet2");
            }
        }
        private string powerSet3;
        public string PowerSet3
        {
            get { return powerSet3; }
            set
            {
                powerSet3 = value;
                RaisePropertyChanged("PowerSet3");
            }
        }
        private string windTempSet;
        public string WindTempSet
        {
            get { return windTempSet; }
            set
            {
                windTempSet = value;
                RaisePropertyChanged("WindTempSet");
            }
        }
        private string windTimeSet;
        public string WindTimeSet
        {
            get { return windTimeSet; }
            set
            {
                windTimeSet = value;
                RaisePropertyChanged("WindTimeSet");
            }
        }
        #endregion

        #region 加热工艺参数设定显示  
        private float actHaetTempSet1;
        public float ActHaetTempSet1
        {
            get { return actHaetTempSet1; }
            set
            {
                actHaetTempSet1 = value;
                RaisePropertyChanged("ActHaetTempSet1");
            }
        }
        private float actHaetTempSet2;
        public float ActHaetTempSet2
        {
            get { return actHaetTempSet2; }
            set
            {
                actHaetTempSet2 = value;
                RaisePropertyChanged("ActHaetTempSet2");
            }
        }
        private float actHaetTempSet3;
        public float ActHaetTempSet3
        {
            get { return actHaetTempSet3; }
            set
            {
                actHaetTempSet3 = value;
                RaisePropertyChanged("ActHaetTempSet3");
            }
        }
        private float actPowerSet1;
        public float ActPowerSet1
        {
            get { return actPowerSet1; }
            set
            {
                actPowerSet1 = value;
                RaisePropertyChanged("ActPowerSet1");
            }
        }
        private float actPowerSet2;
        public float ActPowerSet2
        {
            get { return actPowerSet2; }
            set
            {
                actPowerSet2 = value;
                RaisePropertyChanged("ActPowerSet2");
            }
        }
        private float actPowerSet3;
        public float ActPowerSet3
        {
            get { return actPowerSet3; }
            set
            {
                actPowerSet3 = value;
                RaisePropertyChanged("ActPowerSet3");
            }
        }
        private float actWindTempSet;
        public float ActWindTempSet
        {
            get { return actWindTempSet; }
            set
            {
                actWindTempSet = value;
                RaisePropertyChanged("ActWindTempSet");
            }
        }
        private float actWindTimeSet;
        public float ActWindTimeSet
        {
            get { return actWindTimeSet; }
            set
            {
                actWindTimeSet = value;
                RaisePropertyChanged("ActWindTimeSet");
            }
        }
        #endregion

        #region 压力,流量,能量上下限设定
        private string pressureMax;
        public string PressureMax
        {
            get { return pressureMax; }
            set
            {
                pressureMax = value;
                RaisePropertyChanged("PressureMax");
            }
        }
        private string pressureMin;
        public string PressureMin
        {
            get { return pressureMin; }
            set
            {
                pressureMin = value;
                RaisePropertyChanged("PressureMin");
            }
        }
        private string flowMax;
        public string FlowMax
        {
            get { return flowMax; }
            set
            {
                flowMax = value;
                RaisePropertyChanged("FlowMax");
            }
        }
        private string flowMin;
        public string FlowMin
        {
            get { return flowMin; }
            set
            {
                flowMin = value;
                RaisePropertyChanged("FlowMin");
            }
        }
        private string powerKWSMax;
        public string PowerKWSMax
        {
            get { return powerKWSMax; }
            set
            {
                powerKWSMax = value;
                RaisePropertyChanged("PowerKWSMax");
            }
        }
        private string powerKWSMin;
        public string PowerKWSMin
        {
            get { return powerKWSMin; }
            set
            {
                powerKWSMin = value;
                RaisePropertyChanged("PowerKWSMin");
            }
        }
        #endregion

        #region 压力,流量,能量上下限设定显示
        private float actPressureMax;
        public float ActPressureMax
        {
            get { return actPressureMax; }
            set
            {
                actPressureMax = value;
                RaisePropertyChanged("ActPressureMax");
            }
        }
        private float actPressureMin;
        public float ActPressureMin
        {
            get { return actPressureMin; }
            set
            {
                actPressureMin = value;
                RaisePropertyChanged("ActPressureMin");
            }
        }
        private float actFlowMax;
        public float ActFlowMax
        {
            get { return actFlowMax; }
            set
            {
                actFlowMax = value;
                RaisePropertyChanged("ActFlowMax");
            }
        }
        private float actFlowMin;
        public float ActFlowMin
        {
            get { return actFlowMin; }
            set
            {
                actFlowMin = value;
                RaisePropertyChanged("ActFlowMin");
            }
        }
        private float actPowerKWSMax;
        public float ActPowerKWSMax
        {
            get { return actPowerKWSMax; }
            set
            {
                actPowerKWSMax = value;
                RaisePropertyChanged("ActPowerKWSMax");
            }
        }
        private float actPowerKWSMin;
        public float ActPowerKWSMin
        {
            get { return actPowerKWSMin; }
            set
            {
                actPowerKWSMin = value;
                RaisePropertyChanged("ActPowerKWSMin");
            }
        }
        #endregion

        #region 自动补偿使能开关数据绑定
        private bool autoCompensationEnabled;
        public bool AutoCompensationEnabled
        {
            get { return autoCompensationEnabled; }
            set
            {
                autoCompensationEnabled = value;
                RaisePropertyChanged("AutoCompensationEnabled");
            }
        }
        #endregion

        #region 自动找焊缝使能开关数据绑定
        private bool autoWeldingEnabled;
        public bool AutoWeldingEnabled
        {
            get { return autoWeldingEnabled; }
            set
            {
                autoWeldingEnabled = value;
                RaisePropertyChanged("AutoWeldingEnabled");
            }
        }
        #endregion

        #region 冷却方式选择开关(温度)数据绑定
        private bool tempMode;
        public bool TempMode
        {
            get { return tempMode; }
            set
            {
                tempMode = value;
                RaisePropertyChanged("TempMode");
            }
        }
        #endregion

        #region 冷却方式选择开关(时间)数据绑定
        private bool timeMode;
        public bool TimeMode
        {
            get { return timeMode; }
            set
            {
                timeMode = value;
                RaisePropertyChanged("TimeMode");
            }
        }
        #endregion




        #region Y轴焊缝零位设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> weldingHomeTextChanged;
        public MyCommand<KeyEventArgs> WeldingHomeTextChanged
        {
            get
            {
                if (weldingHomeTextChanged == null)
                    weldingHomeTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.312", float.Parse(WeldingHome));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch(Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return weldingHomeTextChanged;
            }
        }
        #endregion

        #region 焊缝高度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> weldingHeightTextChanged;
        public MyCommand<KeyEventArgs> WeldingHeightTextChanged
        {
            get
            {
                if (weldingHeightTextChanged == null)
                    weldingHeightTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.308", float.Parse(WeldingHeight));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return weldingHeightTextChanged;
            }
        }
        #endregion

        #region Y轴位移补偿零位设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> yCompensationHomeTextChanged;
        public MyCommand<KeyEventArgs> YCompensationHomeTextChanged
        {
            get
            {
                if (yCompensationHomeTextChanged == null)
                    yCompensationHomeTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.188", float.Parse(YCompensationHome));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return yCompensationHomeTextChanged;
            }
        }
        #endregion

        #region Z轴位移补偿零位设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> zCompensationHomeTextChanged;
        public MyCommand<KeyEventArgs> ZCompensationHomeTextChanged
        {
            get
            {
                if (zCompensationHomeTextChanged == null)
                    zCompensationHomeTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.232", float.Parse(ZCompensationHome));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return zCompensationHomeTextChanged;
            }
        }
        #endregion

        #region 位移补偿使能开关设定命令
        private MyCommand autoCompensationEnabledChecked;
        public MyCommand AutoCompensationEnabledChecked
        {
            get
            {
                if (autoCompensationEnabledChecked == null)
                   autoCompensationEnabledChecked = new MyCommand(
                        new Action<object>(
                          e =>
                            {
                                try
                                {
                                    if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                    OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.186.0", AutoCompensationEnabled);
                                    if (!ret.IsSuccess)
                                    {
                                        CMessageBox.Show("写入失败！", "提示");
                                        return;
                                    }
                                        
                                    Lib.S71KConnect.siemensS7Net.Write("DB6.230.0", AutoCompensationEnabled);
                                }
                                catch (Exception ex)
                                {
                                    CMessageBox.Show(ex.Message, "提示");
                                }
                            }));
                return autoCompensationEnabledChecked;
            }
        }
        #endregion

        #region 自动找焊缝使能开关设定命令
        private MyCommand autoWeldingEnabledChecked;
        public MyCommand AutoWeldingEnabledChecked
        {
            get
            {
                if (autoWeldingEnabledChecked == null)
                    autoWeldingEnabledChecked = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                try
                                {
                                    if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                    OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.306.0", AutoWeldingEnabled);
                                    if (!ret.IsSuccess)
                                        CMessageBox.Show("写入失败！", "提示");
                                }
                                catch (Exception ex)
                                {
                                    CMessageBox.Show(ex.Message, "提示");
                                }
                            }));
                return autoWeldingEnabledChecked;
            }
        }
        #endregion

        #region 加热起始位置设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> homePosTextChanged;
        public MyCommand<KeyEventArgs> HomePosTextChanged
        {
            get
            {
                if (homePosTextChanged == null)
                    homePosTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.274", float.Parse(HomePos));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return homePosTextChanged;
            }
        }
        #endregion

        #region 加热起始位置速度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> homeSpeedTextChanged;
        public MyCommand<KeyEventArgs> HomeSpeedTextChanged
        {
            get
            {
                if (homeSpeedTextChanged == null)
                    homeSpeedTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.278", float.Parse(HomeSpeed));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return homeSpeedTextChanged;
            }
        }
        #endregion

        #region 找焊缝最大距离设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> maxPosTextChanged;
        public MyCommand<KeyEventArgs> MaxPosTextChanged
        {
            get
            {
                if (maxPosTextChanged == null)
                    maxPosTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.282", float.Parse(MaxPos));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return maxPosTextChanged;
            }
        }
        #endregion

        #region 找焊缝最大距离速度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> maxSpeedTextChanged;
        public MyCommand<KeyEventArgs> MaxSpeedTextChanged
        {
            get
            {
                if (maxSpeedTextChanged == null)
                    maxSpeedTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.286", float.Parse(MaxSpeed));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return maxSpeedTextChanged;
            }
        }
        #endregion

        #region 感应器距离设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> heatPosTextChanged;
        public MyCommand<KeyEventArgs> HeatPosTextChanged
        {
            get
            {
                if (heatPosTextChanged == null)
                    heatPosTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.286", float.Parse(HeatPos));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return heatPosTextChanged;
            }
        }
        #endregion

        #region 感应器距离速度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> heatSpeedTextChanged;
        public MyCommand<KeyEventArgs> HeatSpeedTextChanged
        {
            get
            {
                if (heatSpeedTextChanged == null)
                    heatSpeedTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.294", float.Parse(HeatSpeed));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return heatSpeedTextChanged;
            }
        }
        #endregion

        #region 喷风距离设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> windPosTextChanged;
        public MyCommand<KeyEventArgs> WindPosTextChanged
        {
            get
            {
                if (windPosTextChanged == null)
                    windPosTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.298", float.Parse(WindPos));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return windPosTextChanged;
            }
        }
        #endregion

        #region 喷风距离速度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> windSpeedTextChanged;
        public MyCommand<KeyEventArgs> WindSpeedTextChanged
        {
            get
            {
                if (windSpeedTextChanged == null)
                    windSpeedTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.302", float.Parse(WindSpeed));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return windSpeedTextChanged;
            }
        }
        #endregion

        #region 段1温度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> haetTempSet1TextChanged;
        public MyCommand<KeyEventArgs> HaetTempSet1TextChanged
        {
            get
            {
                if (haetTempSet1TextChanged == null)
                    haetTempSet1TextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.320", float.Parse(HaetTempSet1));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return haetTempSet1TextChanged;
            }
        }
        #endregion

        #region 段2温度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> haetTempSet2TextChanged;
        public MyCommand<KeyEventArgs> HaetTempSet2TextChanged
        {
            get
            {
                if (haetTempSet2TextChanged == null)
                    haetTempSet2TextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.328", float.Parse(HaetTempSet2));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return haetTempSet2TextChanged;
            }
        }
        #endregion

        #region 段3温度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> haetTempSet3TextChanged;
        public MyCommand<KeyEventArgs> HaetTempSet3TextChanged
        {
            get
            {
                if (haetTempSet3TextChanged == null)
                    haetTempSet3TextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.336", float.Parse(HaetTempSet3));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return haetTempSet3TextChanged;
            }
        }
        #endregion

        #region 段1功率设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> powerSet1TextChanged;
        public MyCommand<KeyEventArgs> PowerSet1TextChanged
        {
            get
            {
                if (powerSet1TextChanged == null)
                    powerSet1TextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.316", float.Parse(PowerSet1));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return powerSet1TextChanged;
            }
        }
        #endregion

        #region 段2功率设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> powerSet2TextChanged;
        public MyCommand<KeyEventArgs> PowerSet2TextChanged
        {
            get
            {
                if (powerSet2TextChanged == null)
                    powerSet2TextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.324", float.Parse(PowerSet2));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return powerSet2TextChanged;
            }
        }
        #endregion

        #region 段3功率设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> powerSet3TextChanged;
        public MyCommand<KeyEventArgs> PowerSet3TextChanged
        {
            get
            {
                if (powerSet3TextChanged == null)
                    powerSet3TextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.332", float.Parse(PowerSet3));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return powerSet3TextChanged;
            }
        }
        #endregion

        #region 喷风选择温度设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> windTempSetTextChanged;
        public MyCommand<KeyEventArgs> WindTempSetTextChanged
        {
            get
            {
                if (windTempSetTextChanged == null)
                    windTempSetTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.346", float.Parse(WindTempSet));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return windTempSetTextChanged;
            }
        }
        #endregion

        #region 喷风选择时间设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> windTimeSetTextChanged;
        public MyCommand<KeyEventArgs> WindTimeSetTextChanged
        {
            get
            {
                if (windTimeSetTextChanged == null)
                    windTimeSetTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.342", float.Parse(WindTimeSet));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return windTimeSetTextChanged;
            }
        }
        #endregion

        #region 喷风选择温度开关设定命令
        private MyCommand tempModeClick;
        public MyCommand TempModeClick
        {
            get
            {
                if (tempModeClick == null)
                    tempModeClick = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                try
                                {
                                    if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                    OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.340.1", TempMode);
                                    if (!ret.IsSuccess)
                                    {
                                        CMessageBox.Show("写入失败！", "提示");
                                        return;
                                    }   
                                    Lib.S71KConnect.siemensS7Net.Write("DB6.340.0", false);
                                }
                                catch (Exception ex)
                                {
                                    CMessageBox.Show(ex.Message, "提示");
                                }
                                
                            }));
                return tempModeClick;
            }
        }
        #endregion

        #region 喷风选择温度开关设定命令
        private MyCommand timeModeClick;
        public MyCommand TimeModeClick
        {
            get
            {
                if (timeModeClick == null)
                    timeModeClick = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                try
                                {
                                    if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                    OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.340.0", TimeMode);
                                    if (!ret.IsSuccess)
                                    {
                                        CMessageBox.Show("写入失败！", "提示");
                                        return;
                                    }
                                    Lib.S71KConnect.siemensS7Net.Write("DB6.340.1", false);
                                }
                                catch (Exception ex)
                                {
                                    CMessageBox.Show(ex.Message, "提示");
                                }
                            }));
                return timeModeClick;
            }
        }
        #endregion

        #region 压力上限设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> pressureMaxTextChanged;
        public MyCommand<KeyEventArgs> PressureMaxTextChanged
        {
            get
            {
                if (pressureMaxTextChanged == null)
                    pressureMaxTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.226", float.Parse(PressureMax));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return pressureMaxTextChanged;
            }
        }
        #endregion

        #region 压力下限设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> pressureMinTextChanged;
        public MyCommand<KeyEventArgs> PressureMinTextChanged
        {
            get
            {
                if (pressureMinTextChanged == null)
                    pressureMinTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.230", float.Parse(PressureMin));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return pressureMinTextChanged;
            }
        }
        #endregion

        #region 流量上限设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> flowMaxTextChanged;
        public MyCommand<KeyEventArgs> FlowMaxTextChanged
        {
            get
            {
                if (flowMaxTextChanged == null)
                    flowMaxTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.234", float.Parse(FlowMax));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return flowMaxTextChanged;
            }
        }
        #endregion

        #region 流量下限设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> flowMinTextChanged;
        public MyCommand<KeyEventArgs> FlowMinTextChanged
        {
            get
            {
                if (flowMinTextChanged == null)
                    flowMinTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.238", float.Parse(FlowMin));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return flowMinTextChanged;
            }
        }
        #endregion

        #region 能量上限设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> powerKWSMaxTextChanged;
        public MyCommand<KeyEventArgs> PowerKWSMaxTextChanged
        {
            get
            {
                if (powerKWSMaxTextChanged == null)
                    powerKWSMaxTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.242", float.Parse(PowerKWSMax));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return powerKWSMaxTextChanged;
            }
        }
        #endregion

        #region 能量下限设定事件(输入框按下Enter时发生)
        private MyCommand<KeyEventArgs> powerKWSMinTextChanged;
        public MyCommand<KeyEventArgs> PowerKWSMinTextChanged
        {
            get
            {
                if (powerKWSMinTextChanged == null)
                    powerKWSMinTextChanged = new MyCommand<KeyEventArgs>(
                        new Action<KeyEventArgs>(
                            e =>
                            {
                                if (e.Key == Key.Enter) //按下回车键
                                {
                                    try
                                    {
                                        if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                                        OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB6.246", float.Parse(PowerKWSMin));
                                        if (!ret.IsSuccess)
                                            CMessageBox.Show("写入失败！", "提示");
                                    }
                                    catch (Exception ex)
                                    {
                                        CMessageBox.Show(ex.Message, "提示");
                                    }
                                }
                            }));
                return powerKWSMinTextChanged;
            }
        }
        #endregion



        List<MyDataRecipe> myDataRecipes = new List<MyDataRecipe>();

        #region 配方新建名称数据绑定
        private string newNameText;
        public string NewNameText
        {
            get { return newNameText; }
            set
            {
                newNameText = value;
                RaisePropertyChanged("NewNameText");
            }
        }
        #endregion

        #region 配方新建名称输入框使能数据绑定
        private bool newNameTextEnable;
        public bool NewNameTextEnable
        {
            get { return newNameTextEnable; }
            set
            {
                newNameTextEnable = value;
                RaisePropertyChanged("NewNameTextEnable");
            }
        }
        #endregion

        #region 配方列表集合绑定
        private ObservableCollection<string> comboBoxItems = new ObservableCollection<string>();
        public ObservableCollection<string> ComboBoxItems
        {
            get { return comboBoxItems; }
            set
            {
                comboBoxItems = value;
                RaisePropertyChanged("ComboBoxItems");
            }
        }
        #endregion

        #region 配方列表内容数据绑定
        private string comboBoxSelectedItem;
        public string ComboBoxSelectedItem
        {
            get { return comboBoxSelectedItem; }
            set
            {
                comboBoxSelectedItem = value;
                RaisePropertyChanged("ComboBoxSelectedItem");
            }
        }
        #endregion

        #region 配方列表索引数据绑定
        private int comboBoxSelectedIndex;
        public int ComboBoxSelectedIndex
        {
            get { return comboBoxSelectedIndex; }
            set
            {
                comboBoxSelectedIndex = value;
                RaisePropertyChanged("ComboBoxSelectedIndex");
            }
        }
        #endregion

        #region 配方数据显示集合绑定
        private ObservableCollection<RecipeDataDisp> recipeData = new ObservableCollection<RecipeDataDisp>();
        public ObservableCollection<RecipeDataDisp> RecipeData
        {
            get { return recipeData; }
            set
            {
                recipeData = value;
                RaisePropertyChanged("RecipeData");
            }
        }
        #endregion

        #region 配方新建按钮事件
        private MyCommand newButtonClick;
        public MyCommand NewButtonClick
        {
            get
            {
                if (newButtonClick == null)
                    newButtonClick = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                if (CMessageBoxResult.Yes == CMessageBox.Show("确定新建一个程序吗?", "提示", CMessageBoxButton.YesNO, CMessageBoxImage.Question))
                                {
                                        CMessageBox.Show("请在输入框填写名称,然后点击保存！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                        NewNameTextEnable = true;//新建名称输入框取消使能    
                                }
                            }));
                return newButtonClick;
            }
        }
        #endregion

        #region 配方保存按钮事件
        private MyCommand saveButtonClick;
        public MyCommand SaveButtonClick
        {
            get
            {
                if (saveButtonClick == null)
                    saveButtonClick = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                if (CMessageBoxResult.No == CMessageBox.Show("确定保存程序吗?", "提示", CMessageBoxButton.YesNO, CMessageBoxImage.Question))
                                {
                                    return;
                                }

                                if ((string.IsNullOrEmpty(NewNameText) || NewNameText.Trim() == string.Empty) && NewNameTextEnable)
                                {
                                    CMessageBox.Show("配方名称不能为空", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                    return;
                                }
                                if ((string.IsNullOrEmpty(ComboBoxSelectedItem) || ComboBoxSelectedItem == string.Empty) && !NewNameTextEnable)
                                {
                                    CMessageBox.Show("没有配方数据记录", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                    return;
                                }


                                if (NewNameTextEnable) //判断是否为新建程序
                                {
                                    var result = from buff in myDataRecipes where !string.IsNullOrEmpty(buff.ListName) && buff.ListName == NewNameText select buff;
                                    if (result == null) return;
                                    if (result.ToList().Count == 0)
                                    {
                                        myDataRecipes.Add(ItemToRecipeList());
                                        Lib.BinaryFile.FileName = fileName;
                                        Lib.BinaryFile.SaveBinary(myDataRecipes.ConvertAll(s => (object)s));
                                        //读取文件内数据
                                        ReadBinaryFileData();
                                        ComboBoxSelectedIndex = ComboBoxItems.Count - 1;
                                        CMessageBox.Show("新建成功！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                    }
                                    else
                                    {
                                        CMessageBox.Show("不能有重复名称!", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                        return;
                                    }
                                }
                                else
                                {
                                    var result = from buff in myDataRecipes where !string.IsNullOrEmpty(buff.ListName) && buff.ListName == ComboBoxSelectedItem select buff;
                                    if (result == null) return;
                                    if (result.ToList().Count == 1)
                                    {
                                        myDataRecipes.Remove(myDataRecipes.FirstOrDefault(obj => obj.ListName == ComboBoxSelectedItem));
                                        myDataRecipes.Add(ItemToRecipeList());
                                        Lib.BinaryFile.FileName = fileName;
                                        Lib.BinaryFile.SaveBinary(myDataRecipes.ConvertAll(s => (object)s));
                                        //读取文件内数据
                                        ReadBinaryFileData();
                                        ComboBoxSelectedIndex = ComboBoxItems.Count - 1;
                                        CMessageBox.Show("保存成功！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                    }
                                    else
                                    {
                                        CMessageBox.Show("未选择配方！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                    }
                                }
                                NewNameTextEnable = false;//新建名称输入框取消使能
                                NewNameText = string.Empty;
                                UpRecipeDataList();
                            }));
                return saveButtonClick;
            }
        }
        #endregion

        #region 配方下载按钮事件
        private MyCommand loadButtonClick;
        public MyCommand LoadButtonClick
        {
            get
            {
                if (loadButtonClick == null)
                    loadButtonClick = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                if (string.IsNullOrEmpty(ComboBoxSelectedItem) || ComboBoxSelectedItem == string.Empty)
                                {
                                    CMessageBox.Show("未选择配方！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                    return;
                                }
                                WriteRecipeToValue();
                                Lib.INIFile.InifileWriteValue("配方", "元素", ComboBoxSelectedIndex.ToString(), Lib.INIFile.iniPath);
                                CMessageBox.Show("下载成功", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                            }));
                return loadButtonClick;
            }
        }
        #endregion

        #region 配方删除按钮事件
        private MyCommand deleteButtonClick;
        public MyCommand DeleteButtonClick
        {
            get
            {
                if (deleteButtonClick == null)
                    deleteButtonClick = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                if (string.IsNullOrEmpty(ComboBoxSelectedItem) || ComboBoxSelectedItem == string.Empty)
                                {
                                    CMessageBox.Show("未选择配方！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                    return;
                                }
                                if (CMessageBoxResult.Yes == CMessageBox.Show("确定删除此程序吗?", "提示", CMessageBoxButton.YesNO, CMessageBoxImage.Question))
                                {
                                    myDataRecipes.Remove(myDataRecipes.FirstOrDefault(obj => obj.ListName == ComboBoxSelectedItem));
                                    Lib.BinaryFile.FileName = fileName;
                                    Lib.BinaryFile.SaveBinary(myDataRecipes.ConvertAll(s => (object)s));
                                    //读取文件内数据
                                    ReadBinaryFileData();
                                    ComboBoxSelectedIndex = ComboBoxItems.Count - 1;
                                    CMessageBox.Show("删除成功！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                                    UpRecipeDataList();
                                }

                            }));
                return deleteButtonClick;
            }
        }
        #endregion

        #region 配方选择列表选择改变事件
        private MyCommand recipeCbxSelectionChanged;
        public MyCommand RecipeCbxSelectionChanged
        {
            get
            {
                UpRecipeDataList();
                if (recipeCbxSelectionChanged == null)
                    recipeCbxSelectionChanged = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                UpRecipeDataList();
                            }));
                return recipeCbxSelectionChanged;
            }
        }
        #endregion

        #region 配方选择列表关闭事件
        private MyCommand recipeCbxDropDownClosed;
        public MyCommand RecipeCbxDropDownClosed
        {
            get
            {
                if (recipeCbxDropDownClosed == null)
                    recipeCbxDropDownClosed = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                UpRecipeDataList();
                            }));
                return recipeCbxDropDownClosed;
            }
        }
        #endregion



        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoPageViewModel()
        {
            UpData();//更新实时参数
            NewNameTextEnable = false;//新建名称输入框取消使能

            //读取文件内数据
            ReadBinaryFileData();
            int comboBoxIndex = int.Parse(Lib.INIFile.InifileReadValue("配方", "元素", Lib.INIFile.iniPath));
            if (ComboBoxItems.Count > comboBoxIndex)
                ComboBoxSelectedIndex = comboBoxIndex;
            else
                ComboBoxSelectedIndex = -1;
            

        }

        /// <summary>
        /// 读取一次所有设置参数IO域
        /// </summary>
        private async void InitValueRead()
        {
            await Task.Run(() =>
            {
                if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                ActWeldingHeight = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.308").Content;
                ActWeldingHome = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.312").Content;
                ActYCompensationHome = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.188").Content;
                ActZCompensationHome = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.232").Content;
                AutoCompensationEnabled = Lib.S71KConnect.siemensS7Net.ReadBool("DB6.186.0").Content;
                AutoWeldingEnabled = Lib.S71KConnect.siemensS7Net.ReadBool("DB6.306.0").Content;

                ActHomePos = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.274").Content;
                ActHomeSpeed = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.278").Content;
                ActMaxPos = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.282").Content;
                ActMaxSpeed = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.286").Content;
                ActHeatPos = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.290").Content;
                ActHeatSpeed = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.294").Content;
                ActWindPos = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.298").Content;
                ActWindSpeed = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.302").Content;

                ActHaetTempSet1 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.320").Content;
                ActHaetTempSet2 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.328").Content;
                ActHaetTempSet3 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.336").Content;
                ActPowerSet1 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.316").Content;
                ActPowerSet2 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.324").Content;
                ActPowerSet3 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.332").Content;
                ActWindTempSet = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.346").Content;
                ActWindTimeSet = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.342").Content;
                TempMode = Lib.S71KConnect.siemensS7Net.ReadBool("DB6.340.1").Content;
                TimeMode = Lib.S71KConnect.siemensS7Net.ReadBool("DB6.340.0").Content;
            });
        }

        /// <summary>
        /// 实时数据刷新
        /// </summary>
        private async void UpData()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (!Lib.S71KConnect.userItem.ConnectionState) return;//PLC未连接成功就不执行下面代码
                    XPos = Lib.S71KConnect.userItem.XActPos;
                    YPos = Lib.S71KConnect.userItem.YActPos;
                    ZPos = Lib.S71KConnect.userItem.ZActPos;
                    YLenght = Lib.S71KConnect.userItem.ActLenght1;
                    ZLenght = Lib.S71KConnect.userItem.ActLenght2;
                    Pressure = Lib.S71KConnect.userItem.ActPressure;
                    Folw = Lib.S71KConnect.userItem.ActFlow1;
                    PowerKWS = Lib.S71KConnect.userItem.ActFlow1;

                    
                    ActWeldingHeight = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.308").Content;
                    ActWeldingHome = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.312").Content;
                    ActYCompensationHome = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.188").Content;
                    ActZCompensationHome = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.232").Content;
                    AutoCompensationEnabled = Lib.S71KConnect.siemensS7Net.ReadBool("DB6.186.0").Content;
                    AutoWeldingEnabled = Lib.S71KConnect.siemensS7Net.ReadBool("DB6.306.0").Content;

                    ActHomePos = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.274").Content;
                    ActHomeSpeed = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.278").Content;
                    ActMaxPos = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.282").Content;
                    ActMaxSpeed = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.286").Content;
                    ActHeatPos = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.290").Content;
                    ActHeatSpeed = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.294").Content;
                    ActWindPos = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.298").Content;
                    ActWindSpeed = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.302").Content;

                    ActHaetTempSet1 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.320").Content;
                    ActHaetTempSet2 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.328").Content;
                    ActHaetTempSet3 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.336").Content;
                    ActPowerSet1 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.316").Content;
                    ActPowerSet2 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.324").Content;
                    ActPowerSet3 = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.332").Content;
                    ActWindTempSet = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.346").Content;
                    ActWindTimeSet = Lib.S71KConnect.siemensS7Net.ReadFloat("DB6.342").Content;
                    TempMode = Lib.S71KConnect.siemensS7Net.ReadBool("DB6.340.1").Content;
                    TimeMode = Lib.S71KConnect.siemensS7Net.ReadBool("DB6.340.0").Content;
                    Thread.Sleep(100);
                }
            });
        }



        
        /// <summary>
        /// 读取所有参数值到Item
        /// </summary>
        /// <returns>返回集合列表</returns>
        private List<MyDataRecipeItem> ReadToRecipeItem()
        {
            List<MyDataRecipeItem> myDatas = new List<MyDataRecipeItem>();
            myDatas.Add(new MyDataRecipeItem("Y轴焊缝零位", "DB6.312", ActWeldingHome, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("焊缝高度", "DB6.308", ActWeldingHeight, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("Y轴补偿零位", "DB6.188", ActYCompensationHome, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("Z轴补偿零位", "DB6.232", ActZCompensationHome, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("Y自动补偿使能", "DB6.186.0", AutoCompensationEnabled, RecipeDataType.MyBool));
            myDatas.Add(new MyDataRecipeItem("Z自动补偿使能", "DB6.230.0", AutoCompensationEnabled, RecipeDataType.MyBool));
            myDatas.Add(new MyDataRecipeItem("自动找焊缝使能", "DB6.306.0", AutoWeldingEnabled, RecipeDataType.MyBool));
            myDatas.Add(new MyDataRecipeItem("X轴起始位置", "DB6.274", ActHomePos, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("X轴起始位速度", "DB6.278", ActHomeSpeed, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("X轴找焊缝最大距离", "DB6.282", ActMaxPos, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("X轴找焊缝速度", "DB6.286", ActMaxSpeed, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("X轴加热距离", "DB6.290", ActHeatPos, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("X轴加热位速度", "DB6.294", ActHeatSpeed, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("X轴喷风距离", "DB6.298", ActWindPos, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("X轴喷风移动速度", "DB6.302", ActWindSpeed, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("起始温度设定", "DB6.320", ActHaetTempSet1, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("转变温度设定", "DB6.328", ActHaetTempSet2, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("最终温度设定", "DB6.336", ActHaetTempSet3, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("起始功率设定", "DB6.316", ActPowerSet1, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("转变功率设定", "DB6.324", ActPowerSet2, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("最终功率设定", "DB6.332", ActPowerSet3, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("喷风温度控制", "DB6.340.1", TempMode, RecipeDataType.MyBool));
            myDatas.Add(new MyDataRecipeItem("喷风时间控制", "DB6.340.0", TimeMode, RecipeDataType.MyBool));
            myDatas.Add(new MyDataRecipeItem("喷风温度设定", "DB6.346", ActWindTempSet, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("喷风时间设定", "DB6.342", ActWindTimeSet, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("风压上限", "DB6.226", ActPressureMax, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("风压下限", "DB6.230", ActPressureMin, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("流量上限", "DB6.234", ActFlowMax, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("流量下限", "DB6.238", ActFlowMin, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("能量上限", "DB6.242", ActPowerKWSMax, RecipeDataType.MyFloat));
            myDatas.Add(new MyDataRecipeItem("能量下限", "DB6.246", ActPowerKWSMin, RecipeDataType.MyFloat));
            return myDatas;
        }


        /// <summary>
        /// 读取一个配方列表到List
        /// </summary>
        private MyDataRecipe ItemToRecipeList()
        {
            MyDataRecipe recipe = new MyDataRecipe();
            if (NewNameTextEnable)
                recipe.ListName = NewNameText.Trim();
            else
                recipe.ListName = ComboBoxSelectedItem;
            recipe.MyDataRecipeList = ReadToRecipeItem();
            return recipe;
        }

        /// <summary>
        /// 读取反序列化文件数据并更新显示列表
        /// </summary>
        private void ReadBinaryFileData()
        {
            ComboBoxItems.Clear();
            Lib.BinaryFile.FileName = fileName;
            myDataRecipes = Lib.BinaryFile.ReadBinary().ConvertAll(s => (MyDataRecipe)s);
            foreach (var item in myDataRecipes)
            {
                ComboBoxItems.Add(item.ListName);
            }
        }
        /// <summary>
        /// 配方内的数据写入变量
        /// </summary>
        private async void WriteRecipeToValue()
        {
            await Task.Run(() =>
            {
                var result = from buff in myDataRecipes where !string.IsNullOrEmpty(buff.ListName) && buff.ListName == ComboBoxSelectedItem select buff;
                if (result == null || result.ToList().Count == 0) return;
                MyDataRecipe bu = result.ToList()[0];
                foreach (var item in bu.MyDataRecipeList)
                {
                    switch (item.DataType)
                    {
                        case RecipeDataType.MyBool:
                            Lib.S71KConnect.siemensS7Net.Write(item.Address, (bool)item.Data);
                            break;
                        case RecipeDataType.MyFloat:
                            Lib.S71KConnect.siemensS7Net.Write(item.Address, (float)item.Data);
                            break;
                        case RecipeDataType.MyInt:
                            Lib.S71KConnect.siemensS7Net.Write(item.Address, (int)item.Data);
                            break;
                    }
                }
            });
        }
        /// <summary>
        /// 更新配方视图里数据列表
        /// </summary>
        private void UpRecipeDataList()
        {
            int i = 1;
            RecipeData.Clear();
            var result = from buff in myDataRecipes where !string.IsNullOrEmpty(buff.ListName) && buff.ListName == ComboBoxSelectedItem select buff;
            if (result == null || result.ToList().Count == 0) return;
            MyDataRecipe bu = result.ToList()[0];
            foreach (var item in bu.MyDataRecipeList)
            {
                RecipeData.Add(new RecipeDataDisp(i, item.ValueName, item.Data.ToString()));
                i++;
            }
        }

    }
}
