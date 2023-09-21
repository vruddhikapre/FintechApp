using Microsoft.EntityFrameworkCore;
using FinTech.Models;

namespace FinTech.Data
    {
        /// <summary>
        /// Mapping data with the database
        /// </summary>
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<FinTechModel> monthly_expenses { get; set; }

        }

    }

