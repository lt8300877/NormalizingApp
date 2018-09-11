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

namespace NormalizingApp.Views.UIControls
{
    /// <summary>
    /// ManualPage.xaml 的交互逻辑
    /// </summary>
    public partial class ManualPage : UserControl
    {
        private Thread m_thread = null;
        private bool threadEnabled = true;
        //X轴数据
        public Lib.DataDisplay xActPos = new Lib.DataDisplay();
        public Lib.DataDisplay xActSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay xSetPos = new Lib.DataDisplay();
        public Lib.DataDisplay xSetSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay xActCurrent = new Lib.DataDisplay();
        //Y轴数据
        public Lib.DataDisplay yActPos = new Lib.DataDisplay();
        public Lib.DataDisplay yActSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay ySetPos = new Lib.DataDisplay();
        public Lib.DataDisplay ySetSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay yActCurrent = new Lib.DataDisplay();
        //Z轴数据
        public Lib.DataDisplay zActPos = new Lib.DataDisplay();
        public Lib.DataDisplay zActSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay zSetPos = new Lib.DataDisplay();
        public Lib.DataDisplay zSetSpeed = new Lib.DataDisplay();
        public Lib.DataDisplay zActCurrent = new Lib.DataDisplay();
        //轴倍率设置
        public Lib.DataDisplay xOverSet = new Lib.DataDisplay();
        public Lib.DataDisplay yOverSet = new Lib.DataDisplay();
        public Lib.DataDisplay zOverSet = new Lib.DataDisplay();
        //功率设置
        public Lib.DataDisplay powerSet = new Lib.DataDisplay();
        //滑块中转变量
        private double m_XOverSetSld, m_YOverSetSld, m_ZOverSetSld, m_PowerSetSld;

        public delegate void UpdateLampdelegate();

        public ManualPage()
        {
            InitializeComponent();
            InitDisplayData();
        }

        /// <summary>
        /// 参数初始化
        /// </summary>
        public void InitDisplayData()
        {
            this.a.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.b.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.c.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.d.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.e.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.f.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.g.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.h.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.i.AddHandler(Button.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);//添加鼠标按下路由事件
            this.a.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            this.b.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            this.c.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            this.d.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            this.e.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            this.f.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            this.g.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            this.h.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            this.i.AddHandler(Button.MouseUpEvent, new MouseButtonEventHandler(Button_MouseUp), true);//添加鼠标弹起路由事件
            //X轴数据
            this.xActPosTbk.DataContext = xActPos;
            this.xActSpeedTbk.DataContext = xActSpeed;
            this.xSetPosTbk.DataContext = xSetPos;
            this.xSetSpeedTbk.DataContext = xSetSpeed;
            this.xActCurrentTbk.DataContext = xActCurrent;
            //Y轴数据
            this.yActPosTbk.DataContext = yActPos;
            this.yActSpeedTbk.DataContext = yActSpeed;
            this.ySetPosTbk.DataContext = ySetPos;
            this.ySetSpeedTbk.DataContext = ySetSpeed;
            this.yActCurrentTbk.DataContext = yActCurrent;
            //Z轴数据
            this.zActPosTbk.DataContext = zActPos;
            this.zActSpeedTbk.DataContext = zActSpeed;
            this.zSetPosTbk.DataContext = zSetPos;
            this.zSetSpeedTbk.DataContext = zSetSpeed;
            this.zActCurrentTbk.DataContext = zActCurrent;
            //功率数据
            this.xOverSetTxb.DataContext = xOverSet;
            this.yOverSetTxb.DataContext = yOverSet;
            this.zOverSetTxb.DataContext = zOverSet;
            this.powerSetTxb.DataContext = powerSet;

            //倍率值读取
            xOverSetSld.Value = Convert.ToDouble(Lib.S71KConnect.ReadItem(Lib.DataType.Int32, "DB1.218"));
            yOverSetSld.Value = Convert.ToDouble(Lib.S71KConnect.ReadItem(Lib.DataType.Int32, "DB1.222"));
            zOverSetSld.Value = Convert.ToDouble(Lib.S71KConnect.ReadItem(Lib.DataType.Int32, "DB1.226"));
            powerSetSld.Value = Convert.ToDouble(Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB7.60"));

            //初始化线程
            m_thread = new Thread(ThreadFunc)
            {
                IsBackground = true
            };
            m_thread.Start();


            
        }
        /// <summary>
        /// 后台刷新界面数据线程
        /// </summary>
        private void ThreadFunc()
        {
            UpdateLampdelegate upLampDelegate = new UpdateLampdelegate(Update);
            while (true)
            {
                while (threadEnabled)
                {
                    //X轴数据更新
                    xActPos.DataText = Lib.S71KConnect.userType.XActPos.ToString("f2");
                    xActSpeed.DataText = Lib.S71KConnect.userType.XActSpeed.ToString("f2");
                    xSetPos.DataText = Lib.S71KConnect.userType.XSetPos.ToString("f2");
                    xSetSpeed.DataText = Lib.S71KConnect.userType.XSetSpeed.ToString("f2");
                    xActCurrent.DataText = Lib.S71KConnect.userType.XActCurrent.ToString("f2");
                    //Y轴数据更新
                    yActPos.DataText = Lib.S71KConnect.userType.YActPos.ToString("f2");
                    yActSpeed.DataText = Lib.S71KConnect.userType.YActSpeed.ToString("f2");
                    ySetPos.DataText = Lib.S71KConnect.userType.YSetPos.ToString("f2");
                    ySetSpeed.DataText = Lib.S71KConnect.userType.YSetSpeed.ToString("f2");
                    yActCurrent.DataText = Lib.S71KConnect.userType.YActCurrent.ToString("f2");
                    //Z轴数据更新
                    zActPos.DataText = Lib.S71KConnect.userType.ZActPos.ToString("f2");
                    zActSpeed.DataText = Lib.S71KConnect.userType.ZActSpeed.ToString("f2");
                    zSetPos.DataText = Lib.S71KConnect.userType.ZSetPos.ToString("f2");
                    zSetSpeed.DataText = Lib.S71KConnect.userType.ZSetSpeed.ToString("f2");
                    zActCurrent.DataText = Lib.S71KConnect.userType.ZActCurrent.ToString("f2");
                    //滑块数据写入
                    Lib.S71KConnect.siemensS7Net.Write("DB1.218", Convert.ToInt32(m_XOverSetSld)); //写入PLC变量
                    Lib.S71KConnect.siemensS7Net.Write("DB1.222", Convert.ToInt32(m_YOverSetSld)); //写入PLC变量
                    Lib.S71KConnect.siemensS7Net.Write("DB1.226", Convert.ToInt32(m_ZOverSetSld)); //写入PLC变量
                    Lib.S71KConnect.siemensS7Net.Write("DB7.60", Convert.ToSingle(m_PowerSetSld)); //写入PLC变量
                     //指示灯委托方法
                    a.Dispatcher.Invoke(upLampDelegate);
                    Thread.Sleep(200);
                }
            }
        }
        //刷新按钮指示状态
        private void Update()
        {
            if (Lib.S71KConnect.userType.JLG_1_SW)
                a.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                a.Background = new SolidColorBrush(Colors.Red);

            if (Lib.S71KConnect.userType.JLG_2_SW)
                b.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                b.Background = new SolidColorBrush(Colors.Red);

            if (Lib.S71KConnect.userType.JLG_3_SW)
                c.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                c.Background = new SolidColorBrush(Colors.Red);

            if (Lib.S71KConnect.userType.JLG_4_SW)
                d.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                d.Background = new SolidColorBrush(Colors.Red);



            if (Lib.S71KConnect.userType.Y_CDJ_QGQW)
                e.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                e.Background = new SolidColorBrush(Colors.Red);

            if (Lib.S71KConnect.userType.Z_CDJ_QGQW)
                f.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                f.Background = new SolidColorBrush(Colors.Red);

            if (Lib.S71KConnect.userType.PQ_ZT)
                g.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                g.Background = new SolidColorBrush(Colors.Red);

            if (Lib.S71KConnect.userType.DY_ZT)
                h.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                h.Background = new SolidColorBrush(Colors.Red);

            if (!Lib.S71KConnect.userType.DY_ZT)
                i.Background = new SolidColorBrush(Colors.LimeGreen);
            else
                i.Background = new SolidColorBrush(Colors.Red);
        }
        /// <summary>
        /// 控制按钮的按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!MainWindow.PLCConnectState) return; //PLC未连接时不进行写操作
            Button btn = (Button)sender;
            switch (btn.Content)
            {
                case "1#气缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.56.0", true); //写入PLC变量
                    break;
                case "2#气缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.56.2", true); //写入PLC变量
                    break;
                case "3#气缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.56.4", true); //写入PLC变量
                    break;
                case "4#气缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.56.6", true); //写入PLC变量
                    break;
                case "Y轴位移缸进退":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.54.0", true); //写入PLC变量
                    break;
                case "Z轴位移缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.54.2", true); //写入PLC变量
                    break;
                case "喷气控制":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.52.0", true); //写入PLC变量
                    break;
                case "加热启动":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.50.0", true); //写入PLC变量
                    break;
                case "加热停止":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.50.1", true); //写入PLC变量
                    break;
            }
        }
        /// <summary>
        /// 控制按钮的弹起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!MainWindow.PLCConnectState) return; //PLC未连接时不进行写操作
            Button btn = (Button)sender;
            switch (btn.Content)
            {
                case "1#气缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.56.0", false); //写入PLC变量
                    break;
                case "2#气缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.56.2", false); //写入PLC变量
                    break;
                case "3#气缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.56.4", false); //写入PLC变量
                    break;
                case "4#气缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.56.6", false); //写入PLC变量
                    break;
                case "Y轴位移缸进退":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.54.0", false); //写入PLC变量
                    break;
                case "Z轴位移缸升降":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.54.2", false); //写入PLC变量
                    break;
                case "喷气控制":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.52.0", false); //写入PLC变量
                    break;
                case "加热启动":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.50.0", false); //写入PLC变量
                    break;
                case "加热停止":
                    Lib.S71KConnect.siemensS7Net.Write("DB7.50.1", false); //写入PLC变量
                    break;
            }
        }


        

        /// <summary>
        /// Slider值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sld_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!MainWindow.PLCConnectState) return; //PLC未连接时不进行写操作
            Slider sld = (Slider)sender;
            switch (sld.Name)
            {
                case "xOverSetSld":
                    m_XOverSetSld = sld.Value;
                    //Lib.S71KConnect.siemensS7Net.Write("DB1.218", Convert.ToInt32(sld.Value)); //写入PLC变量
                    break;
                case "yOverSetSld":
                    m_YOverSetSld = sld.Value;
                    //Lib.S71KConnect.siemensS7Net.Write("DB1.222", Convert.ToInt32(sld.Value)); //写入PLC变量
                    break;
                case "zOverSetSld":
                    m_ZOverSetSld = sld.Value;
                    //Lib.S71KConnect.siemensS7Net.Write("DB1.226", Convert.ToInt32(sld.Value)); //写入PLC变量
                    break;
                case "powerSetSld":
                    m_PowerSetSld = sld.Value;
                    //Lib.S71KConnect.siemensS7Net.Write("DB3.268", Convert.ToSingle(sld.Value)); //写入PLC变量
                    break;
            }
        }
    }
}
