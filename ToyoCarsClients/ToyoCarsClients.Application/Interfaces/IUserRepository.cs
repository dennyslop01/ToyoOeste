using ToyoCarsClients.Domain.Entities;

namespace ToyoCarsClients.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
