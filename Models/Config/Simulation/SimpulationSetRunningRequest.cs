using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config.Simulation
{
    public class SimpulationSetRunningRequest
    {
        public Guid Id { set; get; }
        public bool IsRuning { set; get; }
        public bool Stop { set; get; }
    }
}
