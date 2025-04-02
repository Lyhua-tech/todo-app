using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;

namespace backend.Services
{
    public interface ITodoItemService
    {
        public Task<IEnumerable<TodoResponseDto>> GetAllTodoItemsAsync(int userId, string method, string field, string order);
        public Task<TodoResponseDto> GetOneTodoItemAsync(long id, int userId);

        public Task<TodoResponseDto> CreateTodo(TodoRequestDto todoRequestDto, int userId);

        public Task UpdateTodo(long id, TodoRequestDto todoRequestDto, int userId);

        public Task RemoveTodo(long id, int userId);

        public Task<IEnumerable<TodoResponseDto>> GetAllTaskDaysAsync(long period, int userId);
    }
}