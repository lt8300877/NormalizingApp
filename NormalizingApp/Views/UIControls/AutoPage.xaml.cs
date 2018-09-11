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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NormalizingApp.Views.Messagebox;
namespace NormalizingApp.Views.UIControls
{
    /// <summary>
    /// AutoPage.xaml 的交互逻辑
    /// </summary>
    public partial class AutoPage : UserControl
    {
        private string fileName = @"..\..\Data\Recipe\DataRecipe.xml";//序列化文件路径
        List<NormalizingApp.Model.MyDataRecipe> recipeList = new List<NormalizingApp.Model.MyDataRecipe>(); //定义一个list列表存储配方数据
        private Thread m_thread = null; //定义一个线程
        private bool threadEnabled = true; //定义线程启动条件
        //位置传感器参数
        public Lib.DataDisplay lenght1 = new Lib.DataDisplay();
        public Lib.DataDisplay lenght2 = new Lib.DataDisplay();
        public Lib.DataDisplay lenght1Set = new Lib.DataDisplay();
        public Lib.DataDisplay lenght2Set = new Lib.DataDisplay();
        //轴运动位置参数
        public Lib.DataDisplay x_CoolingPos = new Lib.DataDisplay();
        public Lib.DataDisplay x_HomePos = new Lib.DataDisplay();
        public Lib.DataDisplay x_hfjsjl = new Lib.DataDisplay();
        public Lib.DataDisplay x_jrjl = new Lib.DataDisplay();
        public Lib.DataDisplay x_Speed = new Lib.DataDisplay();
        //加热温度
        public Lib.DataDisplay heatTemp1 = new Lib.DataDisplay();
        public Lib.DataDisplay heatTemp2 = new Lib.DataDisplay();
        public Lib.DataDisplay heatTemp3 = new Lib.DataDisplay();
        public Lib.DataDisplay heatTemp4 = new Lib.DataDisplay();
        public Lib.DataDisplay heatTemp5 = new Lib.DataDisplay();
        //加热功率
        public Lib.DataDisplay heatPower1 = new Lib.DataDisplay();
        public Lib.DataDisplay heatPower2 = new Lib.DataDisplay();
        public Lib.DataDisplay heatPower3 = new Lib.DataDisplay();
        public Lib.DataDisplay heatPower4 = new Lib.DataDisplay();
        public Lib.DataDisplay heatPower5 = new Lib.DataDisplay();
        //间隔时间
        public Lib.DataDisplay intervalTime1 = new Lib.DataDisplay();
        public Lib.DataDisplay intervalTime2 = new Lib.DataDisplay();
        public Lib.DataDisplay intervalTime3 = new Lib.DataDisplay();
        public Lib.DataDisplay intervalTime4 = new Lib.DataDisplay();
        public Lib.DataDisplay intervalTime5 = new Lib.DataDisplay();
        //冷却温度
        public Lib.DataDisplay coolingTemp = new Lib.DataDisplay();



        public AutoPage()
        {
            InitializeComponent();
            DataDisplayInit();
            InitRecipeData();
        }
        /// <summary>
        /// 前台数据绑定初始化
        /// </summary>
        private void DataDisplayInit()
        {
            //位置传感器参数
            TextBlock_actLenght1.DataContext = lenght1;
            TextBlock_actLenght2.DataContext = lenght2;
            TextBox_setLenght1.DataContext = lenght1Set;
            TextBox_setLenght2.DataContext = lenght2Set;
            //轴运动位置参数
            TextBox_XCoolingPos.DataContext = x_CoolingPos;
            TextBox_XHomePos.DataContext = x_HomePos;
            TextBox_Xhfjsjl.DataContext = x_hfjsjl;
            TextBox_Xjrjl.DataContext = x_jrjl;
            TextBox_XSpeedPos.DataContext = x_Speed;
            //加热温度
            TextBox_HeatTemp1.DataContext = heatTemp1;
            TextBox_HeatTemp2.DataContext = heatTemp2;
            TextBox_HeatTemp3.DataContext = heatTemp3;
            TextBox_HeatTemp4.DataContext = heatTemp4;
            TextBox_HeatTemp5.DataContext = heatTemp5;
            //加热功率
            TextBox_HeatPower1.DataContext = heatPower1;
            TextBox_HeatPower2.DataContext = heatPower2;
            TextBox_HeatPower3.DataContext = heatPower3;
            TextBox_HeatPower4.DataContext = heatPower4;
            TextBox_HeatPower5.DataContext = heatPower5;
            //间隔时间
            TextBox_IntervalTime1.DataContext = intervalTime1;
            TextBox_IntervalTime2.DataContext = intervalTime2;
            TextBox_IntervalTime3.DataContext = intervalTime3;
            TextBox_IntervalTime4.DataContext = intervalTime4;
            TextBox_IntervalTime5.DataContext = intervalTime5;
            //冷却温度
            TextBox_CoolingTemp.DataContext = coolingTemp;

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
            while (true)
            {
                while (threadEnabled)
                {
                    //位置传感器参数
                    lenght1.DataText = Lib.S71KConnect.userType.ActLenght1.ToString("F2");
                    lenght2.DataText = Lib.S71KConnect.userType.ActLenght2.ToString("F2");
                    lenght1Set.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.6")).ToString("F2");
                    lenght2Set.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.14")).ToString("F2");
                    //轴运动位置参数
                    x_CoolingPos.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.134")).ToString("F2");
                    x_HomePos.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.138")).ToString("F2");
                    x_hfjsjl.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.146")).ToString("F2");
                    x_jrjl.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.150")).ToString("F2");
                    x_Speed.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.142")).ToString("F2");
                    //加热温度
                    heatTemp1.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.90")).ToString("F2");
                    heatTemp2.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.94")).ToString("F2");
                    heatTemp3.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.98")).ToString("F2");
                    heatTemp4.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.102")).ToString("F2");
                    heatTemp5.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.106")).ToString("F2");
                    //加热温度
                    heatPower1.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.114")).ToString("F2");
                    heatPower2.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.118")).ToString("F2");
                    heatPower3.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.122")).ToString("F2");
                    heatPower4.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.126")).ToString("F2");
                    heatPower5.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.130")).ToString("F2");
                    //间隔时间
                    intervalTime1.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB17.0")).ToString("F2");
                    intervalTime2.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB17.4")).ToString("F2");
                    intervalTime3.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB17.8")).ToString("F2");
                    intervalTime4.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB17.12")).ToString("F2");
                    intervalTime5.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB17.16")).ToString("F2");
                    //冷却温度
                    coolingTemp.DataText = ((float)Lib.S71KConnect.ReadItem(Lib.DataType.Float, "DB6.110")).ToString("F2");
                    Thread.Sleep(500);
                }
            }
        }


        /// <summary>
        /// TEXTBOX按下enter键写入变量，并继续刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tbx = (TextBox)sender;//声明一个TEXTBOX对象
                this.RecipelBx.Focus();//焦点转移一下，让Textbox数据双向绑定事件触发，把当前数据更新到数据源
                switch (tbx.Name)
                {
                    case "TextBox_setLenght1":
                        WritePLCItem("DB6.6", lenght1Set.DataText);//写入变量
                        TextBox_setLenght1.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_setLenght2":
                        WritePLCItem("DB6.14", lenght2Set.DataText);//写入变量
                        this.TextBox_setLenght1.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_XCoolingPos":
                        WritePLCItem("DB6.134", x_CoolingPos.DataText);//写入变量
                        this.TextBox_XCoolingPos.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_XHomePos":
                        WritePLCItem("DB6.138", x_HomePos.DataText);//写入变量
                        this.TextBox_XHomePos.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_Xhfjsjl":
                        WritePLCItem("DB6.146", x_hfjsjl.DataText);//写入变量
                        this.TextBox_Xhfjsjl.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_Xjrjl":
                        WritePLCItem("DB6.150", x_jrjl.DataText);//写入变量
                        this.TextBox_Xjrjl.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_XSpeedPos":
                        WritePLCItem("DB6.142", x_Speed.DataText);//写入变量
                        this.TextBox_XSpeedPos.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatTemp1":
                        WritePLCItem("DB6.90", heatTemp1.DataText);//写入变量
                        this.TextBox_HeatTemp1.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatTemp2":
                        WritePLCItem("DB6.94", heatTemp2.DataText);//写入变量
                        this.TextBox_HeatTemp2.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatTemp3":
                        WritePLCItem("DB6.98", heatTemp3.DataText);//写入变量
                        this.TextBox_HeatTemp3.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatTemp4":
                        WritePLCItem("DB6.102", heatTemp4.DataText);//写入变量
                        this.TextBox_HeatTemp4.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatTemp5":
                        WritePLCItem("DB6.106", heatTemp5.DataText);//写入变量
                        this.TextBox_HeatTemp5.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatPower1":
                        WritePLCItem("DB6.114", heatPower1.DataText);//写入变量
                        this.TextBox_HeatPower1.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatPower2":
                        WritePLCItem("DB6.118", heatPower2.DataText);//写入变量
                        this.TextBox_HeatPower2.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatPower3":
                        WritePLCItem("DB6.122", heatPower3.DataText);//写入变量
                        this.TextBox_HeatPower3.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatPower4":
                        WritePLCItem("DB6.126", heatPower4.DataText);//写入变量
                        this.TextBox_HeatPower4.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_HeatPower5":
                        WritePLCItem("DB6.130", heatPower5.DataText);//写入变量
                        this.TextBox_HeatPower5.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_IntervalTime1":
                        WritePLCItem("DB17.0", intervalTime1.DataText);//写入变量
                        this.TextBox_IntervalTime1.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_IntervalTime2":
                        WritePLCItem("DB17.4", intervalTime2.DataText);//写入变量
                        this.TextBox_IntervalTime2.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_IntervalTime3":
                        WritePLCItem("DB17.8", intervalTime3.DataText);//写入变量
                        this.TextBox_IntervalTime3.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_IntervalTime4":
                        WritePLCItem("DB17.12", intervalTime4.DataText);//写入变量
                        this.TextBox_IntervalTime4.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_IntervalTime5":
                        WritePLCItem("DB17.16", intervalTime5.DataText);//写入变量
                        this.TextBox_IntervalTime5.Focus(); //再次获取本控件焦点
                        break;
                    case "TextBox_CoolingTemp":  
                        WritePLCItem("DB6.110", coolingTemp.DataText);//写入变量
                        this.TextBox_CoolingTemp.Focus(); //再次获取本控件焦点
                        break;
                }
                threadEnabled = true;//启动刷新
            }
        }
        /// <summary>
        /// 获取焦点停止刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            threadEnabled = false;
        }
        /// <summary>
        /// 离开焦点继续刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            threadEnabled = true;//启动刷新
        }
        /// <summary>
        /// string转换写入PLC变量方法
        /// </summary>
        /// <param name="address">PLC变量地址</param>
        /// <param name="source">数据源</param>
        /// <param name="temp">缓存</param>
        private void WritePLCItem(string address, string source)
        {
            if (Single.TryParse(source, out float temp))//判断Textbox当前值能否转换为float类型并输出
                Lib.S71KConnect.siemensS7Net.Write(address, temp); //写入PLC变量
            else
                MessageBox.Show("输入错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Content)
            {
                case "保存":
                    SaveRecipeData();
                    break;
                case "装载":
                    LoadRecipeData();
                    break;
                case "删除":
                    DeleteRecipeData();
                    break;
            }
        }
        /// <summary>
        /// 初始化功能，软件启动时选择之前保存的配方记录
        /// </summary>
        private void InitRecipeData()
        {
            RecipeCbx_DropDownOpened(null, null);
            //读取INI配置文件保存的元素
            int temp = int.Parse(Lib.CofigIni.InifileReadValue("配方", "元素", Lib.CofigIni.iniPath));
            this.RecipeCbx.SelectedIndex = temp;
            RecipeCbx_DropDownClosed(null, null);
        }

        /// <summary>
        /// 保存一条配方数据
        /// </summary>
        private void SaveRecipeData()
        {
            if (string.IsNullOrEmpty(this.RecipeCbx.Text) || this.RecipeCbx.Text.Trim() == string.Empty)
            {
                CMessageBox.Show("配方名称不能为空", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
                return;
            }
            NormalizingApp.Model.MyDataRecipe rec = new NormalizingApp.Model.MyDataRecipe();
            rec.Name = this.RecipeCbx.Text;
            rec.Lenght1Set = float.Parse( lenght1Set.DataText);
            rec.Lenght2Set = float.Parse(lenght2Set.DataText);
            rec.X_CoolingPos = float.Parse(x_CoolingPos.DataText);
            rec.X_HomePos = float.Parse(x_HomePos.DataText);
            rec.X_Speed = float.Parse(x_Speed.DataText);
            rec.HeatTemp1 = float.Parse(heatTemp1.DataText);
            rec.HeatTemp2 = float.Parse(heatTemp2.DataText);
            rec.HeatTemp3 = float.Parse(heatTemp3.DataText);
            rec.HeatTemp4 = float.Parse(heatTemp4.DataText);
            rec.HeatTemp5 = float.Parse(heatTemp5.DataText); 
            rec.CoolingTemp = float.Parse(coolingTemp.DataText);
            rec.HeatPower1 = float.Parse(heatPower1.DataText);
            rec.HeatPower2 = float.Parse(heatPower2.DataText);
            rec.HeatPower3 = float.Parse(heatPower3.DataText);
            rec.HeatPower4 = float.Parse(heatPower4.DataText);
            rec.HeatPower5 = float.Parse(heatPower5.DataText);

            var result = from buff in recipeList where !string.IsNullOrEmpty(buff.Name) && buff.Name == rec.Name select buff;
            if (result == null) return;
            if (result.ToList().Count == 0)
            {
                recipeList.Add(rec);
                Lib.FileBuff.FileName = fileName;
                Lib.FileBuff.SaveBinary(recipeList.ConvertAll(s => (object)s));
                RecipeCbx_DropDownOpened(null, null);
                this.RecipeCbx.SelectedIndex = this.RecipeCbx.Items.Count - 1;
                RecipeCbx_DropDownClosed(null, null);
            }
            else
            {
                NormalizingApp.Model.MyDataRecipe bu = result.ToList()[0];
                bu.Lenght1Set = float.Parse(lenght1Set.DataText);
                bu.Lenght2Set = float.Parse(lenght2Set.DataText);
                bu.X_CoolingPos = float.Parse(x_CoolingPos.DataText);
                bu.X_HomePos = float.Parse(x_HomePos.DataText);
                bu.X_Speed = float.Parse(x_Speed.DataText);
                bu.HeatTemp1 = float.Parse(heatTemp1.DataText);
                bu.HeatTemp2 = float.Parse(heatTemp2.DataText);
                bu.HeatTemp3 = float.Parse(heatTemp3.DataText);
                bu.HeatTemp4 = float.Parse(heatTemp4.DataText);
                bu.HeatTemp5 = float.Parse(heatTemp5.DataText);
                bu.CoolingTemp = float.Parse(coolingTemp.DataText);
                bu.HeatPower1 = float.Parse(heatPower1.DataText);
                bu.HeatPower2 = float.Parse(heatPower2.DataText);
                bu.HeatPower3 = float.Parse(heatPower3.DataText);
                bu.HeatPower4 = float.Parse(heatPower4.DataText);
                bu.HeatPower5 = float.Parse(heatPower5.DataText);
                Lib.FileBuff.FileName = fileName;
                Lib.FileBuff.SaveBinary(recipeList.ConvertAll(s => (object)s));
                RecipeCbx_DropDownClosed(null, null);
            }
            
            
        }

        /// <summary>
        /// 删除一条配方数据
        /// </summary>
        private void DeleteRecipeData()
        {
            if (recipeList.Count == 0) return;
            CMessageBoxResult dr = CMessageBox.Show("你确定删除吗？", "提示", CMessageBoxButton.YesNO, CMessageBoxImage.Warning);
            if (dr == CMessageBoxResult.Yes)
            {
                recipeList.Remove(recipeList.FirstOrDefault(obj => obj.Name == this.RecipeCbx.SelectedItem.ToString()));
                Lib.FileBuff.FileName = fileName;
                Lib.FileBuff.SaveBinary(recipeList.ConvertAll(s => (object)s));
                RecipeCbx_DropDownOpened(null, null);
                RecipeCbx_DropDownClosed(null, null);
            }
        }
        /// <summary>
        /// 装载一条配方数据
        /// </summary>
        private void LoadRecipeData()
        {
            threadEnabled = false;
            //读取INI配置文件保存的配方记录
            Lib.CofigIni.InifileWriteValue("配方", "元素", this.RecipeCbx.SelectedIndex.ToString(), Lib.CofigIni.iniPath);
            foreach (var data in recipeList)
            {
                if (data.Name == this.RecipeCbx.SelectedItem.ToString())
                {
                    Lib.S71KConnect.siemensS7Net.Write("DB6.6", data.Lenght1Set);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.14", data.Lenght2Set);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.134", data.X_CoolingPos);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.138", data.X_HomePos);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.142", data.X_Speed);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.90", data.HeatTemp1);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.94", data.HeatTemp2);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.98", data.HeatTemp3);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.102", data.HeatTemp4);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.106", data.HeatTemp5);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.110", data.CoolingTemp);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.114", data.HeatPower1);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.118", data.HeatPower2);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.122", data.HeatPower3);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.126", data.HeatPower4);
                    Lib.S71KConnect.siemensS7Net.Write("DB6.130", data.HeatPower5);
                }
            }
            threadEnabled = true;
        }
        /// <summary>
        /// 列表下拉框出现时事件，更新combox控件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecipeCbx_DropDownOpened(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                index = RecipeCbx.SelectedIndex;
                RecipeCbx.Items.Clear();
                //检查目录下是否有序列化文件,若存在就反序列化读取，不存在就创new一个新的list
                if (File.Exists(fileName))
                {
                    //recipeList = new List<MyDataRecipe>();
                    Lib.FileBuff.FileName = fileName;
                    recipeList = Lib.FileBuff.ReadBinary().ConvertAll(s => (NormalizingApp.Model.MyDataRecipe)s);
                    foreach (var item in recipeList)
                    {
                        this.RecipeCbx.Items.Add(item.Name);
                    }
                }
                if (RecipeCbx.Items.Count > index)
                    RecipeCbx.SelectedIndex = index;
                else
                    RecipeCbx.SelectedIndex = 0;
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 下拉列表关闭时事件，更新listbox内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecipeCbx_DropDownClosed(object sender, EventArgs e)
        {
            this.RecipelBx.ItemsSource = null;
            if (recipeList == null || this.RecipeCbx.SelectedItem == null) return;
            foreach (var data in recipeList)
            {
                if (data.Name == this.RecipeCbx.SelectedItem.ToString())
                {
                    this.RecipelBx.ItemsSource = new List<UserItem>
                    {
                        new UserItem("Y轴跟踪位置",data.Lenght1Set.ToString()),
                        new UserItem("Z轴跟踪位置",data.Lenght2Set.ToString()),
                        new UserItem("X轴冷却距离",data.X_CoolingPos.ToString()),
                        new UserItem("X轴等料位置",data.X_HomePos.ToString()),
                        new UserItem("X轴速度",data.X_Speed.ToString()),
                        new UserItem("段1加热温度",data.HeatTemp1.ToString()),
                        new UserItem("段2加热温度",data.HeatTemp2.ToString()),
                        new UserItem("段3加热温度",data.HeatTemp3.ToString()),
                        new UserItem("段4加热温度",data.HeatTemp4.ToString()),
                        new UserItem("段5加热温度",data.HeatTemp5.ToString()),
                        new UserItem("冷却温度",data.CoolingTemp.ToString()),
                        new UserItem("段1加热功率",data.HeatPower1.ToString()),
                        new UserItem("段2加热功率",data.HeatPower2.ToString()),
                        new UserItem("段3加热功率",data.HeatPower3.ToString()),
                        new UserItem("段4加热功率",data.HeatPower4.ToString()),
                        new UserItem("段5加热功率",data.HeatPower5.ToString()),
                    };
                }
            }
        }

        
    }

    public class UserItem
    {
        public UserItem(string name, string dat)
        {
            this.Name = name;
            this.Data = dat;
        }
        public string Name { set; get; }
        public string Data { set; get; }
    }

}
