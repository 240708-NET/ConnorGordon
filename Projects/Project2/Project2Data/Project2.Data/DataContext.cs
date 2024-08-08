using Microsoft.EntityFrameworkCore;
using Project2.Models.Actor;
using Project2.Models.User;

namespace Project2.Data {
    public class DataContext : DbContext {
        public DbSet<ActorEnemy> Enemies => Set<ActorEnemy>();

        //  Constructor
        public DataContext() {

        }
        
        //  Constructor [For Non-API use]
        public DataContext(DbContextOptions<DataContext> pDBCOptions) : base(pDBCOptions) {
            
        }

        //  SubMethod of Constructor - On Configuring [For Non-API use]
        protected override void OnConfiguring(DbContextOptionsBuilder pDBCOptionsBuilder) {
            pDBCOptionsBuilder.UseSqlServer(File.ReadAllText("../Project2.Data/ConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder pModelBuilder) {
            pModelBuilder.Entity<GameUser>().HasMany(e => e.UserPlayers)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id);
        }
    }
}