﻿using CRUD_Smartphone_Marca.Model.Entities;
using CRUD_Smartphone_Marca.Model.Exceptions;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.MVC.HttpServices;
using CRUD_Smartphone_Marca.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Smartphone_Marca.MVC.Controllers
{
    [Authorize]
    public class SmartphoneController : Controller
    {
        private readonly ISmartphoneSevice _smartphoneService;
        private readonly IMarcaHttpService _marcaService;

        public SmartphoneController(
            ISmartphoneSevice smartphoneService,
            IMarcaHttpService marcaService)
        {
            _smartphoneService = smartphoneService;
            _marcaService = marcaService;
        }

        // GET: Marca
        public async Task<IActionResult> Index()
        {
            var smartphone = await _smartphoneService.GetAllAsync();

            if (smartphone == null)
                return Redirect("/Identity/Account/Login");
            return View(smartphone);
        }

        // GET: Marca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smartphoneModel = await _smartphoneService.GetByIdAsync(id.Value);
            if (smartphoneModel == null)
            {
                return NotFound();
            }

            return View(smartphoneModel);
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
        public async Task<IActionResult> Create(SmartphoneEntity smartphoneModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _smartphoneService.InsertAsync(smartphoneModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(smartphoneModel);
        }

        // GET: Marca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smartphoneModel = await _smartphoneService.GetByIdAsync(id.Value);
            if (smartphoneModel == null)
            {
                return NotFound();
            }
            return View(smartphoneModel);
        }

        // POST: Marca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SmartphoneEntity smartphoneModel)
        {
            if (id != smartphoneModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _smartphoneService.UpdateAsync(smartphoneModel);                
                return RedirectToAction(nameof(Index));
            }
            return View(smartphoneModel);
        }

        // GET: Marca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smartphoneModel = await _smartphoneService.GetByIdAsync(id.Value);
            if (smartphoneModel == null)
            {
                return NotFound();
            }

            return View(smartphoneModel);
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _smartphoneService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
