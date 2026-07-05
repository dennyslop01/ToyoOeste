using ToyoCarsClients.Business.Interfaces;
using ToyoCarsClients.Domain.Entities;
using ToyoCarsClients.Infraestructure.Data;

namespace ToyoCarsClients.Infraestructure.Repositories
{
    public class EncuestaRepository : IEncuesta
    {
        private AppDbContext _context;

        public EncuestaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EnviarEncuestaAsync(string email, int rating, string comments)
        {
            var encuesta = new Encuestas
            {
                Email = email,
                Rating = rating,
                Comentario = comments
            };

            await _context.Encuestas.AddAsync(encuesta);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
