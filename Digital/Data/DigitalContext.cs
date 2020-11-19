using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Digital.Models;

namespace Digital.Data
{
    public class DigitalContext : DbContext
    {
        public DigitalContext (DbContextOptions<DigitalContext> options)
            : base(options)
        {
        }

        public DbSet<Digital.Models.Dados> Dados { get; set; }
    }
}
