using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
//using Microsoft.Office.Interop;
//using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
namespace NormalizingApp.Access
{
    public class CQServices
    {
        #region 添加数据记录
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="record">数据记录属性</param>
        /// <returns></returns>
        public static int AddHeatRecords(Model.Record record)
        {
            string sql = "insert into [HeatDataSheet]([serialNumber],[operatorName],[jobNumber],[voltage],[current],[power],[frequency],[energy],[temperature1],[temperature2],[temperature3],[temperature4],[flow1],[flow2],[flow3],[pressure],[heatTime],[coolingTime])";
            sql += "values (@serialNumber,@operatorName,@jobNumber,@voltage,@current,@power,@frequency,@energy,@temperature1,@temperature2,@temperature3,@temperature4,@flow1,@flow2,@flow3,@pressure,@heatTime,@coolingTime)";
            OleDbParameter[] para = new OleDbParameter[]
                {
                      new OleDbParameter("@serialNumber",string.IsNullOrEmpty(record.serialNumber) ? "" : record.serialNumber),
                      new OleDbParameter("@operatorName",string.IsNullOrEmpty(record.operatorName) ? "" : record.operatorName),
                      new OleDbParameter("@jobNumber",string.IsNullOrEmpty(record.jobNumber) ? "" : record.jobNumber),
                      new OleDbParameter("@voltage",string.IsNullOrEmpty(record.voltage) ? "" : record.voltage),
                      new OleDbParameter("@current",string.IsNullOrEmpty(record.current) ? "" : record.current),
                      new OleDbParameter("@power",string.IsNullOrEmpty(record.power) ? "" : record.power),
                      new OleDbParameter("@frequency",string.IsNullOrEmpty(record.frequency) ? "" : record.frequency),
                      new OleDbParameter("@energy",string.IsNullOrEmpty(record.energy) ? "" : record.energy),
                      new OleDbParameter("@temperature1",string.IsNullOrEmpty(record.temperature1) ? "" : record.temperature1),
                      new OleDbParameter("@temperature2",string.IsNullOrEmpty(record.temperature2) ? "" : record.temperature2),
                      new OleDbParameter("@temperature3",string.IsNullOrEmpty(record.temperature3) ? "" : record.temperature3),
                      new OleDbParameter("@temperature4",string.IsNullOrEmpty(record.temperature4) ? "" : record.temperature4),
                      new OleDbParameter("@flow1",string.IsNullOrEmpty(record.flow1) ? "" : record.flow1),
                      new OleDbParameter("@flow2",string.IsNullOrEmpty(record.flow2) ? "" : record.flow2),
                      new OleDbParameter("@flow3",string.IsNullOrEmpty(record.flow3) ? "" : record.flow3),
                      new OleDbParameter("@pressure",string.IsNullOrEmpty(record.pressure) ? "" : record.pressure),
                      new OleDbParameter("@heatTime",string.IsNullOrEmpty(record.heatTime.ToString()) ? "" : record.heatTime.ToString()),
                      new OleDbParameter("@coolingTime",string.IsNullOrEmpty(record.coolingTime.ToString()) ? "" : record.coolingTime.ToString()),
                };
            return DBHelp.ExecuteCommand(sql, para);
        }
        #endregion

        #region 获取数据所有记录
        /// <summary>
        /// 获取数据所有记录
        /// </summary>
        /// <returns></returns>
        public static List<Model.Record> GetAllRecords()
        {
            string sqlAll = "select * from [DataSheet]";
            return GetRecordBySql(sqlAll);
        }
        #endregion

        /// <summary>
        /// 根据SQL语句查询一条记录
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static List<string> GetRecordSql(string safeSql)
        {
            List<string> list = new List<string>();
            list.Clear();
            try
            {
                DataSet ds = DBHelp.GetDataSet(safeSql);
                if (ds == null) return list;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(row[0].ToString());
                }
                return list;
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }



        #region 根据SQL语句查询所有的记录
        /// <summary>
        /// 根据SQL语句查询所有的记录
        /// </summary>
        /// <param name="safeSql">SQL语句</param>
        /// <returns></returns>
        public static List<Model.Record> GetRecordBySql(string safeSql)
        {
            List<Model.Record> list = new List<Model.Record>();
            list.Clear();
            try
            {
                DataSet ds = DBHelp.GetDataSet(safeSql);
                if (ds == null) return list;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Model.Record record = new Model.Record();
                    record.serialNumber = row["serialNumber"].ToString();
                    record.operatorName = row["operatorName"].ToString();
                    record.jobNumber = row["jobNumber"].ToString();
                    record.voltage = row["voltage"].ToString();
                    record.current = row["current"].ToString();
                    record.power = row["power"].ToString();
                    record.frequency = row["frequency"].ToString();
                    record.energy = row["energy"].ToString();
                    record.temperature1 = row["temperature1"].ToString();
                    record.temperature2 = row["temperature2"].ToString();
                    record.temperature3 = row["temperature3"].ToString();
                    record.temperature4 = row["temperature4"].ToString();
                    record.flow1 = row["flow1"].ToString();
                    record.flow2 = row["flow2"].ToString();
                    record.flow3 = row["flow3"].ToString();
                    record.pressure = row["pressure"].ToString();
                    record.heatTime = row["heatTime"].ToString();
                    record.coolingTime = row["coolingTime"].ToString();
                    list.Add(record);
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }
        #endregion

        #region 更新一条检测记录
        /// <summary>
        /// 更新一条检测记录
        /// </summary>
        /// <param name="record">数据记录属性</param>
        /// <returns></returns>
        public static bool UpdateKJRecordByNO(Model.Record record)
        {
            try
            {
                //string sql = "update [DataSheet] set ";
                //sql += "[kjh]='" + record.kjh + "'";
                //sql += " where [qh]=" + "'" + record.qh + "'";

                //DBHelp.ExecuteCommand(sql);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }
        #endregion

        #region 删除指定记录
        /// <summary>
        /// 删除指定记录
        /// </summary>
        /// <returns></returns>
        public static bool DeleteRecord(string str)
        {
            try
            {
                //string sql = "delete  from [DataSheet] ";
                string sql = @"DELETE DataSheet.serialNumber FROM DataSheet WHERE(((DataSheet.serialNumber) = '" + str + @"'))"; 
                DBHelp.ExecuteCommand(sql);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        #endregion

        #region 大数据插入Access
        /// <summary>
        /// 大数据插入
        /// </summary>
        /// <param name="sList"></param>
        public static void BulckInsert(List<Model.Record> sList)
        {
            DBHelp.add(sList);
        }

        #endregion











    }
}
