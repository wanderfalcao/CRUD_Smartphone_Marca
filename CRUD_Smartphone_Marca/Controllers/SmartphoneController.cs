using CRUD_Smartphone_Marca.Model.Entities;
using CRUD_Smartphone_Marca.Model.Exceptions;
using CRUD_Smartphone_Marca.Model.Interfaces.Services;
using CRUD_Smartphone_Marca.MVC.HttpServices;
using CRUD_Smartphone_Marca.MVC.ViewModels;
using CRUD_Smartphone_Marca.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Create()
        {
            var smartphoneViewModel = new SmartphoneMarcaAggregateViewModel(await _marcaService.GetAllAsync());
            return View(smartphoneViewModel);
        }

        // POST: Marca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(SmartphoneMarcaAggregateViewModel smartphoneMarcaAggregateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var smartphoneMarcaAggregateEntity = smartphoneMarcaAggregateViewModel.ToAggregateEntity();
                    await _smartphoneService.InsertAsync(smartphoneMarcaAggregateEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(smartphoneMarcaAggregateViewModel);
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

            var smartphoneViewModel = new SmartphoneViewModel(smartphoneModel, await _marcaService.GetAllAsync());

            return View(smartphoneViewModel);
        }

        // POST: Marca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SmartphoneEntity smartphoneEntity)
        {
            if (id != smartphoneEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _smartphoneService.UpdateAsync(smartphoneEntity);
                }
                catch(EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return View(new SmartphoneViewModel(smartphoneEntity));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _smartphoneService.GetByIdAsync(id) == null)
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
            return View(new SmartphoneViewModel(smartphoneEntity));
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
