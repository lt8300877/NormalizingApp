using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NormalizingApp.Views.Model
{
    /// <summary>
    /// 初始化界面上菜单选项的ListView控件后台数据，把所有窗体的名字和内容传递到ListView进行选择
    /// </summary>
    class MainWindowViewModel
    {
        //定义Listbox绑定的数据源属性
        public MainWindowItem[] DemoItems { get; } 
        //空构造函数
        public MainWindowViewModel() { }
        //构造函数
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));
            
            DemoItems = new[]
            {

                new MainWindowItem("机床信息", new UIControls.HomePage()),
                new MainWindowItem("手动控制", new UIControls.ManualPage()),
                new MainWindowItem("自动控制", new UIControls.AutoPage()),
                new MainWindowItem("实时曲线", new UIControls.CurvePage()),
                new MainWindowItem("历史查询", new UIControls.DataQueryPage()),
                new MainWindowItem("故障报警", new UIControls.AlarmSystemPage()),
                new MainWindowItem("系统设置", new UIControls.SystemSet()),
                new MainWindowItem("主题设置", new UIControls.UserPaletteSelector{ DataContext = new PaletteSelectorViewModel() }),
            }; 
        }
        
    }
}
