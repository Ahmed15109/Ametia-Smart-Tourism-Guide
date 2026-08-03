using Grad.DTO;
using Grad.Models;
using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePlacesController : ControllerBase
    {
        private IRepoBase<Type_place> _TypeRepo;

        public TypePlacesController(IRepoBase<Type_place> TypeRepo)
        {
            _TypeRepo = TypeRepo;
        }

        [HttpPost("AddNewType_place")]
        public async Task<IActionResult> AddNewType_place([FromBody] Type_place? place)
        {
            if (!ModelState.IsValid || place == null)
            {
                return BadRequest("Invalid data.");
            }

            await _TypeRepo.CreateAsync(place); // تأكد إن CreateAsync بتنادي SaveChanges
            return Ok("Type_place added successfully.");
        }

        [HttpGet("loadType_place/{id}")]
        public async Task<IActionResult> LoadType_placeById(int id)
        {
            var type = await _TypeRepo.GetByIdAsync(id);

            if (type == null)
                return NotFound("Type_place not found");

            return Ok(type);
        }


        [HttpPut("UpdateType_place")]
        public async Task<IActionResult> UpdateType_place(Type_place city)
        {

            Type_place? oldCity = await _TypeRepo.GetByIdAsync(city.Id);
            if (oldCity == null)
            {
                return NotFound("Type_place not found");
            }

            oldCity.Name = city.Name.ToUpper();
            await _TypeRepo.UpdateAsync(oldCity);

            return Ok("Updated Done");
        }
        [HttpDelete("DeleteType_place")]
        public async Task<IActionResult> DeleteType_place(int city)
        {
            try
            {

                await _TypeRepo.DeleteAsync(city);
                return Ok("Deleted");
            }
            catch
            {
                return BadRequest("Canot Delete This City ");
            }
        }
        [HttpGet("GetType_place")]
        public async Task<IActionResult> GetAllType_place()
        {
            List<Type_place> bankWithCitylist = (List<Type_place>)await _TypeRepo.GetAsyncAll();

            return Ok(bankWithCitylist);
        }
    }
}
