namespace BookOpinions.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public string ContentType { get; set; }

        [Display(Name = "Image Url", ShortName = "Url")]
        public string Url { get; set; }

        public List<Book> Books { get; set; }
    }
}
