using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Identity
{
    [Table("RefreshTokens", Schema = "Identity")]
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }

        public Guid LoginTokenId { get; set; }
        public virtual LoginToken LoginToken { get; set; }        
    }
}
