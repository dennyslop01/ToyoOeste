using System.ComponentModel.DataAnnotations;

namespace ToyoCarsClients.Domain.DTOs.Request
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Por favor, ingresa tu nombre.")] 
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor, ingresa un correo válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor, ingresa una contraseña segura.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor, ingresa tu teléfono móvil.")]
        public string? MovilNumber { get; set; }

        [Required(ErrorMessage = "Por favor, ingresa tu número de documento.")]
        public string? DNINumber { get; set; }

        [Required(ErrorMessage = "Por favor, seleccione el tipo de documento.")]
        public string? TipoDNI { get; set; }
    }

    public class ResetUserDto
    {
        [Required(ErrorMessage = "Por favor, ingresa un correo válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor, ingresa una contraseña segura.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor, ingresa tu teléfono móvil.")]
        public string? MovilNumber { get; set; }

        [Required(ErrorMessage = "Por favor, ingresa tu número de cédula.")]
        public string? DNINumber { get; set; }
    }

    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Por favor, ingresa un correo válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor, ingresa tu teléfono móvil.")]
        [RegularExpression(@"^(0412|0422|0414|0424|0416|0426)\d{7}$",
                            ErrorMessage = "El teléfono debe comenzar con 0412, 0422, 0414, 0424, 0416 o 0426 y tener 11 dígitos.")]
        public string? MovilNumber { get; set; }

        [RegularExpression(@"^(212)\d{7}$",
                    ErrorMessage = "El teléfono debe comenzar con 212 y tener 10 dígitos.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Por favor, ingresa tu número de cédula.")]
        public string? DNINumber { get; set; }
    }

    public class TelefonoJson
    {
        public string codAreaTel { get; set; }
        public string telefono { get; set; }
        public string tipo { get; set; }
    }
}
