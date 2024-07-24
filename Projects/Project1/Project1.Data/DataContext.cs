using Microsoft.EntityFrameworkCore;
using Project1.Models.Actor;

namespace Project1.Data {
    public class DataContext : DbContext {
        public DbSet<GameActor> Enemies => Set<GameActor>();
        
        protected override void OnConfiguring(DbContextOptionsBuilder pDBCOptionBuilder) {
            pDBCOptionBuilder.UseSqlServer(File.ReadAllText("../Project1.Data/ConnectionString"));
        }
    }
}