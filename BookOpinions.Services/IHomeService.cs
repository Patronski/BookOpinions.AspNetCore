using System.Collections.Generic;
using System.Threading.Tasks;
using BookOpinions.Services.Models.Book;

namespace BookOpinions.Services
{
    public interface IHomeService
    {
        Task<List<BookWellsCollectionServiceModel>> GetPopularBooks(int popularBooksCount);
    }
}
