using BookOpinions.Services;
using BookOpinions.Services.Models.Book;
using System.Collections.Generic;

namespace BookOpinions.Web.Models.Book
{
    public class AllBooksViewModel
    {
        public List<BookWellsCollectionServiceModel> Books { get; set; }
        public Pager Pager { get; set; }
        public string SortOrder { get; set; }
        public string Search { get; set; }
    }
}
