using System.Text.Json.Serialization;

namespace Orbita.CareerApi.Models
{
    public class Mission
    {
        public Guid Id { get; set; }
        public Guid CareerPathId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Difficulty { get; set; }
        public int EstimatedMinutes { get; set; }
        public int XpReward { get; set; }

        // Navegação usada só pelo EF Core, não precisa vir no JSON
        [JsonIgnore]
        public CareerPath? CareerPath { get; set; }
    }
}
