using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem?> GetTodo(long id);
        Task CreateTodo(TodoItem todoItem);
        Task UpdateTodo(TodoItem todoItem);
        Task RemoveTodo(long id);

        Task<IEnumerable<TodoItem>> GetTaskCompleteDays(long period);
    }
}