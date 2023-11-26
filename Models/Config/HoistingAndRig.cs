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
        public Hoisting Hoisting { get; set; } = new();
        public RigSize RigSize { get; set; } = new();
    }
   
}
