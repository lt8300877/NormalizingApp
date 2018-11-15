using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NormalizingApp.MVVM;
using NormalizingApp.Messagebox;
using HslCommunication;
using System.Collections.ObjectModel;
using System.Collections;
using NormalizingApp.Lib;

namespace NormalizingApp.ViewModels
{
    public class AlarmSystemPageViewModel:NotifyObject
    {
        Thread thread = null;

        #region ListView数据绑定属性
        private AsyncObservableCollection<Models.Alarms> alarmItems = new AsyncObservableCollection<Models.Alarms>();
        public AsyncObservableCollection<Models.Alarms> AlarmItems
        {
            get { return alarmItems; }
            set
            {
                alarmItems = value;
                RaisePropertyChanged("AlarmItems");
            }
        }
        #endregion

        #region 复位按钮事件
        private MyCommand resetButtonClick;
        public MyCommand ResetButtonClick
        {
            get
            {
                if (resetButtonClick == null)
                    resetButtonClick = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                AlarmItems.Clear();
                                OperateResult ret = Lib.S71KConnect.siemensS7Net.Write("DB5.200.0", true);
                                if (!ret.IsSuccess)
                                    CMessageBox.Show("写入失败！", "提示");
                            }));
                return resetButtonClick;
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public AlarmSystemPageViewModel()
        {
            thread = new Thread(new ThreadStart(UpDataAlarm))
            {
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// 添加报警元素，重复项不添加
        /// </summary>
        /// <param name="t">报警元素信息</param>
        private void AddAlarmItem(Models.Alarms t)
        {
            foreach (Models.Alarms item in AlarmItems)
            {
                if (item.ID == t.ID) return;
            }
            AlarmItems.Add(t);
        }

        byte[] list1;
        byte[] list2;
        BitArray arr1;
        BitArray arr2;

        /// <summary>
        /// 更新报警UI线程
        /// </summary>
        private void UpDataAlarm()
        {
            while (true)
            {
                list1 = BitConverter.GetBytes(S71KConnect.userItem.ActAlarm1);
                list2 = BitConverter.GetBytes(S71KConnect.userItem.ActAlarm2);
                arr1 = new BitArray(list1);
                arr2 = new BitArray(list2);
                if (arr1[0]) AddAlarmItem(new Models.Alarms(3000, DateTime.Now.ToString(), "急停按下"));
                if (arr1[1]) AddAlarmItem(new Models.Alarms(3001, DateTime.Now.ToString(), "系统未上电"));
                if (arr1[2]) AddAlarmItem(new Models.Alarms(3002, DateTime.Now.ToString(), "IGBT电源故障"));
                if (arr1[3]) AddAlarmItem(new Models.Alarms(3003, DateTime.Now.ToString(), "气压不足"));
                if (arr1[4]) AddAlarmItem(new Models.Alarms(3004, DateTime.Now.ToString(), "能量报警"));
                if (arr1[5]) AddAlarmItem(new Models.Alarms(3005, DateTime.Now.ToString(), "喷风模式未选择"));
                if (arr1[6]) AddAlarmItem(new Models.Alarms(3006, DateTime.Now.ToString(), "备用"));
                if (arr1[7]) AddAlarmItem(new Models.Alarms(3007, DateTime.Now.ToString(), "备用"));
                if (arr1[8]) AddAlarmItem(new Models.Alarms(3008, DateTime.Now.ToString(), "X轴伺服故障"));
                if (arr1[9]) AddAlarmItem(new Models.Alarms(3009, DateTime.Now.ToString(), "Y轴伺服故障"));
                if (arr1[10]) AddAlarmItem(new Models.Alarms(3010, DateTime.Now.ToString(), "Z轴伺服故障"));
                if (arr1[11]) AddAlarmItem(new Models.Alarms(3011, DateTime.Now.ToString(), "冷却水温过高"));
                if (arr1[12]) AddAlarmItem(new Models.Alarms(3012, DateTime.Now.ToString(), "冷却水温过低"));
                if (arr1[13]) AddAlarmItem(new Models.Alarms(3013, DateTime.Now.ToString(), "Y轴跟踪气缸前进故障"));
                if (arr1[14]) AddAlarmItem(new Models.Alarms(3014, DateTime.Now.ToString(), "Y轴跟踪气缸后退故障"));
                if (arr1[15]) AddAlarmItem(new Models.Alarms(3015, DateTime.Now.ToString(), "Z轴跟踪气缸上升故障"));

                if (arr2[0]) AddAlarmItem(new Models.Alarms(3016, DateTime.Now.ToString(), "Z轴跟踪气缸下降故障"));
                if (arr2[1]) AddAlarmItem(new Models.Alarms(3017, DateTime.Now.ToString(), "1#顶料缸上升故障"));
                if (arr2[2]) AddAlarmItem(new Models.Alarms(3018, DateTime.Now.ToString(), "1#顶料缸下降故障"));
                if (arr2[3]) AddAlarmItem(new Models.Alarms(3019, DateTime.Now.ToString(), "2#顶料缸上升故障"));
                if (arr2[4]) AddAlarmItem(new Models.Alarms(3020, DateTime.Now.ToString(), "2#顶料缸下降故障"));
                if (arr2[5]) AddAlarmItem(new Models.Alarms(3021, DateTime.Now.ToString(), "3#顶料缸上升故障"));
                if (arr2[6]) AddAlarmItem(new Models.Alarms(3022, DateTime.Now.ToString(), "3#顶料缸下降故障"));
                if (arr2[7]) AddAlarmItem(new Models.Alarms(3023, DateTime.Now.ToString(), "4#顶料缸上升故障"));
                if (arr2[8]) AddAlarmItem(new Models.Alarms(3024, DateTime.Now.ToString(), "4#顶料缸下降故障"));
                if (arr2[9]) AddAlarmItem(new Models.Alarms(3025, DateTime.Now.ToString(), "备用"));
                if (arr2[10]) AddAlarmItem(new Models.Alarms(3026, DateTime.Now.ToString(), "备用"));
                if (arr2[11]) AddAlarmItem(new Models.Alarms(3027, DateTime.Now.ToString(), "备用"));
                if (arr2[12]) AddAlarmItem(new Models.Alarms(3028, DateTime.Now.ToString(), "备用"));
                if (arr2[13]) AddAlarmItem(new Models.Alarms(3029, DateTime.Now.ToString(), "备用"));
                if (arr2[14]) AddAlarmItem(new Models.Alarms(3030, DateTime.Now.ToString(), "备用"));
                if (arr2[15]) AddAlarmItem(new Models.Alarms(3031, DateTime.Now.ToString(), "备用"));

                Thread.Sleep(200);
            }   
        }


       
    }
}
