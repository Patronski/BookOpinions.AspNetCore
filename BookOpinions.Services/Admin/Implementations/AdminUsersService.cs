using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BookOpinions.Data;
using BookOpinions.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace BookOpinions.Services.Admin.Implementations
{
    public class AdminUsersService : IAdminUsersService
    {
        private readonly BookOpinionsDbContext db;

        public AdminUsersService(BookOpinionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
