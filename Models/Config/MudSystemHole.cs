using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{

    public partial class ConfigDetails
    {
        public List<MudSystemHole> Holes { get; set; } = new();
    }
    public partial class MudSystemHole
    {

        public double Interval { get; set; } = 0;
        public double Angle { get; set; } = 0;
        public double FinalAngle { get; set; } = 0;
        public HoleType HoleType { get; set; }
        public string Description { get; set; }

        public double Length { get; set; }
        public double TotalLength { get; set; } = 0;
        public double Depth { get; set; }

    }
    public enum HoleType
    {
        StraightHole=0,
        BuildAngle=1,
        DropAngle=2
    }
}
