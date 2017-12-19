using System;
using System.Collections.Generic;
using System.Text;
using BookOpinions.Data.Models;

namespace BookOpinions.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooksBySortOrder(string sortOrder, string search);
    }
}
