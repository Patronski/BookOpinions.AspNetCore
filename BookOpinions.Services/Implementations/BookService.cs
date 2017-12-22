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
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookOpinions.Services.Implementations
{
    public class BookService : Service, IBookService
    {
        public BookService(BookOpinionsDbContext db, UserManager<User> userManager)
            : base(db, userManager) { }

        #region Private methods

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

            toThisBook.Authors.AddRange(ba);
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

        #endregion Private methods

        public async Task<List<BookWellsCollectionServiceModel>> GetAllBooksBySortOrder(string sortOrder, string search)
        {
            //var books = Db.Books
            //    .Include(b => b.Authors)
            //    .ThenInclude(a => a.Author)
            //    .Include(b => b.Image)
            List<BookWellsCollectionServiceModel> books = new List<BookWellsCollectionServiceModel>();

            var sortToLower = sortOrder?.ToLower();
            switch (sortToLower)
            {
                case "author":
                    books = await Db.Books
                        .ProjectTo<BookWellsCollectionServiceModel>()
                        .OrderBy(b => b.AuthorName)
                        .ToListAsync();
                    break;
                case "title":
                    books = await Db.Books
                        .ProjectTo<BookWellsCollectionServiceModel>()
                        .OrderBy(sb => sb.Title)
                        .ToListAsync();
                    break;
                case "newfirst":
                    books = await Db.Books
                        .ProjectTo<BookWellsCollectionServiceModel>()
                        .ToListAsync();
                    books.Reverse();
                    break;
                case "opinions":
                    books = await Db.Books
                        .ProjectTo<BookWellsCollectionServiceModel>()
                        .OrderByDescending(b => b.OpinionsCount)
                        .ToListAsync();
                    break;
                case "rating":
                    books = await Db.Books
                        .ProjectTo<BookWellsCollectionServiceModel>()
                        .OrderByDescending(b => b.FinalRating)
                        .ToListAsync();
                    break;
                default:
                    books = await Db.Books
                        .ProjectTo<BookWellsCollectionServiceModel>()
                        .ToListAsync();
                    break;
            }

            if (!string.IsNullOrEmpty(search))
            {
                var searchWords = search
                                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(w => w.ToLower());

                books = books.Where(b =>
                {
                    var titleWords = b.Title
                                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(w => w.ToLower());

                    var result = false;
                    foreach (var titleWord in titleWords)
                    {
                        if (searchWords.Any(sw => titleWord.StartsWith(sw)))
                        {
                            result = true;
                        }
                    }
                    return result; //TODO ..
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

        public BookDescriptionServiceModel GetBookDescriptionById(int id)
        {
            var book = this.Db.Books
                .Where(b => b.Id == id)
                .Include(b => b.Authors)
                    .ThenInclude(a => a.Author)
                .Include(b => b.Image)
                .Include(b => b.Rating)
                    .ThenInclude(r => r.User)
                .Include(b => b.Opinions)
                    .ThenInclude(o => o.User)
                .FirstOrDefault();

            var bookDescription = Mapper.Map<BookDescriptionServiceModel>(book);
            //var bookDescription = new BookDescriptionServiceModel
            //{
            //    Id = book.Id,
            //    Title = book.Title,
            //    ImgUrl = book.Image.Url,
            //    TotalVoted = book.Rating.Count,
            //    Opinions = book.Opinions,
            //    UserId = userId 
            //};

            List<SelectListItem> selectItems = new List<SelectListItem>();
            for (int i = 1; i < 6; i++)
            {
                selectItems.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }

            bookDescription.RatingPosibilities.AddRange(selectItems);

            //if (book.Rating.Count != 0)
            //{
            //    bookDescription.FinalRating = Math.Round(
            //            book.Rating.Sum(r => r.Value) / (double)book.Rating.Count,
            //            ServiceConstants.RoundNumbersAfterDecimalPoint);
            //}

            bookDescription.AuthorNames = string.Join(", ", book.Authors
                                                    .Select(a => a.Author.Name));

            return bookDescription;
        }

        public bool AddOpinionForBook(CreateOpinionForBookServiceModel model, string userId )
        {
            var book = this.Db.Books.Find(model.BookId);
            var user = this.Db.Users.Find(userId);

            if (user == null || book == null)
            {
                return false;
            }

            Opinion opinion = Mapper.Map<Opinion>(model);
            opinion.Book = book;
            opinion.User = user;
            opinion.CreatedOn = DateTime.Now;

            this.Db.Opinions.Add(opinion);
            this.Db.SaveChanges();

            return true;
        }

        public void DeleteOpinion(int Id)
        {
            var opinion = this.Db.Opinions.Find(Id);
            this.Db.Opinions.Remove(opinion);
            this.Db.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = this.Db.Books.Find(id);
            this.Db.Books.Remove(book);
            this.Db.SaveChanges();
        }

        public void AddRate(int value, int bookId, string userId)
        {
            var rating = new Rating()
            {
                BookId = bookId,
                UserId = userId,
                Value = value
            };

            Db.Ratings.Add(rating);
            Db.SaveChanges();
        }

        public EditBookViewModel FindBookForEdit(int id)
        {
            Book book = this.Db.Books
                .Where(b => b.Id == id)
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Image)
                .FirstOrDefault();

            EditBookViewModel vm = Mapper.Map<EditBookViewModel>(book);
            
            return vm;
        }

        public void EditBook(EditBookViewModel model)
        {
            var book = Db.Books
                        .Include(b=> b.Image)
                        .Include(b => b.Authors)
                            .ThenInclude(ba=> ba.Author)
                        .Where(b => b.Id == model.Id)
                        .FirstOrDefault();

            if (book != null)
            {
                if (model.AuthorNames != null)
                {
                    var modelAuthors = model
                        .AuthorNames
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(name => name.Trim())
                        .ToList();

                    if (modelAuthors.Count > 0)
                    {
                        var authorsToRemove = new List<BookAuthor>();

                        foreach (var ba in book.Authors)
                        {
                            if (!modelAuthors.Contains(ba.Author.Name))
                            {
                                authorsToRemove.Add(ba);
                            }
                            else
                            {
                                modelAuthors.Remove(ba.Author.Name);
                            }
                        }

                        foreach (var author in authorsToRemove)
                        {
                            book.Authors.Remove(author);
                        }

                        var newAuthors = modelAuthors
                            .Select(name => new Author
                            {
                                Name = name
                            });
                        
                        AddAuthorsToBook(newAuthors, book);
                    }
                }

                book.Image.Url = model.ImageUrl;
                book.Title = model.Title;

                Db.SaveChanges();
            }
        }
    }
}
