using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaAlocacaoLab.API.DTOs.Turma;
using SistemaAlocacaoLab.API.Services;

namespace SistemaAlocacaoLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _service;

        public TurmaController(ITurmaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var turmas = await _service.GetAllAsync();
            return Ok(turmas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var turma = await _service.GetByIdAsync(id);
            if (turma == null)
                return NotFound(new { mensagem = $"Turma com id {id} não encontrada." });
            return Ok(turma);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TurmaRequestDto dto)
        {
            try
            {
                var turma = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = turma.IdTurma }, turma);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TurmaRequestDto dto)
        {
            try
            {
                var turma = await _service.UpdateAsync(id, dto);
                if (turma == null)
                    return NotFound(new { mensagem = $"Turma com id {id} não encontrada." });
                return Ok(turma);
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
                return NotFound(new { mensagem = $"Turma com id {id} não encontrada." });
            return NoContent();
        }
    }
}