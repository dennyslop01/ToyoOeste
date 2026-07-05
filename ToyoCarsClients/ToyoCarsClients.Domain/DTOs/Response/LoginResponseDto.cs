using System.Text.Json.Serialization;

namespace ToyoCarsClients.Domain.DTOs.Response
{
    public class LoginResponseDto
    {
        public bool IsAuthenticated {  get; set; }
        public string Token { get; set; } = null!;
        public string Message { get; set; } = null!;

    }

    public class LoginResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("cod_usuario")]
        public string CodUsuario { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("nombre_cia")]
        public string NombreCia { get; set; }
    }
}
