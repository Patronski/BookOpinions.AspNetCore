using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookOpinions.Data;
using BookOpinions.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookOpinions.Services.Implementations
{
    public class BookService : Service, IBookService
    {
        public BookService(BookOpinionsDbContext db, UserManager<User> userManager)
            : base(db, userManager)
        {
        }

        public List<Book> GetAllBooksBySortOrder(string sortOrder, string search)
        {
            var books = Db.Books.ToList();

            var sortToLower = sortOrder?.ToLower();
            switch (sortToLower)
            {
                case "author":
                    books = books.OrderBy(sb => sb.Authors.Select(a => a.Author.Name)).ToList();
                    break;
                case "title":
                    books = books.OrderBy(sb => sb.Title).ToList();
                    break;
                case "newfirst":
                    books.Reverse();
                    break;
                case "opinions":
                    books = books.OrderByDescending(b => b.Opinions.Count).ToList();
                    break;
                case "rating":
                    books = books.OrderByDescending(b => b.Rating).ToList();
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(search))
            {
                var searchWords = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(w => w.ToLower());
                books = books.Where(b =>
                {
                    var titleWords = b.Title.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(w => w.ToLower());
                    var rezult = false;
                    foreach (var titleWord in titleWords)
                    {
                        if (searchWords.Any(sw => titleWord.StartsWith(sw)))
                        {
                            rezult = true;
                        }
                    }
                    return rezult;
                })
                .ToList();
            }

            return books;
        }
    }
}
