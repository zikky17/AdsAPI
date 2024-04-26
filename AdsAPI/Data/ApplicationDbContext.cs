using AdsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AdModel> Ads { get; set; }
    }
}
