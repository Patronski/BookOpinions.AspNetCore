using BookOpinions.Common.Mapping;
using System.ComponentModel.DataAnnotations;

namespace BookOpinions.Services.Models.Book
{
    public class CreateOpinionForBookServiceModel : IMapFrom<Data.Models.Opinion>
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}
