using System;
using NormalizingApp.Lib;
namespace NormalizingApp.Models
{
    public class UserItems
    {
        /// <summary>
        /// PLC通讯状态
        /// </summary>
        public bool ConnectionState { get; set; }
        /// <summary>
        /// PLC通讯延时
        /// </summary>
        public int ConnectionDelay { get; set; }
        /// <summary>
        /// 系统时间
        /// </summary>
        public string SystempDataTime { get; set; }

        //自动加热启动信号
        public bool HeatRun { get; set; }
        
        //自动喷风启动信号
        public bool CoolingRun { get; set; }
        

        public float Voltage { get; set; } //电压
        public float Current { get; set; } //电流
        public float Frequency { get; set; } //频率
        public float Power { get; set; } //功率
        public float Energy { get; set; } //能量

        public float XActPos { get; set; } 
        public float XActSpeed { get; set; }
        public float XSetPos { get; set; }
        public float XSetSpeed { get; set; }
        public float XActCurrent { get; set; }
        public float YActPos { get; set; }
        public float YActSpeed { get; set; }
        public float YSetPos { get; set; }
        public float YSetSpeed { get; set; }
        public float YActCurrent { get; set; }
        public float ZActPos { get; set; }
        public float ZActSpeed { get; set; }
        public float ZSetPos { get; set; }
        public float ZSetSpeed { get; set; }
        public float ZActCurrent { get; set; }

        public float ActTemp1 { get; set; } //温度1
        public float ActTemp2 { get; set; } //温度2
        public float ActTemp3 { get; set; } //温度3
        public float ActTemp4 { get; set; }//温度4

        public float ActFlow1 { get; set; }//流量1
        public float ActFlow2 { get; set; }//流量2
        public float ActFlow3 { get; set; }//流量3
        public float ActPressure { get; set; }//压力

        public UInt16 ActAlarm1 { get; set; }//报警1
        public UInt16 ActAlarm2 { get; set; }//报警2

        public float ActLenght1 { get; set; }//长度计1
        public float ActLenght2 { get; set; }//长度计2

        public bool Y_CDJ_QGQW { get; set; } //Y轴长度计气缸前位
        public bool Y_CDJ_QGHW { get; set; } //Y轴长度计气缸后位
        public bool Z_CDJ_QGQW { get; set; } //Z轴长度计气缸前位
        public bool Z_CDJ_QGHW { get; set; } //Z轴长度计气缸后位

        public bool JLG_1_SW { get; set; } //举料缸1上位
        public bool JLG_1_XW { get; set; } //举料缸1下位
        public bool JLG_2_SW { get; set; } //举料缸2上位
        public bool JLG_2_XW { get; set; } //举料缸2下位
        public bool JLG_3_SW { get; set; } //举料缸3上位
        public bool JLG_3_XW { get; set; } //举料缸3下位
        public bool JLG_4_SW { get; set; } //举料缸4上位
        public bool JLG_4_XW { get; set; } //举料缸4下位

        public bool DY_ZT { get; set; } //电源状态
        public bool PQ_ZT { get; set; } //喷气状态
        public bool ProcessOk { get; set; } //工件加工完成
    }
}
