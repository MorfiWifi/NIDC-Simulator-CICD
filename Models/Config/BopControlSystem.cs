using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public class BopControlSystem
    {        
        public int DelayToOperate { set; get; } = 20;
        public int NumberOfBottels { set; get; } = 20;
        public int AccumulatorSystemSize { set; get; } = 10;
        public int OilTankVolume { set; get; } = 9;
        public long PrechargePressure { set; get; } = 1000;
        public long AccumulatorMinimumOperatingPressure { set; get; } = 1200;
        public long ElectricPumpOutput { set; get; } = 12;
        public long StartPressure { set; get; } = 2800;
        public long StopPressure { set; get; } = 3000;
        public long AirPlungerPumpOutput { set; get; } = 8;
        public long StartPressure2000Psi { set; get; } = 2600;
        public long StopPressure2000Psi { set; get; } = 2600;


    }
}
