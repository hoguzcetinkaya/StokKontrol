using Abstraction.Options;
using CustomerService.Services;
using Microsoft.EntityFrameworkCore;
using StokKontrol.Data;

namespace CustomerService.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddStokDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StokDbContext>(options =>
            {
                PostgreConnectionOption? postgreOptions = configuration.GetSection("PostgreConnectionOption").Get<PostgreConnectionOption>();
                var connectionString = $"Host={postgreOptions!.Host};Database={postgreOptions.Database};Username={postgreOptions.Username};Password={postgreOptions.Password}";
                options.UseNpgsql(connectionString);
            });
            // Host=localhost;Database=ProductDb;Username=postgres;Password=a1s2d3

            services.AddScoped<ICustomerService, CustomerProvider>();
            return services;
        }

        
    }
    public class StokDbContext : DbContext
    {
        public StokDbContext(DbContextOptions<StokDbContext> options) : base(options)
        {
        }
        public DbSet<Product>       Products      { get; set; }
        public DbSet<Category>      Categories    { get; set; }
        public DbSet<Supplier>      Suppliers     { get; set; }
        public DbSet<Order>         Orders        { get; set; }
        public DbSet<Customer>      Customers     { get; set; }
    }
}
