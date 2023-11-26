using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public partial class ConfigDetails
    {
        public ActiveTankModel ActiveTank { get; set; }=new ActiveTankModel();
    }
    public class ActiveTankModel
    {
        public double ActiveTankVolume { set; get; } = 9;
        public double ReserveTankVolume { set; get; } = 1000;
        public double MudLossProporation { set; get; } = 1200;
        public double PedalFlowMeter { set; get; } = 2800;
    }
}
