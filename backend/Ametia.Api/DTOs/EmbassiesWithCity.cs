namespace Grad.DTO
{
    public class EmbassiesWithCity
    {

        public int Id { get; set; }
        public string EmbassiesName { get; set; } = null!;
        public string EmbassiesCountryName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public EmbassiesWithCity() { }
        public EmbassiesWithCity(int id,string embassiesName,string cityName,string EmbassiesCountryName)
        {
            Id = id;
            EmbassiesName = embassiesName;
            this.EmbassiesCountryName = EmbassiesCountryName;
            CityName = cityName;
        }

    }
}
