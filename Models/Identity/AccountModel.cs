using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Identity
{
    public class AccountModel:ResponseModel
    {
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        
        public bool IsActive { get; set; }
        public int ConfigsCount { get; set; }
        public int SimulationLength { get; set; }
        public virtual List<AccountRoleModel> AccountRoles { get; set; } = new();
        public string AccountRolesString => string.Join(" | ", AccountRoles.Select(x=>x.Role?.Name));

        public override string ToString()
        {
            return $"{FirstName} {LastName} {Mobile}";
        }
    }

    public class CheckLoginResponse : AccountModel
    {
        public bool IsDeveloper { set; get; }
        public bool IsAdmin { set; get; }
        public bool IsCoordinator { set; get; }
        public bool IsAssessor { set; get; }
        public string ProfileImage { set; get; }
    }
}
