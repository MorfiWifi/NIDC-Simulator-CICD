using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseData
{
    public  class OrganizationAdmin:BaseEntity
    {
        public virtual Organization Organization { set; get; }
        public Guid OrganizationId { set; get; }
        public virtual Account Account { set; get; }
        public Guid AccountId { set; get; }
    }
}
