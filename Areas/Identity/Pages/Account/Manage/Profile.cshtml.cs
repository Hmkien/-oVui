using DoVuiHaiNao.Data;
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
    public class ProfileModel(UserManager<ApplicationUser> userManager, DoVuiHaiNaoContext context) : PageModel
    {
        public string AvatarUrl { get; set; }
        public string DisplayName { get; set; }
        public List<QuizHistoryViewModel> QuizHistory { get; set; }

        public class QuizHistoryViewModel
        {
            public string QuizName { get; set; }
            public double Score { get; set; }
            public DateTime PlayedDate { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng.");
            }

            AvatarUrl = "~/Image/avatar.png";
            DisplayName = user.DisplayName ?? "Người chơi";

            QuizHistory = (from result in context.Results
                           join quizz in context.Quizzs
                           on result.QuizId equals quizz.Id
                           where result.UserId == user.Id
                           select new QuizHistoryViewModel
                           {
                               QuizName = quizz.Title,
                               Score = result.Score,
                               PlayedDate = result.CreatedDate
                           }).ToList();

            return Page();
        }
    }
}
