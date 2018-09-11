using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NormalizingApp.Lib
{
    public class FileBuff
    {
        //序列化文件路径属性设置
        public static string FileName{get;set;}
        /// <summary>
        /// 数据采集记录序列化保存到文件
        /// </summary>
        /// <param name="lst"></param>
        public static void SaveBinary(List<object> lst)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(FileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FileName));
                }
                FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, lst);
                fs.Close();
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 读取文件并反序列化出结果
        /// </summary>
        /// <returns></returns>
        public static List<object> ReadBinary()
        {
            List<object> lst = new List<object>();
            if (File.Exists(FileName))
            {
                try
                {
                    FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate);
                    BinaryFormatter bf = new BinaryFormatter();
                    lst = bf.Deserialize(fs) as List<object>;
                    fs.Close();
                }
                catch
                {
                    return lst;
                }
            }
            return lst;
        }

    }
}
