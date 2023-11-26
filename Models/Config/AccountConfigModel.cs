using System;

namespace Models.Config
{
    public partial class AccountConfigModel : BaseModel
    {
        public Guid AccountId { get; set; }
        public Guid ConfigId { get; set; }
        public bool IsActive { get; set; }
    }
}