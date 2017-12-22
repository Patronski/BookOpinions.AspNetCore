using System.Linq;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services.Admin;
using BookOpinions.Web.Areas.Admin.Models.Users;
using BookOpinions.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookOpinions.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUsersService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(
            IAdminUsersService users,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.users.AllAsync();
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return View(new UserListingsViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] no need for this, we have global filter
        public async Task<IActionResult> AddToRole(AddOrRemoveRoleToUserFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details!");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var identityResult = await this.userManager.AddToRoleAsync(user, model.Role);

            if (identityResult.Succeeded)
                TempData.AddSuccessMessage($"The {model.Role} role was successfully added to User {user.Name}!");
            else
                TempData.AddErrorMessage($"The {model.Role} role was not added tor User {user.Name}!");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(AddOrRemoveRoleToUserFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details!");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var identityResult = await this.userManager.RemoveFromRoleAsync(user, model.Role);

            if (identityResult.Succeeded)
                TempData.AddSuccessMessage($"The {model.Role} role was successfully removed from User {user.Name}!");
            else
                TempData.AddErrorMessage($"The {model.Role} role was not removed or didn't exist for User {user.Name}!");

            return RedirectToAction(nameof(Index));
        }
    }
}