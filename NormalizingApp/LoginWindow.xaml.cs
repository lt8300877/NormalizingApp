using System;
using System.Text;
using System.Windows;
using Newtonsoft.Json.Linq;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Windows.Input;


namespace NormalizingApp
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.LoginWindowViewModel(this);
            LoadedThemes();//加载主题
        }
        /// <summary>
        /// 加载主题
        /// </summary>
        private void LoadedThemes()
        {
            //加载原先保存的主题配色
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Palette\Palette.txt"))
            {
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\Palette\Palette.txt", Encoding.UTF8))
                {
                    string temp = sr.ReadToEnd();
                    Palette obj = JObject.Parse(temp).ToObject<Palette>();
                    new PaletteHelper().ReplacePalette(obj);
                }
            }
        }
    }
}
