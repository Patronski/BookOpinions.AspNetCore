using AutoMapper;
using BookOpinions.Common.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BookOpinions.Services.Models.Book
{
    public class EditBookViewModel : IMapFrom<Data.Models.Book>, IHaveCustomMapping
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Image Url", ShortName = "Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Authors separated by comma(,)", ShortName = "Author")]
        public string AuthorNames { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Data.Models.Book, EditBookViewModel>()
                .ForMember(m => m.ImageUrl, cfg => cfg.MapFrom(b => b.Image.Url))
                .ForMember(
                    m => m.AuthorNames,
                    cfg => cfg.MapFrom(
                        b => b.Authors.FirstOrDefault(
                            a => a.BookId == b.Id)
                            .Author.Name));
        }
    }
}
