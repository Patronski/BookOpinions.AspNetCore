using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookOpinions.Data.Models
{
    public class User : IdentityUser
    {
        public List<Rating> Ratings { get; set; } = new List<Rating>();

        public List<Opinion> Opinions { get; set; } = new List<Opinion>();
    }
}
