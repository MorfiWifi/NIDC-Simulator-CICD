using System.ComponentModel.DataAnnotations;

namespace Models.Identity
{
    public class VerifyModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Token { get; set; }
        public string OrganizationUrl { set; get; }
    }
}