using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services.Models.Book;

namespace BookOpinions.Services
{
    public interface IBookService
    {
        Task<List<BookWellsCollectionServiceModel>> GetAllBooksBySortOrder(string sortOrder, string search);

        void AddNewBook(string Title, string ImageUrl, string AuthorName);

        BookDescriptionServiceModel GetBookDescriptionById(int id, string userId);

        bool AddOpinionForBook(CreateOpinionForBookServiceModel model);

        void DeleteOpinion(int id);

        void DeleteBook(int id);

        void AddRate(int value, int bookId, string userId);

        EditBookViewModel FindBookForEdit(int id);

        void EditBook(EditBookViewModel bm);
    }
}
