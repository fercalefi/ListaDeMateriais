using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orcamento.Models;
using Orcamento.Models.ViewModels;
using Orcamento.Services;
using Orcamento.Services.Exception;

namespace Orcamento.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _empresaService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                var empresa1 = await _empresaService.FindAllAsync();
                return View(empresa1);
            }
            await _empresaService.InsertAsync(empresa);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _empresaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _empresaService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe." });
            }

            var obj = await _empresaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe." });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return View(empresa);
            }

            if (id != empresa.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Ids não correspondem." });
            }

            try
            {
                await _empresaService.UpdateAsync(empresa);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });
            }

            var obj = await _empresaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
            }

            return View(obj);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}