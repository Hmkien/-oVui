using System.CodeDom;
using System.Diagnostics;
using DoVuiHaiNao.Data;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoVuiHaiNao.Controllers
{
    public class HomeController : Controller
    {
        private readonly DoVuiHaiNaoContext _context;

        public HomeController(DoVuiHaiNaoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var danhMucs = await _context.Categories.ToListAsync();

            var quizzDanhMuc = await _context.Categories
                .Select(d => new QuizzDanhMucVM
                {
                    Title = d.Title,
                    Icon = d.Icon,
                    ColorIcon = d.ColorIcon,
                    Quizz = _context.Quizzs.Where(q => q.DanhMucId == d.Id).ToList(),
                    QuestionNumber = _context.Questions
                        .Count(e => _context.Quizzs
                            .Where(q => q.DanhMucId == d.Id)
                            .Select(q => q.Id)
                            .Contains(e.QuizzId))
                })
                .ToListAsync();
            var model = new HomeVM
            {
                DanhMucs = danhMucs,
                QuizzDanhMuc = quizzDanhMuc,
                
            };
            return View(model);
        }

    }
}
