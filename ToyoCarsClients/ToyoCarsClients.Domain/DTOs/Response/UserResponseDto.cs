namespace ToyoCarsClients.Domain.DTOs.Response
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? MovilNumber { get; set; }
        public string? DNINumber { get; set; }
    }
}