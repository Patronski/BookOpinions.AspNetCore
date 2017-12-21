using AutoMapper;
using BookOpinions.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BookOpinions.Services.Models.Book
{
    // Good example - why usings must be in the namespace
    public class BooksAllSortedServiceModel : IMapFrom<Data.Models.Book>, IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string AuthorName { get; set; }
        public int OpinionsCount { get; set; }
        public int RatingsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Data.Models.Book, BooksAllSortedServiceModel>()
                .ForMember(m => m.OpinionsCount, cfg => cfg.MapFrom(b=>b.Opinions.Count))
                .ForMember(m => m.RatingsCount, cfg => cfg.MapFrom(b=>b.Rating.Count))
                .ForMember(m => m.ImgUrl, cfg => cfg.MapFrom(b => b.Image.Url))
                .ForMember(
                    m => m.AuthorName,
                    cfg => cfg.MapFrom(
                        b => b.Authors.FirstOrDefault(
                            a => a.BookId == b.Id)
                            .Author.Name));
        }
    }
}
