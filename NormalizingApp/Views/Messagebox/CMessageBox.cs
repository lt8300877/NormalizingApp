using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


/*
CMessageBox.Show("Hello,CMessageBox!",CMessageBoxButton.OKCancel);
CMessageBox.Show("Hello,CMessageBox!", "YesNo",CMessageBoxButton.YesNO);
CMessageBox.Show("Hello,CMessageBox!", "YesNo", CMessageBoxButton.YesNoCancel);
 * 
*/

namespace NormalizingApp.Views.Messagebox
{
    /// <summary>
    /// CMessageBox显示的按钮类型
    /// </summary>
    public enum CMessageBoxButton
    {
        OK = 0,
        OKCancel = 1,
        YesNO = 2,
        YesNoCancel = 3
    }

    /// <summary>
    /// CMessageBox显示的图标类型
    /// </summary>
    public enum CMessageBoxImage
    {
        None = 0,
        Error = 1,
        Question = 2,
        Warning = 3
    }

    /// <summary>
    /// 消息的重点显示按钮
    /// </summary>
    public enum CMessageBoxDefaultButton
    {
        None = 0,
        OK = 1,
        Cancel = 2,
        Yes = 3,
        No = 4
    }
    
    /// <summary>
    /// 消息框的返回值
    /// </summary>
    public enum CMessageBoxResult
    {
        //用户直接关闭了消息窗口
        None = 0,
        //用户点击确定按钮
        OK = 1,
        //用户点击取消按钮
        Cancel = 2,
        //用户点击是按钮
        Yes = 3,
        //用户点击否按钮
        No = 4
    }
    
    public class CMessageBox
    {
        /// <summary>
        /// 提示图标根据类型显示
        /// </summary>
        private static string BoxImageSelect(CMessageBoxImage boxImage)
        {
            string s = string.Empty;
            switch (boxImage)
            {
                case CMessageBoxImage.Error:
                    s = @"../../Resources/error.png";
                    break;
                case CMessageBoxImage.Question:
                    s = @"../../Resources/Question.png";
                    break;
                case CMessageBoxImage.Warning:
                    s = @"../../Resources/warning.png";
                    break;
            }
            return s;
        }
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="cmessageBoxText">消息内容</param>
        public static CMessageBoxResult Show(string cmessageBoxText)
        {
            CMessageBoxWindow window = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window = new CMessageBoxWindow();
            }));
            window.MessageBoxText = cmessageBoxText;
            window.OKButtonVisibility = Visibility.Visible;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window.ShowDialog();
            }));
            return window.Result;
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="cmessageBoxText">消息内容</param>
        /// <param name="caption">消息标题</param>
        public static CMessageBoxResult Show(string cmessageBoxText, string caption)
        {
            CMessageBoxWindow window = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window = new CMessageBoxWindow();
            }));
            window.MessageBoxText = cmessageBoxText;
            window.MessageBoxTitle = caption;
            window.OKButtonVisibility = Visibility.Visible;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window.ShowDialog();
            }));
            return window.Result;
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="cmessageBoxText">消息内容</param>
        /// <param name="CMessageBoxButton">消息框按钮</param>
        public static CMessageBoxResult Show(string cmessageBoxText, CMessageBoxButton CMessageBoxButton)
        {
            CMessageBoxWindow window = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window = new CMessageBoxWindow();
            }));
            window.MessageBoxText = cmessageBoxText;
            switch (CMessageBoxButton)
            {
                case CMessageBoxButton.OK:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.OKCancel:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        window.CancelButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.YesNO:
                    {
                        window.YesButtonVisibility = Visibility.Visible;
                        window.NoButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.YesNoCancel:
                    {
                        window.YesButtonVisibility = Visibility.Visible;
                        window.NoButtonVisibility = Visibility.Visible;
                        window.CancelButtonVisibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        break;
                    }
            }
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window.ShowDialog();
            }));
            return window.Result;
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="cmessageBoxText">消息内容</param>
        /// <param name="caption">消息标题</param>
        /// <param name="CMessageBoxButton">消息框按钮</param>
        public static CMessageBoxResult Show(string cmessageBoxText, string caption, CMessageBoxButton CMessageBoxButton)
        {
            CMessageBoxWindow window = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window = new CMessageBoxWindow();
            }));
            window.MessageBoxText = cmessageBoxText;
            window.MessageBoxTitle = caption;
            switch (CMessageBoxButton)
            {
                case CMessageBoxButton.OK:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.OKCancel:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        window.CancelButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.YesNO:
                    {
                        window.YesButtonVisibility = Visibility.Visible;
                        window.NoButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.YesNoCancel:
                    {
                        window.YesButtonVisibility = Visibility.Visible;
                        window.NoButtonVisibility = Visibility.Visible;
                        window.CancelButtonVisibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        break;
                    }
            }
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window.ShowDialog();
            }));
            return window.Result;
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="cmessageBoxText">消息内容</param>
        /// <param name="caption">消息标题</param>
        /// <param name="CMessageBoxButton">消息框按钮</param>
        /// <param name="CMessageBoxImage">消息框图标</param>
        /// <returns></returns>
        public static CMessageBoxResult Show(string cmessageBoxText, string caption, CMessageBoxButton CMessageBoxButton, CMessageBoxImage CMessageBoxImage)
        {
            CMessageBoxWindow window = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window = new CMessageBoxWindow();
            }));

            window.MessageBoxText = cmessageBoxText;
            window.MessageBoxTitle = caption;
            window.ImagePath = BoxImageSelect(CMessageBoxImage);
            switch (CMessageBoxButton)
            {
                case CMessageBoxButton.OK:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.OKCancel:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        window.CancelButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.YesNO:
                    {
                        window.YesButtonVisibility = Visibility.Visible;
                        window.NoButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.YesNoCancel:
                    {
                        window.YesButtonVisibility = Visibility.Visible;
                        window.NoButtonVisibility = Visibility.Visible;
                        window.CancelButtonVisibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        break;
                    }
            }
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window.ShowDialog();
            }));
            return window.Result;
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="cmessageBoxText">消息内容</param>
        /// <param name="caption">消息标题</param>
        /// <param name="CMessageBoxButton">消息框按钮</param>
        /// <param name="CMessageBoxImage">消息框图标</param>
        /// <param name="CMessageBoxDefaultButton">消息框默认按钮</param>
        /// <returns></returns>
        public static CMessageBoxResult Show(string cmessageBoxText, string caption, CMessageBoxButton CMessageBoxButton, CMessageBoxImage CMessageBoxImage, CMessageBoxDefaultButton CMessageBoxDefaultButton)
        {
            CMessageBoxWindow window = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window = new CMessageBoxWindow();
            }));
            window.MessageBoxText = cmessageBoxText;
            window.MessageBoxTitle = caption;
            window.ImagePath = BoxImageSelect(CMessageBoxImage);
            #region 按钮
            switch (CMessageBoxButton)
            {
                case CMessageBoxButton.OK:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.OKCancel:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        window.CancelButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.YesNO:
                    {
                        window.YesButtonVisibility = Visibility.Visible;
                        window.NoButtonVisibility = Visibility.Visible;
                        break;
                    }
                case CMessageBoxButton.YesNoCancel:
                    {
                        window.YesButtonVisibility = Visibility.Visible;
                        window.NoButtonVisibility = Visibility.Visible;
                        window.CancelButtonVisibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        window.OKButtonVisibility = Visibility.Visible;
                        break;
                    }
            }
            #endregion

            #region 默认按钮
            switch (CMessageBoxDefaultButton)
            {
                case CMessageBoxDefaultButton.OK:
                    {
                        window.OKButtonStyle = ButtonStyle.NormalButtonStyle;
                        window.CancelButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.YesButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.NoButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        break;
                    }
                case CMessageBoxDefaultButton.Cancel:
                    {
                        window.OKButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.CancelButtonStyle = ButtonStyle.NormalButtonStyle;
                        window.YesButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.NoButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        break;
                    }
                case CMessageBoxDefaultButton.Yes:
                    {
                        window.OKButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.CancelButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.YesButtonStyle = ButtonStyle.NormalButtonStyle;
                        window.NoButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        break;
                    }
                case CMessageBoxDefaultButton.No:
                    {
                        window.OKButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.CancelButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.YesButtonStyle = ButtonStyle.NotNormalButtonStyle;
                        window.NoButtonStyle = ButtonStyle.NormalButtonStyle;
                        break;
                    }
                case CMessageBoxDefaultButton.None:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            #endregion

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window.ShowDialog();
            }));
            return window.Result;
        }
    }
}
