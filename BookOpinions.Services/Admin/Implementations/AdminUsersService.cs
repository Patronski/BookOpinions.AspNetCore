using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BookOpinions.Data;
using BookOpinions.Data.Models;
using BookOpinions.Services.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookOpinions.Services.Admin.Implementations
{
    public class AdminUsersService : IAdminUsersService
    {
        private readonly BookOpinionsDbContext db;
        private readonly UserManager<User> userManager;

        public AdminUsersService(BookOpinionsDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
        {
            var models = await this.db
                .Users
                .Select(u => new AdminUserListingServiceModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email
                })
                //.ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();

            foreach (var model in models)
            {
                model.CurrentRoles = await userManager.GetRolesAsync(userManager.FindByIdAsync(model.Id).Result);
            }

            return models;
        }
    }
}
