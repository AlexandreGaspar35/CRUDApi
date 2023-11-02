using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDApi.Context;
using CRUDApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return Ok(contato);
        }

        [HttpGet("ObterId")]
        public IActionResult BuscarPorId(int id)
        {
            var contato = _context.Contatos.Find(id);
            if(contato == null)
                return NotFound();
            return Ok(contato);
        }


        [HttpGet("ObterNome")]
        public IActionResult BuscarPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));

            return Ok(contatos);
        }

        [HttpGet("ObterTodos")]
        public IActionResult BuscarTodos()
        {
            var contatos = _context.Contatos.ToList();
            return Ok(contatos);
        }

        [HttpPut("Atualizar")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if(contatoBanco == null)
                return NotFound();
            
            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);

        }

        [HttpDelete("DeletarPorId")]
        public IActionResult Deletar(int id)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if(contatoBanco == null)
                return NotFound();
            
            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}