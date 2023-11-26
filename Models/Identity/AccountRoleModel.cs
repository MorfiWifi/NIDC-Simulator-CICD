using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Identity
{
    public class AccountRoleModel : BaseModel
    {
        public Guid RoleId { get; set; }
        public virtual RoleModel Role { get; set; }
        
    }
    public class RoleModel : BaseModel
    {
        public string Name { get; set; }
    }
}
