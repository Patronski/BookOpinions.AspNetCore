using AutoMapper;
using BookOpinions.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookOpinions.Services.Models.Book
{
    // Good example - why usings must be in the namespace
    public class BooksAllSortedServiceModel : IMapFrom<Data.Models.Book>, IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Data.Models.Book, BooksAllSortedServiceModel>()
                .ForMember(m => m.ImgUrl, cfg => cfg.MapFrom(b => b.Image.Url));
        }
    }
}
