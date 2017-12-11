namespace BookOpinions.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Book
    {
        //public Book()
        //{
        //    this.Authors = new HashSet<Author>();
        //    this.Opinions = new HashSet<Opinion>();
        //    this.Rating = new HashSet<Rating>();
        //}

        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.TitleMinLength)]
        [MaxLength(DataConstants.TitleMaxLength)]
        public string Title { get; set; }

        public virtual Image Image { get; set; }

        [Display(Name = "Authors separated by comma(,)", ShortName = "Authors")]
        public virtual List<BookAuthor> Authors { get; set; } = new List<BookAuthor>();

        public virtual List<Opinion> Opinions { get; set; } = new List<Opinion>();

        public virtual List<Rating> Rating { get; set; } = new List<Models.Rating>();
    }
}
