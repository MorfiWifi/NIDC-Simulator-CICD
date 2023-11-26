using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public class BOP: BaseModel
    {
        
        public double RamStringDrag { get; set; }
        public double AnnularStringDrag { get; set; }
        public OpenCloseValue AnnularPreventer { get; set; } = new();
        public OpenCloseValue UpperRams { get; set; } = new();
        public OpenCloseValue BlindShearRams { get; set; } = new();
        public OpenCloseValue KillChoke { get; set; } = new();
        public OpenCloseValue LowerRams { get; set; } = new();
    }

    public class OpenCloseValue
    {
        public double Open { get; set; }
        public double Close { get; set; }
    }
}
