using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public partial class ConfigDetails
    {
        public ActiveMudSystem ActiveMudSystem { get; set; } = new();
    }
    public class ActiveMudSystem
    {
        public int MudType1 { get; set; }
        public float Density1 { get; set; }
        public double ActiveMudVolume1 { get; set; }
        public double SettledContents1 { get; set; }
        public double ActiveTotalTankCapacity { set; get; }
        public double InitialTripTankMudVolume { set; get; }
        public double PlasticViscosity1 { get; set; }
        public double YieldPoint1 { get; set; }
        public int MudType2 { get; set; }

        public float Density2 { get; set; }
        public double ActiveMudVolume2 { get; set; }
        public double SettledContents2 { get; set; }
        public double PlasticViscosity2 { get; set; }
        public double YieldPoint2 { get; set; }
        public double ReserveMudVolume { set; get; }
        public int RheologyType { get; set; }

    }
    public enum MudType
    {
        WaterBase = 0,
        OilBase = 1,
    }
    public enum RheologyType
    {
        PowerLaw=0,
        Bingham_Plastic=1,
        Newtonian=2
    }
}
