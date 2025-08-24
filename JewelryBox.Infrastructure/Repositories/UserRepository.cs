using Dapper;
using JewelryBox.Domain.Entities;
using JewelryBox.Domain.Interfaces;
using JewelryBox.Infrastructure.Data;
using JewelryBox.Infrastructure.Services;

namespace JewelryBox.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IQueryService _queryService;

        public UserRepository(IDbConnectionFactory connectionFactory, IQueryService queryService)
        {
            _connectionFactory = connectionFactory;
            _queryService = queryService;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "GetById");
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "GetByEmail");
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "GetByUsername");
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "GetAll");
            return await connection.QueryAsync<User>(query);
        }

        public async Task<User> CreateAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "Create");
            var id = await connection.QuerySingleAsync<int>(query, user);
            user.Id = id;
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "Update");
            await connection.ExecuteAsync(query, user);
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "Delete");
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<bool> ExistsAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "ExistsByEmail");
            var count = await connection.QuerySingleAsync<int>(query, new { Email = email });
            return count > 0;
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "ExistsByUsername");
            var count = await connection.QuerySingleAsync<int>(query, new { Username = username });
            return count > 0;
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = _queryService.GetQuery("User", "UpdateLastLogin");
            await connection.ExecuteAsync(query, new { Id = userId, LastLoginAt = DateTime.UtcNow });
        }
    }
}
