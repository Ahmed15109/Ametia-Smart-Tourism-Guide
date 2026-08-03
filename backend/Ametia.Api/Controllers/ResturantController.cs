using Grad.DTO;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantController : ControllerBase
    {
        private IRepoBase<Restaurant> _restaurant;
        private IRepoBase<City> _city;

        public ResturantController(IRepoBase<Restaurant> restaurant, IRepoBase<City> city)
        {
            _restaurant = restaurant;
            _city = city;
        }

        [HttpPost("CreateRestaurant")]
        public async Task<IActionResult> Create([FromForm] Restaurant? place)
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
            await _restaurant.CreateAsync(place);
            return Ok("Restaurant.h added successfully.");
        }


        [HttpGet("LoadRestaurantById/{id}")]
        public async Task<IActionResult> LoadRestaurantById(int id)
        {
            var place = await _restaurant.GetByIdAsync(id);
            if (place == null)
                return NotFound(); // مهم جداً

            return Ok(place);
        }

        [HttpPut("UpdateRestaurantById")]
        public async Task<IActionResult> Update([FromForm] Restaurant? updatedPlace)
        {
            if (updatedPlace == null || !ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            int id = updatedPlace.Id;
            Restaurant? oldBank = await _restaurant.GetByIdAsync(id);
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
            oldBank.OpiningHour = updatedPlace.OpiningHour;
            oldBank.TypeOfFood = updatedPlace.TypeOfFood;

            await _restaurant.UpdateAsync(oldBank);
            return Ok("Update completed successfully");
        }

        [HttpDelete("DeleteRestaurant")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            try
            {
                await _restaurant.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot delete this Restaurant: " + ex.Message);
            }
        }
        [HttpGet("GetAllRestaurant")]
        public async Task<IActionResult> GetAllRestaurant()
        {
            List<Restaurant>? bankWithCitylist = (List<Restaurant>)await _restaurant.GetAsyncAll();

            return Ok(bankWithCitylist);
        }
    }
}
