using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EStore.Data;
using EStore.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EStore.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.Order.Where(o => o.UserId == user && o.status.Name == "InCart").Include(d => d.Details).ThenInclude(p => p.Product).FirstOrDefaultAsync();
            if (cart != null)
            {
                return View(cart);
            }
            else
            {
                return NoContent();
            }
        }


        public async Task<JsonResult> AddToCart(int id)
        {
            var user =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Cart = await _context.Order.Where(e => e.User.Id == user && e.status.Name == "InCart").FirstOrDefaultAsync();
            var product = await _context.Product.Where(i => i.ProductId == id).FirstOrDefaultAsync();

            if(product == null)
            {
                return Json("Product Does not exist");
            }

            if(product.Stock == 0)
            {
                return Json("Product is out of stock");
            }

            if (Cart != null)
            {
                var item = await _context.OrderDetail.Where(i => i.OrderId == Cart.OrderId && i.ProductID == id).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.Quantity++;
                    item.SubTotal = item.Quantity * product.Price;
                    _context.Update(item);
                }
                else
                {
                    var NewCartItem = new OrderDetail
                    {
                      Order = Cart,
                      ProductID = id,
                      Quantity = 1,
                      SubTotal = product.Price
                    };

                     await _context.AddAsync(NewCartItem);
                }

            }
            else
            {
                var order = new Order
                {
                    OrderStatusID = 1,
                    UserId = user
                };
                order.Details = new List<OrderDetail>
                {
                    new OrderDetail {ProductID = id, Quantity = 1, SubTotal = product.Price}
                };

                await _context.AddAsync(order);
            }
            product.Stock--;
            _context.Update(product);
            await _context.SaveChangesAsync();
            return Json(product.Name + " was added to cart");
        }

        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {


            if (quantity == 0)
            {
                await DeleteFromCart(id);
            }
            else
            {
                var item = await _context.OrderDetail.Where(i => i.OrderDetailId == id).FirstOrDefaultAsync();
                var product = await _context.Product.Where(i => i.ProductId == item.ProductID).FirstOrDefaultAsync();

                if (item.Quantity < quantity && product.Stock > 0)
                {
                    product.Stock--;
                    item.Quantity = quantity;
                    item.SubTotal = product.Price * quantity;
                    _context.Update(item);
                    _context.Update(product);
                }
                else if(item.Quantity > quantity)
                {
                    product.Stock++;
                    item.Quantity = quantity;
                    item.SubTotal = product.Price * quantity;
                    _context.Update(item);
                _context.Update(product);
                }

                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteFromCart(int id)
        {
            var item = await _context.OrderDetail.Where(i => i.OrderDetailId == id).FirstOrDefaultAsync();
            var product = await _context.Product.Where(i => i.ProductId == item.ProductID).FirstOrDefaultAsync();
            product.Stock += item.Quantity;
            _context.Remove(item);
            _context.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Checkout(int id)
        {
            var order = await _context.Order.Where(i => i.OrderId == id).Include(d => d.Details).ThenInclude(p => p.Product).FirstOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }
            order.Total = await _context.OrderDetail.Where(i => i.OrderId == id).SumAsync(t => t.SubTotal);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(int id, [Bind("OrderId,BlockNo,RoadNo,BuildingNo,FlatNo")] Order orderChanges)
        {
            var order = await _context.Order.Where(i => i.OrderId == id).FirstOrDefaultAsync();

            if (order.OrderId != orderChanges.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.BlockNo = orderChanges.BlockNo;
                    order.RoadNo = orderChanges.RoadNo;
                    order.BuildingNo = orderChanges.BuildingNo;
                    order.FlatNo = orderChanges.FlatNo;
                    order.Total = await _context.OrderDetail.Where(i => i.OrderId == id).SumAsync(t => t.SubTotal);
                    order.OrderStatusID++;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (order == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("MyOrders", "Orders");
        }

    }
}
