using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaAlocacaoLab.API.DTOs.Alocacao;
using SistemaAlocacaoLab.API.Services;

namespace SistemaAlocacaoLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlocacaoController : ControllerBase
    {
        private readonly IAlocacaoService _service;

        public AlocacaoController(IAlocacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alocacoes = await _service.GetAllAsync();
            return Ok(alocacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alocacao = await _service.GetByIdAsync(id);
            if (alocacao == null)
                return NotFound(new { mensagem = $"Alocação com id {id} não encontrada." });
            return Ok(alocacao);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlocacaoRequestDto dto)
        {
            try
            {
                var alocacao = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = alocacao.IdAlocacao }, alocacao);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // O PUT da Alocação é só pra mudar o status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string novoStatus)
        {
            try
            {
                var alocacao = await _service.UpdateStatusAsync(id, novoStatus);
                if (alocacao == null)
                    return NotFound(new { mensagem = $"Alocação com id {id} não encontrada." });
                return Ok(alocacao);
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
                return NotFound(new { mensagem = $"Alocação com id {id} não encontrada." });
            return NoContent();
        }
    }
}