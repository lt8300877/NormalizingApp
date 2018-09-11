using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NormalizingApp.Views.Messagebox;
using System.Diagnostics;

namespace NormalizingApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool PLCConnectState = false;//PLC连接状态

        //刷新底部状态栏
        Views.Model.TextItemModel versionDat = new Views.Model.TextItemModel(); //版本号
        Views.Model.TextItemModel clientStatusDat = new Views.Model.TextItemModel();//通讯状态
        Views.Model.TextItemModel informationDat = new Views.Model.TextItemModel();//温馨提示
        Views.Model.TextItemModel serverTimeDat = new Views.Model.TextItemModel();//时间
        Views.Model.TextItemModel serverDelayDat = new Views.Model.TextItemModel();//通讯延时
        Thread UpdataThread; //开一个刷新底部状态栏的后台线程

        public MainWindow()
        {
            InitializeComponent();
            InitData();
            DataContext = new Views.Model.MainWindowViewModel(SoftSnackbar.MessageQueue);
            
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch(btn.Content)
            {
                case "重启计算机":
                    if (CMessageBoxResult.Yes == CMessageBox.Show("确定重启计算机吗？", "提示", CMessageBoxButton.YesNO, CMessageBoxImage.Question))
                    {
                        //点击了显示一个提示的窗体
                        this.TextBlock_DialogMessageConent.Text = "正在退出，请稍后...";
                        DialogHostWait.IsOpen = true;
                        await Task.Delay(1000);
                        DialogHostWait.IsOpen = false;
                        this.Close();
                        System.Environment.Exit(0);
                        Process.Start("shutdown.exe", "-r");//重启
                    }
                    break;
                case "关闭计算机":
                    if (CMessageBoxResult.Yes == CMessageBox.Show("确定关闭计算机吗？", "提示", CMessageBoxButton.YesNO, CMessageBoxImage.Question))
                    {
                        //点击了显示一个提示的窗体
                        this.TextBlock_DialogMessageConent.Text = "正在退出，请稍后...";
                        DialogHostWait.IsOpen = true;
                        await Task.Delay(1000);
                        DialogHostWait.IsOpen = false;
                        this.Close();
                        System.Environment.Exit(0);
                        Process.Start("shutdown.exe", "-s");//关机
                    }
                    break;
                case "退出系统":
                    if (CMessageBoxResult.Yes == CMessageBox.Show("确定退出系统吗？", "提示", CMessageBoxButton.YesNO, CMessageBoxImage.Question))
                    {
                        //点击了显示一个提示的窗体
                        this.TextBlock_DialogMessageConent.Text = "正在退出，请稍后...";
                        DialogHostWait.IsOpen = true;
                        await Task.Delay(1000);
                        DialogHostWait.IsOpen = false;
                        this.Close();
                        System.Environment.Exit(0);
                    }
                    break;

            }
        }

        /// <summary>
        /// 初始化数据(数据库和PLC数据)
        /// </summary>
        private void InitData()
        {
            //初始化底部状态栏的绑定数据
            TextBlock_Version.DataContext = versionDat;//版本号
            TextBlock_ClientStatus.DataContext = clientStatusDat;//通讯状态
            TextBlock_Information.DataContext = informationDat;//温馨提示
            TextBlock_ServerTime.DataContext = serverTimeDat;//时间
            TextBlock_ServerDelay.DataContext = serverDelayDat;//通讯延时
            UpdataThread = new Thread(new ThreadStart(UpButtomStatusBar));
            UpdataThread.IsBackground = true; //设置为后台线程
            UpdataThread.Start();//启动线程

            Access.DBHelp.FileName = Access.DBHelp.fileName;//数据库路径初始化
            //检查数据库文件是否存在，不存在则创建
            if (!Access.DBHelp.CreteFilesName())
            {
                AddStringRenderShow("没有数据库模板！");
                this.Close();
                System.Environment.Exit(0);
            }

            //连接PLC
            if (Lib.S71KConnect.ConnectPLC())
            {
                Lib.S71KConnect.StartPLCRead();
                PLCConnectState = true;
            }
            else
            {
                //Lib.S71KConnect.DisconnectPLC();
                PLCConnectState = false;
                AddStringRenderShow("PLC连接失败！");
            }

        }
        /// <summary>
        /// 底部状态栏数据刷新
        /// </summary>
        private void UpButtomStatusBar()
        {
            while(true)
            {
                versionDat.Content = "1.0.0";
                if(PLCConnectState)
                    clientStatusDat.Content = "已连接";
                else
                {
                    clientStatusDat.Content = "未连接";
                    Lib.S71KConnect.serverDelay = 0;
                }
                    


                informationDat.Content = "夏季高温，注意设备散热！";
                serverTimeDat.Content = DateTime.Now.ToString();
                serverDelayDat.Content = Lib.S71KConnect.serverDelay.ToString();
                Thread.Sleep(500);
            }
        }

        private void AddStringRenderShow(string s)
        {
            var messageQueue = SoftSnackbar.MessageQueue;
            var message = s;

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        /// <summary>
        /// 点击菜单选项事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuToggleButton.IsChecked = false; 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 保存当前的颜色选择
            var p = new PaletteHelper().QueryPalette();
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"Palette.txt", false, Encoding.UTF8))
            {
                sw.Write(JObject.FromObject(p).ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 加载原先保存的主题配色
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Palette.txt"))
            {
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"Palette.txt", Encoding.UTF8))
                {
                    string temp = sr.ReadToEnd();
                    MaterialDesignThemes.Wpf.Palette obj = JObject.Parse(temp).ToObject<MaterialDesignThemes.Wpf.Palette>();
                    new PaletteHelper().ReplacePalette(obj);
                }
            }
            //new PaletteHelper().SetLightDark(true);//设置系统是否为深色
        }
    }
}
