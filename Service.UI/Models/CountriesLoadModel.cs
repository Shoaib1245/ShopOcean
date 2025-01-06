namespace Service.UI.Models
{
    public class CountriesLoadModel
    {
        public class CountryInfo
        {
            public string Country { get; set; }
            public string Region { get; set; }
        }
        public class CountryResponse
        {
            public string Status { get; set; }
            public Dictionary<string, CountryInfo> Data { get; set; }
        }
    }
}
