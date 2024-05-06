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
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AdsController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        public AdsController(ApplicationDbContext dbContext)
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
        /// <response code="401">
        /// You are not authenticated (Log in first)
        /// </response>

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<List<AdModel>>> GetAll()
        {
            return Ok(await _dbContext.Ads.ToListAsync());
        }

        // READ ONE ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ONE Ad from the database
        /// </summary>
        /// <returns>
        /// A single ad
        /// </returns>
        /// <remarks>
        /// Example end point: GET /{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned the ad you requested
        /// </response>
        /// <response code="401">
        /// You are not authenticated (Log in first)
        /// </response>

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

        // CREATE ///////////////////////////////////////////////////////
        /// <summary>
        /// Create an ad and save it to the database
        /// </summary>
        /// <returns>
        /// A created Ad
        /// </returns>
        /// <remarks>
        /// Example end point: POST /Ad
        /// </remarks>
        /// <response code="200">
        /// Successfully created your Ad!
        /// </response>
        /// <response code="401">
        /// You are not authenticated (Log in first)
        /// </response>

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdModel>> PostAd(AdModel ad)
        {
            _dbContext.Ads.Add(ad);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Ads.ToListAsync());
        }

        // UPDATE ///////////////////////////////////////////////////////
        /// <summary>
        /// Updates ALL information about the Ad
        /// </summary>
        /// <returns>
        /// An updated Ad
        /// </returns>
        /// <remarks>
        /// Example end point: PUT /Ad
        /// </remarks>
        /// <response code="200">
        /// Successfully updated your Ad!
        /// </response>
        /// <response code="401">
        /// You are not authenticated (Log in first)
        /// </response>

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdModel>> UpdateAd(AdModel ad)
        {
            var adToUpdate = await _dbContext.Ads.FindAsync(ad.Id);

            if (adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            adToUpdate.Title = ad.Title;
            adToUpdate.Description = ad.Description;
            adToUpdate.Price = ad.Price;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Ads.ToListAsync());
        }

        // DELETE ///////////////////////////////////////////////////////
        /// <summary>
        /// Deletes the Ad
        /// </summary>
        /// <returns>
        /// A deleted Ad
        /// </returns>
        /// <remarks>
        /// Example end point: DELETE /{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully deleted your Ad!
        /// </response>
        /// <response code="401">
        /// You are not authenticated (Log in first)
        /// </response>

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

        // PATCH ///////////////////////////////////////////////////////
        /// <summary>
        /// Updates specific information about an Ad
        /// </summary>
        /// <returns>
        /// An updated Ad
        /// </returns>
        /// <remarks>
        /// Example end point: PATCH /Ad {id}
        /// </remarks>
        /// <response code="200">
        /// Successfully updated your Ad!
        /// </response>
        /// <response code="401">
        /// You are not authenticated (Log in first)
        /// </response>

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

