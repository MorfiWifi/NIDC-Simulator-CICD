using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Identity
{
    public class SignUpModel
    {
        [Required(ErrorMessage ="*")]
        [Display(Name = nameof(Mobile), ResourceType = typeof(Models.ResourceFiles.Validation))]
        public string Mobile { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name = nameof(FirstName), ResourceType = typeof(Models.ResourceFiles.Validation))]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name = nameof(LastName), ResourceType = typeof(Models.ResourceFiles.Validation))]
        public string LastName { get; set; }
        
        [Display(Name = nameof(Company), ResourceType = typeof(Models.ResourceFiles.Validation))]
        public string Company { get; set; }

        public string OrganizationUrl { set; get; }

    }
}
