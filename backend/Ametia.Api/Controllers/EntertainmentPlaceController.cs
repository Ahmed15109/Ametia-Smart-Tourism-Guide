using Grad.Models;
using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntertainmentPlaceController : ControllerBase
    {
        private IRepoBase<EntertainmentPlace> _entertainmentPlace;

        public EntertainmentPlaceController(IRepoBase<EntertainmentPlace> entertainmentPlace)
        {
            _entertainmentPlace = entertainmentPlace;
        }

        [HttpPost("CreateEntertainmentPlace")]
        public async Task<IActionResult> Create([FromForm] EntertainmentPlace? place)
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
            await _entertainmentPlace.CreateAsync(place);
            return Ok("EntertainmentPlace added successfully.");
        }


        [HttpGet("LoadEntertainmentPlaceById/{id}")]
        public async Task<IActionResult> LoadEntertainmentPlaceById(int id)
        {
            var place = await _entertainmentPlace.GetByIdAsync(id);
            if (place == null)
                return NotFound(); // مهم جداً

            return Ok(place);
        }

        [HttpPut("UpdateEntertainmentPlaceById")]
        public async Task<IActionResult> Update([FromForm] EntertainmentPlace? updatedPlace)
        {
            if (updatedPlace == null || !ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            int id = updatedPlace.Id;
            EntertainmentPlace? oldBank = await _entertainmentPlace.GetByIdAsync(id);
            if (oldBank == null)
            {
                return Ok($"No EntertainmentPlace found with ID {id}");
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
            oldBank.OpiningHour = updatedPlace.OpiningHour;
            oldBank.Longitude = updatedPlace.Longitude;
            oldBank.Latitude = updatedPlace.Latitude;
            oldBank.ContactInfo = updatedPlace.ContactInfo;
            oldBank.PlaceType = updatedPlace.PlaceType;

            await _entertainmentPlace.UpdateAsync(oldBank);
            return Ok("Update completed successfully");
        }

        [HttpDelete("DeleteEntertainmentPlace")]
        public async Task<IActionResult> DeleteEntertainmentPlace(int id)
        {
            try
            {
                await _entertainmentPlace.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot delete this EntertainmentPlace: " + ex.Message);
            }
        }
        [HttpGet("GetAllEmbasse")]
        public async Task<IActionResult> GetAllEntertainmentPlace()
        {
            List<EntertainmentPlace>? bankWithCitylist = (List<EntertainmentPlace>)await _entertainmentPlace.GetAsyncAll();

            return Ok(bankWithCitylist);
        }
    }
}
