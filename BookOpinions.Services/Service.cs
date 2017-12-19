using BookOpinions.Data;
using BookOpinions.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookOpinions.Services
{
    public abstract class Service
    {
        protected readonly BookOpinionsDbContext Db;
        protected readonly UserManager<User> UserManager;

        protected Service(BookOpinionsDbContext db, UserManager<User> userManager)
        {
            this.Db = db;
            this.UserManager = userManager;
        }
    }
}
