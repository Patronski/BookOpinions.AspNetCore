using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookOpinions.Data.Models;

namespace BookOpinions.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksBySortOrder(string sortOrder, string search);

        void AddNewBook(string Title, string ImageUrl, string AuthorName);
    }
}
