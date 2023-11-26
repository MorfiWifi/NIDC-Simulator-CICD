using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Config
{
    public partial class SimulationConfig:BaseEntity
    {
        public string Title { set; get; }
        public virtual Folder Folder { set; get; }
        public Guid? FolderId { set; get; }
        public virtual Account Account { set; get; }
        public Guid? AccountId { set; get; }
        public string ConfigDetails { set; get; }
        public virtual ICollection<Simulation> Simulations { set; get; }

    }
}
