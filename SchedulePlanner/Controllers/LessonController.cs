using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulePlanner.Db.Models;
using SchedulePlanner.Db.Repositories;
using SchedulePlanner.Db.Services;
using SchedulePlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchedulePlanner.Controllers
{
    public class LessonController : Controller
    {
        private readonly IScheduleService scheduleService;
        private readonly ILessonRepository lessonRepository;
        private readonly ISubjectRepository subjectRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly UserManager<User> userManager;

        public LessonController(IScheduleService scheduleService, ILessonRepository lessonRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository, UserManager<User> userManager)
        {
            this.scheduleService = scheduleService;
            this.lessonRepository = lessonRepository;
            this.subjectRepository = subjectRepository;
            this.teacherRepository = teacherRepository;
            this.userManager = userManager;
        }

        public IActionResult Index(Guid lessonId)
        {
            var lesson = lessonRepository.GetById(lessonId);
            if (lesson == null)
            {
                return NotFound();
            }

            var viewModel = LessonViewModel.FromModel(lesson);
            return View(viewModel);
        }

        public async Task<IActionResult> CreateAsync(DateTime? date)
        {
            var user = await userManager.GetUserAsync(User);
            var periodId = user.SelectedPeriodId.GetValueOrDefault();
            var subjects = subjectRepository.GetByPeriodId(periodId);
            var subjects1 = subjects
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

            var model = new LessonViewModel
            {
                StartDate = date ?? DateTime.Now,
                PeriodId = periodId,
                Subjects = subjects1
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LessonViewModel model)
        {
            Guid subjectId;
            if (!Guid.TryParse(model.SubjectIdRaw, out subjectId))
            {
                ModelState.AddModelError("SubjectIdRaw", "Выберите предмет");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.GetUserAsync(User);
            var periodId = user.SelectedPeriodId.GetValueOrDefault();
            var lesson = new Lesson
            {
                Id = Guid.NewGuid(),
                PeriodId = periodId,
                SubjectId = subjectId,
                LessonType = model.LessonType,
                RecurrenceType = model.RecurrenceType,
                StartDate = model.StartDate,
                RepeatsCount = model.RepeatsCount,
                StartTime = model.StartTime,
                DurationMinutes = model.DurationMinutes,
                Location = model.Location,
                TeacherId = model.TeacherId
            };

            lessonRepository.Add(lesson);

            return RedirectToAction("Index", new { lessonId = lesson.Id });
        }
        // Загрузка формы (GET)
        public IActionResult LoadSubjectForm()
        {
            return PartialView("_AddSubjectForm", new SubjectViewModel());
        }

        // Обработка формы (POST)
        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                var subject = new Subject
                {
                    Id = Guid.NewGuid(),
                    PeriodId = user.SelectedPeriodId.GetValueOrDefault(),
                    Name = model.Name,
                    Color = model.Color
                };
                subjectRepository.Add(subject);

                return Json(new { success = true, id = subject.Id, name = subject.Name });
            }

            return PartialView("_AddSubjectForm", model); // Если есть ошибки — вернуть HTML
        }
        public async Task<IActionResult> DeleteAsync(Guid lessonId)
        {
            lessonRepository.Delete(lessonId);
            return RedirectToAction("Index", "Home");
        }
    }
}
