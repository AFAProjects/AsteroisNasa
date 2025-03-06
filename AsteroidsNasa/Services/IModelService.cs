using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidsNasaBussiness.Model;


namespace AsteroidsNasaAplicationMappers.Services
{
    public interface IModelService
    {
        Task<Asteroid> GetAsteroid(DateOnly date);
    }
}
