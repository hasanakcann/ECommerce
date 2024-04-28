using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entities;
using System.Data;

namespace MultiShop.Discount.Context
{
    public class DapperContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        ///     Tabloların veritabanına yansıtılması için aşağıda bağlantı kurulur.
        ///     https://learn.microsoft.com/tr-tr/ef/core/dbcontext-configuration/
        /// </summary>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=TR-ADN-JL50733;initial Catalog=MultiShopDiscountDb;integrated Security=true; Trusted_Connection = SSPI; Encrypt = false; TrustServerCertificate = true");
        //}

        public DbSet<Coupon> Coupons { get; set; }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
 