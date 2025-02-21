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
             Quizz = _context.Quizzs
             .Where(q => q.DanhMucId == d.Id)
             .Select(q => new Quizz
             {
                 Id = q.Id,
                 Title = q.Title,
                 Description = q.Description,
                 ImageQuizz = q.ImageQuizz,
                 QuestionNumber = _context.Questions
                     .Where(e => e.QuizzId == q.Id)
                     .Count()
             })
             .ToList()
         })
            .ToListAsync();

            var model = new HomeVM
            {
                DanhMucs = danhMucs,
                QuizzDanhMuc = quizzDanhMuc,

            };
            return View(model);
        }
        public async Task<IActionResult> CategoryQuizz(int Id)
        {
            var danhMucs = await _context.Categories.ToListAsync();
            var danhMuc = await _context.Categories
                .Where(e => e.Id == Id)
                .ToListAsync();
            var quizzList = await _context.Quizzs
                .Where(q => q.DanhMucId == Id)
                .Select(q => new Quizz
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    ImageQuizz = q.ImageQuizz,
                    QuestionNumber = _context.Questions.Count(e => e.QuizzId == q.Id)
                })
                .ToListAsync();
            var quizzDanhMuc = danhMuc.Select(d => new QuizzDanhMucVM
            {
                Title = d.Title,
                Icon = d.Icon,
                ColorIcon = d.ColorIcon,
                Quizz = quizzList
            }).ToList();

            var model = new HomeVM
            {
                DanhMucs = danhMucs,
                QuizzDanhMuc = quizzDanhMuc
            };

            return View("Index", model);
        }
        public async Task<IActionResult> getQuizz(int id)
        {
            var quizz = await _context.Quizzs.FindAsync(id);
            var QuestionNumber = _context.Questions.Count(e => e.QuizzId == id);


            if (quizz == null)
            {
                return NotFound();
            }
            var MucDo = await _context.MucDos.FindAsync(quizz.MucDoId);
            var quizzList = await _context.Quizzs
                .Where(q => q.DanhMucId == quizz.DanhMucId)
                .Select(q => new Quizz
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    ImageQuizz = q.ImageQuizz,
                    QuestionNumber = _context.Questions.Count(e => e.QuizzId == q.Id),
                    MucDo = MucDo
                })
                .ToListAsync();

            var model = new QuizzVM
            {
                Quizz = quizz,
                Quizzs = quizzList,
                NumberQuestion = QuestionNumber

            };
            return View(model);
        }
        public async Task<IActionResult> StartQuizz(int Id)
        {
            var questions = await _context.Questions
                .Where(q => q.QuizzId == Id)
                .Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                    QuizzId = q.QuizzId,
                    QuestionText = q.QuestionText,
                    Image = q.Image,
                    Answers = q.Answers.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect,
                        Image = a.Image
                    }).ToList()
                })
                .ToListAsync();

            var quizSubmission = new QuizSubmissionViewModel
            {
                QuizzId = Id,
                Questions = questions,
                DurationInMinutes = _context.Quizzs.Where(e => e.Id == Id).Select(q => q.DurationInMinutes).FirstOrDefault()
            };

            return View(quizSubmission);
        }



    }

}
