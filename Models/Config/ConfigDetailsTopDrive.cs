using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public partial class ConfigDetails
    {
        public TopDrive TopDriveModel { set; get; } = new();
    }
    public partial class TopDrive
    {
        public double TopDriveWeight { set; get; } = 1000;
        public long RotationAcceleration { set; get; } = 1200;
        public long RotaryTableRotationAcceletation { set; get; } = 12;
    }

}
