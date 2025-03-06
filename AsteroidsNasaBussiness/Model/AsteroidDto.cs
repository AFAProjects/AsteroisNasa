using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsNasaBussiness.Model
{
    public class AsteroidDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double MaxDiameter { get; set; }
        public double RelativeVelocity { get; set; }
        public string CloseApproachDate { get; set; }
        public string OrbitingBody { get; set; }
        public bool Dangerous { get; set; }
    }
}
