using AsteroidsNasaBussiness.Model;
using AsteroidsNasaDataAccess;

namespace AsteroidsNasaAplicationMappers.Mappers
{
    public class ModelModelMapper
    {
        public static List<Asteroid> Map(List<Asteroid> asteroids)
        {
            var topAsteroids = asteroids
                .Where(a => a.Dangerous)
                .OrderByDescending(a => a.Diameter) 
                .Select(a => new Asteroid
                {
                    Name = a.Name,
                    Diameter = a.Diameter,
                    Speed = a.Speed,
                    Date = a.Date,
                    Planet = a.Planet,
                    Dangerous = a.Dangerous
                })
                .ToList();
            return topAsteroids;
        }
    }
}
