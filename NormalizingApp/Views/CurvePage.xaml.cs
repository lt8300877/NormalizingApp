using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NormalizingApp.Lib;
using System.Windows.Media;
using NormalizingApp.Messagebox;
using NormalizingApp.ViewModels;

namespace NormalizingApp.Views
{
    /// <summary>
    /// CurvePage.xaml 的交互逻辑
    /// </summary>
    public partial class CurvePage : UserControl
    {
        /// <summary>
        /// 枚举更新list数据的先后顺序
        /// </summary>
        private enum DataOrder
        {
            [IODescription("先来的加热数据")]
            Heat,
            [IODescription("后来的冷却数据")]
            Cooling
        }
        private string fileName = @"..\..\Data\Temp\dat.xml"; //序列化文件路径
        private List<Models.DataBaseRecord> recLst = new List<Models.DataBaseRecord>();
        //曲线数据源定义
        private ObservableDataSource<Point> dataSource1;//温度1
        private ObservableDataSource<Point> dataSource2;//温度2
        private ObservableDataSource<Point> dataSource3;//温度3
        private ObservableDataSource<Point> dataSource4;//压力
        private ObservableDataSource<Point> dataSource5;//冷却温度
        private ObservableDataSource<Point> dataSource6;//流量1
        private ObservableDataSource<Point> dataSource7;//流量2
        private ObservableDataSource<Point> dataSource8;//流量3

        //曲线记录当前时间点(当前时间-起始时间=间隔时间)
        private double timeSecondHeat = 0;
        private double timeSecondCooling = 0;
        //曲线起始记录时间点
        private DateTime HeatStartTime;
        private DateTime CoolingStartTime;

        // 定义刷新曲线后台线程
        Thread thread = null;
        //定义加热和冷却采集线程使能
        private bool heatEnabed = false;
        private bool coolingEnabed = false;

        //定义变量通知
        public static MyValueEvent<bool> heatStart = new MyValueEvent<bool>();
        public static MyValueEvent<bool> coolingStart = new MyValueEvent<bool>();
        public static MyValueEvent<bool> processOk = new MyValueEvent<bool>();

        /// <summary>
        /// 曲线控件属性初始化
        /// </summary>
        private void CurveInitialize()
        {
            dataSource1 = new ObservableDataSource<Point>();
            dataSource1.SetXYMapping(p => p);
            dataSource2 = new ObservableDataSource<Point>();
            dataSource2.SetXYMapping(p => p);
            dataSource3 = new ObservableDataSource<Point>();
            dataSource3.SetXYMapping(p => p);
            dataSource4 = new ObservableDataSource<Point>();
            dataSource4.SetXYMapping(p => p);
            dataSource5 = new ObservableDataSource<Point>();
            dataSource5.SetXYMapping(p => p);
            dataSource6 = new ObservableDataSource<Point>();
            dataSource6.SetXYMapping(p => p);
            dataSource7 = new ObservableDataSource<Point>();
            dataSource7.SetXYMapping(p => p);
            dataSource8 = new ObservableDataSource<Point>();
            dataSource8.SetXYMapping(p => p);
            plotter1.AddLineGraph(dataSource1, Colors.Red, 2, "轨顶温度");
            plotter1.AddLineGraph(dataSource2, Colors.Green, 2, "轨脚边温度");
            plotter1.AddLineGraph(dataSource3, Colors.Blue, 2, "内轨脚温度");
            plotter3.AddLineGraph(dataSource5, Colors.Red, 2, "喷风压力");
            plotter2.AddLineGraph(dataSource4, Colors.Red, 2, "冷却温度");
            plotter4.AddLineGraph(dataSource6, Colors.Green, 2, "轨顶流量");
            plotter4.AddLineGraph(dataSource7, Colors.Blue, 2, "轨腰流量");
            plotter4.AddLineGraph(dataSource8, Colors.Yellow, 2, "轨底流量");
        }

        public CurvePage()
        {
            InitializeComponent();
            CurveInitialize();//曲线控件属性初始化
            InitDisplayData();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitDisplayData()
        {
            //加热启动
            heatStart.OnMyValueChanged += (s, ee) =>
            {
                if(heatStart.MyValue)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        HeatCurveStart();
                        heatEnabed = true;
                    }));
                }
                else
                {
                    heatEnabed = false;
                }
            };
            //喷风启动
            coolingStart.OnMyValueChanged += (s, ee) =>
            {
                if (coolingStart.MyValue)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        ViewModels.LoginWindowViewModel.GetProductNumber();//获取焊缝编号
                        CoolingCurveStart();
                        coolingEnabed = true;
                    }));
                }
                else
                {
                    coolingEnabed = false;
                }
            };
            //加工完成
            processOk.OnMyValueChanged += (s, ee) =>
            {
                if (processOk.MyValue)
                {
                    CheckFileBuff();//检查序列化文件
                    LoginWindowViewModel.PartNumberAuto();//焊缝编号自增
                }
                    
            };

            //初始化加热采集和冷却采集线程
            thread = new Thread(new ThreadStart(UpdataCurve))
            {
                IsBackground = true
            };
            thread.Start();//启动线程
        }

        /// <summary>
        /// 加热曲线开始采集
        /// </summary>
        private void HeatCurveStart()
        {
            LoginWindowViewModel.GetProductNumber();//获取焊缝编号
            dataSource1.Collection.Clear();
            dataSource2.Collection.Clear();
            dataSource3.Collection.Clear();
            HeatStartTime = DateTime.Now;
        }
        /// <summary>
        /// 冷却曲线开始采集
        /// </summary>
        private void CoolingCurveStart()
        {
            LoginWindowViewModel.GetProductNumber();//获取焊缝编号
            dataSource4.Collection.Clear();
            dataSource5.Collection.Clear();
            dataSource6.Collection.Clear();
            dataSource7.Collection.Clear();
            dataSource8.Collection.Clear();
            CoolingStartTime = DateTime.Now;
        }
        /// <summary>
        /// 曲线数据更新线程
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void UpdataCurve()
        {
            while (true)
            {
                if (heatEnabed)
                {
                    //将实时信息装载到曲线集合源
                    timeSecondHeat = ((DateTime.Now - HeatStartTime).TotalSeconds);
                    Point point1 = new Point(timeSecondHeat, S71KConnect.userItem.ActTemp1);
                    Point point2 = new Point(timeSecondHeat, S71KConnect.userItem.ActTemp2);
                    Point point3 = new Point(timeSecondHeat, S71KConnect.userItem.ActTemp3);
                    //添加数据源
                    dataSource1.AppendAsync(base.Dispatcher, point1);
                    dataSource2.AppendAsync(base.Dispatcher, point2);
                    dataSource3.AppendAsync(base.Dispatcher, point3);
                    EarlierData();
                }
                if (coolingEnabed)
                {
                    //将实时信息装载到曲线集合源
                    timeSecondCooling = ((DateTime.Now - CoolingStartTime).TotalSeconds);
                    Point point4 = new Point(timeSecondCooling, S71KConnect.userItem.ActTemp4);
                    Point point5 = new Point(timeSecondCooling, S71KConnect.userItem.ActPressure);
                    Point point6 = new Point(timeSecondCooling, S71KConnect.userItem.ActFlow1);
                    Point point7 = new Point(timeSecondCooling, S71KConnect.userItem.ActFlow2);
                    Point point8 = new Point(timeSecondCooling, S71KConnect.userItem.ActFlow3);
                    //添加数据源
                    dataSource4.AppendAsync(base.Dispatcher, point4);
                    dataSource5.AppendAsync(base.Dispatcher, point5);
                    dataSource6.AppendAsync(base.Dispatcher, point6);
                    dataSource7.AppendAsync(base.Dispatcher, point7);
                    dataSource8.AppendAsync(base.Dispatcher, point8);
                    LaterData();
                }
                Thread.Sleep(200);
            }
        }




        /// <summary>
        /// 加热数据更新到list
        /// </summary>
        public void EarlierData()
        {
            Models.DataBaseRecord rec = new Models.DataBaseRecord
            {
                heatTime = timeSecondHeat.ToString("f1"),
                serialNumber = LoginWindowViewModel.productNumber.ProductNumberFull,
                operatorName = LoginWindowViewModel.productNumber.OperatorName,
                jobNumber = LoginWindowViewModel.productNumber.WorksNumber,
                voltage = S71KConnect.userItem.Voltage.ToString("f1"),
                current = S71KConnect.userItem.Current.ToString("f1"),
                power = S71KConnect.userItem.Power.ToString("f1"),
                frequency = S71KConnect.userItem.Frequency.ToString("f1"),
                energy = S71KConnect.userItem.Energy.ToString("f1"),
                temperature1 = S71KConnect.userItem.ActTemp1.ToString("f1"),
                temperature2 = S71KConnect.userItem.ActTemp2.ToString("f1"),
                temperature3 = S71KConnect.userItem.ActTemp3.ToString("f1")
            };
            UpdateData(rec, DataOrder.Heat); //载入数据到xml
        }
        /// <summary>
        /// 冷却数据更新到list
        /// </summary>
        public void LaterData()
        {
            Models.DataBaseRecord rec = new Models.DataBaseRecord
            {
                coolingTime = (timeSecondCooling).ToString("f1"),
                serialNumber = LoginWindowViewModel.productNumber.ProductNumberFull,
                operatorName = LoginWindowViewModel.productNumber.OperatorName,
                jobNumber = LoginWindowViewModel.productNumber.WorksNumber,
                pressure = S71KConnect.userItem.ActPressure.ToString("f1"),
                temperature4 = S71KConnect.userItem.ActTemp4.ToString("f1"),
                flow1 = S71KConnect.userItem.ActFlow1.ToString("f1"),
                flow2 = S71KConnect.userItem.ActFlow2.ToString("f1"),
                flow3 = S71KConnect.userItem.ActFlow3.ToString("f1")
            };
            UpdateData(rec, DataOrder.Cooling); //载入数据到xml
        }
        /// <summary>
        /// 更新数据方法
        /// </summary>
        /// <param name="lst">列表对象</param>
        private void UpdateData(Models.DataBaseRecord rec, DataOrder dataorder)
        {
            if (dataorder == DataOrder.Cooling)
            {
                var result = from buff in recLst where string.IsNullOrEmpty(buff.temperature4) && buff.serialNumber == rec.serialNumber select buff;
                if (result == null) return;
                if (result.ToList().Count == 0)
                {
                    recLst.Add(rec);
                }
                else
                {
                    Models.DataBaseRecord bu = result.ToList()[0];
                    bu.coolingTime = (timeSecondCooling).ToString("f1");
                    bu.pressure = rec.pressure;
                    bu.temperature4 = rec.temperature4;
                    bu.flow1 = rec.flow1;
                    bu.flow2 = rec.flow2;
                    bu.flow3 = rec.flow3;
                }
            }
            else if (dataorder == DataOrder.Heat)
            {
                recLst.Add(rec);
            }
            BinaryFile.FileName = fileName;
            BinaryFile.SaveBinary(recLst.ConvertAll(s => (object)s));
        }
        /// <summary>
        /// 检查序列化文件是否存在，如果存在就读取再保存到access数据库，不存在就new一个新的list
        /// </summary>
        private void CheckFileBuff()
        {
            try
            {
                //检查目录下是否有序列化文件,若存在就反序列化读取，不存在就创new一个新的list
                if (File.Exists(fileName))
                {
                    DataBase.DBHelp.FileName = DataBase.DBHelp.fileName; //指定数据库路径
                    BinaryFile.FileName = fileName;//指定序列化文件路径
                    //读取出序列化文件的内容到List表
                    recLst = BinaryFile.ReadBinary().ConvertAll(s => (Models.DataBaseRecord)s); //读取序列化文件内容到list
                    Models.DataBaseRecord rec = recLst[0]; //获取出列表第一个参数项
                    List<string> listTem = new List<string>(); //实例化一个类属性让下面SQL语读出查找的项用来做判断

                    listTem = DataBase.CQServices.GetRecordSql(@"SELECT DataSheet.serialNumber
                                                            FROM DataSheet
                                                            WHERE(((DataSheet.serialNumber) = '" + rec.serialNumber + @"'))");
                    
                    //判断数据库里有无重复编号，有的话就删除，没有就插入
                    if(listTem.Count > 0) 
                        DataBase.CQServices.DeleteRecord(rec.serialNumber); //删除重复项

                    DataBase.CQServices.BulckInsert(recLst);//向ACCESS数据库添加数据列表
                    File.Delete(fileName);//删除序列化文件
                    recLst.Clear();//清空数据列表

                }
                else
                {
                    recLst.Clear();
                }
            }
            catch(Exception ex)
            {
                CMessageBox.Show(ex.Message, "提示");
                return;
            }
        }
    }
}
