using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using apprueba.Models;

namespace apprueba.Data
{
    public class appruebaContext : DbContext
    {
        public appruebaContext (DbContextOptions<appruebaContext> options)
            : base(options)
        {
        }

        public DbSet<apprueba.Models.Producto> Producto { get; set; } = default!;

        public DbSet<apprueba.Models.Orden>? Orden { get; set; }
    }
}
