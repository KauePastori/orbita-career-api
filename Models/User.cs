namespace Orbita.CareerApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CurrentRole { get; set; } = null!;
        public int WeeklyAvailableHours { get; set; }
    }
}
