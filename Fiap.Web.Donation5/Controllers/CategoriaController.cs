using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fiap.Web.Donation5.Data;
using Fiap.Web.Donation5.Models;
using Fiap.Web.Donation5.Repository;

namespace Fiap.Web.Donation5.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaController(DataContext context)
        {
            _categoriaRepository = new CategoriaRepository(context);
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(_categoriaRepository.FindAll());
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaModel = _categoriaRepository.FindById((int)id);

            if (categoriaModel == null)
            {
                return NotFound();
            }

            return View(categoriaModel);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaModel categoriaModel)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.Insert(categoriaModel);
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaModel);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaModel = _categoriaRepository.FindById((int)id);
            if (categoriaModel == null)
            {
                return NotFound();
            }
            return View(categoriaModel);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaModel categoriaModel)
        {
            if (id != categoriaModel.CategoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoriaRepository.Update(categoriaModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaModelExists(categoriaModel.CategoriaId))
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
            return View(categoriaModel);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaModel = _categoriaRepository.FindById((int)id);
            if (categoriaModel == null)
            {
                return NotFound();
            }

            return View(categoriaModel);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaModel = _categoriaRepository.FindById((int)id);
            if (categoriaModel != null)
            {
                _categoriaRepository.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaModelExists(int id)
        {
            var categoriaModel = _categoriaRepository.FindById(id);

            if (categoriaModel != null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
