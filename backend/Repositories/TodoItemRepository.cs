using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using backend.Database;
using Dapper;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly SqlConnectionFactory _dbContext;

        public TodoItemRepository(SqlConnectionFactory dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems(string method = "", string field = "", string order = "")
        {
            using var connection = _dbContext.CreateConnection();
            if ( method == "sortBy"){
                field = string.IsNullOrEmpty(field) ? "Id" : field;

                var allowedFields = new[] { "Id", "Title", "IsCompleted", "CreatedAt", "UpdatedAt", "CompleteAt" };
                if (!allowedFields.Contains(field, StringComparer.OrdinalIgnoreCase))
                {
                    field = "Id";
                }
                order = (order?.ToUpper() == "DESC") ? "DESC" : "ASC";

                string sqlSort = $"SELECT * FROM TodoItems ORDER BY {field} {order}";
                
                return await connection.QueryAsync<TodoItem>(sqlSort);
            }
            string sql = "SELECT Id, Title, IsCompleted, Content, CompleteAt, CreatedAt, UpdatedAt FROM TodoItems";
            
            var tasks = await connection.QueryAsync<TodoItem>(sql);
            
            return tasks;
        }

        public async Task<TodoItem?> GetTodo(long id)
        {
            using var connection = _dbContext.CreateConnection();
            string sql = "SELECT Id, Title, IsCompleted, Content, CompleteAt, CreatedAt, UpdatedAt FROM TodoItems WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<TodoItem>(sql, new { Id = id });
        }

        public async Task CreateTodo(TodoItem todoItem)
        {
            todoItem.CreatedAt = DateTime.UtcNow;
            todoItem.UpdatedAt = DateTime.UtcNow;
            
            using var connection = _dbContext.CreateConnection();
            string sql = @"
                INSERT INTO TodoItems (Title, IsCompleted, Content, CompleteAt, CreatedAt, UpdatedAt) 
                VALUES (@Title, @IsCompleted, @Content, @CompleteAt, @CreatedAt, @UpdatedAt);
                SELECT CAST(SCOPE_IDENTITY() as bigint)";
                
            var id = await connection.QuerySingleAsync<long>(sql, todoItem);
            todoItem.Id = id;
        }

        public async Task UpdateTodo(TodoItem todoItem)
        {
            todoItem.UpdatedAt = DateTime.UtcNow;
            
            using var connection = _dbContext.CreateConnection();
            string sql = @"
                UPDATE TodoItems 
                SET Title = @Title, 
                    IsCompleted = @IsCompleted, 
                    Content = @Content, 
                    CompleteAt = @CompleteAt, 
                    UpdatedAt = @UpdatedAt 
                WHERE Id = @Id";
                
            await connection.ExecuteAsync(sql, todoItem);
        }

        public async Task RemoveTodo(long id)
        {
            using var connection = _dbContext.CreateConnection();
            string sql = "DELETE FROM TodoItems WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<TodoItem>> GetTaskCompleteDays(long period)
        {
            using var connection = _dbContext.CreateConnection();
            string sql = @"
                SELECT Id, Title, IsCompleted, Content, CompleteAt, CreatedAt, UpdatedAt 
                FROM TodoItems
                WHERE CompleteAt IS NOT NULL 
                AND CompleteAt >= @StartDate 
                AND CompleteAt <= @EndDate";

            return await connection.QueryAsync<TodoItem>(sql, new 
            { 
                StartDate = DateTime.UtcNow.AddDays(-period), 
                EndDate = DateTime.UtcNow 
            });
        }
    }
}