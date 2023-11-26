using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Identity
{
    public class OTPLoginModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string Username { get; set; }
        public string OrganizationUrl { set; get; }
    }

    public class OTPVerifyModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }
        public string OrganizationUrl { set; get; }

    }

}