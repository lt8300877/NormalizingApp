using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// CurvePage.xaml 的交互逻辑
    /// </summary>
    public partial class CurvePage : UserControl
    {
        /// <summary>
        /// 枚举更新list数据的先后顺序
        /// </summary>
        private enum DataOrder
        {
            /// <summary>
            /// 先来的数据
            /// </summary>
            [IODescription("先来的加热数据")]
            Earlier,
            /// <summary>
            /// 后来的数据
            /// </summary>
            [IODescription("后来的冷却数据")]
            Later
        }
        private string fileName = @"..\..\Data\Temp\dat.xml"; //序列化文件路径

        private List<NormalizingApp.Model.Record> recLst;

        private Lib.FileBuff fileBuff = new Lib.FileBuff();

        //textbox数据绑定
        //温度
        public Lib.DataDisplay actTemp1 = new Lib.DataDisplay();

        public Lib.DataDisplay actTemp2 = new Lib.DataDisplay();

        public Lib.DataDisplay actTemp3 = new Lib.DataDisplay();

        //压力
        public Lib.DataDisplay actPressure = new Lib.DataDisplay();

        //冷却温度
        public Lib.DataDisplay actTemp4 = new Lib.DataDisplay();

        //流量
        public Lib.DataDisplay actFlow1 = new Lib.DataDisplay();

        public Lib.DataDisplay actFlow2 = new Lib.DataDisplay();

        public Lib.DataDisplay actFlow3 = new Lib.DataDisplay();

        
        

        //曲线数据绑定
        private ObservableDataSource<Point> dataSource1;//温度1
        private ObservableDataSource<Point> dataSource2;//温度2
        private ObservableDataSource<Point> dataSource3;//温度3
        private ObservableDataSource<Point> dataSource4;//压力
        private ObservableDataSource<Point> dataSource5;//冷却温度
        private ObservableDataSource<Point> dataSource6;//流量1
        private ObservableDataSource<Point> dataSource7;//流量2
        private ObservableDataSource<Point> dataSource8;//流量3

        

        private LineGraph graph1 = new LineGraph();
        private LineGraph graph2 = new LineGraph();
        private LineGraph graph3 = new LineGraph();
        private LineGraph graph4 = new LineGraph();
        private LineGraph graph5 = new LineGraph();
        private LineGraph graph6 = new LineGraph();
        private LineGraph graph7 = new LineGraph();
        private LineGraph graph8 = new LineGraph();
        private double timeSecondHeat = 0;
        private double timeSecondCooling = 0;
        bool edgeHeat = false;
        bool edgeCooling = false;
        //开一个WPF定时器采集启动停止信号
        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();

        // 定义刷新曲线后台线程
        Thread heatThread = null;
        //定义加热和冷却采集线程使能
        private bool heatEnabed = false;
        private bool coolingEnabed = false;

        public CurvePage()
        {
            InitializeComponent();
            InitDisplayData();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitDisplayData()
        {
            //plotter1.Viewport.Visible = new System.Windows.Rect(-1, 400, 30, 1500);//初始化坐标
            //plotter2.Viewport.Visible = new System.Windows.Rect(-1, 0, 30, 1000);//初始化坐标
            //plotter3.Viewport.Visible = new System.Windows.Rect(-1, 0, 30, 100);//初始化坐标
            //plotter4.Viewport.Visible = new System.Windows.Rect(-1, 400, 30, 1500);//初始化坐标
            this.actTemp1Tbk.DataContext = actTemp1;
            this.actTemp2Tbk.DataContext = actTemp2;
            this.actTemp3Tbk.DataContext = actTemp3;

            this.actPressureTbk.DataContext = actPressure;

            this.actTemp4Tbk.DataContext = actTemp4;

            this.actFlow1Tbk.DataContext = actFlow1;
            this.actFlow2Tbk.DataContext = actFlow2;
            this.actFlow3Tbk.DataContext = actFlow3;




            //WPF定时器
            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            readDataTimer.Start();
            //初始化加热采集和冷却采集线程
            heatThread = new Thread(new ThreadStart(UpdataCurve));
            heatThread.IsBackground = true;
            heatThread.Start();

            //检查序列化文件
            CheckFileBuff();
        }
        /// <summary>
        /// 扫描PLC加热信号和冷却信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timeCycle(object sender, EventArgs e)
        {
            if (!MainWindow.PLCConnectState) return; //PLC未连接时不进行写操作
            LoginWindow.GetPartNumber(); //获取焊缝编号

            if (Lib.S71KConnect.userType.HeatRun && !edgeHeat) //采集到加热启动信号后开始记录
            {
                HeatCurveClear();
                HeatCurveStart();
                heatEnabed = true;
            }
            else if (!Lib.S71KConnect.userType.HeatRun)//加热停止后关闭线程
            {
                //HeatCurveClear();//清除曲线
                heatEnabed = false;
            }

            if (Lib.S71KConnect.userType.CoolingRun && !edgeCooling)//采集加到冷却启动信号后开始记录
            {
                CoolingCurveClear();
                CoolingCurveStart();
                coolingEnabed = true;
            }
            else if (!Lib.S71KConnect.userType.CoolingRun) //冷却停止后关闭线程
            {
                //CoolingCurveClear();//清除曲线
                coolingEnabed = false;
            }

            if (!Lib.S71KConnect.userType.CoolingRun && edgeCooling) //冷却关闭后焊缝编号自加1
            {
                LoginWindow.PartNumberAuto();//焊缝编号自增
            }
            edgeHeat = Lib.S71KConnect.userType.HeatRun;
            edgeCooling = Lib.S71KConnect.userType.CoolingRun;
        }

        /// <summary>
        /// 加热曲线开始采集
        /// </summary>
        private void HeatCurveStart()
        {
            if (timeSecondHeat == 0)
            {
                dataSource1 = new ObservableDataSource<Point>();
                dataSource1.SetXYMapping(p => p);
                dataSource2 = new ObservableDataSource<Point>();
                dataSource2.SetXYMapping(p => p);
                dataSource3 = new ObservableDataSource<Point>();
                dataSource3.SetXYMapping(p => p);
                graph1 = plotter1.AddLineGraph(dataSource1, Colors.Red, 2, "轨顶温度");
                graph2 = plotter1.AddLineGraph(dataSource2, Colors.Green, 2, "轨脚边温度");
                graph3 = plotter1.AddLineGraph(dataSource3, Colors.Blue, 2, "内轨脚温度");
            }

        }
        /// <summary>
        /// 冷却曲线开始采集
        /// </summary>
        private void CoolingCurveStart()
        {
            if (timeSecondCooling == 0)
            {
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
                graph4 = plotter2.AddLineGraph(dataSource4, Colors.Red, 2, "压力");
                graph5 = plotter3.AddLineGraph(dataSource5, Colors.Red, 2, "冷却温度");
                graph6 = plotter4.AddLineGraph(dataSource6, Colors.Green, 2, "轨顶流量");
                graph7 = plotter4.AddLineGraph(dataSource7, Colors.Blue, 2, "轨腰流量");
                graph8 = plotter4.AddLineGraph(dataSource8, Colors.Yellow, 2, "轨底流量");
            }
        }

        /// <summary>
        /// 清除加热曲线数据
        /// </summary>
        private void HeatCurveClear()
        {
            if (timeSecondHeat == 0) return;
            timeSecondHeat = 0;
            plotter1.Children.Remove(graph1);
            plotter1.Children.Remove(graph2);
            plotter1.Children.Remove(graph3);
            dataSource1 = new ObservableDataSource<Point>();
            dataSource2 = new ObservableDataSource<Point>();
            dataSource3 = new ObservableDataSource<Point>();
        }
        /// <summary>
        /// 清除冷却曲线数据
        /// </summary>
        private void CoolingCurveClear()
        {
            if (timeSecondCooling == 0) return;
            timeSecondCooling = 0;

            plotter2.Children.Remove(graph4);
            plotter3.Children.Remove(graph5);
            plotter4.Children.Remove(graph6);
            plotter4.Children.Remove(graph7);
            plotter4.Children.Remove(graph8);
            dataSource4 = new ObservableDataSource<Point>();
            dataSource5 = new ObservableDataSource<Point>();
            dataSource6 = new ObservableDataSource<Point>();
            dataSource7 = new ObservableDataSource<Point>();
            dataSource8 = new ObservableDataSource<Point>();
        }
        /// <summary>
        /// 曲线数据更新线程
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void UpdataCurve()
        {
            while(true)
            {
                if(heatEnabed)
                {
                    //文本显示读取变量
                    actTemp1.DataText = Lib.S71KConnect.userType.ActTemp1.ToString("f1") + "℃";
                    actTemp2.DataText = Lib.S71KConnect.userType.ActTemp2.ToString("f1") + "℃";
                    actTemp3.DataText = Lib.S71KConnect.userType.ActTemp3.ToString("f1") + "℃";
                    //曲线加载读取的变量信息
                    Point point1 = new Point(timeSecondHeat / 5, Lib.S71KConnect.userType.ActTemp1);
                    Point point2 = new Point(timeSecondHeat / 5, Lib.S71KConnect.userType.ActTemp2);
                    Point point3 = new Point(timeSecondHeat / 5, Lib.S71KConnect.userType.ActTemp3);
                    //数据更新到曲线
                    dataSource1.AppendAsync(base.Dispatcher, point1);
                    dataSource2.AppendAsync(base.Dispatcher, point2);
                    dataSource3.AppendAsync(base.Dispatcher, point3);

                    EarlierData();
                    timeSecondHeat++;
                }
                if (coolingEnabed)
                {
                    actPressure.DataText = Lib.S71KConnect.userType.ActPressure.ToString("f1") + "Mpa";
                    actTemp4.DataText = Lib.S71KConnect.userType.ActTemp4.ToString("f1") + "℃";
                    actFlow1.DataText = Lib.S71KConnect.userType.ActFlow1.ToString("f1") + "L/min";
                    actFlow2.DataText = Lib.S71KConnect.userType.ActFlow2.ToString("f1") + "L/min";
                    actFlow3.DataText = Lib.S71KConnect.userType.ActFlow3.ToString("f1") + "L/min";
                    
                    Point point4 = new Point(timeSecondCooling / 5, Lib.S71KConnect.userType.ActPressure);
                    Point point5 = new Point(timeSecondCooling / 5, Lib.S71KConnect.userType.ActTemp4);
                    Point point6 = new Point(timeSecondCooling / 5, Lib.S71KConnect.userType.ActFlow1);
                    Point point7 = new Point(timeSecondCooling / 5, Lib.S71KConnect.userType.ActFlow2);
                    Point point8 = new Point(timeSecondCooling / 5, Lib.S71KConnect.userType.ActFlow3);

                    dataSource4.AppendAsync(base.Dispatcher, point4);
                    dataSource5.AppendAsync(base.Dispatcher, point5);
                    dataSource6.AppendAsync(base.Dispatcher, point6);
                    dataSource7.AppendAsync(base.Dispatcher, point7);
                    dataSource8.AppendAsync(base.Dispatcher, point8);
                    LaterData();
                    timeSecondCooling++;
                }
                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// 加热数据更新到list
        /// </summary>
        public void EarlierData()
        {
            NormalizingApp.Model.Record rec = new NormalizingApp.Model.Record();
            rec.heatTime = (timeSecondHeat / 5).ToString("f1");
            rec.serialNumber = LoginWindow.partNumber.spliceNumber;
            rec.operatorName = LoginWindow.partNumber.operatorNameSet;

            //rec.jobNumber = Login.partNumber.jodnumberSet;
            rec.voltage = Lib.S71KConnect.userType.Voltage.ToString("f1");
            rec.current = Lib.S71KConnect.userType.Current.ToString("f1");
            rec.power = Lib.S71KConnect.userType.Power.ToString("f1");
            rec.frequency = Lib.S71KConnect.userType.Frequency.ToString("f1");
            rec.energy = Lib.S71KConnect.userType.Energy.ToString("f1");
            rec.temperature1 = Lib.S71KConnect.userType.ActTemp1.ToString("f1");
            rec.temperature2 = Lib.S71KConnect.userType.ActTemp2.ToString("f1");
            rec.temperature3 = Lib.S71KConnect.userType.ActTemp3.ToString("f1");
            UpdateData(rec, DataOrder.Earlier); //载入数据到xml
        }
        /// <summary>
        /// 冷却数据更新到list
        /// </summary>
        public void LaterData()
        {
            NormalizingApp.Model.Record rec = new NormalizingApp.Model.Record();
            rec.coolingTime = (timeSecondCooling / 5).ToString("f1");
            rec.serialNumber = LoginWindow.partNumber.spliceNumber;
            rec.operatorName = LoginWindow.partNumber.operatorNameSet;

            //rec.jobNumber = Login.partNumber.jodnumberSet;
            rec.pressure = Lib.S71KConnect.userType.ActPressure.ToString("f1");
            rec.temperature4 = Lib.S71KConnect.userType.ActTemp4.ToString("f1");
            rec.flow1 = Lib.S71KConnect.userType.ActFlow1.ToString("f1");
            rec.flow2 = Lib.S71KConnect.userType.ActFlow2.ToString("f1");
            rec.flow3 = Lib.S71KConnect.userType.ActFlow3.ToString("f1");
            
            UpdateData(rec, DataOrder.Later); //载入数据到xml
        }
        /// <summary>
        /// 更新数据方法
        /// </summary>
        /// <param name="lst">列表对象</param>
        private void UpdateData(NormalizingApp.Model.Record rec, DataOrder dataorder)
        {
            if (dataorder == DataOrder.Later)
            {
                var result = from buff in recLst where string.IsNullOrEmpty(buff.temperature4) && buff.serialNumber == rec.serialNumber select buff;
                if (result == null) return;
                if (result.ToList().Count == 0)
                {
                    recLst.Add(rec);
                }
                else
                {
                    NormalizingApp.Model.Record bu = result.ToList()[0];
                    bu.coolingTime = (timeSecondCooling / 5).ToString("f1");
                    bu.pressure = rec.pressure;
                    bu.temperature4 = rec.temperature4;
                    bu.flow1 = rec.flow1;
                    bu.flow2 = rec.flow2;
                    bu.flow3 = rec.flow3;
                }
            }
            else if (dataorder == DataOrder.Earlier)
            {
                recLst.Add(rec);
            }
            Lib.FileBuff.FileName = fileName;
            Lib.FileBuff.SaveBinary(recLst.ConvertAll(s => (object)s));
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
                    Access.DBHelp.FileName = Access.DBHelp.fileName; //指定数据库路径

                    Lib.FileBuff.FileName = fileName;//指定序列化文件路径

                    recLst = Lib.FileBuff.ReadBinary().ConvertAll(s => (NormalizingApp.Model.Record)s); //读取序列化文件内容到list

                    NormalizingApp.Model.Record rec = recLst[0]; //获取出列表第一个参数项

                    List<string> listTem = new List<string>(); //实例化一个类属性让下面SQL语读出查找的项用来做判断

                    listTem = Access.CQServices.GetRecordSql(@"SELECT DataSheet.serialNumber
                                                            FROM DataSheet
                                                            WHERE(((DataSheet.serialNumber) = '" + rec.serialNumber + @"'))");
                    
                    //判断数据库里有无重复编号，有的话就删除，没有就插入
                    if(listTem.Count > 0) 
                    {
                        Access.CQServices.DeleteRecord(rec.serialNumber); //删除重复项
                    }

                    Access.CQServices.BulckInsert(recLst);

                    File.Delete(fileName);

                    recLst = new List<NormalizingApp.Model.Record>();

                }
                else
                {

                    recLst = new List<NormalizingApp.Model.Record>();

                }
            }
            catch
            {
                //return;
            }
        }
    }
}
