using System.Collections.Generic;
using System.Threading.Tasks;
using BookOpinions.Services.Admin.Models;

namespace BookOpinions.Services.Admin
{
    public interface IAdminUsersService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
