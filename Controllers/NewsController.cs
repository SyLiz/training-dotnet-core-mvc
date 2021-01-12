using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcNewsFlash.Data;
using MvcNewsFlash.Models;

using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MvcNewsFlash.Controllers
{
    public class NewsController : Controller
    {
        private readonly MvcNewsFlashContext _context; //<-- Access database
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(MvcNewsFlashContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: News
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<News>.CreateAsync(_context.News, pageNumber ?? 1, 8));
            //return View(await _context.News.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Description,ImageFile")] News news)
        {
            if (ModelState.IsValid)
            {
                // Save Image to wwwRoot/Images
                string wwwRootPath = _hostEnvironment.WebRootPath; //get directory of wwwRoot
                string fileName = "temp-49a23190f0f8c50a56d054ed4e855f44.jpeg";
                if (news.ImageFile != null)
                {
                    fileName = Path.GetFileNameWithoutExtension(news.ImageFile.FileName); // get file name only
                    string extension = Path.GetExtension(news.ImageFile.FileName); // .jpg .png ETC.

                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension; // stored ImagePath to Database

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using var filestream = new FileStream(path, FileMode.Create);
                    await news.ImageFile.CopyToAsync(filestream);
                }

                //insert record
                news.ImagePath = fileName;

                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Description,ImagePath,ImageFile")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                // Save Image to wwwRoot/Images
                string wwwRootPath = _hostEnvironment.WebRootPath; //get directory of wwwRoot
                string fileName = news.ImagePath;
                if (news.ImageFile != null)
                {
                    fileName = Path.GetFileNameWithoutExtension(news.ImageFile.FileName); // get file name only
                    string extension = Path.GetExtension(news.ImageFile.FileName); // .jpg .png ETC.

                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension; // stored ImagePath to Database

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using var filestream = new FileStream(path, FileMode.Create);
                    await news.ImageFile.CopyToAsync(filestream);

                    //delete old image from wwwroot/Images
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", news.ImagePath);
                    if (System.IO.File.Exists(imagePath) && news.ImagePath != "temp-49a23190f0f8c50a56d054ed4e855f44.jpeg")
                        System.IO.File.Delete(imagePath);


                }

                //string fileName = Path.GetFileNameWithoutExtension(news.ImageFile.FileName); // get file name only

                //insert record
                news.ImagePath = fileName;

                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);

            //delete image from wwwroot/Images
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", news.ImagePath);
            if (System.IO.File.Exists(imagePath) && news.ImagePath != "temp-49a23190f0f8c50a56d054ed4e855f44.jpeg")
                System.IO.File.Delete(imagePath);

            //delete record from Database
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
