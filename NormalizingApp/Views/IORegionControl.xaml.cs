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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NormalizingApp.MVVM;
using NormalizingApp.Messagebox;
namespace NormalizingApp.Views
{
    /// <summary>
    /// IORegionControl.xaml 的交互逻辑
    /// </summary>
    public partial class IORegionControl : UserControl
    {
        public static readonly DependencyProperty InputTextProperty = DependencyProperty.Register("InputText", typeof(string), typeof(IORegionControl), new PropertyMetadata("TextBox", new PropertyChangedCallback(OnInputTextChanged)));
        public static readonly DependencyProperty OutputTextProperty = DependencyProperty.Register("OutputText", typeof(string), typeof(IORegionControl), new PropertyMetadata("TextBox", new PropertyChangedCallback(OnOutputTextChanged)));


        public string InputText
        {
            get { return (string)GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }
        public string OutputText
        {
            get { return (string)GetValue(OutputTextProperty); }
            set { SetValue(OutputTextProperty, value); }
        }


        static void OnInputTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((IORegionControl)sender).OnInputValueChanged(args);
        }
        static void OnOutputTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((IORegionControl)sender).OnOutputValueChanged(args);
        }


        protected void OnInputValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            InputTextBox.Text = e.NewValue.ToString();
        }
        protected void OnOutputValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            OutputTextBox.Text = e.NewValue.ToString();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IORegionControl()
        {
            InitializeComponent();
            InputTextBox.Visibility = Visibility.Collapsed;
            OutputTextBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 输出框获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Visibility = Visibility.Collapsed; //隐藏控件
            InputTextBox.Visibility = Visibility.Visible; //显示控件
            InputTextBox.Focus();//获得焦点
            if (OutputText == null) InputText = "";
            InputText = OutputText; //输出值传给输入框
            InputTextBox.SelectAll();//选择全部内容

        }
        /// <summary>
        /// 输入框和输出框任意一个失去焦点事件(隐藏输入框，显示输出框)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            InputTextBox.Visibility = Visibility.Collapsed;
            OutputTextBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 输入框按下键盘(隐藏输入框，显示输出框)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) 
            {
                InputTextBox.Visibility = Visibility.Collapsed;
                OutputTextBox.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// 输入框内容改变时更新属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputText = InputTextBox.Text;
        }
    }

}
