using Microsoft.EntityFrameworkCore;
using Project1.Models.Actor;

namespace Project1.Data {
    public class DataContext : DbContext {
        public DbSet<GameActor> Enemies => Set<GameActor>();
        
        public DataContext(DbContextOptions<DataContext> pDBCOptions) : base(pDBCOptions) {
            
        }
    }
}