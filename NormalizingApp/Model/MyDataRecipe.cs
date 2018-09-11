using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalizingApp.Model
{
    [Serializable]
    public class MyDataRecipe
    {
        public string Name { get; set; }
        public float Lenght1Set { get; set; }
        public float Lenght2Set { get; set; }
        public float X_CoolingPos { get; set; }
        public float X_HomePos { get; set; }
        public float X_Speed { get; set; }
        public float HeatTemp1 { get; set; }
        public float HeatTemp2 { get; set; }
        public float HeatTemp3 { get; set; }
        public float HeatTemp4 { get; set; }
        public float HeatTemp5 { get; set; }
        public float CoolingTemp { get; set; }
        public float HeatPower1 { get; set; }
        public float HeatPower2 { get; set; }
        public float HeatPower3 { get; set; }
        public float HeatPower4 { get; set; }
        public float HeatPower5 { get; set; }

    }
}
