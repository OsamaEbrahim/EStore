using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EStore.Data;
using EStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using EStore.Services;

namespace EStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnviornment;
        private FilesManager fileManager;

        public CategoriesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            
            _context = context;
            _webHostEnviornment = hostEnvironment;
            fileManager = new FilesManager(hostEnvironment);
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            // this will return the username
           //ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View(await _context.Category.ToListAsync());
        }

        // GET: Categories
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CategoriesList()
        {
            return View(await _context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Thumbnail")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Thumbnail != null)
                {
                    string path = "Images/Categories/" + category.Name + "/";
                    await fileManager.UploadFile(path, category.Thumbnail);
                    category.ThumbnailPath = fileManager.FilePath;
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CategoriesList));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Thumbnail")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (category.Thumbnail != null)
                    {
                        string path = "Images/Categories/" + category.Name + "/";
                        fileManager.DeleteFolder(path);
                        await fileManager.UploadFile(path, category.Thumbnail);
                        category.ThumbnailPath = fileManager.FilePath;
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CategoriesList));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CategoriesList));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
