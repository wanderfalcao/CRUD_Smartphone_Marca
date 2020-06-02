using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_Smartphone_Marca.Models;
using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Data.Context;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.Model.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace CRUD_Smartphone_Marca.Controllers
{
    [Authorize]
    public class MarcaController : Controller
    {
        private readonly IMarcaService _MarcaService;

        public MarcaController(IMarcaService IMarcaService)
        {
            _MarcaService = IMarcaService;
        }

        // GET: Marca
        public async Task<IActionResult> Index()
        {
            return View(await _MarcaService.GetAllAsync());
        }

        // GET: Marca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaModel = await _MarcaService.GetByIdAsync(id.Value);
            if (marcaModel == null)
            {
                return NotFound();
            }

            return View(marcaModel);
        }

        // GET: Marca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Pais,qtdSmartphone")] MarcaEntity marcaModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _MarcaService.InsertAsync(marcaModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(marcaModel);
        }

        // GET: Marca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaModel = await _MarcaService.GetByIdAsync(id.Value);
            if (marcaModel == null)
            {
                return NotFound();
            }
            return View(marcaModel);
        }

        // POST: Marca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Pais,qtdSmartphone")] MarcaEntity marcaModel)
        {
            if (id != marcaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _MarcaService.UpdateAsync(marcaModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _MarcaService.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(marcaModel);
        }

        // GET: Marca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaModel = await _MarcaService.GetByIdAsync(id.Value);
            if (marcaModel == null)
            {
                return NotFound();
            }

            return View(marcaModel);
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _MarcaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckNome(string nome, int id)
        {
            if (await _MarcaService.CheckNomeAsync(nome, id))
            {
                return Json($"Nome: {nome} já existe!");
            }

            return Json(true);
        }
    }
}
