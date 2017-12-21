﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookOpinions.Data;
using BookOpinions.Data.Models;
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

        public List<Book> GetPopularBooks(int popularBooksCount)
        {
            var books = Db
                .Books
                .Include(b => b.Image)
                .OrderBy(b => b.Title)
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
