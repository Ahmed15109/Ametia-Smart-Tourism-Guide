using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Grad.Models;
using System.Threading.Tasks;
using Grad.DTO;
using Microsoft.EntityFrameworkCore;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbassiesController : ControllerBase
    {
        private  IRepoBase<Embasse> _embassyRepo;
        private  IRepoBase<City> _city;

        public EmbassiesController(IRepoBase<Embasse> embassyRepo, IRepoBase<City> city)
        {
            _embassyRepo = embassyRepo;
            _city = city;
        }

        [HttpPost("CreateEmbasses")]
        public async Task<IActionResult> Create([FromForm] Embasse? place)
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
            await _embassyRepo.CreateAsync(place);
            return Ok("Embasse added successfully.");
        }


        [HttpGet("LoadEmbasseById/{id}")]
        public async Task<IActionResult> LoadEmbasseById(int id)
        {
            var embasse = await _embassyRepo.GetByIdAsync(id);
            if (embasse == null)
                return NotFound();
            return Ok(embasse);
        }


        [HttpPut("UpdateEmbasseById")]
        public async Task<IActionResult> Update([FromForm] Embasse? updatedPlace)
        {
            if (updatedPlace == null || !ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            int id = updatedPlace.Id;
            Embasse? oldBank = await _embassyRepo.GetByIdAsync(id);
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
            oldBank.CityId = updatedPlace.CityId;
            oldBank.WorkingHours = updatedPlace.WorkingHours;
            oldBank.Longitude = updatedPlace.Longitude;
            oldBank.Latitude = updatedPlace.Latitude;
            oldBank.ContactInfo = updatedPlace.ContactInfo;
            oldBank.Country = updatedPlace.Country;

            await _embassyRepo.UpdateAsync(oldBank);
            return Ok("Update completed successfully");
        }

        [HttpDelete("DeleteEmbasse/{id}")]
        public async Task<IActionResult> DeleteEmbasse(int id)
        {
            try
            {
                await _embassyRepo.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot delete this bank: " + ex.Message);
            }
        }
        [HttpGet("GetAllEmbasse")]
        public async Task<IActionResult> GetAllEmbasse()
        {
            List<Embasse>? bankWithCitylist = (List<Embasse>)await _embassyRepo.GetAsyncAll();

            return Ok(bankWithCitylist);
        }
    }
}
