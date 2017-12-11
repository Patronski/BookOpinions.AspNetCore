using BookOpinions.Data.Models;

namespace BookOpinions.Web.Models.Home
{
    public class SimpleBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public Author Author { get; set; }
        public int OpinionsCount { get; set; }
        public double Rating { get; set; }
    }
}
