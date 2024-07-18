using Microsoft.EntityFrameworkCore;

namespace HelloEF {
    class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Hello Again!");

            Pet myPet = new Pet{Name = "Sil", Cuteness = 9, Chaos = 100, Species = "Cat"};
            System.Console.WriteLine(myPet.Speak());

            //string ConnectionString = File.ReadAllText("./connectionstring");

            //DbContextOptionsBuilder<DataContext> ContextOptions = new DbContextOptionsBuilder<DataContext>().UseSqlServer(ConnectionString);
            //DataContext Context = new DataContext(ContextOptions.Options);

            //DbContextOptions<DataContext> ContextOptions = new DbContextOptionsBuilder<DataContext>().UseSqlServer(ConnectionString).Options;
            //DataContext Context = new DataContext(ContextOptions);
        }
    }

    class Pet {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cuteness { get; set; }
        public long Chaos { get; set; }
        public string Species { get; set; }

        public string Speak() {
            return $"Hello, I'm {this.Name}";
        }
    }

    class DataContext : DbContext {
        public DbSet<Pet> Pets => Set<Pet>();
        /*
        public DataContext(DbContextOptions<DataContext> pDBCOptions) : base(pDBCOptions) {

        }
        */

        protected override void OnConfiguring(DbContextOptionsBuilder pDBCOptionsBuilder) {
            string ConnectionString = File.ReadAllText("./connectionstring");
            pDBCOptionsBuilder.UseSQLServer(ConnectionString);
        }
    }
}