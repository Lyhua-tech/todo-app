using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;

namespace backend.Services
{
    public interface ITodoItemService
    {
        public Task<IEnumerable<TodoResponseDto>> GetAllTodoItemsAsync();
        public Task<TodoResponseDto> GetOneTodoItemAsync(long id);

        public Task<TodoResponseDto> CreateTodo(TodoRequestDto todoRequestDto);

        public Task UpdateTodo(long id, TodoRequestDto todoRequestDto);

        public Task RemoveTodo(long id);

        public Task<IEnumerable<TodoResponseDto>> GetAllTaskDaysAsync(long period);
    }
}