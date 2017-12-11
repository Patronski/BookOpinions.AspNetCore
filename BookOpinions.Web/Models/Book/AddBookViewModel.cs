using System.ComponentModel.DataAnnotations;

namespace BookOpinions.Web.Models.Book
{
    public class AddBookViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Image Url", ShortName = "Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Authors separated by comma(,)", ShortName = "Author")]
        public string AuthorName { get; set; }
    }
}
