using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Config
{
    public class Folder : BaseEntity
    {
        public string Title{set;get;}
        public bool IsPublic { set; get; } = false;
        public virtual Account Account { set; get; }
        public Guid AccountId { set; get; }
    }
}
