using Microsoft.EntityFrameworkCore;
using ToyoCarsClients.Application.Interfaces;
using ToyoCarsClients.Domain.Entities;
using ToyoCarsClients.Infraestructure.Data;

namespace ToyoCarsClients.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            var useraux = await GetUserByEmailAsync(user.Email);
            if (useraux != null)
            {
                throw new Exception("El correo electrónico ya está en uso.");
            }

            var userced = await _context.Users.FirstOrDefaultAsync(x => x.DNINumber == user.DNINumber);
            if (userced != null)
            {
                throw new Exception("El número de cédula ya está en uso.");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user!;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user!;
        }

        public async Task<User> UpdateAsync(User user)
        {
            User useraux = await GetUserByEmailAsync(user.Email);
            if (useraux == null)
                throw new Exception("El correo electrónico no existe.");

            if(useraux.MovilNumber != user.MovilNumber)
                throw new Exception("El número movil no existe.");

            if (useraux.DNINumber != user.DNINumber)
                throw new Exception("El número de cédula no existe.");

            useraux.PasswordHash = user.PasswordHash;

            _context.Users.Update(useraux);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
