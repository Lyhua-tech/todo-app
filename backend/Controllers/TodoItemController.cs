using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos;
using backend.Repositories;
using backend.Services;

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

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery(Name = "sortBy")] string? field = null, [FromQuery(Name = "order")] string? order = "ASC")
        {
            var method = string.IsNullOrEmpty(field) ? "" : "sortBy";

            var todoItems = await _todoService.GetAllTodoItemsAsync(method, field ?? "Id", order ?? "ASC");

            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(long id){
            try{
                var todoItem = await _todoService.GetOneTodoItemAsync(id);
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

            await _todoService.CreateTodo(todoRequestDto);

            return CreatedAtAction(nameof(GetOne), new {id = todoRequestDto.Id}, todoRequestDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(long id, TodoRequestDto todoRequestDto){
            try{
                await _todoService.UpdateTodo(id, todoRequestDto);
                
                return NoContent();

            } catch(KeyNotFoundException){
                
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(long id){
            try{
                await _todoService.RemoveTodo(id);

                return NoContent();
            } catch(KeyNotFoundException){
                return NotFound();
            }
        }

        [HttpGet("complete/{period}")]
        public async Task<ActionResult> GetCompletedTask(long period){
            var taskComplete =  await _todoService.GetAllTaskDaysAsync(period);

            return Ok(taskComplete);
        }
    }
}