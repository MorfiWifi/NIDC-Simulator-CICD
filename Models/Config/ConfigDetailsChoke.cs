using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public partial  class ConfigDetails
    {
        public Chocke ChockeModel { set; get; } = new();
    }
    public partial class Chocke
    {
        public double ChockeDiameter { set; get; } = 9;
        public long ChangeRate { set; get; } = 1000;
        public long ChokeConstant { set; get; } = 1200;
        public long ChokeLineLength { set; get; } = 12;
        public long ChokeLineId { set; get; } = 2800;
        public long ChokeLineVolume { set; get; } = 3000;
    }
        
}
