namespace BookOpinions.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.NameMinLength)]
        [MaxLength(DataConstants.NameMaxLength)]
        public string Name { get; set; }

        public List<BookAuthor> Books { get; set; } = new List<BookAuthor>();
    }
}
