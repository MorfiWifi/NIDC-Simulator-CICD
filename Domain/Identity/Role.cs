using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Identity
{
    [Table("Roles", Schema = "Identity")]
    public class Role : BaseEntity
    {
        public string Name { get; set; }
    }
}
