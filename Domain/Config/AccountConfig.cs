using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain.Config
{
    public partial class AccountConfig : BaseEntity
    {
        public Guid? AccountId { get; set; }
        public Guid? ConfigId { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { set; get; }
        [ForeignKey(("ConfigId"))]
        public virtual SimulationConfig SimulationConfig { set; get; }
    }
}