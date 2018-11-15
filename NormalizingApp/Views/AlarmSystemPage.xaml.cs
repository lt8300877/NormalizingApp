using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HslCommunication.LogNet;

namespace NormalizingApp.Views
{
    /// <summary>
    /// AlarmSystemPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmSystemPage : UserControl
    {
        
        //实例化一个报警记录文件
        public static ILogNet logNet = new LogNetSingle(@"..\..\Data\Log\Alarmlog.txt");



        public AlarmSystemPage()
        {
            InitializeComponent();
        }
        
    }
}
