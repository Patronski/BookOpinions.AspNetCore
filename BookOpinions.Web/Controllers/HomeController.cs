using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services;
using Microsoft.AspNetCore.Mvc;
using BookOpinions.Web.Models;
using BookOpinions.Web.Models.Home;
using Microsoft.AspNetCore.Identity;

namespace BookOpinions.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService service;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public HomeController(IHomeService service, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.service = service;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var popularBooks = this.service.GetPopularBooks(WebConstants.PopularBooksCount);

            var vms = popularBooks.Select(b => new HomePopularBookViewModel
            {
                Id = b.Id,
                ImgUrl = b.Image.Url,
                Title = b.Title
            });

            return View(vms);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
