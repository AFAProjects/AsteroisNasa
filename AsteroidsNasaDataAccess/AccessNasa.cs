
using System.Net.Http;
using System.Web;
using AsteroidsNasaBussiness.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsteroidsNasaDataAccess
{
    public class AccessNasa : IAccessNasa
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccessNasa(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Asteroid>> GetAsteroids(int days)
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(days).ToString("yyyy-MM-dd");

            string requestUrl = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";

            HttpClient client = _clientFactory.CreateClient();
            string response = await client.GetStringAsync(requestUrl);

            var data = JsonConvert.DeserializeObject<RootObject>(response);

            if (data != null && data.near_earth_objects != null)
            {
                List<Asteroid> asteroids = new List<Asteroid>();

                foreach (var dateEntry in data.near_earth_objects)
                {
                    foreach (var asteroidData in dateEntry.Value)
                    {
                        if (asteroidData.close_approach_data != null && asteroidData.close_approach_data.Count > 0)
                        {
                            var closeApproach = asteroidData.close_approach_data[0];

                            var asteroid = new Asteroid
                            {
                                id = asteroidData.id,
                                name = asteroidData.name,
                                neo_reference_id = asteroidData.neo_reference_id,
                                nasa_jpl_url = asteroidData.nasa_jpl_url,
                                absolute_magnitude_h = asteroidData.absolute_magnitude_h,
                                estimated_diameter = asteroidData.estimated_diameter,
                                is_potentially_hazardous_asteroid = asteroidData.is_potentially_hazardous_asteroid,
                                is_sentry_object = asteroidData.is_sentry_object,
                                close_approach_data = asteroidData.close_approach_data
                            };

                            if (asteroid.is_potentially_hazardous_asteroid)
                            {
                                asteroid.relative_velocity_kmh = closeApproach.relative_velocity.kilometers_per_hour;
                                asteroid.orbiting_body = closeApproach.orbiting_body;
                                asteroid.close_approach_date = closeApproach.close_approach_date;

                                asteroids.Add(asteroid);
                            }
                        }
                    }
                }

                return asteroids;
            }
            else
            {
                return new List<Asteroid>();
            }

            //JObject data = JObject.Parse(response);
            //    List<Asteroid> asteroids = new List<Asteroid>();
            //    var nearEarthObjects = data["near_earth_objects"];
            //    foreach (var asteroidsDatas in nearEarthObjects)
            //    {
            //        foreach (var asteroidData in asteroidsDatas.Values())
            //        {
            //            var asteroid = new Asteroid
            //            {
            //                Name = asteroidData["name"].ToString(),
            //                Diameter = Convert.ToDouble(asteroidData["estimated_diameter"]["kilometers"]["estimated_diameter_max"]),
            //                Speed = Convert.ToDouble(asteroidData["close_approach_data"][0]["relative_velocity"]["kilometers_per_hour"]),
            //                Date = DateTime.Parse(asteroidData["close_approach_data"][0]["close_approach_date"].ToString()),
            //                Planet = asteroidData["close_approach_data"][0]["orbiting_body"].ToString(),
            //                Dangerous = asteroidData["is_potentially_hazardous_asteroid"].ToObject<bool>()
            //            };
            //            if (asteroid.Dangerous)
            //            {
            //                asteroids.Add(asteroid);
            //            }
            //        }
            //    }
            //    return asteroids;
            //}
        }
    }
}
