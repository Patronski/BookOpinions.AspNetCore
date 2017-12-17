using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookOpinions.Data.Models
{
    public class User : IdentityUser
    {
        //[Required]
        //[MinLength(DataConstants.NameMinLength)]
        //[MaxLength(DataConstants.NameMaxLength)]
        //public string Name { get; set; }

        //public DateTime Birtdate { get; set; }

        public List<Rating> Ratings { get; set; } = new List<Rating>();

        public List<Opinion> Opinions { get; set; } = new List<Opinion>();
    }
}
