using DoVuiHaiNao.Data;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoVuiHaiNao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class adminController : Controller
    {
        private readonly DoVuiHaiNaoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public adminController(UserManager<ApplicationUser> userManager, DoVuiHaiNaoContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var Quizz = (await _context.Quizzs.ToListAsync()).Count();
            var userCount = _userManager.Users.Count();
            var Quizzs = _context.Quizzs.ToList();
            var model = new AdminVM
            {
                SoNguoiThamGia = 888,
                SoQuizz = Quizz,
                NguoiThamGia = userCount,
                QuizzList = Quizzs
            };

            return View(model);
        }

    }
}
