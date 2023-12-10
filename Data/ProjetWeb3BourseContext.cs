using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetWeb3Bourse.Models;

namespace ProjetWeb3Bourse.Data
{
    public class ProjetWeb3BourseContext : DbContext
    {
        public ProjetWeb3BourseContext (DbContextOptions<ProjetWeb3BourseContext> options)
            : base(options)
        {
        }

        public DbSet<ProjetWeb3Bourse.Models.Bourse> Bourse { get; set; } = default!;
    }
}
