namespace Orbita.CareerApi.Models
{
    public class CareerPath
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Area { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Level { get; set; } = "Iniciante";

        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
    }
}
