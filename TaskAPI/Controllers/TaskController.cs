using Microsoft.AspNetCore.Mvc;
using TaskAPI.Model;
using TaskAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace TaskAPI.Controllers 
{ 
    [Route("Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;
        public TaskController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> listarTarefas(int userId)
        {
            var lista = await _context.Tasks
               .Where(t => t.UserId == userId)
               .ToListAsync();

            return Ok(lista);
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> criarTarefa([FromBody] Tasks tarefa)
        {

            _context.Tasks.Add(tarefa);
            await _context.SaveChangesAsync();
            return Ok(tarefa);
        }

        [HttpPut]
        [Route("update")]

        public async Task<IActionResult> editarTarefa(int id, [FromBody] Tasks atualizartarefas)
        {
            
            var tarefas = await _context.Tasks.FindAsync(id);
            if (tarefas == null) return NotFound();

          

            tarefas.Tarefa = atualizartarefas.Tarefa;
            tarefas.Status = atualizartarefas.Status;
            tarefas.DataTarefa = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]

        public async Task<IActionResult> deletarTarefa(int id)
        {
            var deletar = await _context.Tasks.FindAsync(id);
            if (deletar == null) return NotFound();

            _context.Tasks.Remove(deletar);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }










}

