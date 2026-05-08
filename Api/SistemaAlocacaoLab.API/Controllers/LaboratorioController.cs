using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaAlocacaoLab.API.DTOs.Laboratorio;
using SistemaAlocacaoLab.API.Services;

namespace SistemaAlocacaoLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratorioController : ControllerBase
    {
        private readonly ILaboratorioService _service;

        public LaboratorioController(ILaboratorioService service)
        {
            _service = service;
        }

        // GET api/laboratorio
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var laboratorios = await _service.GetAllAsync();
            return Ok(laboratorios);
        }

        // GET api/laboratorio/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var laboratorio = await _service.GetByIdAsync(id);

            if (laboratorio == null)
                return NotFound(new { mensagem = $"Laboratório com id {id} não encontrado." });

            return Ok(laboratorio);
        }

        // POST api/laboratorio
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LaboratorioRequestDto dto)
        {
            try
            {
                var laboratorio = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = laboratorio.IdLaboratorio }, laboratorio);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PUT api/laboratorio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LaboratorioRequestDto dto)
        {
            try
            {
                var laboratorio = await _service.UpdateAsync(id, dto);

                if (laboratorio == null)
                    return NotFound(new { mensagem = $"Laboratório com id {id} não encontrado." });

                return Ok(laboratorio);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // DELETE api/laboratorio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado = await _service.DeleteAsync(id);

            if (!deletado)
                return NotFound(new { mensagem = $"Laboratório com id {id} não encontrado." });

            return NoContent();
        }
    }
}