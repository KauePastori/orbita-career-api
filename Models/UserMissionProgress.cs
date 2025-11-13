using System.Text.Json.Serialization;

namespace Orbita.CareerApi.Models
{
    public class UserMissionProgress
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MissionId { get; set; }
        public string Status { get; set; } = "Pendente";
        public DateTime? CompletedAt { get; set; }

        // Navegações usadas pelo EF Core, não precisam vir no JSON
        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public Mission? Mission { get; set; }
    }
}
