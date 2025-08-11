using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using inventoryapp.Models;

namespace inventoryapp.Data
{
    public class inventoryappContext : DbContext
    {
        public inventoryappContext (DbContextOptions<inventoryappContext> options)
            : base(options)
        {
        }

        public DbSet<inventoryapp.Models.Material> Material { get; set; } = default!;
    }
}
