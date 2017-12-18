using BookOpinions.Common.Mapping;
using BookOpinions.Data.Models;

namespace BookOpinions.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
