using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using NormalizingApp.Views.Messagebox;
using System.Globalization;
using System.Threading;

namespace NormalizingApp.Views.UIControls
{
    /// <summary>
    /// DataQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataQueryPage : UserControl
    {
        /// <summary>
        /// 按钮枚举
        /// </summary>
        private enum ButtonType
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H
        }
        private List<NormalizingApp.Model.Record> list = new List<NormalizingApp.Model.Record>();
        //曲线数据绑定
        private LineGraph graph1 = new LineGraph();//曲线1
        private LineGraph graph2 = new LineGraph();//曲线2
        private LineGraph graph3 = new LineGraph();//曲线3

        public DataQueryPage()
        {
            InitializeComponent();
            //数据库查询日期改为初始设定的日期
            this.datePickerCtrl.SelectedDate = LoginWindow.partNumber.accessDateSet;
        }

        /// <summary>
        /// 曲线数据源选择按钮集合事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ButtonType btnType;
            Enum.TryParse<ButtonType>(btn.Tag.ToString(), out btnType);
            CurveDisp(btnType);
        }

        /// <summary>
        /// 查找出数据库中编号的重复项(查找出serialNumber列不同的项相当于查找工件品种数量)
        /// </summary>
        public void SqlInquireRepetition()
        {
            List<string> listTem = new List<string>();
            this.partNumberlBx.Items.Clear();
            listTem = Access.CQServices.GetRecordSql(@"SELECT First(DataSheet.serialNumber) AS [serialNumber], Count(DataSheet.serialNumber) AS 记录总行数
                                                FROM DataSheet
                                                GROUP BY DataSheet.serialNumber
                                                HAVING(((Count(DataSheet.serialNumber)) > 1))");

            foreach (string item in listTem)
            {
                this.partNumberlBx.Items.Add(item);
            }
        }
        /// <summary>
        /// 根据文本查找编号列的所有行(条件是serialNumber列等于选择的内容，最后以ID升序排列)
        /// </summary>
        /// <param name="text"></param>
        public void SqlInquireText(string text)
        {

            list = Access.CQServices.GetRecordBySql(@"SELECT [serialNumber], [operatorName], [jobNumber], [heatTime], [voltage], [current], [power], [frequency], [energy], [temperature1], [temperature2], [temperature3], [coolingTime], [temperature4], [pressure], [flow1], [flow2], [flow3]
                                                    FROM DataSheet
                                                    WHERE(((DataSheet.serialNumber) = '" + text + @"'))
                                                    ORDER BY DataSheet.ID");

        }
        /// <summary>
        /// 获取数据库路径
        /// </summary>
        /// <returns></returns>
        private string GetFilePath()
        {
            string year = this.datePickerCtrl.SelectedDate.Value.Date.ToString("yyyy") + @"\";
            string month = this.datePickerCtrl.SelectedDate.Value.Date.ToString("yyyyMM") + @"\";
            string day = this.datePickerCtrl.SelectedDate.Value.Date.ToString("yyyyMMdd") + @".accdb";
            string namePath = @"..\..\Data\WorkData\" + year + month + day;
            return namePath;

        }

        //日期选择控件数据发生改变时更新数据库连接
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (File.Exists(GetFilePath()))
                {
                    Access.DBHelp.FileName = GetFilePath();
                    SqlInquireRepetition();
                }
                else
                {
                    //CMessageBox.Show("数据不存在！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Error);
                    return;
                }
            }
            catch
            {
                return;
            }


            //string msg = DBHelp.FileName;
            //MessageBox.Show(msg);
        }
        //listbox选择改变后执行
        private void partNumberlBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string partNumber = partNumberlBx.SelectedItem.ToString();
                Access.DBHelp.FileName = GetFilePath();
                //disNumberTbk.Text = "当前焊缝编号:" + partNumber;
                SqlInquireText(partNumber);
                //数据刷新到DataGrid 
                this.DataGrid1.ItemsSource = null;//清除DataGrid数据
                this.DataGrid1.AutoGenerateColumns = false;//不自动创建列
                this.DataGrid1.ItemsSource = list;//把access数据库查询到的数据添加到数据表格里
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 曲线数据选择显示
        /// </summary>
        /// <param name="bt"></param>
        private void CurveDisp(ButtonType bt)
        {    
            switch (bt)
            {
                case ButtonType.A://电压
                    VoltageCurve();
                    break;
                case ButtonType.B://电流
                    CurrentCurve();
                    break;
                case ButtonType.C://功率
                    PowerCurve();
                    break;
                case ButtonType.D://能量
                    EnergyCurve();
                    break;
                case ButtonType.E://加热温度
                    HeatTemperatureCurve();
                    break;
                case ButtonType.F://冷却温度
                    CoolingTemperatureCurve();
                    break;
                case ButtonType.G://流量
                    CoolingFlowCurve();
                    break;
                case ButtonType.H://压力
                    CoolingPressureCurve();
                    break;

            }
        }

        /// <summary>
        /// 定义一个曲线显示的结构图
        /// </summary>
        public struct RecordInfo
        {
            /// <summary>
            /// 曲线时间轴X属性
            /// </summary>
            /// <value>The Time.</value>
            public double Time { get; set; }
            /// <summary>
            /// 曲线数据轴Y属性
            /// </summary>
            /// <value>The Data.</value>
            public double Data { get; set; }
        }
        /// <summary>
        /// 把列表结构体数据转换成曲线图识别的数据源
        /// </summary>
        /// <param name="rates">列表结构体数据传入</param>
        /// <returns></returns>
        private EnumerableDataSource<RecordInfo> CreateRecordDataSource(List<RecordInfo> rates)
        {
            EnumerableDataSource<RecordInfo> ds = new EnumerableDataSource<RecordInfo>(rates);
            ds.SetXMapping(ci => ci.Time);
            ds.SetYMapping(ci => ci.Data);
            return ds;
        }
        
        /// <summary>
        /// 电压曲线显示
        /// </summary>
        private void VoltageCurve()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            List<RecordInfo> voltage = new List<RecordInfo>();
            CurveClear();//清除曲线
            foreach (NormalizingApp.Model.Record item in list)
            {
                if (string.IsNullOrEmpty(item.heatTime))
                    break; //item.heatTime = "0";
                if (string.IsNullOrEmpty(item.voltage))
                    item.voltage = "0";
                voltage.Add(new RecordInfo { Time = double.Parse(item.heatTime, culture), Data = double.Parse(item.voltage, culture) });
            }
            graph1 = plotter1.AddLineGraph(CreateRecordDataSource(voltage), Colors.Red, 2, "电压(V)");
            plotter1.FitToView();
        }
        
        /// <summary>
        /// 电流曲线显示
        /// </summary>
        private void CurrentCurve()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            List<RecordInfo> current = new List<RecordInfo>();
            CurveClear();//清除曲线
            foreach (NormalizingApp.Model.Record item in list)
            {
                if (string.IsNullOrEmpty(item.heatTime))
                    break; //item.heatTime = "0";
                if (string.IsNullOrEmpty(item.current))
                    item.current = "0";
                current.Add(new RecordInfo { Time = double.Parse(item.heatTime, culture), Data = double.Parse(item.current, culture) });
            }
            graph1 = plotter1.AddLineGraph(CreateRecordDataSource(current), Colors.Red, 2, "电流(A)");
            plotter1.FitToView();
        }
        /// <summary>
        /// 功率曲线显示
        /// </summary>
        private void PowerCurve()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            List<RecordInfo> power = new List<RecordInfo>();
            CurveClear();//清除曲线
            foreach (NormalizingApp.Model.Record item in list)
            {
                if (string.IsNullOrEmpty(item.heatTime))
                    break; //item.heatTime = "0";
                if (string.IsNullOrEmpty(item.power))
                    item.power = "0";
                power.Add(new RecordInfo { Time = double.Parse(item.heatTime, culture), Data = double.Parse(item.power, culture) });
            }
            graph1 = plotter1.AddLineGraph(CreateRecordDataSource(power), Colors.Red, 2, "功率(Kw)");
            plotter1.FitToView();
        }
        /// <summary>
        /// 能量曲线显示
        /// </summary>
        private void EnergyCurve()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            List<RecordInfo> energy = new List<RecordInfo>();
            CurveClear();//清除曲线
            foreach (NormalizingApp.Model.Record item in list)
            {
                if (string.IsNullOrEmpty(item.heatTime))
                    break; //item.heatTime = "0";
                if (string.IsNullOrEmpty(item.energy))
                    item.energy = "0";
                energy.Add(new RecordInfo { Time = double.Parse(item.heatTime, culture), Data = double.Parse(item.energy, culture) });
            }
            graph1 = plotter1.AddLineGraph(CreateRecordDataSource(energy), Colors.Red, 2, "能量(Kws)");
            plotter1.FitToView();
        }
        /// <summary>
        /// 加热温度曲线显示
        /// </summary>
        private void HeatTemperatureCurve()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            List<RecordInfo> temperature1 = new List<RecordInfo>();
            List<RecordInfo> temperature2 = new List<RecordInfo>();
            List<RecordInfo> temperature3 = new List<RecordInfo>();
            CurveClear();//清除曲线
            foreach (NormalizingApp.Model.Record item in list)
            {
                if (string.IsNullOrEmpty(item.heatTime))
                    break;
                if (string.IsNullOrEmpty(item.temperature1))
                    item.temperature1 = "0";
                if (string.IsNullOrEmpty(item.temperature2))
                    item.temperature2 = "0";
                if (string.IsNullOrEmpty(item.temperature3))
                    item.temperature3 = "0";
                temperature1.Add(new RecordInfo { Time = double.Parse(item.heatTime, culture), Data = double.Parse(item.temperature1, culture) });
                temperature2.Add(new RecordInfo { Time = double.Parse(item.heatTime, culture), Data = double.Parse(item.temperature2, culture) });
                temperature3.Add(new RecordInfo { Time = double.Parse(item.heatTime, culture), Data = double.Parse(item.temperature3, culture) });
            }
            graph1 = plotter1.AddLineGraph(CreateRecordDataSource(temperature1), Colors.Red, 2, "轨顶温度(℃)");
            graph2 = plotter1.AddLineGraph(CreateRecordDataSource(temperature2), Colors.Green, 2, "轨脚边温度(℃)");
            graph3 = plotter1.AddLineGraph(CreateRecordDataSource(temperature3), Colors.Blue, 2, "内轨脚温度(℃)");
            plotter1.FitToView();
        }
        /// <summary>
        /// 冷却温度曲线显示
        /// </summary>
        private void CoolingTemperatureCurve()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            List<RecordInfo> temperature4 = new List<RecordInfo>();
            CurveClear();//清除曲线
            foreach (NormalizingApp.Model.Record item in list)
            {
                if (string.IsNullOrEmpty(item.coolingTime))
                    break; //item.heatTime = "0";
                if (string.IsNullOrEmpty(item.temperature4))
                    item.temperature4 = "0";
                temperature4.Add(new RecordInfo { Time = double.Parse(item.coolingTime, culture), Data = double.Parse(item.temperature4, culture) });
            }
            graph1 = plotter1.AddLineGraph(CreateRecordDataSource(temperature4), Colors.Red, 2, "冷却温度(℃)");
            plotter1.FitToView();
        }
        /// <summary>
        /// 冷却空气流量曲线显示
        /// </summary>
        private void CoolingFlowCurve()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            List<RecordInfo> flow1 = new List<RecordInfo>();
            List<RecordInfo> flow2 = new List<RecordInfo>();
            List<RecordInfo> flow3 = new List<RecordInfo>();
            CurveClear();//清除曲线
            foreach (NormalizingApp.Model.Record item in list)
            {
                if (string.IsNullOrEmpty(item.coolingTime))
                    break;
                if (string.IsNullOrEmpty(item.flow1))
                    item.flow1 = "0";
                if (string.IsNullOrEmpty(item.flow2))
                    item.flow2 = "0";
                if (string.IsNullOrEmpty(item.flow3))
                    item.flow3 = "0";
                flow1.Add(new RecordInfo { Time = double.Parse(item.coolingTime, culture), Data = double.Parse(item.flow1, culture) });
                flow2.Add(new RecordInfo { Time = double.Parse(item.coolingTime, culture), Data = double.Parse(item.flow2, culture) });
                flow3.Add(new RecordInfo { Time = double.Parse(item.coolingTime, culture), Data = double.Parse(item.flow3, culture) });
            }
            graph1 = plotter1.AddLineGraph(CreateRecordDataSource(flow1), Colors.Red, 2, "轨顶流量(L/min)");
            graph2 = plotter1.AddLineGraph(CreateRecordDataSource(flow2), Colors.Green, 2, "轨腰流量(L/min)");
            graph3 = plotter1.AddLineGraph(CreateRecordDataSource(flow3), Colors.Blue, 2, "轨底流量(L/min)");
            plotter1.FitToView();
        }
        /// <summary>
        /// 压力曲线显示
        /// </summary>
        private void CoolingPressureCurve()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            List<RecordInfo> preussre = new List<RecordInfo>();
            CurveClear();//清除曲线
            foreach (NormalizingApp.Model.Record item in list)
            {
                if (string.IsNullOrEmpty(item.coolingTime))
                    break; //item.heatTime = "0";
                if (string.IsNullOrEmpty(item.pressure))
                    item.pressure = "0";
                preussre.Add(new RecordInfo { Time = double.Parse(item.coolingTime, culture), Data = double.Parse(item.pressure, culture) });
            }
            graph1 = plotter1.AddLineGraph(CreateRecordDataSource(preussre), Colors.Red, 2, "压力(℃)");
            plotter1.FitToView();
        }

        /// <summary>
        /// 清除加热曲线数据
        /// </summary>
        private void CurveClear()
        {
            plotter1.Children.Remove(graph1);
            plotter1.Children.Remove(graph2);
            plotter1.Children.Remove(graph3);
            plotter1.Viewport.Visible = new System.Windows.Rect(0, 0, 0, 0);//初始化坐标
        }
    }
}
