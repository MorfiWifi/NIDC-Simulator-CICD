using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain.Config
{
    public partial class AccountUnit : BaseEntity
    {
        public Guid UnitId { get; set; }
        public Guid AccountId { get; set; }

        [ForeignKey("UnitId")]
        public virtual Unit? Unit { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account? Account { get; set; }
    }
}