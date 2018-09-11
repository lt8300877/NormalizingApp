using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using HslCommunication;
using HslCommunication.BasicFramework;
using Newtonsoft.Json.Linq;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Text.RegularExpressions;
using NormalizingApp.Views.Messagebox;
using NormalizingApp.Model;
namespace NormalizingApp
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static Model.PartNumber partNumber = new Model.PartNumber(); //声明全局编号
        private string pattern = @"^[0-9]*$"; //正则表达式字符(零件编号输入框)
        private string namePath = @"..\..\Data\Name\name.txt"; //姓名存储文件路径

        public LoginWindow()
        {
            InitializeComponent();
            InitControlProperty();
        }

        //获取编号
        public static string GetPartNumber()
        {
            partNumber.spliceNumber = partNumber.toponymSet + partNumber.lineNameSet + partNumber.groupNameSet + partNumber.dateSet + partNumber.numberSet.ToString();
            return partNumber.spliceNumber;
        }
        //编号增1
        public static void PartNumberAuto()
        {
            //编号自加1 并存入硬盘
            LoginWindow.partNumber.numberSet++;
            Lib.CofigIni.InifileWriteValue("设置", "接头号", partNumber.numberSet.ToString(), Lib.CofigIni.iniPath);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //加载原先保存的主题配色
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Palette\Palette.txt"))
            {
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Palette\Palette.txt", Encoding.UTF8))
                {
                    string temp = sr.ReadToEnd();
                    MaterialDesignThemes.Wpf.Palette obj = JObject.Parse(temp).ToObject<MaterialDesignThemes.Wpf.Palette>();
                    new PaletteHelper().ReplacePalette(obj);
                }
            }
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }
        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        /// <summary>
        /// 初始化控件属性
        /// </summary>
        private void InitControlProperty()
        {
            this.LineNameCbx.Items.Add("热1线");
            this.LineNameCbx.Items.Add("热2线");
            this.LineNameCbx.Items.Add("热3线");
            this.GroupNameCbx.Items.Add("S班");
            this.GroupNameCbx.Items.Add("R班");
            this.GroupNameCbx.Items.Add("T");
            this.DatePickerCtl.SelectedDate = DateTime.Now.Date;
            partNumber.toponymSet = "07";//地名代号
            try
            {
                string[] lines = File.ReadAllLines(namePath);
                foreach (string line in lines)
                {
                    this.OperatorNameCbx.Items.Add(line);
                }
                this.LineNameCbx.SelectedIndex = int.Parse(Lib.CofigIni.InifileReadValue("设置", "生产线名称", Lib.CofigIni.iniPath));
                this.GroupNameCbx.SelectedIndex = int.Parse(Lib.CofigIni.InifileReadValue("设置", "班组", Lib.CofigIni.iniPath));
                this.NumberTbx.Text = Lib.CofigIni.InifileReadValue("设置", "接头号", Lib.CofigIni.iniPath);
                this.OperatorNameCbx.SelectedIndex = int.Parse(Lib.CofigIni.InifileReadValue("设置", "姓名", Lib.CofigIni.iniPath));
            }
            catch
            {
                return;
            }

        }

        /// <summary>
        /// 线名称设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineNameCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (LineNameCbx.SelectedIndex)
            {
                case 0:
                    partNumber.lineNameSet = "1";
                    break;
                case 1:
                    partNumber.lineNameSet = "2";
                    break;
                case 2:
                    partNumber.lineNameSet = "3";
                    break;
            }
            Lib.CofigIni.InifileWriteValue("设置", "生产线名称", LineNameCbx.SelectedIndex.ToString(), Lib.CofigIni.iniPath);
        }
        /// <summary>
        /// 班组设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupNameCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GroupNameCbx.SelectedIndex)
            {
                case 0: partNumber.groupNameSet = "S"; break;
                case 1: partNumber.groupNameSet = "R"; break;
                case 2: partNumber.groupNameSet = "T"; break;
            }
            Lib.CofigIni.InifileWriteValue("设置", "班组", GroupNameCbx.SelectedIndex.ToString(), Lib.CofigIni.iniPath);
        }

        /// <summary>
        /// 日期选择设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePickerCtl_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            partNumber.dateSet = DatePickerCtl.SelectedDate.Value.Date.ToString("yyMMdd");//焊缝编号日期设置
            partNumber.accessDateSet = (DateTime)DatePickerCtl.SelectedDate;//access数据库日期设置
    }
        /// <summary>
        /// 接头编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            Match m = Regex.Match(NumberTbx.Text, pattern);   // 匹配正则表达式
            if (!m.Success)   // 输入的不是数字
            {
                CMessageBox.Show("只能输入数字！", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                if(NumberTbx.SelectionStart > 0)
                {
                    NumberTbx.Text = NumberTbx.Text.Remove(NumberTbx.SelectionStart - 1, 1);
                }
                NumberTbx.SelectionStart = NumberTbx.Text.Length;
            }
            else if (NumberTbx.Text != "")
            {
                try
                {
                    partNumber.numberSet = Convert.ToInt32(NumberTbx.Text);
                    Lib.CofigIni.InifileWriteValue("设置", "接头号", partNumber.numberSet.ToString(), Lib.CofigIni.iniPath);
                }
                catch
                {
                    MessageBox.Show("只能输入数字！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (NumberTbx.SelectionStart > 0)
                    {
                        NumberTbx.Text = NumberTbx.Text.Remove(NumberTbx.SelectionStart - 1, 1);
                    }
                    NumberTbx.SelectionStart = NumberTbx.Text.Length;
                    return;
                }
            }
        }
        /// <summary>
        /// 操作员名称设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatorNameCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OperatorNameCbx.SelectedIndex == -1) return;
            partNumber.operatorNameSet = OperatorNameCbx.SelectedItem.ToString();
            Lib.CofigIni.InifileWriteValue("设置", "姓名", OperatorNameCbx.SelectedIndex.ToString(), Lib.CofigIni.iniPath);
        }

    }
}
