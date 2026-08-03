using Grad.Models;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistnationController : ControllerBase
    {
        private IRepoBase<City> _city;
        private IRepoBase<Bank> _bank;
        private IRepoBase<Embasse> _embasse;
        private IRepoBase<EntertainmentPlace> _entertainmentPlace;
        private IRepoBase<Hotel> _hotel;
        private IRepoBase<Restaurant> _restaurant;
        private IRepoBase<Tourismt_Place> _tourismt_place;
        private IRepoBase<TransportProvider> _transprovider;

        public DistnationController(IRepoBase<City> city, IRepoBase<Bank> Bank, IRepoBase<Embasse> embasse, IRepoBase<EntertainmentPlace> Ent, IRepoBase<Hotel> hotl, IRepoBase<Restaurant> rest, IRepoBase<Tourismt_Place> Tp, IRepoBase<TransportProvider> TPR)
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
        [HttpGet("Tack one city from list")]
        public async Task<List<City>> AllCityForDistination()
        {

            List<City>? cities = (List<City>)await _city.GetAsyncAll();
            return cities;
        }
        [HttpGet("TopReatingBank/{id}")]
        public async Task<List<Bank>> TopReatingBank(int id)
        {
            var banks = await _bank.GetAsyncAll();

            if (banks == null || !banks.Any())
                return new List<Bank>();

            return banks
                .Where(b => b.CityId == id)
                .OrderByDescending(b => b.Rating)
                .ToList();
        }

        [HttpGet("TopHotel/{id}")]
        public async Task<List<Hotel>> TopHotel(int id)
        {
            List<Hotel>? Hotel = (List<Hotel>)await _hotel.GetAsyncAll();
            int cityid = id;
            Hotel = Hotel.Where(x => x.CityId == cityid).OrderByDescending(x => x.Rating).ToList();

            return Hotel;

        }
        [HttpGet("TopRestaurant/{id}")]
        public async Task<List<Restaurant>> TopRestaurant(int id)
        {
            List<Restaurant>? res = (List<Restaurant>)await _restaurant.GetAsyncAll();
            int cityid = id;
            res = res.Where(x => x.CityId == cityid).OrderByDescending(x => x.Rating).ToList();

            return res;

        }
         [HttpGet("TopTourismt_Place/{id}")]
        public async Task<List<Tourismt_Place>> TopTourismt_Place(int id)
        {
            List<Tourismt_Place>? Tourismt_Place = (List<Tourismt_Place>)await _tourismt_place.GetAsyncAll();
            int cityid = id;

            Tourismt_Place = Tourismt_Place.Where(x => x.CityId == cityid).OrderByDescending(x => x.Rating).ToList();

            return Tourismt_Place;

        }
        [HttpGet("TopEmbassies/{id}")]
        public async Task<List<Embasse>> TopEmbassies(int id)
        {
            List<Embasse>? Tourismt_Place = (List<Embasse>)await _embasse.GetAsyncAll();
            int cityid = id;

            Tourismt_Place = Tourismt_Place.Where(x => x.CityId == cityid).ToList();

            return Tourismt_Place;

        }
         [HttpGet("TopEntertainmentPlace/{id}")]
        public async Task<List<EntertainmentPlace>> TopEntertainmentPlace(int id)
        {
            List<EntertainmentPlace>? Tourismt_Place = (List<EntertainmentPlace>)await _entertainmentPlace.GetAsyncAll();
            int cityid = id;

            Tourismt_Place = Tourismt_Place.Where(x => x.CityId == cityid).ToList();

            return Tourismt_Place;

        }

    }
}
