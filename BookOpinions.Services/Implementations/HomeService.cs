using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BookOpinions.Data;
using BookOpinions.Data.Models;
using BookOpinions.Services.Models.Book;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookOpinions.Services.Implementations
{
    public class HomeService : Service, IHomeService
    {
        public HomeService(BookOpinionsDbContext db, UserManager<User> userManager)
            :base(db, userManager)
        {
        }

        public async Task<List<BookWellsCollectionServiceModel>> GetPopularBooks(int popularBooksCount)
        {
            var books = await Db
                .Books
                .ProjectTo<BookWellsCollectionServiceModel>()
                .OrderBy(b => b.Title)
                .Take(popularBooksCount)
                .ToListAsync();

            return books;
        }
    }
}
