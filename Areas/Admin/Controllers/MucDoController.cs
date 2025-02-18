using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Data;
using DoVuiHaiNao.Models;
using Microsoft.AspNetCore.Authorization;

namespace DoVuiHaiNao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MucDoController : Controller
    {
        private readonly DoVuiHaiNaoContext _context;

        public MucDoController(DoVuiHaiNaoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.MucDos.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MucDo mucDo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mucDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mucDo);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mucDo = await _context.MucDos.FindAsync(id);
            if (mucDo == null)
            {
                return NotFound();
            }
            return View(mucDo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MucDo mucDo)
        {
            if (id != mucDo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mucDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MucDoExists(mucDo.Id))
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
            return View(mucDo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mucDo = await _context.MucDos.FindAsync(id);
            if (mucDo != null)
            {
                _context.MucDos.Remove(mucDo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MucDoExists(int id)
        {
            return _context.MucDos.Any(e => e.Id == id);
        }
    }
}
