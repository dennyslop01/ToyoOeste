using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToyoCarsClients.Domain.DTOs.Response
{
    public class DatosCliente
    {
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; }

        [JsonPropertyName("apellidos")]
        public string? Apellido { get; set; }
        
        [JsonPropertyName("direccion")]
        public string? Direccion { get; set; }
        
        [JsonPropertyName("telefono_domicilio")]
        public string? TelefonoDomicilio { get; set; }
        
        [JsonPropertyName("telefono_oficina")]
        public string? TelefonoOficina { get; set; }
        
        [JsonPropertyName("telefono_movil")]
        public string? TelefonoMovil { get; set; }
        
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
