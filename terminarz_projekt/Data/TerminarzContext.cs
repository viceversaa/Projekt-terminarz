using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using terminarz_projekt.Models;

namespace terminarz_projekt.Data
{
    public class TerminarzContext : DbContext
    {
        public TerminarzContext (DbContextOptions<TerminarzContext> options)
            : base(options)
        {
        }

        public DbSet<terminarz_projekt.Models.UserModel> UserModel { get; set; } = default!;
        public DbSet<terminarz_projekt.Models.Osoby> Osoby { get; set; } = default!;
    }
}
