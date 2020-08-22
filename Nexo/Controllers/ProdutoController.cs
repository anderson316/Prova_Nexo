using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nexo.Data;
using Nexo.Models;

namespace Nexo.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly SystemContext _context;

        public ProdutoController(SystemContext context)
        {
            _context = context;
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            var systemContext = _context.Produtos.Include(p => p.Fornecedor);
            return View(await systemContext.ToListAsync());
        }

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoModel = await _context.Produtos
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id_Produto == id);
            if (produtoModel == null)
            {
                return NotFound();
            }

            return View(produtoModel);
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            ViewData["ID_Fornecedor"] = new SelectList(_context.Fornecedores, "ID_Fornecedor", "ID_Fornecedor");
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Produto,Nome,Valor,Data_Cadastro,ID_Fornecedor")] ProdutoModel produtoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Fornecedor"] = new SelectList(_context.Fornecedores, "ID_Fornecedor", "ID_Fornecedor", produtoModel.ID_Fornecedor);
            return View(produtoModel);
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoModel = await _context.Produtos.FindAsync(id);
            if (produtoModel == null)
            {
                return NotFound();
            }
            ViewData["ID_Fornecedor"] = new SelectList(_context.Fornecedores, "ID_Fornecedor", "ID_Fornecedor", produtoModel.ID_Fornecedor);
            return View(produtoModel);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Produto,Nome,Valor,Data_Cadastro,ID_Fornecedor")] ProdutoModel produtoModel)
        {
            if (id != produtoModel.Id_Produto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoModelExists(produtoModel.Id_Produto))
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
            ViewData["ID_Fornecedor"] = new SelectList(_context.Fornecedores, "ID_Fornecedor", "ID_Fornecedor", produtoModel.ID_Fornecedor);
            return View(produtoModel);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoModel = await _context.Produtos
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id_Produto == id);
            if (produtoModel == null)
            {
                return NotFound();
            }

            return View(produtoModel);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtoModel = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produtoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoModelExists(int id)
        {
            return _context.Produtos.Any(e => e.Id_Produto == id);
        }
    }
}
