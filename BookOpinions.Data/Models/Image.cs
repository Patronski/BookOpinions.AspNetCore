namespace BookOpinions.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public int Id { get; set; }

        public byte[] data { get; set; }

        public string ContentType { get; set; }

        public string Name { get; set; }

        [Display(Name = "Image Url", ShortName = "Url")]
        public string Url { get; set; }
    }
}
