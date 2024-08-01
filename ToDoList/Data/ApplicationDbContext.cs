using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Models.Item> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var bulider = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connect = bulider.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connect);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
