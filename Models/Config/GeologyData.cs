using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public class GeologyData
    {
        public int Index { set; get; }
        public string FormationName { set; get; }
        public double FromDepth { set; get; }
        public double ToDepth { set; get; }
        public double UCS { set; get; }
        public double FrictionAngle{ set; get; }
        public double PorePressureGradient { set; get; }
        public double FracturePressureGradient { set; get; }
        public double TempGradient { set; get; }
    }
    public partial class ConfigDetails
    {
        public List<GeologyData> GeologyData { get; set; } = new List<GeologyData>();
    }
}
