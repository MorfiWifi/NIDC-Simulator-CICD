using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public class LastShowLeakOff
    {
        [Required(ErrorMessage = "*")]
        public int FormationNo { set; get; }
        public double ShowDepth { set; get; } = 5200;
        public double LeakoffPressure { set; get; } = 0.88;
        public double BreakdownPressure { set; get; } = 0.9;

        public double FractionPrpagationPressure { set; get; } = 0.85;
        public double Permeability { set; get; } = 0.88;
        public bool InactiveFracture { set; get; }


    }
    public partial class ConfigDetails
    {
        public LastShowLeakOff LastShowLeakOffModel { set; get; } = new LastShowLeakOff();
        public LastShowLeakOff LastZoneLeakOffModel { set; get; } = new LastShowLeakOff();

    }
}
