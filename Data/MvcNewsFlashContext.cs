using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MvcNewsFlash.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcNewsFlash.Data
{
    public class MvcNewsFlashContext : DbContext
    {
        public MvcNewsFlashContext(DbContextOptions<MvcNewsFlashContext> options)
            : base(options)
        {
        }
        public DbSet<News> News { get; set; }
    }
}
