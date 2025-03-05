using AsteroidsNasaBussiness.Model;
using AsteroidsNasaDataAccess;

namespace AsteroidsNasaAplicationMappers.Mappers
{
    public class ModelModelMapper
    {
        public static List<Asteroid> Map(List<Asteroid> asteroids)
        {
            var topAsteroids = asteroids
                .Where(a => a.is_potentially_hazardous_asteroid)
                .OrderByDescending(a => a.estimated_diameter.kilometers.estimated_diameter_max) 
                .Select(a => new Asteroid
                {
                    name = a.name,
                    estimated_diameter = a.estimated_diameter,
                    relative_velocity_kmh = a.close_approach_data[0].relative_velocity.kilometers_per_hour,
                    close_approach_date =a.close_approach_data[0].close_approach_date,
                    orbiting_body = a.close_approach_data[0].orbiting_body,
                    is_potentially_hazardous_asteroid = a.is_potentially_hazardous_asteroid
                })
                .ToList();
            return topAsteroids;
        }
    }
}
