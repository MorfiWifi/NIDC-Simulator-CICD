using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Identity
{
    [Table("AccountRoles", Schema = "Identity")]
    public class AccountRole : BaseEntity
    {
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
