using EStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EStore.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult Dashboard()
        {


            return View();
        }

        public JsonResult chtOrdersByStatus()
        {
            var res = _context.Order.GroupBy(o => o.status.Name).Select(r => new { OrderType = r.Key, NumOfOrders = r.Count()}).ToList();
          
/*            **both queries return the same result**
 *            
              var result = from s in _context.Order
                         group s by s.status.Name into c
                         select new {c.Key, count = c.Count()};*/
            return Json(res);
        }

    }
}
