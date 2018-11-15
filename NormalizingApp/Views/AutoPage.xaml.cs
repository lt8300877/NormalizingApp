using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NormalizingApp.Messagebox;

namespace NormalizingApp.Views
{
    /// <summary>
    /// AutoPage.xaml 的交互逻辑
    /// </summary>
    public partial class AutoPage : UserControl
    {
        private string fileName = @"..\..\Data\Recipe\DataRecipe.xml";//序列化文件路径
        List<Models.MyDataRecipeItem> recipeList = new List<Models.MyDataRecipeItem>(); //定义一个list列表存储配方数据

        public AutoPage()
        {
            InitializeComponent();
        }
        
        
       
        

       
        /// <summary>
        /// 初始化功能，软件启动时选择之前保存的配方记录
        /// </summary>
        private void InitRecipeData()
        {
            RecipeCbx_DropDownOpened(null, null);
            //读取INI配置文件保存的元素
            int temp = int.Parse(Lib.INIFile.InifileReadValue("配方", "元素", Lib.INIFile.iniPath));
            //this.RecipeCbx.SelectedIndex = temp;
            RecipeCbx_DropDownClosed(null, null);
        }

        /// <summary>
        /// 保存一条配方数据
        /// </summary>
        private void SaveRecipeData()
        {
            //if (string.IsNullOrEmpty(this.RecipeCbx.Text) || this.RecipeCbx.Text.Trim() == string.Empty)
            //{
            //    CMessageBox.Show("配方名称不能为空", "提示", CMessageBoxButton.OK, CMessageBoxImage.Warning);
            //    return;
            //}
            //Models.MyDataRecipe rec = new Models.MyDataRecipe();
            //rec.Name = this.RecipeCbx.Text;
            //rec.Lenght1Set = float.Parse( lenght1Set.DataText);
            //rec.Lenght2Set = float.Parse(lenght2Set.DataText);
            //rec.X_CoolingPos = float.Parse(x_CoolingPos.DataText);
            //rec.X_HomePos = float.Parse(x_HomePos.DataText);
            //rec.X_Speed = float.Parse(x_Speed.DataText);
            //rec.HeatTemp1 = float.Parse(heatTemp1.DataText);
            //rec.HeatTemp2 = float.Parse(heatTemp2.DataText);
            //rec.HeatTemp3 = float.Parse(heatTemp3.DataText);
            //rec.HeatTemp4 = float.Parse(heatTemp4.DataText);
            //rec.HeatTemp5 = float.Parse(heatTemp5.DataText); 
            //rec.CoolingTemp = float.Parse(coolingTemp.DataText);
            //rec.HeatPower1 = float.Parse(heatPower1.DataText);
            //rec.HeatPower2 = float.Parse(heatPower2.DataText);
            //rec.HeatPower3 = float.Parse(heatPower3.DataText);
            //rec.HeatPower4 = float.Parse(heatPower4.DataText);
            //rec.HeatPower5 = float.Parse(heatPower5.DataText);

            //var result = from buff in recipeList where !string.IsNullOrEmpty(buff.Name) && buff.Name == rec.Name select buff;
            //if (result == null) return;
            //if (result.ToList().Count == 0)
            //{
            //    recipeList.Add(rec);
            //    Lib.BinaryFile.FileName = fileName;
            //    Lib.BinaryFile.SaveBinary(recipeList.ConvertAll(s => (object)s));
            //    RecipeCbx_DropDownOpened(null, null);
            //    this.RecipeCbx.SelectedIndex = this.RecipeCbx.Items.Count - 1;
            //    RecipeCbx_DropDownClosed(null, null);
            //}
            //else
            //{
            //    NormalizingApp.Models.MyDataRecipe bu = result.ToList()[0];
            //    bu.Lenght1Set = float.Parse(lenght1Set.DataText);
            //    bu.Lenght2Set = float.Parse(lenght2Set.DataText);
            //    bu.X_CoolingPos = float.Parse(x_CoolingPos.DataText);
            //    bu.X_HomePos = float.Parse(x_HomePos.DataText);
            //    bu.X_Speed = float.Parse(x_Speed.DataText);
            //    bu.HeatTemp1 = float.Parse(heatTemp1.DataText);
            //    bu.HeatTemp2 = float.Parse(heatTemp2.DataText);
            //    bu.HeatTemp3 = float.Parse(heatTemp3.DataText);
            //    bu.HeatTemp4 = float.Parse(heatTemp4.DataText);
            //    bu.HeatTemp5 = float.Parse(heatTemp5.DataText);
            //    bu.CoolingTemp = float.Parse(coolingTemp.DataText);
            //    bu.HeatPower1 = float.Parse(heatPower1.DataText);
            //    bu.HeatPower2 = float.Parse(heatPower2.DataText);
            //    bu.HeatPower3 = float.Parse(heatPower3.DataText);
            //    bu.HeatPower4 = float.Parse(heatPower4.DataText);
            //    bu.HeatPower5 = float.Parse(heatPower5.DataText);
            //    Lib.BinaryFile.FileName = fileName;
            //    Lib.BinaryFile.SaveBinary(recipeList.ConvertAll(s => (object)s));
            //    RecipeCbx_DropDownClosed(null, null);
            //}


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
                //recipeList.Remove(recipeList.FirstOrDefault(obj => obj.Name == this.RecipeCbx.SelectedItem.ToString()));
                Lib.BinaryFile.FileName = fileName;
                Lib.BinaryFile.SaveBinary(recipeList.ConvertAll(s => (object)s));
                RecipeCbx_DropDownOpened(null, null);
                RecipeCbx_DropDownClosed(null, null);
            }
        }
        /// <summary>
        /// 装载一条配方数据
        /// </summary>
        private void LoadRecipeData()
        {
            //读取INI配置文件保存的配方记录
            //Lib.INIFile.InifileWriteValue("配方", "元素", this.RecipeCbx.SelectedIndex.ToString(), Lib.INIFile.iniPath);
            //foreach (var data in recipeList)
            //{
            //    if (data.Name == this.RecipeCbx.SelectedItem.ToString())
            //    {
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.6", data.Lenght1Set);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.14", data.Lenght2Set);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.134", data.X_CoolingPos);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.138", data.X_HomePos);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.142", data.X_Speed);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.90", data.HeatTemp1);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.94", data.HeatTemp2);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.98", data.HeatTemp3);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.102", data.HeatTemp4);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.106", data.HeatTemp5);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.110", data.CoolingTemp);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.114", data.HeatPower1);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.118", data.HeatPower2);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.122", data.HeatPower3);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.126", data.HeatPower4);
            //        //Lib.S71KConnect.siemensS7Net.Write("DB6.130", data.HeatPower5);
            //    }
            //}
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
                //index = RecipeCbx.SelectedIndex;
                //RecipeCbx.Items.Clear();
                ////检查目录下是否有序列化文件,若存在就反序列化读取，不存在就创new一个新的list
                //if (File.Exists(fileName))
                //{
                //    //recipeList = new List<MyDataRecipe>();
                //    Lib.BinaryFile.FileName = fileName;
                //    recipeList = Lib.BinaryFile.ReadBinary().ConvertAll(s => (NormalizingApp.Models.MyDataRecipe)s);
                //    foreach (var item in recipeList)
                //    {
                //        this.RecipeCbx.Items.Add(item.Name);
                //    }
                //}
                //if (RecipeCbx.Items.Count > index)
                //    RecipeCbx.SelectedIndex = index;
                //else
                //    RecipeCbx.SelectedIndex = 0;
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
            ////this.RecipelBx.ItemsSource = null;
            //if (recipeList == null || this.RecipeCbx.SelectedItem == null) return;
            //foreach (var data in recipeList)
            //{
            //    if (data.Name == this.RecipeCbx.SelectedItem.ToString())
            //    {
            //        //this.RecipelBx.ItemsSource = new List<UserItem>
            //        //{
            //        //    //new UserItem("Y轴跟踪位置",data.Lenght1Set.ToString()),
            //        //    //new UserItem("Z轴跟踪位置",data.Lenght2Set.ToString()),
            //        //    //new UserItem("X轴冷却距离",data.X_CoolingPos.ToString()),
            //        //    //new UserItem("X轴等料位置",data.X_HomePos.ToString()),
            //        //    //new UserItem("X轴速度",data.X_Speed.ToString()),
            //        //    //new UserItem("段1加热温度",data.HeatTemp1.ToString()),
            //        //    //new UserItem("段2加热温度",data.HeatTemp2.ToString()),
            //        //    //new UserItem("段3加热温度",data.HeatTemp3.ToString()),
            //        //    //new UserItem("段4加热温度",data.HeatTemp4.ToString()),
            //        //    //new UserItem("段5加热温度",data.HeatTemp5.ToString()),
            //        //    //new UserItem("冷却温度",data.CoolingTemp.ToString()),
            //        //    //new UserItem("段1加热功率",data.HeatPower1.ToString()),
            //        //    //new UserItem("段2加热功率",data.HeatPower2.ToString()),
            //        //    //new UserItem("段3加热功率",data.HeatPower3.ToString()),
            //        //    //new UserItem("段4加热功率",data.HeatPower4.ToString()),
            //        //    //new UserItem("段5加热功率",data.HeatPower5.ToString()),
            //        //};
                //}
            //}
        }
    }

    public class UserItem
    {
        public UserItem(string name, string dat)
        {
            Name = name;
            Data = dat;
        }
        public string Name { set; get; }
        public string Data { set; get; }
    }

}
