using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Grad.Models;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private IRepoBase<City> _city;
        private IRepoBase<Bank> _bank;
        private IRepoBase<Embasse> _embasse;
        private IRepoBase<EntertainmentPlace> _entertainmentPlace;
        private IRepoBase<Hotel> _hotel;
        private IRepoBase<Restaurant> _restaurant;
        private IRepoBase<Tourismt_Place> _tourismt_place;
        private IRepoBase<TransportProvider> _transprovider;

        public ServicesController(IRepoBase<City>city, IRepoBase<Bank> Bank, IRepoBase<Embasse> embasse, IRepoBase<EntertainmentPlace> Ent, IRepoBase<Hotel> hotl, IRepoBase<Restaurant> rest, IRepoBase<Tourismt_Place> Tp, IRepoBase<TransportProvider> TPR)
        {
            _city = city;
            _bank = Bank;
            _embasse = embasse;
            _entertainmentPlace = Ent;
            _hotel = hotl;
            _transprovider = TPR;
            _tourismt_place = Tp;
            _restaurant = rest;

        }
        [HttpGet("GetCity")]
        public async Task<IActionResult> GetAllcity()
        {
            return Ok(await _city.GetAsyncAll());
        }
         [HttpGet("GetBank")]
        public async Task<IActionResult> GetAllBank()
        {
            return Ok(await _bank.GetAsyncAll());
        }
         [HttpGet("GetEmbasses")]
        public async Task<IActionResult> GetAllEmbasse()
        {
            return Ok(await _embasse.GetAsyncAll());
        }
        [HttpGet("GetEntartinmentPlace")]
        public async Task<IActionResult> GetAllEntartinmentPlace()
        {
            return Ok(await _entertainmentPlace.GetAsyncAll());
        }
        [HttpGet("GetHotel")]
        public async Task<IActionResult> GetAllHotel()
        {
            return Ok(await _hotel.GetAsyncAll());
        }
         [HttpGet("GetRestuarant")]
        public async Task<IActionResult> GetAllRestaurent()
        {
            return Ok(await _restaurant.GetAsyncAll());
        }
         [HttpGet("GetToursimPlaces")]
        public async Task<IActionResult> GetAllTourisemPlace()
        {
            return Ok(await _tourismt_place.GetAsyncAll());
        }
        [HttpGet("Get All TransPort Provider")]
        public async Task<IActionResult> GetAllTransProvider()
        {
            return Ok(await _transprovider.GetAsyncAll());
        }

    }
}
