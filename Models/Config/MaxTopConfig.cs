using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public class MaxTopConfig
    {
        public double MaxTopOfStringAccelration { set; get; } = 9;
        public double TravlingBlockWeight { set; get; } = 1000;
        public double KellyAndSwivleWeight { set; get; } = 1200;
        public int NumberOfLine { set; get; } = 12;
        public double DrillingLineBreakingLoad { set; get; } = 2800;
    }
    public partial class ConfigDetails
    {
        public MaxTopConfig maxTopConfig { get; set; } = new MaxTopConfig();
    }
}
