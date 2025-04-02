using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Database;
using backend.Models;
using Dapper;

namespace backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnectionFactory _dbContext;

        public UserRepository(SqlConnectionFactory dbContext){
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByIdAsync(int userId){
            using var connection = _dbContext.CreateConnection();
            string sql = @"SELECT * FROM Users WHERE Id = @Id";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = userId});
        }

        public async Task<User?> GetUserByUsernameAsync(string username){
            using var connection = _dbContext.CreateConnection();

            string sql = @"SELECT * FROM Users WHERE Username = @Username";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username});
        }

        public async Task<User?> GetUserByEmailAsync(string email){
            using var connection = _dbContext.CreateConnection();
            
            string sql = @"SELECT * FROM Users WHERE Email = @Email";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new{ Email = email});
        }

        public async Task<int> CreateUserAsync(User user){
            using var connection = _dbContext.CreateConnection();

            string sql = @"INSERT INTO Users(Username, Email, PasswordHash, PasswordSalt, CreatedAt) VALUES(@Username, @Email, @PasswordHash, @PasswordSalt, @CreatedAt)
            SELECT CAST(SCOPE_IDENTITY() as int)";

            return await connection.QuerySingleAsync<int>(sql, user);
        }
    }
}