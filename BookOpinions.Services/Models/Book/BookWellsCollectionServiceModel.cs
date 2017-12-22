using AutoMapper;
using BookOpinions.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BookOpinions.Services.Models.Book
{
    // Good example - why usings must be in the namespace
    public class BookWellsCollectionServiceModel : IMapFrom<Data.Models.Book>, IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string AuthorName { get; set; }
        public int OpinionsCount { get; set; }
        public double FinalRating { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Data.Models.Book, BookWellsCollectionServiceModel>()
                .ForMember(m => m.OpinionsCount, cfg => cfg.MapFrom(b => b.Opinions.Count))
                .ForMember(m => m.ImgUrl, cfg => cfg.MapFrom(b => b.Image.Url))

                .ForMember(
                    m => m.AuthorName,
                    cfg => cfg.MapFrom(
                        b => b.Authors.FirstOrDefault(
                            a => a.BookId == b.Id)
                            .Author.Name))

                .ForMember(m => m.FinalRating, cfg => cfg.MapFrom(b =>
                    (b.Rating.Count == 0)
                    ?
                    0d
                    :
                    Math.Round(
                            b.Rating.Sum(r => r.Value)
                            /
                            (double)b.Rating.Count,
                        ServiceConstants.RoundNumbersAfterDecimalPoint)));
        }
    }
}
