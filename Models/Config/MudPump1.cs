using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public class MudPump : CementPump
    {

    }
    public class CementPump
    {
        public double VolumetricOutput { set; get; } = 6.5008;
        public double PumpLinerDiameter { set; get; } = 10;
        public double PumpOutputBblStroke { set; get; } = 10;
        public double PumpStroke { set; get; } = 10;
        public double MaximumStroke { set; get; } = 12;
        public double Pressure { set; get; } = 0.9;
        public double VolumetricEfficiency { set; get; } = 80;
        public double MechanicalEfficiency { set; get; } = 1;
        public long PumpRateChange { set; get; } = 4;
        public long DelayToShutdown { set; get; } = 120;
        public long SurfaceLineLenght { set; get; } = 4;
        public long SurfaceLineID { set; get; } = 120;
        public double ReliefValvePressure { set; get; } = 0.9;

    }
}
