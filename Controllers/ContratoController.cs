using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using concilig.Models;
using desafioConcilig.Data;
using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Http;




namespace desafioConcilig.Controllers
{
    public class ContratoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContratoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contrato
        public async Task<IActionResult> Index()
        {
            var contratos = await _context.Contrato
                .Include(c => c.Cliente)
                .Include(p => p.Produto)
                
                .AsNoTracking()

                .ToListAsync();
            //return View(await _context.Contrato.ToListAsync());
            return View(contratos);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Upload(IFormFile file)
        {
            return View();
        }

        // GET: Contrato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .FirstOrDefaultAsync(m => m.id == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // GET: Contrato/Create
        public IActionResult Create()
        {
            ViewData["Produtos"] = new SelectList(_context.Produto, "id", "descricao");
            ViewData["Clientes"] = new SelectList(_context.Cliente, "id", "nome");
            return View();
        }

        // POST: Contrato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,referencia,valor,dataVencimento")] Contrato contrato, int Cliente, int Produto)
        {
            if (ModelState.IsValid)
            {
                contrato.Cliente = await _context.Cliente.FindAsync(Cliente);
                contrato.Produto = await _context.Produto.FindAsync(Produto);
                _context.Add(contrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contrato);
        }

        // GET: Contrato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .Include(c => c.Cliente)
                .Include(p => p.Produto)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.id == id);
            if (contrato == null)
            {
                return NotFound();
            }
            ViewData["Clientes"] = new SelectList(_context.Cliente, "id", "nome", contrato.Cliente.id);
            ViewData["Produtos"] = new SelectList(_context.Produto, "id", "descricao", contrato.Produto.id);
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,referencia,valor,dataVencimento")] Contrato contrato, int cliente_id, int produto_id)
        {
            if (id != contrato.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //contrato = await _context.Contrato.FindAsync(id);
                    
                    contrato.Cliente = await _context.Cliente.FindAsync(cliente_id);
                    contrato.Produto = await _context.Produto.FindAsync(produto_id);
                  
                    _context.Update(contrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratoExists(contrato.id))
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
            return View(contrato);
        }

        // GET: Contrato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contrato = await _context.Contrato
                .FirstOrDefaultAsync(m => m.id == id);
            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        // POST: Contrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contrato = await _context.Contrato.FindAsync(id);
            _context.Contrato.Remove(contrato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


     

        private bool ContratoExists(int id)
        {
            return _context.Contrato.Any(e => e.id == id);
        }
    }
}
