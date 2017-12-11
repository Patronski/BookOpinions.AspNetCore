using BookOpinions.Services;
using BookOpinions.Web.Models.Home;
using System.Collections.Generic;

namespace BookOpinions.Web.Models.Book
{
    public class AllBooksViewModel
    {
        public IEnumerable<SimpleBookViewModel> Books { get; set; }
        public Pager Pager { get; set; }
        public string SortOrder { get; set; }
        public string Search { get; set; }
    }
}
