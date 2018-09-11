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
using HslCommunication.LogNet;

namespace NormalizingApp.Views.UIControls
{
    /// <summary>
    /// AlarmSystemPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmSystemPage : UserControl
    {
        
        //实例化一个报警记录文件
        public static ILogNet logNet = new LogNetSingle(@"..\..\Data\Log\Alarmlog.txt");

        private NormalizingApp.Model.AlarmManager alarmManager = new NormalizingApp.Model.AlarmManager(); //实例化队列管理类

        // 定义刷新曲线后台线程
        Thread UpAlarmdataThread = null;

        public AlarmSystemPage()
        {
            InitializeComponent();
            //初始化加热采集和冷却采集线程
            UpAlarmdataThread = new Thread(new ThreadStart(upAlarm));
            UpAlarmdataThread.IsBackground = true;
            UpAlarmdataThread.Start();
        }

        List<AlarmItem> alarmListData = new List<AlarmItem>();
        /// <summary>
        /// 添加报警信息到列表
        /// </summary>
        /// <param name="ac"></param>
        private void AddAlarmQueue(ref int i,NormalizingApp.Model.AlarmContent ac)
        {

            AlarmItem ai = new AlarmItem(i.ToString(), DateTime.Now.ToString(), ac.ID.ToString(), ac.Message);
            foreach (AlarmItem item in alarmListData)
            {
                if (item.Id.ToString() == ac.ID.ToString()) return;
            }

            alarmListData.Add(ai);
            i++;
        }

        public delegate void UpdateAlarmdelegate();

        /// <summary>
        /// 读取PLC故障位信息后台线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void upAlarm()
        {
            UpdateAlarmdelegate upDateListViewDelegate = new UpdateAlarmdelegate(updata);

            while (true)
            {
                int i = 1;
                alarmListData.Clear();
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.193.0").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1000);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.193.1").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1001);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.193.2").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1002);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.193.3").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1003);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.193.4").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1004);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.193.5").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1005);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.193.6").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1006);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.193.7").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1007);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.192.0").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1008);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.192.1").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1009);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.192.2").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1010);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.192.3").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1011);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.192.4").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1012);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.192.5").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1013);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.192.6").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1014);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.192.7").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1015);

                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.195.0").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1016);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.195.1").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1017);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.195.2").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1018);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.195.3").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1019);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.195.4").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1020);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.195.5").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1021);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.195.6").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1022);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.195.7").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1023);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.194.0").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1024);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.194.1").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1025);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.194.2").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1026);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.194.3").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1027);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.194.4").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1028);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.194.5").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1029);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.194.6").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1030);
                if (Lib.S71KConnect.siemensS7Net.ReadBool("DB5.194.7").Content) AddAlarmQueue(ref i, NormalizingApp.Model.AlarmList.F1031);

                //通过调用委托
                listView_Aarm.Dispatcher.Invoke(upDateListViewDelegate);
            }
        }
        //刷新LISTVIEW
        private void updata()
        {
            //对listview绑定的数据源进行更新
            listView_Aarm.Items.Clear();

            foreach (AlarmItem item in alarmListData)
            {
                listView_Aarm.Items.Add(item);
            }
        }

        /// <summary>
        /// 复位按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.PLCConnectState) return; //PLC未连接时不进行写操作
            Lib.S71KConnect.siemensS7Net.Write("DB5.200", true);
        }

        /// <summary>
        /// LISTVIEW数据绑定属性
        /// </summary>
        public class AlarmItem
        {
            public AlarmItem(string number,string date,string id,string message)
            {
                Number = number;
                Date = date;
                Id = id;
                Message = message;
            }
            public string Number { set; get; }
            public string Date { set; get; }
            public string Id { set; get; }
            public string Message { set; get; }
        }
    }
}
