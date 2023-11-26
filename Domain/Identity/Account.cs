using Domain.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Identity
{
    [Table("Accounts", Schema = "Identity")]
    public partial class Account : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int ConfigsCount { get; set; }
        public int SimulationLength { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<LoginToken> LoginTokens { get; set; }
        
        public Guid? ParentAccountId { set; get; }
        public virtual  Organization Organization { set; get; }
        public Guid? OrganizationId { set; get; }
    }
}
