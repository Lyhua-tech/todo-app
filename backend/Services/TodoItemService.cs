using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repo;

        public TodoItemService(ITodoItemRepository repo){
            _repo = repo;
        }

        public async Task<IEnumerable<TodoResponseDto>> GetAllTodoItemsAsync(string method = "", string field = "", string order = "") {
            var todoItems = await _repo.GetTodoItems(method, field, order);
            return todoItems.Select(p => new TodoResponseDto{
                Id = p.Id,
                Title = p.Title,
                IsCompleted = p.IsCompleted,
                Content = p.Content,
                CompleteAt = p.CompleteAt,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.CreatedAt
            });
        }

        public async Task<TodoResponseDto> GetOneTodoItemAsync(long id){
            var todoItem = await _repo.GetTodo(id);

            if (todoItem == null) {
                throw new KeyNotFoundException("Task is not found.");
            }


            return new TodoResponseDto{
                Id = todoItem.Id,
                Title = todoItem.Title,
                IsCompleted = todoItem.IsCompleted,
                Content = todoItem.Content,
                CompleteAt = todoItem.CompleteAt,
                CreatedAt = todoItem.CreatedAt,
                UpdatedAt = todoItem.CreatedAt
            };
        }

        public async Task<TodoResponseDto> CreateTodo(TodoRequestDto todoRequestDto){
            var newTodo = new TodoItem{
                Title = todoRequestDto.Title,
                IsCompleted = todoRequestDto.IsCompleted,
                Content = todoRequestDto.Content
            };

            await _repo.CreateTodo(newTodo);

            return new TodoResponseDto{
                Id = newTodo.Id,
                Title = newTodo.Title,
                IsCompleted = newTodo.IsCompleted,
                Content = newTodo.Content
            };
        }

        public async Task UpdateTodo(long id, TodoRequestDto todoRequestDto){
            var todoItem = await _repo.GetTodo(id);
            var cambodiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            if (todoItem == null) {
                throw new KeyNotFoundException("Task is not found.");
            } 

            if (todoRequestDto.Title != null){
                todoItem.Title = todoRequestDto.Title;
            }
            
            todoItem.IsCompleted = todoRequestDto.IsCompleted;

            if(todoItem.IsCompleted == true){
                todoItem.CompleteAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cambodiaTimeZone);
            } else{
                todoItem.CompleteAt = null;
            }

            await _repo.UpdateTodo(todoItem);
        }

        public async Task RemoveTodo(long id){
            var todoItem = await _repo.GetTodo(id);

            if (todoItem == null){
                throw new KeyNotFoundException("Task is not found");
            }

            await _repo.RemoveTodo(id);
        }

        public async Task<IEnumerable<TodoResponseDto>> GetAllTaskDaysAsync(long period){
            var todoItem7Days =  await _repo.GetTaskCompleteDays(period);

            return todoItem7Days.Select(p => new TodoResponseDto{
                Id = p.Id,
                Title = p.Title,
                IsCompleted = p.IsCompleted,
                Content = p.Content,
                CompleteAt = p.CompleteAt,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.CreatedAt
            });
        }
    }
}