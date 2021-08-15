using EStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EStore.ViewModels;
using EStore.Data;
using Microsoft.EntityFrameworkCore;

namespace EStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {

            _context = context;

        }

        public IActionResult Index()
        {
            var recentlyAdded = _context.Product.Include(p =>p.ImagesPaths).OrderByDescending(i => i.ProductId).Take(3).ToList();
            var lowInStock = _context.Product.Include(p => p.ImagesPaths).OrderBy(i => i.Stock).Take(3).ToList();


            HomeViewModel HomeViewModel = new HomeViewModel()
            {
                RecentlyAdded = recentlyAdded,
                LowInStock = lowInStock,
            };

            return View(HomeViewModel);
        }

    }
}
