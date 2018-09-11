using NormalizingApp.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NormalizingApp.Model
{
    public class PartNumber
    {
        public  string spliceNumber { get; set; } //接头编号
        public  string toponymSet { get; set; } //地名07
        public  string lineNameSet { get; set; }//线名1
        public  string groupNameSet { get; set; }//班组R/S
        public  string dateSet { get; set; }//日期
        public  int numberSet { get; set; } //接头编号
        public  string operatorNameSet { get; set; } //操作员姓名
        public  string jodnumberSet { get; set; }//操作员工号
        public DateTime accessDateSet; //数据库日期
    }
}
