using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NormalizingApp.Model
{
    [Serializable]
    public class Record
    {
        public int ID { get; set; }
        public string heatTime { get; set; }//加热时间
        public string coolingTime { get; set; }//冷却时间
        public string serialNumber { get; set; } //零件编号
        public string operatorName { get; set; }//操作员名称
        public string jobNumber { get; set; } //工号
        public string voltage { get; set; } //电压
        public string current { get; set; }//电流
        public string power { get; set; }//功率
        public string frequency { get; set; } //频率
        public string energy { get; set; }//能量
        public string temperature1 { get; set; }//温度1
        public string temperature2 { get; set; }//温度2
        public string temperature3 { get; set; }//温度3
        public string temperature4 { get; set; }//温度4
        public string flow1 { get; set; }//流量1
        public string flow2 { get; set; }//流量2
        public string flow3 { get; set; }//流量3
        public string pressure { get; set; }//压力
    }
}
