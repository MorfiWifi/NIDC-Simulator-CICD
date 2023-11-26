using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public  class GenratorsConfig
    {
        public int NumberOfGenerators { set; get; } = 4;
        public int GeneratorPowerRating { set; get; } = 1200;
        public int MudPump1 { set; get; } = 1600;
        public int MudPump2 { set; get; } = 1600;
        public int CementPump { set; get; } = 1600;

        public int RotaryTable { set; get; } = 800;
        public int Drawworks { set; get; } = 2000;
        public int TopDrive { set; get; } = 1000;

    }
    public partial class ConfigDetails
    {
        public GenratorsConfig GenratorsConfig { set; get; }= new GenratorsConfig();
    }
}
