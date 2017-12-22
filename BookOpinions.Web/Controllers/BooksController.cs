using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services;
using BookOpinions.Web.Infrastructure.Filters;
using BookOpinions.Web.Models.Book;
using BookOpinions.Web.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BookOpinions.Web.Infrastructure.Extensions;
using BookOpinions.Services.Models.Book;

namespace BookOpinions.Web.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService service;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public BooksController(IBookService service, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.service = service;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [Route("Books/All/{sortOrder?}/{page:int?}/{search?}")]
        public async Task<IActionResult> All(string sortOrder, int? page, string search)
        {
            var sortedBooks = await this.service.GetAllBooksBySortOrder(sortOrder, search);

            Pager pager = new Pager(
                sortedBooks.Count,
                page,
                WebConstants.BooksAllBooksOnPage);

            var booksInPage = sortedBooks
                .Skip((pager.CurrentPage - 1) * pager.ItemsOnPage)
                .Take(pager.ItemsOnPage)
                .ToList();

            var viewModel = new AllBooksViewModel
            {
                Books = booksInPage,
                Pager = pager,
                SortOrder = sortOrder,
                Search = search
            };

            return View(viewModel);
        }

        [Authorize(Roles = WebConstants.AdminRole
            + "," + WebConstants.BooksModeratorRole
            + "," + WebConstants.CommentsModeratorRole
            + "," + WebConstants.ShopModeratorRole
            + "," + WebConstants.AddingBooksRole)]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole
            + "," + WebConstants.BooksModeratorRole
            + "," + WebConstants.CommentsModeratorRole
            + "," + WebConstants.ShopModeratorRole
            + "," + WebConstants.AddingBooksRole)]
        //[ValidateModelState]
        public ActionResult Add(AddBookFormModel model)
        {
            if (ModelState.IsValid)
            {
                this.service.AddNewBook(model.Title, model.ImageUrl, model.AuthorName);
                TempData[WebConstants.TempDataAddedBookMessageKey] = $"Successfully added book {model.Title}!";

                return this.RedirectToAction(nameof(BooksController.All), "books");
            }

            return this.RedirectToAction("add", model);
        }

        [Route("books/{id:int}")]
        public IActionResult Description(int id)
        {
            var userId = this.User.GetUserId();

            var vm = this.service.GetBookDescriptionById(id, userId);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Opinion(CreateOpinionForBookServiceModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.service.AddOpinionForBook(model))
                {
                    return RedirectToAction(nameof(this.Description), routeValues: new { id = model.BookId });
                }
            }
            TempData[WebConstants.TempDataAddedOpinionMessageKey] = "The opinion is not created!";

            return RedirectToAction(nameof(this.Description), routeValues: new { id = model.BookId });
        }

        [HttpPost]
        //[Authorize(Roles = "User, Admin")]
        public IActionResult DeleteOpinion(int opinionId, int bookId)
        {
            this.service.DeleteOpinion(opinionId);
            return this.RedirectToAction(nameof(this.Description), new { id = bookId });
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole)]
        public IActionResult Delete(string delete, int id)
        {
            if (delete == WebConstants.ConfirmBookDeletion)
            {
                this.service.DeleteBook(id);
                TempData[WebConstants.TempDataDeleteBookCaptcha] = $"Successfully deleted book with id = {id}";
                return this.RedirectToAction(nameof(this.All));
            }
            TempData[WebConstants.TempDataDeleteBookCaptcha] = $"Write lowercase \"delete\" without quotes to delete this book!";
            return this.RedirectToAction(nameof(this.Description), routeValues: new { id });
        }

        [HttpPost]
        public IActionResult AddRate(int value, int bookId)
        {
            if (value >= 1 && value <= 5)
            {
                var userId = this.User.GetUserId();
                service.AddRate(value, bookId, userId);
            }
            
            return this.RedirectToAction(nameof(this.Description), routeValues: new { id = bookId });
        }

        [Authorize(Roles = WebConstants.AdminRole)]
        public ActionResult Edit(int id)
        {
            var vm = this.service.FindBookForEdit(id);

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole)]
        public ActionResult Edit(EditBookViewModel bm)
        {
            if (ModelState.IsValid)
            {
                this.service.EditBook(bm);
                TempData[WebConstants.TempDataAddedBookMessageKey] = $"Successfully edited book {bm.Title}!";

                return this.RedirectToAction(nameof(this.Description), bm.Id);
            }
            return this.RedirectToAction(nameof(this.Edit), bm);
        }
    }
}