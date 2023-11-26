using SimulationOutPutValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public class FormationData
    {
     
        public int Index { set; get; }
        public string FormationName { set; get; }
        public double FormationTop { set; get; }
        public double FormationThickness { set; get; }
        public double Drillability { set; get; }
        public double Abrasiveness { set; get; }
        public double ThresholdWeight { set; get; }
        public double PorePressureGradiant { set; get; }
        public bool Reservoir { set; get; }
        public bool Shoe { set; get; }

    }
    public partial class ConfigDetails
    {
        public List<FormationData> Formations { get; set; }=new List<FormationData>();
        public Reservoir Reservoir { get; set; } = new ();

    }
}
