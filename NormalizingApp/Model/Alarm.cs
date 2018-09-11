using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalizingApp.Model
{
    public class AlarmContent //报警实体类
    {
        public int ID { get; set; }
        public string Message { get; set; }
    }

    public class AlarmList
    {
        public static AlarmContent F1000 = new AlarmContent() { ID = 1000, Message = "急停按下" };
        public static AlarmContent F1001 = new AlarmContent() { ID = 1001, Message = "系统未上电" };
        public static AlarmContent F1002 = new AlarmContent() { ID = 1002, Message = "IGBT电源故障" };
        public static AlarmContent F1003 = new AlarmContent() { ID = 1003, Message = "气压不足" };
        public static AlarmContent F1004 = new AlarmContent() { ID = 1004, Message = "能量报警" };
        public static AlarmContent F1005 = new AlarmContent() { ID = 1005, Message = "备用" };
        public static AlarmContent F1006 = new AlarmContent() { ID = 1006, Message = "备用" };
        public static AlarmContent F1007 = new AlarmContent() { ID = 1007, Message = "备用" };
        public static AlarmContent F1008 = new AlarmContent() { ID = 1008, Message = "X轴伺服故障" };
        public static AlarmContent F1009 = new AlarmContent() { ID = 1009, Message = "Y轴伺服故障" };
        public static AlarmContent F1010 = new AlarmContent() { ID = 1010, Message = "Z轴伺服故障" };
        public static AlarmContent F1011 = new AlarmContent() { ID = 1011, Message = "冷却水温过高" };
        public static AlarmContent F1012 = new AlarmContent() { ID = 1012, Message = "冷却水温过低" };
        public static AlarmContent F1013 = new AlarmContent() { ID = 1013, Message = "Y轴跟踪气缸前进故障" };
        public static AlarmContent F1014 = new AlarmContent() { ID = 1014, Message = "Y轴跟踪气缸后退故障" };
        public static AlarmContent F1015 = new AlarmContent() { ID = 1015, Message = "Z轴跟踪气缸上升故障" };
        public static AlarmContent F1016 = new AlarmContent() { ID = 1016, Message = "Z轴跟踪气缸下降故障" };
        public static AlarmContent F1017 = new AlarmContent() { ID = 1017, Message = "1#顶料缸上升故障" };
        public static AlarmContent F1018 = new AlarmContent() { ID = 1018, Message = "1#顶料缸下降故障" };
        public static AlarmContent F1019 = new AlarmContent() { ID = 1019, Message = "2#顶料缸上升故障" };
        public static AlarmContent F1020 = new AlarmContent() { ID = 1020, Message = "2#顶料缸下降故障" };
        public static AlarmContent F1021 = new AlarmContent() { ID = 1021, Message = "3#顶料缸上升故障" };
        public static AlarmContent F1022 = new AlarmContent() { ID = 1022, Message = "3#顶料缸下降故障" };
        public static AlarmContent F1023 = new AlarmContent() { ID = 1023, Message = "4#顶料缸上升故障" };
        public static AlarmContent F1024 = new AlarmContent() { ID = 1024, Message = "4#顶料缸下降故障" };
        public static AlarmContent F1025 = new AlarmContent() { ID = 1025, Message = "急停按下" };
        public static AlarmContent F1026 = new AlarmContent() { ID = 1026, Message = "急停按下" };
        public static AlarmContent F1027 = new AlarmContent() { ID = 1027, Message = "急停按下" };
        public static AlarmContent F1028 = new AlarmContent() { ID = 1028, Message = "急停按下" };
        public static AlarmContent F1029 = new AlarmContent() { ID = 1029, Message = "急停按下" };
        public static AlarmContent F1030 = new AlarmContent() { ID = 1030, Message = "急停按下" };
        public static AlarmContent F1031 = new AlarmContent() { ID = 1031, Message = "急停按下" };
    }


    public class AlarmManager //队列管理类
    {
        public Queue<AlarmContent> queue = new Queue<AlarmContent>();
        /// <summary>
        /// 向列表中添加
        /// </summary>
        /// <param name="p"></param>
        public void Add(AlarmContent p)
        {
            queue.Enqueue(p);
        }
        /// <summary>
        /// 移除并返回第一个元素
        /// </summary>
        /// <returns></returns>
        public AlarmContent Get()
        {
            return queue.Dequeue();
        }
        /// <summary>
        /// 判断队列中是否存在此元素
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsGet(AlarmContent p)
        {
            bool resule = false;
            resule = queue.Contains(p);
            return resule;
        }
        /// <summary>
        /// 判断此队列是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsHaveElement()
        {
            if (queue.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 获取队列元素总数
        /// </summary>
        /// <returns></returns>
        public int GetQueueCount()
        {
            return queue.Count;
        }
        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
            queue.Clear();
        }
    }
}
