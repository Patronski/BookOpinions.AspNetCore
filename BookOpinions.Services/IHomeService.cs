using System;
using System.Collections.Generic;
using System.Text;
using BookOpinions.Data.Models;

namespace BookOpinions.Services
{
    public interface IHomeService
    {
        IEnumerable<Book> GetPopularBooks(int popularBooksCount);
    }
}
