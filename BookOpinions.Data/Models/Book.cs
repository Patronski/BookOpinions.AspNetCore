namespace BookOpinions.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.TitleMinLength)]
        [MaxLength(DataConstants.TitleMaxLength)]
        public string Title { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }

        [Display(Name = "Authors separated by comma(,)", ShortName = "Authors")]
        public List<BookAuthor> Authors { get; set; } = new List<BookAuthor>();

        public List<Opinion> Opinions { get; set; } = new List<Opinion>();

        public List<Rating> Rating { get; set; } = new List<Rating>();
    }
}
