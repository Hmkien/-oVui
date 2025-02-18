using DoVuiHaiNao.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string AvatarUrl { get; set; }
        public string DisplayName { get; set; }
        public List<QuizHistoryViewModel> QuizHistory { get; set; }

        public class QuizHistoryViewModel
        {
            public string QuizName { get; set; }
            public int Score { get; set; }
            public DateTime PlayedDate { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng.");
            }

            AvatarUrl = "~/Image/avatar.png";
            DisplayName = user.DisplayName ?? "Người chơi";

            // Lấy lịch sử chơi quiz (giả lập dữ liệu)
            QuizHistory = new List<QuizHistoryViewModel>
            {
                new QuizHistoryViewModel { QuizName = "Đố vui kiến thức", Score = 85, PlayedDate = DateTime.Now.AddDays(-1) },
                new QuizHistoryViewModel { QuizName = "Thử thách IQ", Score = 92, PlayedDate = DateTime.Now.AddDays(-3) },
                new QuizHistoryViewModel { QuizName = "Ai là triệu phú?", Score = 75, PlayedDate = DateTime.Now.AddDays(-7) }
            };

            return Page();
        }
    }
}
