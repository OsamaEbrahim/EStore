using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EStore.Data;
using EStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EStore.Services;
using Microsoft.Extensions.Configuration;

namespace EStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration Configuration { get; }



        public OrdersController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;

        }

        // GET: Orders
        public async Task<IActionResult> MyOrders()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Order.Where(i => i.UserId == user && i.status.Name != "InCart").Include(s => s.status);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(s => s.status)
                .Include(d => d.Details)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> MyOrderDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(s => s.status)
                .Include(d => d.Details)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,status,UserId,Total,CreatedAt")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageOrders));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageOrders()
        {
            var applicationDbContext = _context.Order.Where(i => i.status.Name != "InCart").Include(o => o.User).Include(s => s.status);
            ViewData["Status"] = new SelectList(_context.OrderStatus.Where(o => o.Name != "InCart" || o.Name != "Placed"), "Id", "Name");

            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(_context.OrderStatus.Skip(2), "OrderStatusId", "Name", order.OrderStatusID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [Bind("OrderId,OrderStatusID")] Order orderChanges)
        {
            var order = await _context.Order.Where(i => i.OrderId == id).Include(u => u.User).Include(s => s.status).FirstOrDefaultAsync();
            if (order.OrderId != orderChanges.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.OrderStatusID = orderChanges.OrderStatusID;
                    order.UpdatedAt = DateTime.Now;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                string subject = "Order #" + order.OrderId + " Status Update";
                string body = "Your order status has been updated";
                var emailSender = new EmailsSender(Configuration);
                emailSender.SendEmail(order.User.Email, subject, body);
                return RedirectToAction(nameof(ManageOrders));
            }
            ViewData["Status"] = new SelectList(_context.OrderStatus.Where(o => o.Name != "InCart" || o.Name != "Placed"), "OrderStatusId", "Name", orderChanges.OrderStatusID);
            return View(order);

        }
    }


}
