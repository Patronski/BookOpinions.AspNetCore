using System.ComponentModel.DataAnnotations;

namespace BookOpinions.Web.Models.Book
{
    public class AddBookFormModel
    {
        [Required]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string AuthorName { get; set; }
    }
}
