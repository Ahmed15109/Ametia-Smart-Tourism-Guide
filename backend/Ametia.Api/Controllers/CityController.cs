using Grad.DTO;
using Grad.Models;
using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private IRepoBase<Bank> _BankRepo;
        private IRepoBase<City> _city;
        private readonly AppDbContext _appDbContext;

        public CityController(IRepoBase<Bank> BankRepo, IRepoBase<City> city,AppDbContext appDbContext)
        {
            _BankRepo = BankRepo;
            _city = city;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async  Task<IActionResult> AddNewCity([FromBody]string ?city)
        {
            if (string.IsNullOrWhiteSpace(city) || !ModelState.IsValid)
            {
                return BadRequest("Invalid city name.");
            }
            city= city.ToUpper();
            City city2 = new City();
            city2.Name = city;
            await _city.CreateAsync(city2);

            return Ok("City ADD ");
        }
        [HttpGet("loadcity")]
        public async Task<IActionResult> LoadCityById(int id)
        {
             City? city = await _city.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetInt32("CityId",id);
            return Ok(city);

        }
        private async Task< List<Bank>> lstbanAsync(int id )
        {
            List<Bank>? lstbank = (List<Bank>) await _BankRepo.GetAsyncAll();
            lstbank = lstbank.Where(x => x.CityId == id).ToList();
            return lstbank;
        }
        [HttpPut("UpdateCity")]
        public async Task< IActionResult> UpdateCity(City city)
        {

            City? oldCity = await _city.GetByIdAsync(city.Id);
            if (oldCity == null)
            {
                return NotFound();
            }

            oldCity.Name= city.Name.ToUpper();
            await _city.UpdateAsync(oldCity);
            HttpContext.Session.Remove("CityId");
            return Ok("Updated Done");
        }
        [HttpDelete("DeleteCity/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                await _city.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch
            {
                return BadRequest("Cannot delete this city. It may be referenced by another table.");
            }
        }

        [HttpGet("GetCity")]
        public async Task<IActionResult> GetAllCity()
        {
            List<City> lstcity = (List<City>)await _city.GetAsyncAll();

            return Ok(lstcity);
        }
    }
}
