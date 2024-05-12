using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefas.Context;
using GerenciadorTarefas.Entities;
using GerenciadorTarefas.Enums;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaContext _context;

        public TarefaController(TarefaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name ="ObterTarefa")]
        public ActionResult<Tarefa> ObterPorId(int id)
        {
            var tarefa = _context.Tarefa.Find(id);
            return Ok(tarefa);
        }
        [HttpGet("ObterPorTitulo")]
        public ActionResult<Tarefa> ObterPortitulo(string titulo)
        {
            var tarefa = _context.Tarefa.FirstOrDefault(p => p.Titulo.Contains(titulo));
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public ActionResult<IEnumerable<Tarefa>> ObterPorData([FromQuery] DateTime data)
        {
            var tarefa = _context.Tarefa.Where(p => p.Data == data).ToList();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public ActionResult<IEnumerable<Tarefa>> ObterPorStatus(EnumStatus status)
        {
            var tarefa = _context.Tarefa.Where(p => p.Status == status);
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public ActionResult<IEnumerable<Tarefa>> ObterTodos()
        {
            return _context.Tarefa.AsNoTracking().ToList();
        }

        [HttpPost]
        public ActionResult NovaTarefa(Tarefa tarefa)
        {
            _context.Tarefa.Add(tarefa);
            _context.SaveChanges();

            if (tarefa is null)
            {
                return BadRequest("Preencha todos os campos!");
            }

            return new CreatedAtRouteResult("ObterTarefa", new { id = tarefa.Id }, tarefa);
        }
        [HttpPut("{id}")]
        public ActionResult AtualizarTarefa(int id, Tarefa tarefa)
        {
            var tarefaAtualizada = _context.Tarefa.Find(id);
            
            if(tarefaAtualizada is null)
            {
                return NotFound("O id informado não foi encontrado. Tente novamente.");
            }

            tarefaAtualizada.Titulo = tarefa.Titulo;
            tarefaAtualizada.Descricao = tarefa.Descricao;
            tarefaAtualizada.Data = tarefa.Data;
            tarefaAtualizada.Status = tarefa.Status;
            _context.SaveChanges();
            return Ok(tarefa);
        }

        [HttpDelete]
        public ActionResult DeletarTarefa(int id)
        {
            var tarefa = _context.Tarefa.Find(id);
            if (tarefa is null)
            {
                return NotFound("O id informado não foi encontrado. Tente novamente.");
            }
            _context.Tarefa.Remove(tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        }
    }
}
