using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Identity
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessage = null, ErrorMessageResourceType = typeof(ResourceFiles.Validation))]
        [Display(Name = nameof(Username), ResourceType = typeof(ResourceFiles.Validation))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessage = null, ErrorMessageResourceType = typeof(ResourceFiles.Validation))]
        [Display(Name = nameof(Password), ResourceType = typeof(ResourceFiles.Validation))]
        public string Password { get; set; }
        public string OrganizationUrl { set; get; }

    }
}