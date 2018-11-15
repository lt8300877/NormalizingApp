using System;
using System.Collections.Generic;
using System.Text;
using ADOX;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using DAO = Microsoft.Office.Interop.Access.Dao;
using System.IO;


namespace NormalizingApp.DataBase
{
    public class DBHelp
    {
        private static OleDbConnection connection;
        public static string fileName = @"..\..\Data\WorkData\" + ViewModels.LoginWindowViewModel.productNumber.AccessDate.Date.ToString("yyyy") +
                                @"\" + ViewModels.LoginWindowViewModel.productNumber.AccessDate.Date.ToString("yyyyMM") +
                                @"\" + ViewModels.LoginWindowViewModel.productNumber.AccessDate.Date.ToString("yyyyMMdd") + ".accdb";
        private static string fileNameTemplate = @"..\..\Data\AccessTemplate\Template.accdb";

        public static string FileName { get; set; }

        /// <summary>
        /// 创建工作数据目录和数据库
        /// </summary>
        public static bool CreteFilesName()
        {
            if(!Directory.Exists(Path.GetDirectoryName(FileName)))
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FileName));
                    if (CopyAccessDB())
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    if (CopyAccessDB())
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                
            }

        }
        /// <summary>
        /// 复制且重命名数据库到工作目录下
        /// </summary>
        public static bool CopyAccessDB()
        {
            try
            {
                //判断路径是否存在，若不存在就创建
                if (!Directory.Exists(Path.GetDirectoryName(fileNameTemplate)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileNameTemplate));
                }
                //判断文件是否存在
                if (File.Exists(fileNameTemplate))
                {
                    if(!File.Exists(FileName))
                    {
                        File.Copy(fileNameTemplate, FileName);
                        return true;
                    }
                    else return true;

                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建ACCESS数据文件
        /// </summary>
        /// <returns></returns>
        public static bool CreateDB() //创建access数据库
        {
            ////创建数据库 
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data source =" + FileName;   //accdb 数据库
            Catalog catalog = new Catalog();
            try
            {
                if(!File.Exists(FileName))
                catalog.Create(connectionString);
                else
                return true;
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 数据库连接属性
        /// </summary>
        public static OleDbConnection Connection
        {

                //string strPath = Application.StartupPath;
                //int x = strPath.IndexOf("bin");
                //strPath = strPath.Substring(0, x-1);
                //strPath += filename;  
            get
            {
                //string connectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data source=" + FileName;   //mdb 数据库
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data source =" + FileName;   //accdb 数据库
                if (connection == null)
                {
                    connection = new OleDbConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                }
                else if (connection.State == ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Open)   //更改新的连接
                {
                    connection.Close();
                    connection = null;
                    connection = new OleDbConnection(connectionString);
                    connection.Open();
                }
                return connection;
            }

            
        }
        //ExecuteCommand sql语句方法
        public static int ExecuteCommand(string safeSql)   
        {
            OleDbCommand cmd = new OleDbCommand(safeSql, Connection);            
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        //ExecuteCommand sql语句方法＋参数形式
        public static int ExecuteCommand(string sql, params OleDbParameter[] values)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }

        //GetReader sql语句方法
        public static  OleDbDataReader GetReader(string safeSql)
        {
            OleDbCommand cmd = new OleDbCommand(safeSql, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        //GetReader sql语句方法＋参数形式 
        public static OleDbDataReader GetReader(string sql, params OleDbParameter[] values)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            OleDbDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        //GetDataSet  sql语句方法
        public static DataSet GetDataSet(string safesql)
        {
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand(safesql, Connection);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }

        /// <summary>
        /// 获取数据库所有表名
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllTableName()
        {
            List<string> tableName = new List<string>();

            try
            {
                //判断文件是否存在
                if (File.Exists(FileName))
                {
                    if (Connection.State == ConnectionState.Closed)
                        Connection.Open();
                    DataTable dt = Connection.GetSchema("Tables");
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[3].ToString() == "TABLE")
                            tableName.Add(row[2].ToString());
                    }
                }
                return tableName;
            }
            catch
            {
                Connection.Close();
                return tableName;
            }
        }


        #region 创建数据表格和固定字段
        /// <summary>
        /// 创建数据表格和固定字段
        /// </summary>
        /// <returns></returns>
        public static bool CreateSheetData(string tableName)
        {
            try
            {
                OleDbCommand cmdStr = new OleDbCommand();
                cmdStr.Connection = Connection;
                cmdStr.CommandText = @"create table " + tableName + " (ID int IDENTITY (1,1) primary key, 焊缝编号 text, 操作员 text, 工号 text, 加热时间 text, 电压 text," +
                    "电流 text, 功率 text, 能量 text, 轨顶温度 text, 轨脚温度 text, 内轨脚温度 text, 喷风时间 text, 喷风温度 text, 轨顶流量 text, 轨腰流量 text, 轨底流量 text," +
                    "喷风压力 text)";
                cmdStr.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                Connection.Close();
                return false;
            }

        }
        #endregion

        #region 数据批量插入到数据库  
        /// <summary>
        /// 数据批量插入到数据库  
        /// </summary>
        /// <param name = "sList" ></ param >
        public static void add(List<Models.DataBaseRecord> sList)
        {
            DAO.DBEngine dbEngine = new DAO.DBEngine();
            DAO.Database db = dbEngine.OpenDatabase(FileName);
            DAO.Recordset rs = db.OpenRecordset("DataSheet");
            DAO.Field[] myFields = new DAO.Field[18];
            myFields[0] = rs.Fields["serialNumber"];
            myFields[1] = rs.Fields["operatorName"];
            myFields[2] = rs.Fields["jobNumber"];
            myFields[3] = rs.Fields["voltage"];
            myFields[4] = rs.Fields["current"];
            myFields[5] = rs.Fields["power"];
            myFields[6] = rs.Fields["frequency"];
            myFields[7] = rs.Fields["energy"];
            myFields[8] = rs.Fields["temperature1"];
            myFields[9] = rs.Fields["temperature2"];
            myFields[10] = rs.Fields["temperature3"];
            myFields[11] = rs.Fields["temperature4"];
            myFields[12] = rs.Fields["flow1"];
            myFields[13] = rs.Fields["flow2"];
            myFields[14] = rs.Fields["flow3"];
            myFields[15] = rs.Fields["pressure"];
            myFields[16] = rs.Fields["heatTime"];
            myFields[17] = rs.Fields["coolingTime"];

            for (int i = 0; i < sList.Count; i++)
            {
                rs.AddNew();
                myFields[0].Value = sList[i].serialNumber;
                myFields[1].Value = sList[i].operatorName;
                myFields[2].Value = sList[i].jobNumber;
                myFields[3].Value = sList[i].voltage;
                myFields[4].Value = sList[i].current;
                myFields[5].Value = sList[i].power;
                myFields[6].Value = sList[i].frequency;
                myFields[7].Value = sList[i].energy;
                myFields[8].Value = sList[i].temperature1;
                myFields[9].Value = sList[i].temperature2;
                myFields[10].Value = sList[i].temperature3;
                myFields[11].Value = sList[i].temperature4;
                myFields[12].Value = sList[i].flow1;
                myFields[13].Value = sList[i].flow2;
                myFields[14].Value = sList[i].flow3;
                myFields[15].Value = sList[i].pressure;
                myFields[16].Value = sList[i].heatTime;
                myFields[17].Value = sList[i].coolingTime;
                rs.Update();
            }
            rs.Close();
            db.Close();
        }
        #endregion






    }
}
