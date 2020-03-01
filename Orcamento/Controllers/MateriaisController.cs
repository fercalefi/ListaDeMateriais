using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orcamento.Services;
using Orcamento.Models;
using Orcamento.Services.Exception;
using Orcamento.Models.ViewModels;
using System.Diagnostics;

namespace Orcamento.Controllers
{
    public class MateriaisController : Controller
    {
        private readonly MateriaisService _materiaisService;

        public MateriaisController(MateriaisService materiaisService)
        {
            _materiaisService = materiaisService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _materiaisService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Materiais materiais) 
        {
            if (!ModelState.IsValid)
            {
                var materiais1 = await _materiaisService.FindAllAsync();
                return View(materiais1);
            }
            await _materiaisService.InsertAsync(materiais);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _materiaisService.FindByIdAsync(id.Value);

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
                await _materiaisService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe." });
            }

            var obj = await _materiaisService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existe." });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Materiais materiais)
        {
            if (!ModelState.IsValid)
            {
                return View(materiais);
            }

            if (id != materiais.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Ids não correspondem." });
            }

            try
            {
                await _materiaisService.UpdateAsync(materiais);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int?id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });
            }

            var obj = await _materiaisService.FindByIdAsync(id.Value);

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