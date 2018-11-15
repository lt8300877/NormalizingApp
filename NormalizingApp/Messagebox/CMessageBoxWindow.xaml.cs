using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NormalizingApp.Messagebox
{
    /// <summary>
    /// 消息对话框按钮样式
    /// </summary>
    public enum ButtonStyle
    {
        NormalButtonStyle = 0,
        NotNormalButtonStyle = 1
    }

    /// <summary>
    /// CMessageBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CMessageBoxWindow : Window
    {
        #region 成员
        private Style MaterialDesignRaisedButton;

        private Style MaterialDesignRaisedAccentButton;
        #endregion

        #region 属性
        public string MessageBoxTitle
        {
            get;
            set;
        }

        public string MessageBoxText
        {
            get;
            set;
        }

        public string ImagePath
        {
            get;
            set;
        }

        public Visibility OKButtonVisibility
        {
            get;
            set;
        }

        public Visibility CancelButtonVisibility
        {
            get;
            set;
        }

        public Visibility YesButtonVisibility
        {
            get;
            set;
        }

        public Visibility NoButtonVisibility
        {
            get;
            set;
        }

        public ButtonStyle OKButtonStyle
        {
            set
            {
                if (value == ButtonStyle.NormalButtonStyle)
                {
                    OKButton.Style = MaterialDesignRaisedButton;
                }
                else if (value == ButtonStyle.NotNormalButtonStyle)
                {
                    OKButton.Style = MaterialDesignRaisedAccentButton;
                }
            }
        }

        public ButtonStyle CancelButtonStyle
        {
            set
            {
                if (value == ButtonStyle.NormalButtonStyle)
                {
                    CancelButton.Style = MaterialDesignRaisedButton;
                }
                else if (value == ButtonStyle.NotNormalButtonStyle)
                {
                    CancelButton.Style = MaterialDesignRaisedAccentButton;
                }
            }
        }

        public ButtonStyle YesButtonStyle
        {
            set
            {
                if (value == ButtonStyle.NormalButtonStyle)
                {
                    YesButton.Style = MaterialDesignRaisedButton;
                }
                else if (value == ButtonStyle.NotNormalButtonStyle)
                {
                    YesButton.Style = MaterialDesignRaisedAccentButton;
                }
            }
        }

        public ButtonStyle NoButtonStyle
        {
            set
            {
                if (value == ButtonStyle.NormalButtonStyle)
                {
                    NoButton.Style = MaterialDesignRaisedButton;
                }
                else if (value == ButtonStyle.NotNormalButtonStyle)
                {
                    NoButton.Style = MaterialDesignRaisedAccentButton;
                }
            }
        }

        public CMessageBoxResult Result;
        #endregion

        #region 构造函数
        public CMessageBoxWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            MessageBoxTitle = "提示";
            OKButtonVisibility = System.Windows.Visibility.Collapsed;
            CancelButtonVisibility = System.Windows.Visibility.Collapsed;
            YesButtonVisibility = System.Windows.Visibility.Collapsed;
            NoButtonVisibility = System.Windows.Visibility.Collapsed;

            MaterialDesignRaisedButton = this.FindResource("MaterialDesignRaisedButton") as Style;
            MaterialDesignRaisedAccentButton = this.FindResource("MaterialDesignRaisedAccentButton") as Style;

            Result = CMessageBoxResult.None;
        }
        #endregion

        #region 事件
        private void OnMouseLeftButtonDownAtTitlee(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Result = CMessageBoxResult.OK;
            this.Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = CMessageBoxResult.Yes;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Result = CMessageBoxResult.No;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = CMessageBoxResult.Cancel;
            this.Close();
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Result = CMessageBoxResult.None;
            this.Close();
        }

        private void MinWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Win_Loaded(object sender, RoutedEventArgs e)
        {
            BlurEffect effect = new BlurEffect();
            effect.Radius = 6;
            effect.KernelType = KernelType.Gaussian;
            Application.Current.Windows[0].Effect = effect;
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            Application.Current.Windows[0].Effect = null;
        }
        #endregion
    }
}
