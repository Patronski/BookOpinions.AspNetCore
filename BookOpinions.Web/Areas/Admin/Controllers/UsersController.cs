using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services.Admin;
using BookOpinions.Services.Admin.Models;
using BookOpinions.Web.Areas.Admin.Models.Users;
using BookOpinions.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
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

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.Name} successfully added to the {model.Role} role");
            return RedirectToAction(nameof(Index));
        }
    }
}