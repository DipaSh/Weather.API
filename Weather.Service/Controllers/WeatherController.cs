using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Weather.Service.Helpers;

namespace Weather.Service.Controllers
{
    public class WeatherController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> RetrieveWeatherINFOAsync(string citiesFile)
        {
            //Read input file
            Dictionary<long, string> dctCities = FileHelper.RetrieveCities(citiesFile);

            //Call OpenWeather API 
            foreach (var city in dctCities)
            {
                string result = await OpenWeatherAPIHelper.GetCityInfo(city.Key);
                //Store the results in Output folder
                FileHelper.SaveAPIResponse(city.Key, result);

            }

            return Ok();
            
        }
    }
}
