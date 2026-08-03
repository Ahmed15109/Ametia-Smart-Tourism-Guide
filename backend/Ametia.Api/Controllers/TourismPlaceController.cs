using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourismPlaceController : ControllerBase
    {
        private readonly IRepoBase<Tourismt_Place> _placeRepo;

        public TourismPlaceController(IRepoBase<Tourismt_Place> placeRepo)
        {
            _placeRepo = placeRepo;
        }

        [HttpPost("CreateTourismPlace")]
        public async Task<IActionResult> CreateTourismPlace([FromForm] Tourismt_Place? place)
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
            await _placeRepo.CreateAsync(place);
            return Ok("Restaurant.h added successfully.");
        }


        [HttpGet("LoadTourismt_PlaceById/{id}")]
        public async Task<IActionResult> LoadTourismt_PlaceById(int id)
        {
            var place = await _placeRepo.GetByIdAsync(id);
            if (place == null)
                return NotFound(); // مهم جداً

            return Ok(place);
        }

        [HttpPut("UpdateTourismt_PlaceById")]
        public async Task<IActionResult> Update([FromForm] Tourismt_Place? updatedPlace)
        {
            if (updatedPlace == null || !ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            int id = updatedPlace.Id;
            Tourismt_Place? oldBank = await _placeRepo.GetByIdAsync(id);
            if (oldBank == null)
            {
                return Ok($"No Tourismt_Place found with ID {id}");
            }
            if (updatedPlace.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updatedPlace.ImageFile.CopyToAsync(memoryStream);
                    updatedPlace.ImageBytes = memoryStream.ToArray();                 }
                oldBank.ImageBytes = updatedPlace.ImageBytes;
            }

            oldBank.Name = updatedPlace.Name.ToUpper();
            oldBank.CityId = updatedPlace.CityId;
            oldBank.TicketPrice = updatedPlace.TicketPrice;
            oldBank.Longitude = updatedPlace.Longitude;
            oldBank.Latitude = updatedPlace.Latitude;
            oldBank.Rating = updatedPlace.Rating;
            oldBank.Typeofplaceid = updatedPlace.Typeofplaceid;
            oldBank.Discription = updatedPlace.Discription;


            await _placeRepo.UpdateAsync(oldBank);
            return Ok("Update completed successfully");
        }

        [HttpDelete("DeleteTourismt_Place")]
        public async Task<IActionResult> DeleteTourismt_Place(int id)
        {
            try
            {
                await _placeRepo.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot delete this bank: " + ex.Message);
            }
        }

        [HttpGet("GetAllTourismt_Place")]
        public async Task<IActionResult> GetAllTourismt_Place()
        {
            List<Tourismt_Place>? bankWithCitylist = (List<Tourismt_Place>)await _placeRepo.GetAsyncAll();

            return Ok(bankWithCitylist);
        }

    }
}
