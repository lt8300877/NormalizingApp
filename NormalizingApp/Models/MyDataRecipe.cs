using System;
using System.Collections.Generic;
using NormalizingApp.MVVM;
namespace NormalizingApp.Models
{
    /// <summary>
    /// 配方数据类型绑定
    /// </summary>
    public enum RecipeDataType
    {
        MyBool = 1,
        MyFloat = 2,
        MyInt = 3,

    }


    /// <summary>
    /// 配方条目数据绑定
    /// </summary>
    [Serializable]
    public class MyDataRecipeItem
    {
        public string ValueName { get; set; }
        public string Address { get; set; }
        public object Data { get; set; }
        public RecipeDataType DataType { get; set; }

        public MyDataRecipeItem(string _valueName,string _address, object _data, RecipeDataType _type )
        {
            ValueName = _valueName;
            Address = _address;
            Data = _data;
            DataType = _type;
        }
    }


    /// <summary>
    /// 配方集合数据绑定
    /// </summary>
    [Serializable]
    public class MyDataRecipe
    {
        public string ListName { get; set; }
        public List<MyDataRecipeItem> MyDataRecipeList { get; set; }

    }



    /// <summary>
    /// 配方视图显示数据绑定
    /// </summary>
    public class RecipeDataDisp : NotifyObject
    {
        public RecipeDataDisp(int _namber, string _name, string _dat)
        {
            number = _namber;
            name = _name;
            data = _dat;
        }
        

        public int number;
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                RaisePropertyChanged("Number");
            }
        }

        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        private string data;
        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                RaisePropertyChanged("Data");
            }
        }
    }
}
