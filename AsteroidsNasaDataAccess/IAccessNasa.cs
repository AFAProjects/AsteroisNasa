using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidsNasaBussiness.Model;

namespace AsteroidsNasaDataAccess
{
    public interface IAccessNasa
    {
        Task<List<Asteroid>> GetAsteroids(int days);
    }
}
