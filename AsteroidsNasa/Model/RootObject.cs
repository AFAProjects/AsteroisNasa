using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsNasaBussiness.Model
{
    public class RootObject
    {
        public Links links { get; set; }
        public int element_count { get; set; }
        public Dictionary<string, List<Asteroid>> near_earth_objects { get; set; }
    }
}
