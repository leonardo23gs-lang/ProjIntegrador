using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaAlocacaoLab.API.DTOs.Software;
using SistemaAlocacaoLab.API.Services;

namespace SistemaAlocacaoLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : ControllerBase
    {
        private readonly ISoftwareService _service;

        public SoftwareController(ISoftwareService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var softwares = await _service.GetAllAsync();
            return Ok(softwares);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var software = await _service.GetByIdAsync(id);
            if (software == null)
                return NotFound(new { mensagem = $"Software com id {id} não encontrado." });
            return Ok(software);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SoftwareRequestDto dto)
        {
            try
            {
                var software = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = software.IdSoftware }, software);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SoftwareRequestDto dto)
        {
            try
            {
                var software = await _service.UpdateAsync(id, dto);
                if (software == null)
                    return NotFound(new { mensagem = $"Software com id {id} não encontrado." });
                return Ok(software);
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
                return NotFound(new { mensagem = $"Software com id {id} não encontrado." });
            return NoContent();
        }
    }
}