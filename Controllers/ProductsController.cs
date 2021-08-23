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
using Microsoft.AspNetCore.Hosting;
using EStore.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnviornment;
        private readonly UserManager<ApplicationUser> _userManager;
        private FilesManager fileManager;
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHostEnviornment = hostEnvironment;
            fileManager = new FilesManager(_webHostEnviornment);
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string currentFilter, string sortOrder, int? CategoryId, int pg = 1)
        {

            if (searchString == null)
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CategoryId"] = CategoryId;
            ViewData["sortOrder"] = sortOrder;


            var Parameters = new Dictionary<string, string>();
            Parameters.Add("CategoryId", CategoryId.ToString());
            Parameters.Add("sortOrder", sortOrder);
            Parameters.Add("CurrentFilter", searchString);


            this.ViewBag.ParametersDictionary = Parameters;

            var Products = from s in _context.Product
                        select s;
            Products = Products.Include(p => p.Category).Include(i => i.ImagesPaths.Take(1));

            if (CategoryId != null)
            {
                Products = Products.Where(d => d.CategoryId == CategoryId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                Products = Products.Where(a => a.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "NameDecending":
                    Products = Products.OrderByDescending(s => s.Name);
                    ViewData["CurrSort"] = "Name: Z-A";
                    break;
                case "PriceAcending":
                    Products = Products.OrderBy(s => s.Price);
                    ViewData["CurrSort"] = "Price: Low-High";
                    break;
                case "PriceDecending":
                    Products = Products.OrderByDescending(s => s.Price);
                    ViewData["CurrSort"] = "Price: High-Low";
                    break;
                default:
                    Products = Products.OrderBy(s => s.Name);
                    ViewData["CurrSort"] = "Name: A-Z";
                    break;
            }

            const int pageSize = 9;
            if (pg < 1)
            {
                pg = 1;
            }

            int TotalRecords = Products.Count();
            var pager = new Pagination(TotalRecords, pg, pageSize);
            int SkippedRecords = (pg - 1) * pageSize;

            Products = Products.Skip(SkippedRecords).Take(pager.PageSize);

            pager.ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            pager.ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            this.ViewBag.Pager = pager;

            return View(await Products.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductsList(string searchString, string currentFilter, string sortOrder, int? CategoryId, int pg = 1)
        {
            if (searchString == null)
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CategoryId"] = CategoryId;
            ViewData["sortOrder"] = sortOrder;


            var Parameters = new Dictionary<string, string>();
            Parameters.Add("CategoryId", CategoryId.ToString());
            Parameters.Add("sortOrder", sortOrder);
            Parameters.Add("CurrentFilter", searchString);


            this.ViewBag.ParametersDictionary = Parameters;

            var Products = from s in _context.Product
                           select s;
            Products = Products.Include(p => p.Category);

            if (CategoryId != null)
            {
                Products = Products.Where(d => d.CategoryId == CategoryId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                Products = Products.Where(a => a.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "NameDecending":
                    Products = Products.OrderByDescending(s => s.Name);
                    ViewData["CurrSort"] = "Name: Z-A";
                    break;
                case "PriceAcending":
                    Products = Products.OrderBy(s => s.Price);
                    ViewData["CurrSort"] = "Price: Low-High";
                    break;
                case "PriceDecending":
                    Products = Products.OrderByDescending(s => s.Price);
                    ViewData["CurrSort"] = "Price: High-Low";
                    break;
                default:
                    Products = Products.OrderBy(s => s.Name);
                    ViewData["CurrSort"] = "Name: A-Z";
                    break;
            }

            const int pageSize = 10;
            if (pg < 1)
            {
                pg = 10;
            }

            int TotalRecords = Products.Count();
            var pager = new Pagination(TotalRecords, pg, pageSize);
            int SkippedRecords = (pg - 1) * pageSize;

            Products = Products.Skip(SkippedRecords).Take(pager.PageSize);

            pager.ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            pager.ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            this.ViewBag.Pager = pager;

            return View(await Products.ToListAsync());
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(i => i.ImagesPaths)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> ProductPage(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(i => i.ImagesPaths)
                .FirstOrDefaultAsync(m => m.Name == name);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Cost,Price,Stock,CategoryId,Images")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Images != null)
                {
                    product.ImagesPaths = new List<ProductImage>();
                    string path = "Images/Products/" + product.Name + "/";

                    foreach (var file in product.Images)
                    {
                        await fileManager.UploadFile(path, file);
                        var ImagePath = new ProductImage()
                        {
                            Path = fileManager.FilePath
                        };
                        product.ImagesPaths.Add(ImagePath);
                    }
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProductsList));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Cost,Price,Stock,CategoryId,Images")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.Images != null)
                    {
                        product.ImagesPaths = new List<ProductImage>();
                        string path = "Images/Products/" + product.Name + "/";

                        foreach (var file in product.Images)
                        {
                            await fileManager.UploadFile(path, file);
                            var ImagePath = new ProductImage()
                            {
                                Path = fileManager.FilePath
                            };
                            product.ImagesPaths.Add(ImagePath);
                        }
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ProductsList));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }


        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            fileManager.DeleteFolder("Images/Products/" + product.Name);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProductsList));
        }

/*        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var image = await _context.ProductImage.FindAsync(id);
            fileManager.DeleteFile(image.Path);
            _context.ProductImage.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProductsList));
        }*/

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }

    }
}
