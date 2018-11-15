using System;
using System.Collections.Generic;

namespace NormalizingApp.Lib
{
    public class MyValueEvent<T>
    {
        private T myValue;

        public T MyValue
        {
            get { return myValue;}
            set
            {
                if (Compare(value, myValue) == false)
                {
                    myValue = value;
                    WhenMyValueChange();
                }
                    
                myValue = value;
            }
        }

        private bool Compare(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        //定义的委托
        public delegate void MyValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyValueChanged OnMyValueChanged;

        public MyValueEvent()
        {
            myValue = default(T);
            OnMyValueChanged += new MyValueChanged(AfterMyValueChanged);
        }

        private void AfterMyValueChanged(object sender, EventArgs e)
        {
            //do something
        }

        //事件触发函数
        public void WhenMyValueChange()
        {
            OnMyValueChanged?.Invoke(this, null);
        }
    }
}
