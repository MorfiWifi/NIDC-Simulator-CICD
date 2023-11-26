using System.Collections.Generic;

namespace Domain.Config
{
    public partial class Unit : BaseEntity
    {
        public bool IsDefault { get; set; }
        public string Category { get; set; } // Pressure , Length ... = (UnitCategory.ToString())
        public string Name { get; set; } // custom name (SI , US , EN , IR )
        public double Coefficient { get; set; } = 1; // si system has no ...
        public string System { get; set; }
        public string Template { set; get; }


        public virtual  ICollection<AccountUnit> AccountUnits { get; set; }
    }
}