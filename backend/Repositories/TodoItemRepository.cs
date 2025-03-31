using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoController.Database;
using TodoController.Models;

namespace TodoController.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public TodoItemRepository(TodoContext context){
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems(){
            var completeOneDay = DateTime.UtcNow.AddHours(-24);
            return await _context.TodoItems.Where(p => p.CompleteAt == null || p.CompleteAt >= completeOneDay).ToListAsync();
        }

        public async Task<TodoItem?> GetTodo(long id){
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task CreateTodo(TodoItem todoItem){
            await _context.TodoItems.AddAsync(todoItem);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateTodo(TodoItem todoItem){
            _context.TodoItems.Update(todoItem);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveTodo(long id){
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem != null){
                _context.TodoItems.Remove(todoItem);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TodoItem>> GetTaskCompleteDays(long period)
        {
            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddDays(-period);

            
            return await _context.TodoItems
                .Where(t => t.CompleteAt != null && t.CompleteAt >= startDate
                        && t.CompleteAt <= endDate)
                .ToListAsync();
        }
    }
}