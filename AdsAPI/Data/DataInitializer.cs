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
            if (!_dbContext.Ads.Any(a => a.Title == "Bicycle"))
            {
                _dbContext.Add(new AdModel
                {
                    Title = "Bicycle",
                    Description = "Brand new bicycle for sale!",
                    Price = 500,
                    Created = DateTime.Now,
                });
            }

            if (!_dbContext.Ads.Any(a => a.Title == "Video Game"))
            {
                _dbContext.Add(new AdModel
                {
                    Title = "Video Game",
                    Description = "Retro Pac man game for sale!",
                    Price = 2500,
                    Created = DateTime.Now,
                });
            }
        }
    }
}
