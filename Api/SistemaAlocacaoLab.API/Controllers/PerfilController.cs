using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaAlocacaoLab.API.DTOs.Perfil;
using SistemaAlocacaoLab.API.Services;

namespace SistemaAlocacaoLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _service;

        public PerfilController(IPerfilService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var perfis = await _service.GetAllAsync();
            return Ok(perfis);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var perfil = await _service.GetByIdAsync(id);
            if (perfil == null)
                return NotFound(new { mensagem = $"Perfil com id {id} não encontrado." });
            return Ok(perfil);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PerfilRequestDto dto)
        {
            try
            {
                var perfil = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = perfil.IdPerfil }, perfil);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PerfilRequestDto dto)
        {
            try
            {
                var perfil = await _service.UpdateAsync(id, dto);
                if (perfil == null)
                    return NotFound(new { mensagem = $"Perfil com id {id} não encontrado." });
                return Ok(perfil);
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
                return NotFound(new { mensagem = $"Perfil com id {id} não encontrado." });
            return NoContent();
        }
    }
}