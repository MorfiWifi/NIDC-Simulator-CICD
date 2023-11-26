using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Identity
{
    [Table("LoginTokens", Schema = "Identity")]
    public class LoginToken: BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpireationDate { get; set; }
        public bool Used { get; set; }

        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        public virtual RefreshToken RefreshToken { get; set; }
    }
}
