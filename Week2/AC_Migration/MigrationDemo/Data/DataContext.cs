using Microsoft.EntityFrameworkCore;
using MigrationDemo.Models;

namespace MigrationDemo.Data {
    public class DataContext : DbContext {
        public DbSet<Departments> departments { get; set; }
        public DbSet<Employees> employees { get; set; }

        public DataContext(DbContextOptions<DataContext> pDBCOptions) : base(pDBCOptions) {

        }
    }
}