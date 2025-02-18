using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Data;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace DoVuiHaiNao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DanhMucsController : Controller
    {
        private readonly DoVuiHaiNaoContext _context;

        public DanhMucsController(DoVuiHaiNaoContext context)
        {
            _context = context;
        }

        // GET: Admin/DanhMucs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Icon,ColorIcon,Id,CreatedDate,Status")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                danhMuc.Status = Status.Approved;
                _context.Add(danhMuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMuc);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.Categories.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Icon,ColorIcon,Id,CreatedDate,Status")] DanhMuc danhMuc)
        {
            if (id != danhMuc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucExists(danhMuc.Id))
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
            return View(danhMuc);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMuc = await _context.Categories.FindAsync(id);
            if (danhMuc != null)
            {
                _context.Categories.Remove(danhMuc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
