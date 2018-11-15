using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using NormalizingApp.MVVM;
namespace NormalizingApp.ViewModels
{
    public class LoginWindowViewModel :NotifyObject
    {
        LoginWindow loginWindow = null; //实例化一个空的编号设置窗体对象
        public static Models.ProductNumber productNumber = new Models.ProductNumber(); //实例化编号设置的实体类
        private string namePath = @"..\..\Data\Name\name.txt"; //姓名存储文件路径

        //焊缝编号显示
        private string _productNumberFull;
        public string ProductNumberFull
        {
            get { return _productNumberFull; }
            set
            {
                _productNumberFull = value;
                RaisePropertyChanged("ProductNumberFull");
            }
        }

        //操作员名称列表
        private ObservableCollection<string> _operatorName;
        public ObservableCollection<string> OperatorName
        {
            get { return _operatorName; }
            set
            {
                _operatorName = value;
                RaisePropertyChanged("OperatorName");
            }   
        }
        //操作员选择索引
        private int _operatorNameIndex;
        public int OperatorNameIndex
        {
            get { return _operatorNameIndex; }
            set
            {
                _operatorNameIndex = value;
                RaisePropertyChanged("OperatorNameIndex");
            }
        }
        //生产线名称列表
        private ObservableCollection<string> _lineName;
        public ObservableCollection<string> LineName
        {
            get { return _lineName; }
            set
            {
                _lineName = value;
                RaisePropertyChanged("LineName");
            }
        }
        //生产线名称选择索引
        private int _lineNameNameIndex;
        public int LineNameNameIndex
        {
            get { return _lineNameNameIndex; }
            set
            {
                _lineNameNameIndex = value;
                RaisePropertyChanged("LineNameNameIndex");
            }
        }
        //班组名称列表
        private ObservableCollection<string> _groupName;
        public ObservableCollection<string> GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged("GroupName");
            }
        }
        //班组名称选择索引
        private int _groupNameNameIndex;
        public int GroupNameNameIndex
        {
            get { return _groupNameNameIndex; }
            set
            {
                _groupNameNameIndex = value;
                RaisePropertyChanged("GroupNameNameIndex");
            }
        }
        //生产日期选择
        private DateTime _selectDate;
        public DateTime SelectData
        {
            get { return _selectDate; }
            set
            {
                _selectDate = value;
                RaisePropertyChanged("SelectData");
            }
        }
        //接头号填写
        private int _spliceNumber;
        public int SpliceNumber
        {
            get { return _spliceNumber; }
            set
            {
                _spliceNumber = value;
                RaisePropertyChanged("SpliceNumber");
            }
        }
        //操作员下拉框绑定事件(选择改变时)
        private MyCommand _operatorNameComboBoxSelectionChanged;
        public MyCommand OperatorNameComboBoxSelectionChanged
        {
            get
            {
                if (_operatorNameComboBoxSelectionChanged == null)
                    _operatorNameComboBoxSelectionChanged = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                if (OperatorNameIndex < 0) return; //索引小于0就返回
                                productNumber.OperatorName = OperatorName[OperatorNameIndex]; //当前选择值传给实体类
                                Lib.INIFile.InifileWriteValue("设置", "姓名", OperatorNameIndex.ToString(), Lib.INIFile.iniPath);//写入INI
                                ProductNumberFull = GetProductNumber();//更新窗口完整编号显示
                            }));
                return _operatorNameComboBoxSelectionChanged;
            }
        }
        //生产线名称下拉框绑定事件(选择改变时)
        private MyCommand _lineNameNameComboBoxSelectionChanged;
        public MyCommand LineNameNameComboBoxSelectionChanged
        {
            get
            {
                if (_lineNameNameComboBoxSelectionChanged == null)
                    _lineNameNameComboBoxSelectionChanged = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                switch (LineNameNameIndex)
                                {
                                    case 0:
                                        productNumber.LineName = "1";
                                        break;
                                    case 1:
                                        productNumber.LineName = "2";
                                        break;
                                    case 2:
                                        productNumber.LineName = "3";
                                        break;
                                }
                                Lib.INIFile.InifileWriteValue("设置", "生产线名称", LineNameNameIndex.ToString(), Lib.INIFile.iniPath);
                                ProductNumberFull = GetProductNumber();//更新窗口完整编号显示
                            }));
                return _lineNameNameComboBoxSelectionChanged;
            }
        }
        //班组下拉框绑定事件(选择改变时)
        private MyCommand _groupNameComboBoxSelectionChanged;
        public MyCommand GroupNameComboBoxSelectionChanged
        {
            get
            {
                if (_groupNameComboBoxSelectionChanged == null)
                    _groupNameComboBoxSelectionChanged = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                switch (GroupNameNameIndex)
                                {
                                    case 0:
                                        productNumber.GroupName = "S";
                                        break;
                                    case 1:
                                        productNumber.GroupName = "R";
                                        break;
                                    case 2:
                                        productNumber.GroupName = "T";
                                        break;
                                }
                                Lib.INIFile.InifileWriteValue("设置", "班组", GroupNameNameIndex.ToString(), Lib.INIFile.iniPath);
                                ProductNumberFull = GetProductNumber();//更新窗口完整编号显示
                            }));
                return _groupNameComboBoxSelectionChanged;
            }
        }
        //日期选择下拉框绑定事件(选择改变时)
        private MyCommand _datePickerSelectedDateChanged;
        public MyCommand DatePickerSelectedDateChanged
        {
            get
            {
                if (_datePickerSelectedDateChanged == null)
                    _datePickerSelectedDateChanged = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                productNumber.Date = SelectData.Date.ToString("yyMMdd");//焊缝编号日期设置
                                productNumber.AccessDate = SelectData;//access数据库日期设置
                                ProductNumberFull = GetProductNumber();//更新窗口完整编号显示
                            }));
                return _datePickerSelectedDateChanged;
            }
        }
        //编号填写框绑定事件(输入改变时)
        private MyCommand _spliceNumberTextChanged;
        public MyCommand SpliceNumberTextChanged
        {
            get
            {
                if (_spliceNumberTextChanged == null)
                    _spliceNumberTextChanged = new MyCommand(
                        new Action<object>(
                            e =>
                            {
                                productNumber.SpliceNumber = SpliceNumber;
                                Lib.INIFile.InifileWriteValue("设置", "接头号", productNumber.SpliceNumber.ToString(), Lib.INIFile.iniPath);
                                ProductNumberFull = GetProductNumber();//更新窗口完整编号显示
                            }));
                return _spliceNumberTextChanged;
            }
        }
        //确定按钮命令
        private MyCommand _okButtonCommand;
        public MyCommand OkButtonCommand
        {
            get
            {
                if (_okButtonCommand == null)
                    _okButtonCommand = new MyCommand(
                        new Action<object>(
                            o => 
                            {
                                MainWindow mw = new MainWindow();//实例化主窗体
                                mw.Show();//显示主窗体
                                loginWindow.Close();//关闭当前窗体
                            }));//载入新窗体
                return _okButtonCommand;
            }
        }
        //退出按钮命令
        private MyCommand _exitButtonCommand;
        public MyCommand ExitButtonCommand
        {
            get
            {
                if (_exitButtonCommand == null)
                    _exitButtonCommand = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                loginWindow.Close(); //退出窗体
                                Environment.Exit(0);//结束进程
                            }));//退出系统
                return _exitButtonCommand;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="win">传入编号设置窗体对象</param>
        public LoginWindowViewModel(LoginWindow win)
        {
            loginWindow = win;//编号设置窗体对象传进来
            OperatorName = new ObservableCollection<string>();
            LineName = new ObservableCollection<string>();
            GroupName = new ObservableCollection<string>();
            LineName.Add("1");
            LineName.Add("2");
            LineName.Add("3");
            GroupName.Add("S");
            GroupName.Add("R");
            GroupName.Add("T");
            SelectData = DateTime.Now.Date; //日期选择控件设定为当前系统日期
            productNumber.AreaName = "07";
            try
            {
                string[] lines = File.ReadAllLines(namePath);//读取操作员名称集合
                foreach (string line in lines)//将读取的集合添加到list
                {
                    OperatorName.Add(line);
                }
                OperatorNameIndex = int.Parse(Lib.INIFile.InifileReadValue("设置", "姓名", Lib.INIFile.iniPath));
                LineNameNameIndex = int.Parse(Lib.INIFile.InifileReadValue("设置", "生产线名称", Lib.INIFile.iniPath));
                GroupNameNameIndex = int.Parse(Lib.INIFile.InifileReadValue("设置", "班组", Lib.INIFile.iniPath));
                SpliceNumber = int.Parse(Lib.INIFile.InifileReadValue("设置", "接头号", Lib.INIFile.iniPath));
                productNumber.LineName = LineName[LineNameNameIndex];
                productNumber.GroupName = GroupName[GroupNameNameIndex];
                productNumber.Date = SelectData.Date.ToString("yyMMdd");//焊缝编号日期设置
                productNumber.AccessDate = SelectData;//access数据库日期设置
                productNumber.SpliceNumber = SpliceNumber;
                ProductNumberFull = GetProductNumber();//更新窗口完整编号显示
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 获取完整焊缝编号
        /// </summary>
        /// <returns></returns>
        public static string GetProductNumber()
        {
            productNumber.ProductNumberFull = productNumber.AreaName + productNumber.LineName + productNumber.GroupName + productNumber.Date + productNumber.SpliceNumber.ToString();
            return productNumber.ProductNumberFull;
        }
        /// <summary>
        /// 编号自增
        /// </summary>
        public static void PartNumberAuto()
        {
            //编号自加1 并存入硬盘
            productNumber.SpliceNumber++;
            Lib.INIFile.InifileWriteValue("设置", "接头号", productNumber.SpliceNumber.ToString(), Lib.INIFile.iniPath);
        }
    }
}
