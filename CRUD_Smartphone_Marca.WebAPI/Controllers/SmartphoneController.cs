using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Entities;
using CRUD_Smartphone_Marca.Model.Exceptions;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class SmartphoneController : ControllerBase
    {
        private readonly ISmartphoneSevice _smartphoneService;

        public SmartphoneController(
            ISmartphoneSevice marcaService)
        {
            _smartphoneService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartphoneEntity>>> GetSmartphoneEntity()
        {
            var marca = await _smartphoneService.GetAllAsync();
            return Ok(marca.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SmartphoneEntity>> GetSmartphoneEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var smartphoneEntity = await _smartphoneService.GetByIdAsync(id);

            if (smartphoneEntity == null)
            {
                return NotFound();
            }

            return Ok(smartphoneEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmartphoneEntity(int id, SmartphoneEntity smartphoneEntity)
        {
            if (id != smartphoneEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _smartphoneService.UpdateAsync(smartphoneEntity);
                return Ok();
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SmartphoneEntity>> PostSmartphoneEntity(SmartphoneMarcaAggregateEntity smartphoneMarcaAggregateEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _smartphoneService.InsertAsync(smartphoneMarcaAggregateEntity);

                return Ok(smartphoneMarcaAggregateEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Smartphone/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SmartphoneEntity>> DeleteSmartphoneEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var smartphoneEntity = await _smartphoneService.GetByIdAsync(id);
            if (smartphoneEntity == null)
            {
                return NotFound();
            }

            await _smartphoneService.DeleteAsync(id);

            return Ok(smartphoneEntity);
        }

    }
}
