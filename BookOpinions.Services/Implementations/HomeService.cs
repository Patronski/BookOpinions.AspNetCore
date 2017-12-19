using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookOpinions.Data;
using BookOpinions.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookOpinions.Services.Implementations
{
    public class HomeService : IHomeService
    {
        private readonly BookOpinionsDbContext db;
        private readonly UserManager<User> userManager;

        public HomeService(BookOpinionsDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IEnumerable<Book> GetPopularBooks(int popularBooksCount)
        {
            var books = this.db
                .Books
                .Take(popularBooksCount)
                .ToList();
            //    .Select(b => new SimpleBookViewModel
            //{
            //    Id = b.Id,
            //    ImgUrl = b.Image.Url,
            //    Title = b.Title
            //});

            return books;
        }
    }
}
