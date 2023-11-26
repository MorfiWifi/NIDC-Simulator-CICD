using Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseData
{
    public class OrganizationAdminModel : BaseModel
    {
        public OrganizationModel Organization { set; get; }
        public Guid OrganizationId { set; get; }
        public virtual AccountModel Account { set; get; }
        public Guid AccountId { set; get; }
    }
}
