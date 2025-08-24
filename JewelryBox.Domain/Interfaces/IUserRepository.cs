using JewelryBox.Domain.Entities;

namespace JewelryBox.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
        Task UpdateLastLoginAsync(int userId);
    }
}
