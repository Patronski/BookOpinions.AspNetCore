using System.ComponentModel.DataAnnotations;

namespace BookOpinions.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
