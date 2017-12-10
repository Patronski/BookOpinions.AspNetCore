namespace BookOpinions.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
            this.Opinions = new HashSet<Opinion>();
            this.Rating = new HashSet<Rating>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual Image Image { get; set; }

        [Display(Name = "Authors separated by comma(,)", ShortName = "Authors")]
        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Opinion> Opinions { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
    }
}
