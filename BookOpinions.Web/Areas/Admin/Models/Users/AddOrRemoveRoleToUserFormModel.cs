using System.ComponentModel.DataAnnotations;

namespace BookOpinions.Web.Areas.Admin.Models.Users
{
    public class AddOrRemoveRoleToUserFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
