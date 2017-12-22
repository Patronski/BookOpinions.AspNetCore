﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookOpinions.Web.Areas.Shop.Controllers
{
    [Area(WebConstants.ShopArea)]
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}