using System;

namespace NormalizingApp.Models
{
    public class ProductNumber
    {
        /// <summary>
        /// 完整接头编号
        /// </summary>
        public string ProductNumberFull { get; set; }
        /// <summary>
        /// 地名07
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 线名
        /// </summary>
        public string LineName { get; set; }
        /// <summary>
        /// 班组R/S
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 接头编号
        /// </summary>
        public int SpliceNumber { get; set; } 
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 操作员工号
        /// </summary>
        public string WorksNumber { get; set; }
        /// <summary>
        /// 数据库日期
        /// </summary>
        public DateTime AccessDate { get; set; }
    }
}
