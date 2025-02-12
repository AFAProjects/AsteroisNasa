namespace AsteroidsNasaBussiness.Model
{
    public class Asteroid
    {
        public string Name {  get; set; }
        public double Diameter {  get; set; }
        public double Speed { get; set; }
        public DateTime Date { get; set; }
        public string Planet {  get; set; }
        public bool Dangerous { get; set; }
    }
}
