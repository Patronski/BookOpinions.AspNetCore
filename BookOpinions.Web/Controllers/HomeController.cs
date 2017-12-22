using System.Diagnostics;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services;
using Microsoft.AspNetCore.Mvc;
using BookOpinions.Web.Models;
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

        public async Task<IActionResult> Index()
        {
            var popularBooks = await this.service.GetPopularBooks(WebConstants.PopularBooksCount);
            
            return View(popularBooks);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
