using System.ComponentModel.DataAnnotations;

namespace BookOpinions.Web.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
