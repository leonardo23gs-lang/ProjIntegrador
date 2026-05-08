using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaAlocacaoLab.API.DTOs.Usuario;
using SistemaAlocacaoLab.API.Services;

namespace SistemaAlocacaoLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _service.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { mensagem = $"Usuário com id {id} não encontrado." });
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioRequestDto dto)
        {
            try
            {
                var usuario = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, usuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioRequestDto dto)
        {
            try
            {
                var usuario = await _service.UpdateAsync(id, dto);
                if (usuario == null)
                    return NotFound(new { mensagem = $"Usuário com id {id} não encontrado." });
                return Ok(usuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado = await _service.DeleteAsync(id);
            if (!deletado)
                return NotFound(new { mensagem = $"Usuário com id {id} não encontrado." });
            return NoContent();
        }
    }
}