using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Grad.Models;
using Grad.Repo.Base;
using WebApplication4.Models;
using Grad.DTO;
using Microsoft.EntityFrameworkCore;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private IRepoBase<Bank> _BankRepo;
        private IRepoBase<City> _city;
        private readonly AppDbContext _appDbContext;

        public BankController(IRepoBase<Bank> BankRepo, IRepoBase<City> city,AppDbContext appDbContext)
        {
            _BankRepo = BankRepo;
            _city = city;
            _appDbContext = appDbContext;
        }

        [HttpPost("CreateBank")]
        public async Task<IActionResult> Create([FromForm] Bank?  place)
        {

            if (place == null || !ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }
            if (place.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await place.ImageFile.CopyToAsync(memoryStream);
                    place.ImageBytes = memoryStream.ToArray(); // ✅ Store image as byte[]
                }
            }

            place.Name = place.Name.ToUpper();
            await _BankRepo.CreateAsync(place);
            return Ok("Bank added successfully.");
        }

        // Return all Banks
        [HttpGet("GetAllBanks")]
        public async Task<IActionResult> GetAllBanks()
        {
            List<Bank>? bankWithCitylist = (List<Bank>)await _BankRepo.GetAsyncAll();

            return Ok(bankWithCitylist);
        }
        // Returns a Bank through id
        [HttpGet("LoadBankById/{id}")]
        public async Task<IActionResult> LoadBankById(int id)
        {
            var place = await _BankRepo.GetByIdAsync(id);
            if (place == null)
                return NotFound(); // مهم جداً

            return Ok(place);
        }
        [HttpPut("UpdateBankById")]
        public async Task<IActionResult> Update([FromForm] Bank ? updatedPlace)
        {
            if (updatedPlace == null || !ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            int id = updatedPlace.Id;
            Bank? oldBank = await _BankRepo.GetByIdAsync(id);
            if (oldBank == null)
            {
                return Ok($"No bank found with ID {id}");
            }
            if (updatedPlace.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updatedPlace.ImageFile.CopyToAsync(memoryStream);
                    updatedPlace.ImageBytes = memoryStream.ToArray(); // ✅ Store image as byte[]
                }
               oldBank.ImageBytes = updatedPlace.ImageBytes;
            }

            oldBank.Name = updatedPlace.Name.ToUpper();
            oldBank.Rating = updatedPlace.Rating;
            oldBank.ScialMedia = updatedPlace.ScialMedia;
            oldBank.Longitude = updatedPlace.Longitude;
            oldBank.Latitude = updatedPlace.Latitude;
            oldBank.CityId = updatedPlace.CityId;

            await _BankRepo.UpdateAsync(oldBank);
            return Ok("Update completed successfully");
        }
        [HttpDelete("DeleteBank")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            try
            {
                await _BankRepo.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot delete this bank: " + ex.Message);
            }
        }

    }
}
