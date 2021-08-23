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
            var recentlyAdded = _context.Product.Include(p => p.ImagesPaths.Take(1)).OrderByDescending(i => i.ProductId).Take(3).ToList();
            var lowInStock = _context.Product.Where(s => s.Stock != 0).Include(p => p.ImagesPaths.Take(1)).OrderBy(i => i.Stock).Take(3).ToList();
            //this query will count the products that are in cart also, and will not take into account the quantity ordered 
            var mostSelling = _context.Product.Include(p => p.ImagesPaths.Take(1)).Include(d => d.InOrders).OrderByDescending(m => m.InOrders.Count()).Take(3).ToList();

            HomeViewModel HomeViewModel = new HomeViewModel()
            {
                RecentlyAdded = recentlyAdded,
                LowInStock = lowInStock,
                MostSelling = mostSelling
            };

            return View(HomeViewModel);
        }

    }
}
