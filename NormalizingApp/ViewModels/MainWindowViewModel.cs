using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using NormalizingApp.MVVM;
using NormalizingApp.Models;
using NormalizingApp.Messagebox;
using System.Threading;
namespace NormalizingApp.ViewModels
{
    /// <summary>
    /// 初始化界面上菜单选项的ListView控件后台数据，把所有窗体的名字和内容传递到ListView进行选择
    /// </summary>
    class MainWindowViewModel : NotifyObject
    {
        //定义Listbox绑定的数据源属性
        public Models.MainWindowItem[] DemoItems { get; }
        private Thread uiThread; //开一个线程更新UI绑定属性

        //软件版本号
        private double _versionNumber;
        public double VersionNumber
        {
            get { return _versionNumber; }
            set
            {
                _versionNumber = value;
                RaisePropertyChanged("VersionNumber");
            }
        }
        //通讯连接状态
        private bool _connectionState;
        public bool ConnectionState
        {
            get { return _connectionState; }
            set
            {
                _connectionState = value;
                RaisePropertyChanged("ConnectionState");
            }
        }
        //焊缝编号显示
        private string _productNumber;
        public string ProductNumber
        {
            get { return _productNumber; }
            set
            {
                _productNumber = value;
                RaisePropertyChanged("ProductNumber");
            }
        }
        //系统时间显示
        private string _systempDateTime;
        public string SystempDateTime
        {
            get { return _systempDateTime; }
            set
            {
                _systempDateTime = value;
                RaisePropertyChanged("SystempDateTime");
            }
        }
        //通讯延时显示
        private int _connectionDelay;
        public int ConnectionDelay
        {
            get { return _connectionDelay; }
            set
            {
                _connectionDelay = value;
                RaisePropertyChanged("ConnectionDelay");
            }
        }


        //窗体关闭时执行事件
        private MyCommand _windowClosing;
        public MyCommand WindowClosing
        {
            get
            {
                if (_windowClosing == null)
                    _windowClosing = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                // 保存当前的颜色选择
                                var p = new PaletteHelper().QueryPalette();
                                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Palette\Palette.txt", false, Encoding.UTF8))
                                {
                                    sw.Write(JObject.FromObject(p).ToString());
                                }
                            }));
                return _windowClosing;
            }
        }
        //窗体载入时执行事件
        private MyCommand _windowLoaded;
        public MyCommand WindowLoaded
        {
            get
            {
                if (_windowLoaded == null)
                    _windowLoaded = new MyCommand(
                        new Action<object>(
                            o =>
                            {
                                // 加载原先保存的主题配色
                                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Palette\Palette.txt"))
                                {
                                    using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Palette\Palette.txt", Encoding.UTF8))
                                    {
                                        string temp = sr.ReadToEnd();
                                        MaterialDesignThemes.Wpf.Palette obj = JObject.Parse(temp).ToObject<MaterialDesignThemes.Wpf.Palette>();
                                        new PaletteHelper().ReplacePalette(obj);
                                    }
                                }
                                //new PaletteHelper().SetLightDark(true);//设置系统是否为深色
                            }));
                return _windowLoaded;
            }
        }

        
        //空构造函数
        public MainWindowViewModel() { }
        //构造函数
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            InitData();//初始化数据库和PLC连接
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));
            
            DemoItems = new[]
            {
                new MainWindowItem("机床信息", new Views.HomePage(){ DataContext = new HomePageViewModel()}),
                new MainWindowItem("手动控制", new Views.ManualPage(){ DataContext = new ManualPageViewModel()}),
                new MainWindowItem("自动控制", new Views.AutoPage(){ DataContext = new AutoPageViewModel()}),
                new MainWindowItem("实时曲线", new Views.CurvePage(){ DataContext = new CurvePageViewModel()}),
                new MainWindowItem("历史查询", new Views.DataQueryPage()),
                new MainWindowItem("故障报警", new Views.AlarmSystemPage(){ DataContext = new AlarmSystemPageViewModel()}),
                new MainWindowItem("系统设置", new Views.SystemSet()),
                new MainWindowItem("主题设置", new Views.UserPaletteSelector{ DataContext = new PaletteSelectorViewModel() }),
            };
            
            
        }

        /// <summary>
        /// 初始化数据(数据库和PLC数据)
        /// </summary>
        private void InitData()
        {
            DataBase.DBHelp.FileName = DataBase.DBHelp.fileName;//数据库路径初始化
            //检查数据库文件是否存在，不存在则创建
            if (!DataBase.DBHelp.CreteFilesName())
            {
                CMessageBox.Show("没有数据库模板", "提示");
                Environment.Exit(0);
            }
            cc:
            //连接PLC
            if (Lib.S71KConnect.ConnectPLC())
            {
                Lib.S71KConnect.StartPLCRead();
            }
            else
            {
                Lib.S71KConnect.StopPLCRead();
                goto cc;
            }

            VersionNumber = 1.2;//显示版本号
            ProductNumber = LoginWindowViewModel.productNumber.ProductNumberFull; //显示焊缝编号
            //启动更新线程
            if (Lib.S71KConnect.userItem.ConnectionState)
            {
                uiThread = new Thread(new ThreadStart(UpDataThread));
                uiThread.IsBackground = true;
                uiThread.Start();
            }
        }
        /// <summary>
        /// 线程方法
        /// </summary>
        private void UpDataThread()
        {
            while(true)
            {
                ConnectionDelay = Lib.S71KConnect.userItem.ConnectionDelay;//通讯延时
                ConnectionState = Lib.S71KConnect.userItem.ConnectionState;//通讯状态
                SystempDateTime = Lib.S71KConnect.userItem.SystempDataTime;//系统时间
                ProductNumber = LoginWindowViewModel.productNumber.ProductNumberFull;//焊缝编号
                Thread.Sleep(500);
            }
        }
    }
}
