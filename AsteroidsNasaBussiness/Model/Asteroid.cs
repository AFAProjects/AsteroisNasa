using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsNasaBussiness.Model
{

    public class Asteroid
    {
        public string id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string nasa_jpl_url { get; set; }
        public double absolute_magnitude_h { get; set; }
        public EstimatedDiameter estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public List<CloseApproachData> close_approach_data { get; set; }
        public bool is_sentry_object { get; set; }


        public double relative_velocity_kmh { get; set; }
        public string orbiting_body { get; set; }
        public string close_approach_date { get; set; }
    }
}
