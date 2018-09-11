using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
namespace NormalizingApp.Lib
{
    //定义数据类型枚举
    public enum DataType
    {
        Bool,
        Byte,
        Int16,
        UInt16,
        Int32,
        UInt32,
        Float,
        Int64,
        UInt64,
        Double,
        String
    }

    public class S71KConnect
    {
        public static SiemensS7Net siemensS7Net = new SiemensS7Net(SiemensPLCS.S1200, "192.168.0.1") { ConnectTimeOut = 1000 };
        public static Model.UserType userType = new Model.UserType();
        private static System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBackgroundReadPlc));
        /// <summary>
        /// 长连接(调用此方法就是使用了长连接，如果不调用直接读取数据，那就是短连接)
        /// </summary>
        /// <returns></returns>
        public static bool ConnectPLC()
        {
            OperateResult connect = siemensS7Net.ConnectServer();
            if (connect.IsSuccess) return true;
            else return false;
        }
        /// <summary>
        /// 断开连接，也就是关闭了长连接，如果再去请求数据，就变成了短连接
        /// </summary>
        public static void DisconnectPLC()
        {
            siemensS7Net.ConnectClose();
        }



        /// <summary>
        /// 读取单个变量，返回结果
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="address">地址</param>
        /// <returns></returns>
        public static object ReadItem(DataType type, string address)
        {
            object result = null;
            switch (type)
            {
                case DataType.Bool:
                    result = siemensS7Net.ReadBool(address).Content;
                    break;
                case DataType.Byte:
                    result = siemensS7Net.ReadByte(address).Content;
                    break;
                case DataType.Int16:
                    result = siemensS7Net.ReadInt16(address).Content;
                    break;
                case DataType.UInt16:
                    result = siemensS7Net.ReadUInt16(address).Content;
                    break;
                case DataType.Int32:
                    result = siemensS7Net.ReadInt32(address).Content;
                    break;
                case DataType.UInt32:
                    result = siemensS7Net.ReadUInt32(address).Content;
                    break;
                case DataType.Float:
                    result = siemensS7Net.ReadFloat(address).Content;
                    break;
                case DataType.Int64:
                    result = siemensS7Net.ReadInt64(address).Content;
                    break;
                case DataType.UInt64:
                    result = siemensS7Net.ReadUInt64(address).Content;
                    break;
                case DataType.Double:
                    result = siemensS7Net.ReadDouble(address).Content;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 读取所有变量
        /// </summary>
        /// <param name="ut">数据对象</param>
        public static void ReadItems()
        {

            OperateResult<byte[]> read = siemensS7Net.Read("DB2.0", 140);
            {
                if (read.IsSuccess)
                {
                    userType.Voltage = siemensS7Net.ByteTransform.TransSingle(read.Content, 0);
                    userType.Current = siemensS7Net.ByteTransform.TransSingle(read.Content, 4);
                    userType.Frequency = siemensS7Net.ByteTransform.TransSingle(read.Content, 8);
                    userType.Power = siemensS7Net.ByteTransform.TransSingle(read.Content, 12);
                    userType.Energy = siemensS7Net.ByteTransform.TransSingle(read.Content, 16);

                    userType.XActPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 20);
                    userType.XActSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 24);
                    userType.XSetPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 28);
                    userType.XSetSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 32);
                    userType.XActCurrent = siemensS7Net.ByteTransform.TransSingle(read.Content, 36);

                    userType.YActPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 40);
                    userType.YActSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 44);
                    userType.YSetPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 48);
                    userType.YSetSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 52);
                    userType.YActCurrent = siemensS7Net.ByteTransform.TransSingle(read.Content, 56);

                    userType.ZActPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 60);
                    userType.ZActSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 64);
                    userType.ZSetPos = siemensS7Net.ByteTransform.TransSingle(read.Content, 68);
                    userType.ZSetSpeed = siemensS7Net.ByteTransform.TransSingle(read.Content, 72);
                    userType.ZActCurrent = siemensS7Net.ByteTransform.TransSingle(read.Content, 76);

                    userType.ActTemp1 = siemensS7Net.ByteTransform.TransSingle(read.Content, 80);
                    userType.ActTemp2 = siemensS7Net.ByteTransform.TransSingle(read.Content, 84);
                    userType.ActTemp3 = siemensS7Net.ByteTransform.TransSingle(read.Content, 88);
                    userType.ActTemp4 = siemensS7Net.ByteTransform.TransSingle(read.Content, 92);

                    userType.ActFlow1 = siemensS7Net.ByteTransform.TransSingle(read.Content, 96);
                    userType.ActFlow2 = siemensS7Net.ByteTransform.TransSingle(read.Content, 100);
                    userType.ActFlow3 = siemensS7Net.ByteTransform.TransSingle(read.Content, 104);

                    userType.ActPressure = siemensS7Net.ByteTransform.TransSingle(read.Content, 108);

                    userType.HeatRun = Convert.ToBoolean(read.Content[112]);
                    userType.CoolingRun = Convert.ToBoolean(read.Content[113]);

                    userType.ActAlarm1 = siemensS7Net.ByteTransform.TransInt16(read.Content, 114);
                    userType.ActAlarm2 = siemensS7Net.ByteTransform.TransInt16(read.Content, 116);

                    userType.ActLenght1 = siemensS7Net.ByteTransform.TransSingle(read.Content, 118);
                    userType.ActLenght2 = siemensS7Net.ByteTransform.TransSingle(read.Content, 122);

                    userType.Y_CDJ_QGQW = Convert.ToBoolean(read.Content[126]);
                    userType.Y_CDJ_QGHW = Convert.ToBoolean(read.Content[127]);
                    userType.Z_CDJ_QGQW = Convert.ToBoolean(read.Content[128]);
                    userType.Z_CDJ_QGHW = Convert.ToBoolean(read.Content[129]);

                    userType.JLG_1_SW = Convert.ToBoolean(read.Content[130]);
                    userType.JLG_1_XW = Convert.ToBoolean(read.Content[131]);
                    userType.JLG_2_SW = Convert.ToBoolean(read.Content[132]);
                    userType.JLG_2_XW = Convert.ToBoolean(read.Content[133]);
                    userType.JLG_3_SW = Convert.ToBoolean(read.Content[134]);
                    userType.JLG_3_XW = Convert.ToBoolean(read.Content[135]);
                    userType.JLG_4_SW = Convert.ToBoolean(read.Content[136]);
                    userType.JLG_4_XW = Convert.ToBoolean(read.Content[137]);
                    userType.DY_ZT = Convert.ToBoolean(read.Content[138]);
                    userType.PQ_ZT = Convert.ToBoolean(read.Content[139]);
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
            // 结束后台读取的线程
            thread.Abort();
        }
        public static int serverDelay; //通讯延时
        private static void ThreadBackgroundReadPlc()
        {
            while (true)
            {
                DateTime dt = System.DateTime.Now;
                ReadItems();//读取DB块数据
                
                //System.Threading.Thread.Sleep(10);// 两次读取的时间间隔
                double dd = (DateTime.Now - dt).TotalMilliseconds;
                serverDelay = (int)dd;
            }
        }
    }
}
