using AdsAPI.Data;
using AdsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AdModelController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        public AdModelController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // READ ALL ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ALL Ads from the database
        /// </summary>
        /// <returns>
        /// A full list of ALL Ads
        /// </returns>
        /// <remarks>
        /// Example end point: GET /Ad
        /// </remarks>
        /// <response code="200">
        /// Successfully returned a full list of ALL Ads
        /// </response>

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<List<AdModel>>> GetAll()
        {
            return Ok(await _dbContext.Ads.ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        [Route("{id}")]
        public async Task<ActionResult<AdModel>> GetOne(int id)
        {
            var ad = _dbContext.Ads.Find(id);

            if (ad == null)
            {
                return BadRequest("Ad not found");
            }
            return Ok(ad);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdModel>> PostAd(AdModel ad)
        {
            _dbContext.Ads.Add(ad);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Ads.ToListAsync());
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdModel>> UpdateAd(AdModel ad)
        {
            var adToUpdate = await _dbContext.Ads.FindAsync(ad.Id);

            if (adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            adToUpdate.Name = ad.Name;
            adToUpdate.Description = ad.Description;
            adToUpdate.Price = ad.Price;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Ads.ToListAsync());
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult<AdModel>> Delete(int id)
        {
            var adToDelete = await _dbContext.Ads.FindAsync(id);

            if (adToDelete == null)
            {
                return BadRequest("Ad not found");
            }

            _dbContext.Ads.Remove(adToDelete);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Ads.ToListAsync());
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult<AdModel>>
            PatchHero(JsonPatchDocument ad, int id)
        {
            var adToUpdate = await
                _dbContext.Ads.FindAsync(id);

            if (adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            ad.ApplyTo(adToUpdate);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Ads.ToListAsync());
        }

    }
}

