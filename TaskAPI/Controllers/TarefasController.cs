using Microsoft.AspNetCore.Mvc;
using TaskAPI.Model;
using TaskAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace TaskAPI.Controllers 
{ 
    [Route("Tarefas")]
    [ApiController]

    
    public class TarefasController : ControllerBase 
    {
        private readonly TarefasContext _context;
        public TarefasController(TarefasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarefas()
        {
           return Ok(await _context.Tarefas.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Tarefas tarefas)
        {
            tarefas.DataTarefa = DateTime.Now;
            _context.Tarefas.Add(tarefas);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateTask), new { id = tarefas.Id }, tarefas);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Tarefas atualizartarefas)
        {
            var tarefas = await _context.Tarefas.FindAsync(id);
            if (tarefas == null) return NotFound();

            tarefas.Tarefa = atualizartarefas.Tarefa;
            tarefas.Status = atualizartarefas.Status;
            tarefas.DataTarefa = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tarefas.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tarefas.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }










}

