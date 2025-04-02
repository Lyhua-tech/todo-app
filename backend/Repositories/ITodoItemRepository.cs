using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItems(int userId, string method, string field, string order);
        Task<TodoItem?> GetTodo(long id, int userId);
        Task CreateTodo(TodoItem todoItem);
        Task UpdateTodo(TodoItem todoItem, int userId);
        Task RemoveTodo(long id, int userId);

        Task<IEnumerable<TodoItem>> GetTaskCompleteDays(long period, int userId);
    }
}