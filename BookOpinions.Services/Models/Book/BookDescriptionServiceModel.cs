using AutoMapper;
using BookOpinions.Common.Mapping;
using BookOpinions.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookOpinions.Services.Models.Book
{
    public class BookDescriptionServiceModel : IMapFrom<Data.Models.Book>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public List<Opinion> Opinions { get; set; }

        [Required]
        public string Title { get; set; }

        public string ImgUrl { get; set; }

        public string AuthorNames { get; set; }

        public string UserId { get; set; }

        public List<SelectListItem> RatingPosibilities { get; set; } = new List<SelectListItem>();

        public double FinalRating { get; set; }

        public int TotalVoted { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Data.Models.Book, BookDescriptionServiceModel>()
                .ForMember(m => m.ImgUrl, cfg => cfg.MapFrom(b =>
                                                b.Image.Url))

                .ForMember(m => m.TotalVoted, cfg => cfg.MapFrom(b =>
                                                b.Rating.Count))

                .ForMember(m => m.FinalRating, cfg => cfg.MapFrom(b =>
                        (b.Rating.Count == 0)
                        ?
                        0d
                        :
                        (double)Math.Round(
                                (double)b.Rating.Sum(r => r.Value)
                                /
                                (double)b.Rating.Count,
                            ServiceConstants.RoundNumbersAfterDecimalPoint)))

                .ForMember(m => m.AuthorNames, cfg => cfg.MapFrom(b =>
                                                string.Join(", ", b.Authors
                                                    .Select(a => a.Author.Name))
                ));

            //.Where(a => a.BookId == b.Id)
            //.Select(ba=>ba.Author)));
        }
    }
}
