﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services;
using BookOpinions.Web.Infrastructure.Filters;
using BookOpinions.Web.Models.Book;
using BookOpinions.Web.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookOpinions.Web.Controllers
{
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

        [Route("Books/All/{sortOrder?}/{page?}/{search?}")]
        public async Task<ActionResult> All(string sortOrder, int? page, string search)
        {
            var sortedBooks = await this.service.GetAllBooksBySortOrder(sortOrder, search);

            Pager pager = new Pager(
                sortedBooks.Count,
                page,
                WebConstants.BooksAllBooksOnPage);

            var booksVm = sortedBooks
                .Select(b => new SimpleBookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImgUrl = b.Image.Url
                });
                //.Skip((pager.CurrentPage - 1) * pager.ItemsOnPage)
                //.Take(pager.ItemsOnPage);

            var viewModel = new AllBooksViewModel
            {
                Books = booksVm,
                Pager = pager,
                SortOrder = sortOrder,
                Search = search
            };

            return View(viewModel);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        //[ValidateModelState]
        public ActionResult Add(AddBookFormModel model)
        {
            if (ModelState.IsValid)
            {
                this.service.AddNewBook(model.Title, model.ImageUrl, model.AuthorName);
                TempData["AddedBook"] = $"Successfully added book {model.Title}!";

                return this.RedirectToAction("all", "books");
            }

            return this.RedirectToAction("add", model);
        }
    }
}