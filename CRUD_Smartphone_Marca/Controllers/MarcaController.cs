using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_Smartphone_Marca.Domain.Models;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.Model.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CRUD_Smartphone_Marca.MVC.HttpServices;
using System.Net;

namespace CRUD_Smartphone_Marca.Controllers
{
    [Authorize]
    public class MarcaController : Controller
    {
        private readonly IMarcaHttpService _marcaService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MarcaController(
            IMarcaHttpService marcaService,
            SignInManager<IdentityUser> signInManager)
        {
            _marcaService = marcaService;
            _signInManager = signInManager;
        }

        // GET: Marca
        public async Task<IActionResult> Index()
        {
            var marcas = await _marcaService.GetAllAsync();

            if(marcas == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            return View(marcas);
        }

        // GET: Marca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpResponseMessage = await _marcaService.GetByIdHttpAsync(id.Value);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    await _signInManager.SignOutAsync();
                    return Redirect("/Identity/Account/Login");
                }
                else
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, message);

                    var marcas = await _marcaService.GetAllAsync();
                    return View(nameof(Index), marcas);
                }
            }

            var marcaModel = await _marcaService.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(MarcaEntity marcaEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _marcaService.InsertAsync(marcaEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(marcaEntity);
        }

        // GET: Marca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaModel = await _marcaService.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Edit(int id, MarcaEntity marcaEntity)
        {
            if (id != marcaEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _marcaService.UpdateAsync(marcaEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _marcaService.GetByIdAsync(id) == null)
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
            return View(marcaEntity);
        }

        // GET: Marca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaModel = await _marcaService.GetByIdAsync(id.Value);
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
            await _marcaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckNome(string nome, int id)
        {
            if (await _marcaService.CheckNomeAsync(nome, id))
            {
                return Json($"Nome: {nome} já existe!");
            }

            return Json(true);
        }
    }
}
