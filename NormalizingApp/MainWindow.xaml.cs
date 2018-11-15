using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using NormalizingApp.Messagebox;
namespace NormalizingApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainWindowViewModel(SoftSnackbar.MessageQueue);
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
    }
}
