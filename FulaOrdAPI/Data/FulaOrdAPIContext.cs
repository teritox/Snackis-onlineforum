using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FulaOrdAPI.Models;

namespace FulaOrdAPI.Data
{
    public class FulaOrdAPIContext : DbContext
    {
        public FulaOrdAPIContext (DbContextOptions<FulaOrdAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FulaOrdAPI.Models.FulaOrd> FulaOrd { get; set; }
    }
}
