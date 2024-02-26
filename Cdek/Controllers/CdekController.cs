using Microsoft.AspNetCore.Mvc;
using CdekSdk;
using CdekSdk.DataContracts;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.Intrinsics.X86;
using Cdek.Models;
using Microsoft.EntityFrameworkCore;



namespace Cdek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CdekController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;
        public readonly City[]? _allCities;

        public CdekController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _allCities = GetCitiesAsync().Result;
        }
        
        [HttpGet("GetCities")]
        public IActionResult GetAllCities()
        {
            return (_allCities != null) ? Ok(_allCities) : BadRequest();
        }
        [HttpGet("GetPackages")]
        public IActionResult GetPackages()
        {
            var packages = _applicationContext.Set<Cargo>().ToList();
            return (packages != null) ? Ok(packages) : BadRequest();
        }

        [HttpPost("Price")]
        public async Task<IActionResult> GetPrice(Cargo package)
        {
            try
            {
                var tariff = GetTariff(package);
                _applicationContext.Set<Cargo>().AddRange(package);
                await Task.Run(() => _applicationContext.SaveChanges());
                return Ok(string.Concat((tariff != null) ? tariff.TotalSum : -1 , " ", (tariff != null) ? tariff.Currency : " "));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPriceById/{id}")]
        public ActionResult Get(long id)
        {
            var package = _applicationContext.Set<Cargo>().Find(id);

            if (package == null)
            {
                return NotFound();
            }
            try
            {
                var tariff = GetTariff(package);
                return Ok(string.Concat((tariff != null) ? tariff.TotalSum : -1, " ", (tariff != null) ? tariff.Currency : " "));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        private async Task<City[]?> GetCitiesAsync() {
            var client = new HttpClient();
            var result = await client.GetAsync(new Uri("http://integration.cdek.ru/v1/location/cities/json?"));
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.StatusCode.ToString());
            return JsonConvert.DeserializeObject<City[]>(result.Content.ReadAsStringAsync().Result);
        }

        private TariffResponse? GetTariff(Cargo package) {
            var client = new CdekClient { };
            int FromCity = 0;
            int ToCity = 0;
            if (_allCities != null)
            {
                foreach (var item in _allCities)
                {
                    if (item.fiasGuid == package.FromLocation) FromCity = Int32.Parse(item.cityCode);
                    if (item.fiasGuid == package.ToLocation) ToCity = Int32.Parse(item.cityCode);
                }
            }
            var tariff =  client.CalculateTariff(new TariffRequest
            {
                TariffCode = 480, // as returned by CalculateTariffList
                DeliveryType = DeliveryType.Delivery,
                FromLocation = new Location { CityCode = FromCity },
                ToLocation = new Location { CityCode = ToCity },
                Packages =
                {
                    new PackageSize
                    {
                        Weight = (int)(package.Weight * 1000),
                        Height = (int)(package.Height * 100),
                        Width = (int)(package.Width * 100),
                        Length = (int)(package.Length * 100)
                    }
                }
            });
            return tariff;
        }

    }
}
