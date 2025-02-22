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
using DoVuiHaiNao.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DoVuiHaiNao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuizzsController : Controller
    {
        private readonly DoVuiHaiNaoContext _context;

        public QuizzsController(DoVuiHaiNaoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doVuiHaiNaoContext = _context.Quizzs.Include(q => q.DanhMuc).Include(e => e.MucDo);
            return View(await doVuiHaiNaoContext.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["DanhMucId"] = new SelectList(_context.Categories, "Id", "Title");
            ViewData["MucDo"] = new SelectList(_context.MucDos, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Quizz quizz)
        {
            if (ModelState.IsValid)
            {
                quizz.Status = Status.Approved;

                var mucdo = await _context.MucDos.FindAsync(quizz.MucDoId);
                quizz.MucDo = mucdo;

                if (quizz.File != null)
                {

                    quizz.ImageQuizz = $"~/Image/{await ImportFile(quizz.File)}";
                }

                _context.Add(quizz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DanhMucId"] = new SelectList(_context.Categories, "Id", "Discriminator", quizz.DanhMucId);
            ViewData["MucDo"] = new SelectList(_context.MucDos, "Id", "Name", quizz.MucDoId);
            return View(quizz);
        }
        private async Task<string> ImportFile(IFormFile file)
        {
            var fileName = file.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await _context.Quizzs.FindAsync(id);
            if (quizz == null)
            {
                return NotFound();
            }
            ViewData["MucDo"] = new SelectList(_context.MucDos, "Id", "Name", quizz.MucDoId);
            ViewData["DanhMucId"] = new SelectList(_context.Categories, "Id", "Title", quizz.DanhMucId);
            return View(quizz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Quizz quizz)
        {
            if (id != quizz.Id)
            {
                return NotFound();
            }

            var oQuizz = await _context.Quizzs.FindAsync(id);
            if (oQuizz == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["MucDo"] = new SelectList(_context.MucDos, "Id", "Name", quizz.MucDoId);
                ViewData["DanhMucId"] = new SelectList(_context.Categories, "Id", "Title", quizz.DanhMucId);
                return View(quizz);
            }

            try
            {
                if (quizz.File != null)
                {
                    quizz.ImageQuizz = $"~/Image/{await ImportFile(quizz.File)}";
                }
                else
                {
                    quizz.ImageQuizz = oQuizz.ImageQuizz;
                }

                _context.Entry(oQuizz).CurrentValues.SetValues(quizz);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizzExists(quizz.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quizz = await _context.Quizzs.FindAsync(id);
            if (quizz != null)
            {
                _context.Quizzs.Remove(quizz);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizzExists(int id)
        {
            return _context.Quizzs.Any(e => e.Id == id);
        }
        [HttpGet]
        public async Task<IActionResult> Reject(int Id)
        {
            var model = await _context.Quizzs.FindAsync(Id);
            if (model == null)
            {
                return NotFound();
            }
            model.Status = Status.Pending;
            _context.Quizzs.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Approve(int Id)
        {
            var model = await _context.Quizzs.FindAsync(Id);
            if (model == null)
            {
                return NotFound();
            }
            model.Status = Status.Approved;
            _context.Quizzs.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Config(int Id)
        {
            var quizzExisting = await _context.Quizzs
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == Id);

            if (quizzExisting == null)
            {
                return NotFound();
            }

            var model = new QuizSubmissionViewModel
            {
                QuizzId = quizzExisting.Id,
                Questions = quizzExisting.Questions.Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    Image = q.Image,
                    Answers = q.Answers.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect,
                        Image = a.Image
                    }).ToList()
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfigQuestion(QuizSubmissionViewModel viewModel)
        {
            if (viewModel?.Questions == null || !viewModel.Questions.Any())
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var quizz = await _context.Quizzs.FindAsync(viewModel.QuizzId);
            if (quizz == null)
            {
                return BadRequest("Bài Quiz không tồn tại.");
            }
            var oQuestion = await _context.Questions
                          .Where(e => e.QuizzId == viewModel.QuizzId)
                          .ToListAsync();
            var oAnswer = await _context.Answers
                .Where(a => oQuestion.Select(q => q.Id).Contains(a.QuestionId))
                .ToListAsync();
            _context.Answers.RemoveRange(oAnswer);
            _context.Questions.RemoveRange(oQuestion);
            await _context.SaveChangesAsync();

            var questions = new List<Question>();
            var answers = new List<Answer>();
            foreach (var item in viewModel.Questions)
            {
                var image = item.File != null ? await ImportFile(item.File) : null;

                var question = new Question
                {
                    QuizzId = viewModel.QuizzId,
                    QuestionText = item.QuestionText,
                    Quiz = quizz,
                    Image = image
                };
                questions.Add(question);
                foreach (var answer in item.Answers)
                {
                    var imageAS = answer.File != null ? await ImportFile(answer.File) : null;

                    answers.Add(new Answer
                    {
                        AnswerText = answer.AnswerText,
                        IsCorrect = answer.IsCorrect,
                        Image = imageAS,
                        Question = question
                    });
                }
            }

            await _context.Questions.AddRangeAsync(questions);
            await _context.Answers.AddRangeAsync(answers);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
