using System.Text.Json.Serialization;

namespace ToyoCarsClients.Domain.DTOs.Response
{
    public class VehiculoTaller
    {
        [JsonPropertyName("modelo_cod")]
        public string? ModeloCod { get; set; }

        [JsonPropertyName("modelo_desc")]
        public string? ModeloDesc { get; set; }

        [JsonPropertyName("placa")]
        public string? Placa { get; set; }

        [JsonPropertyName("año")]
        public int? Anio { get; set; }

        [JsonPropertyName("vin")]
        public string? Vin { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("numero_os")]
        public string? NumeroOs { get; set; }

        [JsonPropertyName("nom_dpto")]
        public string? NomDpto { get; set; }

        [JsonPropertyName("subtipo_os")]
        public string? SubtipoOs { get; set; }

        [JsonPropertyName("fecha_recep")]
        public string? FechaRecep { get; set; }

        [JsonPropertyName("fecha_prometida")]
        public string? FechaPrometida { get; set; }

        [JsonPropertyName("estado_os")]
        public string? EstadoOs { get; set; }

        [JsonPropertyName("asesor")]
        public string? Asesor { get; set; }
    }

}
