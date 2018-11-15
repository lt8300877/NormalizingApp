using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormalizingApp.MVVM;
using NormalizingApp.Messagebox;
namespace NormalizingApp.ViewModels
{
    public class TextInputBoxViewModel:NotifyObject
    {
        public static string inputText;
        private string inputTextBox;
        public string InputTextBox
        {
            get { return inputTextBox; }
            set
            {
                inputTextBox = value;
                RaisePropertyChanged("InputTextBox");
            }
        }

        #region 确定按钮事件
        private MyCommand enterButtonClick;
        public MyCommand EnterButtonClick
        {
            get
            {
                if (enterButtonClick == null)
                    enterButtonClick = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                if(InputTextBox=="")
                                {
                                    CMessageBox.Show("输入不能为空!", "提示");
                                    return;
                                }
                                inputText = InputTextBox;
                            }));
                return enterButtonClick;
            }
        }
        #endregion

        #region 取消按钮事件
        private MyCommand cancelButtonClick;
        public MyCommand CancelButtonClick
        {
            get
            {
                if (cancelButtonClick == null)
                    cancelButtonClick = new MyCommand(
                        new Action<object>(
                            e =>
                            {

                            }));
                return cancelButtonClick;
            }
        }
        #endregion

    }
}
