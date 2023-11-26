using System.Collections.Generic;
using Domain.Config;

namespace Domain.Identity
{
    public partial class Account
    {
        public virtual ICollection<AccountUnit> AccountUnits  { get; set; }
    }
}