using AdsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdsAPI.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
            _dbContext.SaveChanges();
        }

        private void SeedData()
        {
            if (!_dbContext.Ads.Any(a => a.Name == "Bicycle"))
            {
                _dbContext.Add(new AdModel
                {
                    Name = "Bicycle",
                    Description = "Brand new bicycle for sale!",
                    Price = 500,
                });
            }

            if (!_dbContext.Ads.Any(a => a.Name == "Video Game"))
            {
                _dbContext.Add(new AdModel
                {
                    Name = "Video Game",
                    Description = "Retro Pac man game for sale!",
                    Price = 2500,
                });
            }
        }
    }
}
