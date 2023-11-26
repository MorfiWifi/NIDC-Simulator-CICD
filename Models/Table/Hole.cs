using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Table
{
    public  class Hole: BaseModel
    {
        public int Interval { get; set; }
        public string HoleType { get; set; }
        public string Description   { get; set; }
        public int FinalAngel { get; set; }
        public int TotalLength { get; set; }
        public int MeasuedDepth { get; set; }
    }
}
