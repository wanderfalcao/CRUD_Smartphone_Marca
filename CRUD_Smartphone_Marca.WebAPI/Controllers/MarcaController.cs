using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Exceptions;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(
            IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaEntity>>> GetMarcaEntity()
        {
            var marca = await _marcaService.GetAllAsync();
            return Ok(marca.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaEntity>> GetMarcaEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var marcaEntity = await _marcaService.GetByIdAsync(id);

            if (marcaEntity == null)
            {
                return NotFound("Mensagem de not found");
            }

            return marcaEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarcaEntity(int id, MarcaEntity marcaEntity)
        {
            if (id != marcaEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _marcaService.UpdateAsync(marcaEntity);
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MarcaEntity>> PostMarcaEntity(MarcaEntity marcaEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _marcaService.InsertAsync(marcaEntity);

            return Ok(marcaEntity);
        }

        // DELETE: api/Marca/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MarcaEntity>> DeleteMarcaEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var marcaEntity = await _marcaService.GetByIdAsync(id);
            if (marcaEntity == null)
            {
                return NotFound();
            }

            await _marcaService.DeleteAsync(id);

            return Ok(marcaEntity);
        }

        [HttpGet("CheckNome/{nome}/{id}")]
        public async Task<ActionResult<bool>> CheckNomeAsync(string nome, int id)
        {
            var isNameValid = await _marcaService.CheckNomeAsync(nome, id);

            return isNameValid;
        }

    }
}
