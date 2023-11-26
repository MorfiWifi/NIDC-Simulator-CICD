using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public partial class ConfigDetails
    {
        public CasingLiner CasingLiner { get; set; } = new();
    }
    public class CasingLiner
    {
        public double CasingDepth { get; set; } = new();
        public double CasingId { get; set; } = new();
        public double CasingOd { get; set; } = new();
        public double CasingWeight { get; set; } = new();
        public double CasingCollapsePressure { get; set; } = new();
        public double CasingTensileStrength { get; set; } = new();
        public double LinerTopDepth { get; set; } = new();
        public double LinerLength { get; set; } = new();
        public double LinerId { get; set; } = new();
        public double LinerOd { get; set; } = new();
        public double LinerWeight { get; set; } = new();
        public double LinerCollapsePressure { get; set; } = new();
        public double LinerTensileStrength { get; set; } = new();
        public double OpenHoleId { get; set; } = new();
        public double OpenHoleLength { get; set; } = new();
    }
}
