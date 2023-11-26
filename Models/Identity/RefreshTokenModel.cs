using System.ComponentModel.DataAnnotations;

namespace Models.Identity
{
    public class RefreshTokenModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string Token { get; set; }
    }
}