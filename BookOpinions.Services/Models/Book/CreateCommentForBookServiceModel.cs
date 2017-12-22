using AutoMapper;
using BookOpinions.Common.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
