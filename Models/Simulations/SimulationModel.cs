using Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{

    public class SimulationModel : BaseModel
    {        
        public string Title { set; get; }
        public bool IsRunning { set; get; }        
        public Guid ConfigId { set; get; }
        public string SimulationFieldsJson { set; get; }
        public SimulationFeilds SimulationFeilds { set; get; } = new();
        public double AnimationRate { set; get; }
    }
    public class SimulationModelWithoutJson : BaseModel
    {
        public string Title { set; get; }
        public bool IsRunning { set; get; }
        public Guid ConfigId { set; get; }
        public SimulationFeilds SimulationFeilds { set; get; } = new();
        public double AnimationRate { set; get; }
    }
    public class SimulationUserModel : BaseModel
    {
        public Guid SimulationId { set; get; }
        public virtual AccountModel Account { set; get; }
        public Guid AccountId { set; get; }
        public string UserRole { set; get; }

    }
}

