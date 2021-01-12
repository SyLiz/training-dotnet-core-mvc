using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcNewsFlash.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using MvcNewsFlash.Data;

namespace MvcNewsFlash.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcNewsFlashContext _context; //<-- Access database

        public HomeController(ILogger<HomeController> logger, MvcNewsFlashContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<News>.CreateAsync(_context.News.OrderByDescending(x => x.Id), pageNumber ?? 1, 4));
            //return View(await _context.News.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
