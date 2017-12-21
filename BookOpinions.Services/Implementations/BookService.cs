using System;
using System.Collections.Generic;
using System.Linq;
using BookOpinions.Data;
using BookOpinions.Data.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper.QueryableExtensions;
using BookOpinions.Services.Models.Book;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookOpinions.Services.Implementations
{
    public class BookService : Service, IBookService
    {
        public BookService(BookOpinionsDbContext db, UserManager<User> userManager)
            : base(db, userManager)
        {
        }

        public async Task<List<Book>> GetAllBooksBySortOrder(string sortOrder, string search)
        {
            var books = await Db.Books
                        .Include(b => b.Authors)
                        .ThenInclude(a => a.Author)
                        .Include(b => b.Image)
                        .ToListAsync();

            var sortToLower = sortOrder?.ToLower();
            switch (sortToLower)
            {
                case "author":
                    books = books.OrderBy(sb => sb.Authors.Select(ba => ba.Author.Name)).ToList();
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

        public void AddNewBook(string Title, string ImageUrl, string AuthorName)
        {
            var authors = AuthorName
                        .Split(',')
                        .Select(name => new Author
                        {
                            Name = name.Trim(),
                        })
                        .ToList();

            Image image = new Image()
            {
                Name = ServiceConstants.ImageForBookNamePrefix + Title,
                Url = ImageUrl
            };

            Book book = new Book()
            {
                Title = Title,
                Image = image,
            };

            book.Authors = AddAuthorsToBook(authors, book);

            Db.Books.Add(book);
            Db.SaveChanges();
        }

        private List<BookAuthor> AddAuthorsToBook(IEnumerable<Author> authors, Book toThisBook)
        {
            List<BookAuthor> ba = new List<BookAuthor>(authors.Count());
            foreach (var author in authors)
            {
                ba.Add(new BookAuthor
                {
                    Author = author,
                    Book = toThisBook
                });
            }

            return ba;
        }

        private List<BookAuthor> AddBooksToAuthor(IEnumerable<Book> books, Author toThisAuthor)
        {
            List<BookAuthor> ba = new List<BookAuthor>(books.Count());
            foreach (var book in books)
            {
                ba.Add(new BookAuthor
                {
                    Author = toThisAuthor,
                    Book = book
                });
            }

            return ba;
        }
    }
}
