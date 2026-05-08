using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaAlocacaoLab.API.DTOs.Disciplina;
using SistemaAlocacaoLab.API.Services;

namespace SistemaAlocacaoLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaService _service;

        public DisciplinaController(IDisciplinaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var disciplinas = await _service.GetAllAsync();
            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var disciplina = await _service.GetByIdAsync(id);

            if (disciplina == null)
                return NotFound(new { mensagem = $"Disciplina com id {id} não encontrada." });

            return Ok(disciplina);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DisciplinaRequestDto dto)
        {
            try
            {
                var disciplina = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = disciplina.IdDisciplina }, disciplina);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DisciplinaRequestDto dto)
        {
            try
            {
                var disciplina = await _service.UpdateAsync(id, dto);

                if (disciplina == null)
                    return NotFound(new { mensagem = $"Disciplina com id {id} não encontrada." });

                return Ok(disciplina);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletada = await _service.DeleteAsync(id);

            if (!deletada)
                return NotFound(new { mensagem = $"Disciplina com id {id} não encontrada." });

            return NoContent();
        }
    }
}