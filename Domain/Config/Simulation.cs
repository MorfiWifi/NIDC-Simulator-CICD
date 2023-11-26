using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Config
{
    public class Simulation:BaseEntity
    {
        public string Title { set; get; }
        public bool IsRunning { set; get; }
        public virtual SimulationConfig Config { set; get; }
        public Guid ConfigId { set; get; }
        public string ConfigDetails { set; get; }
        public virtual List<SimulationUser> Users { set; get; }
        public string SimulationFieldsJson { set; get; }

    }
    public class SimulationUser:BaseEntity
    {
        public virtual Simulation Simulation { set; get; }
        public Guid? SimulationId { set; get; }
        public virtual Account Account { set; get; }
        public Guid? AccountId { set; get; }
        public string UserRole { set; get; }
        public bool Creator { get; set; }
    }
}
