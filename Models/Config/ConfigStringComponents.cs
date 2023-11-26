using SimulationOutPutValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public partial class ConfigDetails
    {
        public List<StringComponent> StringComponents { get; set; } = new();
        public BitDefenition BitDefenition { set; get; } = new();
        public int BitCode { set; get; }
    }

    public class StringComponent
    {
        public int Component { get; set; }
        public double Length { get; set; }
        public double OD { get; set; }
        public int ID { get; set; }
        public double WeightLength { get; set; }
        public double TotalLength { get; set; }
        public double NumberOfJoint { set; get; }
        public double LengthPerJoint { set; get; }


    }
    public enum Component
    {
        Bit = 0,
        Stabilizer = 1,
        Collar = 2,
        DrillPipe = 3,
        Heavyweight = 4,
    }
    public enum BitType
    {
        Cone = 0,
        PDC = 1,
        Rock = 2
    }
}
