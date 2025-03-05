
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

            var data = JsonConvert.DeserializeObject<AsteroidResponse>(response);

            if (data.NearEarthObjects != null)
            {
                List<Asteroid> asteroids = new List<Asteroid>();

                foreach (var dateEntry in data.NearEarthObjects)
                {
                    foreach (var asteroidData in dateEntry.Value)
                    {
                        if (asteroidData.CloseApproachData != null && asteroidData.CloseApproachData.Count > 0)
                        {
                            var asteroid = new Asteroid
                            {
                                Name = asteroidData.Name,
                                Diameter = asteroidData.EstimatedDiameter.KilometersMax,
                                Speed = asteroidData.CloseApproachData[0].RelativeVelocity.KilometersPerHour,
                                Date = asteroidData.CloseApproachData[0].CloseApproachDate,
                                Planet = asteroidData.CloseApproachData[0].OrbitingBody,
                                Dangerous = asteroidData.IsPotentiallyHazardousAsteroid
                            };
                            if (asteroid.Dangerous)
                            {
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
