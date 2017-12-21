using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services.Models.Book;

namespace BookOpinions.Services
{
    public interface IHomeService
    {
        Task<List<BookWellsCollectionServiceModel>> GetPopularBooks(int popularBooksCount);
    }
}
