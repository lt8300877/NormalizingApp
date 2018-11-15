using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication;
using HslCommunication.Profinet.Siemens;


namespace NormalizingApp.Lib
{
    public class S71KConnect
    {
        public static SiemensS7Net siemensS7Net = new SiemensS7Net(SiemensPLCS.S1200, "192.168.0.1") { ConnectTimeOut = 10000 };
        public static Models.UserItems userItem = new Models.UserItems();
        private static System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBackgroundReadPlc));
        /// <summary>
        /// 长连接(调用此方法就是使用了长连接，如果不调用直接读取数据，那就是短连接)
        /// </summary>
        /// <returns></returns>
        public static bool ConnectPLC()
        {
            OperateResult connect = siemensS7Net.ConnectServer();
            if (connect.IsSuccess)
            {
                userItem.ConnectionState = true;
                return true;
            }
            else
            {
                userItem.ConnectionState = false;
                return false;
            }
                
        }
        /// <summary>
        /// 断开连接，也就是关闭了长连接，如果再去请求数据，就变成了短连接
        /// </summary>
        public static void DisconnectPLC()
        {
            siemensS7Net.ConnectClose();
        }
        /// <summary>
        /// 读取所有变量
        /// </summary>
        /// <param name="ut">数据对象</param>
        public static void ReadItems()
        {

            OperateResult<byte[]> read = siemensS7Net.Read("DB2.0", 141);
            {
                if (read.IsSuccess)
                {
                    userItem.Voltage = siemensS7Net.ByteTransform.TransSingle(read.Content, 0);
                    userItem.Current = siemensS7Net.ByteTransform.TransSingle(read.Content, 4);
                    userItem.Frequency = siemensS7Net.ByteTransform.TransSingle(read.Content, 8);
                    userItem.Power = siemensS7Net.ByteTransform.TransSingle(read.Content, 12);
                    userItem.Energy = siemensS7Net.ByteTransform.TransSingle(read.Content, 16);

                    userItem.XActPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 20);
                    userItem.XActSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 24);
                    userItem.XSetPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 28);
                    userItem.XSetSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 32);
                    userItem.XActCurrent = siemensS7Net.ByteTransform.TransSingle(read.Content, 36);

                    userItem.YActPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 40);
                    userItem.YActSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 44);
                    userItem.YSetPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 48);
                    userItem.YSetSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 52);
                    userItem.YActCurrent = siemensS7Net.ByteTransform.TransSingle(read.Content, 56);

                    userItem.ZActPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 60);
                    userItem.ZActSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 64);
                    userItem.ZSetPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 68);
                    userItem.ZSetSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 72);
                    userItem.ZActCurrent = siemensS7Net.ByteTransform.TransSingle(read.Content, 76);

                    userItem.ActTemp1 = siemensS7Net.ByteTransform.TransSingle(read.Content, 80);
                    userItem.ActTemp2 = siemensS7Net.ByteTransform.TransSingle(read.Content, 84);
                    userItem.ActTemp3 = siemensS7Net.ByteTransform.TransSingle(read.Content, 88);
                    userItem.ActTemp4 = siemensS7Net.ByteTransform.TransSingle(read.Content, 92);

                    userItem.ActFlow1 = siemensS7Net.ByteTransform.TransSingle(read.Content, 96);
                    userItem.ActFlow2 = siemensS7Net.ByteTransform.TransSingle(read.Content, 100);
                    userItem.ActFlow3 = siemensS7Net.ByteTransform.TransSingle(read.Content, 104);

                    userItem.ActPressure = siemensS7Net.ByteTransform.TransSingle(read.Content, 108);

                    userItem.HeatRun = siemensS7Net.ByteTransform.TransBool(read.Content, 112);
                    userItem.CoolingRun = siemensS7Net.ByteTransform.TransBool(read.Content, 113);
                    Views.CurvePage.heatStart.MyValue = userItem.HeatRun;
                    Views.CurvePage.coolingStart.MyValue = userItem.CoolingRun;


                    userItem.ActAlarm1 = siemensS7Net.ByteTransform.TransUInt16(read.Content, 114);
                    userItem.ActAlarm2 = siemensS7Net.ByteTransform.TransUInt16(read.Content, 116);

                    userItem.ActLenght1 = siemensS7Net.ByteTransform.TransSingle(read.Content, 118);
                    userItem.ActLenght2 = siemensS7Net.ByteTransform.TransSingle(read.Content, 122);

                    userItem.Y_CDJ_QGQW = siemensS7Net.ByteTransform.TransBool(read.Content,126);
                    userItem.Y_CDJ_QGHW = siemensS7Net.ByteTransform.TransBool(read.Content, 127);
                    userItem.Z_CDJ_QGQW = siemensS7Net.ByteTransform.TransBool(read.Content, 128);
                    userItem.Z_CDJ_QGHW = siemensS7Net.ByteTransform.TransBool(read.Content, 129);

                    userItem.JLG_1_SW = siemensS7Net.ByteTransform.TransBool(read.Content, 130);
                    userItem.JLG_1_XW = siemensS7Net.ByteTransform.TransBool(read.Content, 131);
                    userItem.JLG_2_SW = siemensS7Net.ByteTransform.TransBool(read.Content, 132);
                    userItem.JLG_2_XW = siemensS7Net.ByteTransform.TransBool(read.Content, 133);
                    userItem.JLG_3_SW = siemensS7Net.ByteTransform.TransBool(read.Content, 134);
                    userItem.JLG_3_XW = siemensS7Net.ByteTransform.TransBool(read.Content, 135);
                    userItem.JLG_4_SW = siemensS7Net.ByteTransform.TransBool(read.Content, 136);
                    userItem.JLG_4_XW = siemensS7Net.ByteTransform.TransBool(read.Content, 137);
                    userItem.DY_ZT = siemensS7Net.ByteTransform.TransBool(read.Content, 138);
                    userItem.PQ_ZT = siemensS7Net.ByteTransform.TransBool(read.Content, 139);
                    userItem.ProcessOk = siemensS7Net.ByteTransform.TransBool(read.Content, 140);
                    Views.CurvePage.processOk.MyValue = userItem.ProcessOk;
                }
            }
        }
        /// <summary>
        /// 启动读取PLC数据线程
        /// </summary>
        public static void StartPLCRead()
        {
            // 启动后台读取的线程
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 停止读取PLC数据线程
        /// </summary>
        public static void StopPLCRead()
        {
            //结束后台读取的线程
            thread.Abort();
        }
        private static  void ThreadBackgroundReadPlc()
        {
            while (true)
            {
                DateTime dt = DateTime.Now;         
                userItem.SystempDataTime = dt.ToString();
                ReadItems();//读取DB块数据
                double dd = (DateTime.Now - dt).TotalMilliseconds;
                userItem.ConnectionDelay = (int)dd;
            }
        }
    }
}
