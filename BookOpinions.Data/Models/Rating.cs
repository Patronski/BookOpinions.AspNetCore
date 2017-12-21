using System.ComponentModel.DataAnnotations;

namespace BookOpinions.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Value { get; set; }

        public int BookId { get; set; }

        [Required]
        public Book Book { get; set; }

        public string UserId { get; set; }

        [Required]
        public User User { get; set; }
    }
}
