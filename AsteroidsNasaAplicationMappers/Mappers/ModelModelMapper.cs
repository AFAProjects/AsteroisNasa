using AsteroidsNasaBussiness.DTO;
using AsteroidsNasaBussiness.Model;
using AsteroidsNasaDataAccess;

namespace AsteroidsNasaAplicationMappers.Mappers
{
    public class ModelModelMapper
    {
        public static List<AsteroidDto> Map(List<Asteroid> asteroids)
        {
            var topAsteroids = asteroids
                .Where(a => a.is_potentially_hazardous_asteroid)
                .OrderByDescending(a => a.estimated_diameter.kilometers.estimated_diameter_max)
                .Select(a => new AsteroidDto
                { 
                    ID = a.id,
                    Name = a.name ,
                    MaxDiameter = a.estimated_diameter.kilometers.estimated_diameter_max,
                    RelativeVelocity =a.close_approach_data[0].relative_velocity.kilometers_per_hour,
                    CloseApproachDate = a.close_approach_data[0].close_approach_date,
                    OrbitingBody = a.close_approach_data[0].orbiting_body,
                    Dangerous = a.is_potentially_hazardous_asteroid
                })
                .ToList();

            return topAsteroids;
        }
    }
}
