using EFConnStringPOC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFConnStringPOC.Context
{
    public class MainDbContext : DbContext
    {
        private readonly IHttpContextAccessor accessor;

        public MainDbContext(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        private string GetConnectionString()
        {
            // -> default connection string
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFConnStringPOC;Trusted_Connection=True;MultipleActiveResultSets=true";

            // -> the conn string will be based on the url, example:
            //      mycustomer.webapp.com
            //      in the example above, I have to get "mycustomer" (first piece) of the url string.
            //      this key will be the "tenant".
            //      with this info I can compose the new conn string.
            var host = accessor?.HttpContext?.Request?.Host.ToString();

            if (!string.IsNullOrWhiteSpace(host) && host.Contains("."))
            {
                var tenantKey = host.Substring(0, host.IndexOf("."));
                // -> ... compose new connection string
                //connectionString = $"Server=(localdb)\\mssqllocaldb;Database={tenantKey}EFConnStringPOC;Trusted_Connection=True;MultipleActiveResultSets=true";
            }

            return connectionString;
        }
    }
}
