namespace ToyoCarsClients.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? MovilNumber { get; set; }
        public string? DNINumber { get; set; }
        public string? TipoDNI { get; set; }
    }
}
