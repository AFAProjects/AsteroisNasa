using AsteroidsNasaBussiness.Model;
using AsteroidsNasaDataAccess;
namespace AsteroidsNasaAplicationMappers.Mappers
{
    public class ModelModelMapper
    {
        public static Asteroid Map(AccessNasa model)
        {
            return new Asteroid
            {
                //Name = model.Name,
                //Diameter=,
                //Speed=,
                //Planet=,
                //Date=,
                //Dangerous=
            };
        }
    }
}
