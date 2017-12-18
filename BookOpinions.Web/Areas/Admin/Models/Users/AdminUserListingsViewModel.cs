using System.Collections.Generic;
using BookOpinions.Services.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookOpinions.Web.Areas.Admin.Models.Users
{
    public class AdminUserListingsViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
