using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private  IRepoBase<Hotel> _hotelRepo;

        public HotelsController(IRepoBase<Hotel> hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        [HttpPost("CreateHotel")]
        public async Task<IActionResult> Create([FromForm] Hotel? place)
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
            await _hotelRepo.CreateAsync(place);
            return Ok("EntertainmentPlace added successfully.");
        }


        [HttpGet("LoadHotelById/{id}")]
        public async Task<IActionResult> LoadHotelById(int id)
        {
            var place = await _hotelRepo.GetByIdAsync(id);
            if (place == null)
                return NotFound(); // مهم جداً

            return Ok(place);
        }

        [HttpPut("UpdateHotelById")]
        public async Task<IActionResult> Update([FromForm] Hotel? updatedPlace)
        {
            if (updatedPlace == null || !ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            int id = updatedPlace.Id;
            Hotel? oldBank = await _hotelRepo.GetByIdAsync(id);
            if (oldBank == null)
            {
                return Ok($"No Hotel found with ID {id}");
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
            oldBank.PhoneNumber = updatedPlace.PhoneNumber;
            oldBank.Longitude = updatedPlace.Longitude;
            oldBank.Latitude = updatedPlace.Latitude;
            oldBank.Rating = updatedPlace.Rating;
            oldBank.ScialMedia = updatedPlace.ScialMedia;

            await _hotelRepo.UpdateAsync(oldBank);
            return Ok("Update completed successfully");
        }

        [HttpDelete("DeleteHotel")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            try
            {
                await _hotelRepo.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot delete this Hotel: " + ex.Message);
            }
        }
        [HttpGet("GetAllHotel")]
        public async Task<IActionResult> GetAllHotel()
        {
            List<Hotel>? bankWithCitylist = (List<Hotel>)await _hotelRepo.GetAsyncAll();

            return Ok(bankWithCitylist);
        }
    }
}
