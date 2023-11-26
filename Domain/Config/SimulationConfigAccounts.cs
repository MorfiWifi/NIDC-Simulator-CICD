using System.Collections.Generic;

namespace Domain.Config
{
    public partial class SimulationConfig
    {
        public virtual ICollection<AccountConfig> AccountConfigs { set; get; }
    }
}