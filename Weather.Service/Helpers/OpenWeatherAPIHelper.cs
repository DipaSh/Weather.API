using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Weather.Service.Helpers
{
    public static class OpenWeatherAPIHelper
    {
        readonly static string ApiKey = ConfigurationManager.AppSettings["OpenWeatherApiKey"];
        readonly static string ApiUrl = ConfigurationManager.AppSettings["OpenWeatherApiURL"];

        public static async Task<string> GetCityInfo(long cityId)
        {
            string result = null;
            try
            {
                string apiPath = ApiUrl.Replace("*CityId*", cityId.ToString()).Replace("*AppId*", ApiKey).Replace(",", "&");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(apiPath);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return result;
        }
    }
}