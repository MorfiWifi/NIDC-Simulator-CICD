using System.Collections.Generic;

namespace Models.Config
{
    public partial class ConfigDetails
    {
        public List<UnitModel> Units { get; set; } = new List<UnitModel>();
    }
}