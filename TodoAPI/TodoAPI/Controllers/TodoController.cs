using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TodoController(TodoDbContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoList()
        {
          if (_context.TodoList == null)
          {
              return NotFound();
          }
            return await _context.TodoList.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("getById")]
        //[Route("getByIdl")]
        public async Task<ActionResult<TodoList>> GetTodoList(int id)
        {
          if (_context.TodoList == null)
          {
              return NotFound();
          }
            var todoList = await _context.TodoList.FindAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            return todoList;
        }

        // PUT: api/Todo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("putById")]
        //[Route("putById")]
        public async Task<IActionResult> PutTodoList(int id, TodoList todoList)
        {
            if (id != todoList.TodoNumber)
            {
                return BadRequest();
            }

            _context.Entry(todoList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("post")]
        public async Task<ActionResult<TodoList>> PostTodoList(TodoList todoList)
        {
          if (_context.TodoList == null)
          {
              return Problem("Entity set 'TodoDbContext.TodoList'  is null.");
          }
            _context.TodoList.Add(todoList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoList", new { id = todoList.TodoNumber }, todoList);
        }

        // DELETE: api/Todo/5
        [HttpDelete("deleteById")]
        //[Route("deleteById")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            if (_context.TodoList == null)
            {
                return NotFound();
            }
            var todoList = await _context.TodoList.FindAsync(id);
            if (todoList == null)
            {
                return NotFound();
            }

            _context.TodoList.Remove(todoList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoListExists(int id)
        {
            return (_context.TodoList?.Any(e => e.TodoNumber == id)).GetValueOrDefault();
        }
    }
}
