using Microsoft.EntityFrameworkCore;
using HW.Models;

namespace HW.Data
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// 
        /// DataContext instance, you can execute SQL 
        ///  commands to insert, update, and delete data in the database.
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Fintech> Fintech { get; set; }
        public DbSet<FinTechModel> monthly_expenses { get; set; }

        public DbSet<CustomerService> customerservice { get; set; }
    }
}
