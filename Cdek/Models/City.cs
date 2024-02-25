using System.Diagnostics;

namespace Cdek.Models
{
    public class City
    {
        public string cityName { get; set; } = String.Empty;
        public string cityCode { get; set; } = String.Empty;
        public string cityUuid { get; set; } = String.Empty;
        public string country { get; set; } = String.Empty;
        public string countryCode { get; set; } = String.Empty;
        public string region { get; set; } = String.Empty;
        public string regionCode { get; set; } = String.Empty;
        public string regionFiasGuid { get; set; } = String.Empty;
        public string subRegion { get; set; } = String.Empty;
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string kladr { get; set; } = String.Empty;
        public string fiasGuid { get; set; } = String.Empty;
        public double paymentLimit { get; set; }
        public string timezone { get; set; } = String.Empty;

    }
}


