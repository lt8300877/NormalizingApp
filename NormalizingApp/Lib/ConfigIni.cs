using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NormalizingApp.Lib
{
    public static class CofigIni  //配置INI文件读写API
    {
        public static string iniPath = @"..\..\Data\CFG\LoginCFG.ini";//ini配置文件路径
        [DllImport("kernel32", CharSet = CharSet.Unicode)] // 写入配置文件的接口
        private static extern long WritePrivateProfileString(
        string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)] // 读取配置文件的接口
        private static extern int GetPrivateProfileString(
        string section, string key, string def,
        StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键值</param>
        /// <param name="value">写入数据</param>
        /// <param name="path">INI文件路径</param>
        public static void InifileWriteValue(
        string section, string key, string value, string path)
        {
            //创建INI文件
            if (!Directory.Exists(Path.GetDirectoryName(iniPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(iniPath));
                if (!File.Exists(iniPath))
                {
                    File.Create(iniPath);
                }
            }
            WritePrivateProfileString(section, key, value, path);
        }
        /// <summary>
        /// 读取INI配置文件内容
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键值</param>
        /// <param name="path">INI文件路径</param>
        /// <returns></returns>
        public static string InifileReadValue(
        string section, string key, string path)
        {
            StringBuilder sb = new StringBuilder(255);
            //创建INI文件
            if (!Directory.Exists(Path.GetDirectoryName(iniPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(iniPath));
                if (!File.Exists(iniPath))
                {
                    File.Create(iniPath);
                }
            }
            GetPrivateProfileString(section, key, "", sb, 255, path);
            return sb.ToString().Trim();
        }
    }
}
