using System.ComponentModel.DataAnnotations;

namespace ToyoCarsClients.Domain.Entities
{
    public class Encuestas
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, califique nuestro servicio.")]
        [Range(1, 5, ErrorMessage = "La calificación debe ser entre 1 y 5.")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Los comentarios no pueden exceder los 500 caracteres.")]
        public string Comentario { get; set; }
    }
}
