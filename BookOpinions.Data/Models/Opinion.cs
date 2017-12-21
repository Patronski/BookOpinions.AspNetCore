namespace BookOpinions.Data.Models
{
    using System;

    public class Opinion
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
