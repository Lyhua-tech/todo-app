using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos;
using backend.Repositories;
using backend.Services;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoService;

        public TodoItemController(ITodoItemService todoService){
            _todoService = todoService;
        } 

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }
            return int.Parse(userIdClaim.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery(Name = "sortBy")] string? field = null, [FromQuery(Name = "order")] string? order = "ASC")
        {
            var method = string.IsNullOrEmpty(field) ? "" : "sortBy";
            int userId = GetUserId();

            var todoItems = await _todoService.GetAllTodoItemsAsync(userId, method, field ?? "Id", order ?? "ASC");

            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(long id){
            try{
                int userId = GetUserId();
                var todoItem = await _todoService.GetOneTodoItemAsync(id, userId);
                return Ok(todoItem);
            } catch (KeyNotFoundException){     
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] TodoRequestDto todoRequestDto){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            int userId = GetUserId();
            await _todoService.CreateTodo(todoRequestDto, userId);

            return CreatedAtAction(nameof(GetOne), new {id = todoRequestDto.Id}, todoRequestDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(long id, TodoRequestDto todoRequestDto){
            try{
                int userId = GetUserId();
                await _todoService.UpdateTodo(id, todoRequestDto, userId);
                
                return NoContent();

            } catch(KeyNotFoundException){
                
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(long id){
            try{
                int userId = GetUserId();
                await _todoService.RemoveTodo(id, userId);

                return NoContent();
            } catch(KeyNotFoundException){
                return NotFound();
            }
        }

        [HttpGet("complete/{period}")]
        public async Task<ActionResult> GetCompletedTask(long period){
            int userId = GetUserId();
            var taskComplete =  await _todoService.GetAllTaskDaysAsync(period, userId);

            return Ok(taskComplete);
        }
    }
}