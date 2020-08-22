using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nexo.Data;
using Nexo.Models;

namespace Nexo.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly SystemContext _context;

        public FornecedorController(SystemContext context)
        {
            _context = context;
        }

        // GET: Fornecedor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornecedores.ToListAsync());
        }

        // GET: Fornecedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorModel = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.ID_Fornecedor == id);

            PopularProdutos(fornecedorModel);
            VerificarMaster(fornecedorModel);
            if (fornecedorModel == null)
            {
                return NotFound();
            }

            return View(fornecedorModel);
        }

        private void PopularProdutos(FornecedorModel fornecedorModel)
        {
           using (_context)
            {
                var query = _context.Produtos.Where(s => s.ID_Fornecedor == fornecedorModel.ID_Fornecedor);

                foreach (var produto in query)
                {
                    fornecedorModel.Produtos.Add(produto);
                }
            }
        }

        // GET: Fornecedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Fornecedor,Nome,CNPJ,Ativo,Data_Cadastro")] FornecedorModel fornecedorModel)
        {
            if (ModelState.IsValid)
            {
                VerificarData(fornecedorModel);
                _context.Add(fornecedorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedorModel);
        }

        // GET: Fornecedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorModel = await _context.Fornecedores.FindAsync(id);
            if (fornecedorModel == null)
            {
                return NotFound();
            }
            return View(fornecedorModel);
        }

        // POST: Fornecedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Fornecedor,Nome,CNPJ,Ativo,Data_Cadastro")] FornecedorModel fornecedorModel)
        {
            if (id != fornecedorModel.ID_Fornecedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    VerificarData(fornecedorModel);
                    _context.Update(fornecedorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorModelExists(fornecedorModel.ID_Fornecedor))
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
            return View(fornecedorModel);
        }

        // GET: Fornecedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorModel = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.ID_Fornecedor == id);
            if (fornecedorModel == null)
            {
                return NotFound();
            }

            return View(fornecedorModel);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedorModel = await _context.Fornecedores.FindAsync(id);
            _context.Fornecedores.Remove(fornecedorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void VerificarData(FornecedorModel fornecedorModel)
        {
            var diffTemp = DateTime.Now.Date - fornecedorModel.Data_Cadastro.Date;

            if (diffTemp.Days >= 1827)
            {
                fornecedorModel.Status = TiposStatus.Prata;
            }
        }

        private void VerificarMaster(FornecedorModel fornecedorModel)
        {
            int quantidadeProdutos = fornecedorModel.Produtos.Count;

            if (quantidadeProdutos >= 2000 && quantidadeProdutos < 5000)
            {
                fornecedorModel.Status = TiposStatus.Ouro;
            }
            else if (quantidadeProdutos >= 5000 && quantidadeProdutos < 10000)
            {
                fornecedorModel.Status = TiposStatus.Platina;
            }
            else if (quantidadeProdutos >= 10000)
            {
                fornecedorModel.Status = TiposStatus.Diamante;
            }
        }

        private bool FornecedorModelExists(int id)
        {
            return _context.Fornecedores.Any(e => e.ID_Fornecedor == id);
        }
    }
}
