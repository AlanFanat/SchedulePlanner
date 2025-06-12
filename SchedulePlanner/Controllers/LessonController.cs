using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        public IActionResult Delete(Guid lessonId)
        {
            lessonRepository.Delete(lessonId);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(Guid lessonId)
        {
            var lesson = lessonRepository.GetById(lessonId);
            if (lesson == null)
            {
                return NotFound();
            }

            var user = await userManager.GetUserAsync(User);
            var periodId = user.SelectedPeriodId.GetValueOrDefault();
            var subjects = subjectRepository.GetByPeriodId(periodId);
            var teachers = teacherRepository.GetByPeriodId(periodId);

            var viewModel = LessonViewModel.FromModel(lesson);
            /*viewModel.Subjects = subjects
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();
            viewModel.Teachers = teachers
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();*/

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid lessonId, LessonViewModel model)
        {
            if (lessonId != model.Id)
            {
                return NotFound();
            }

            Guid subjectId;
            if (!Guid.TryParse(model.SubjectIdRaw, out subjectId))
            {
                ModelState.AddModelError("SubjectIdRaw", "Выберите предмет");
            }

            if (!ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                var periodId = user.SelectedPeriodId.GetValueOrDefault();
                var subjects = subjectRepository.GetByPeriodId(periodId);
                var teachers = teacherRepository.GetByPeriodId(periodId);

                model.Subjects = subjects
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }).ToList();
                model.Teachers = teachers
                    .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name
                    }).ToList();

                return View(model);
            }

            var lesson = lessonRepository.GetById(lessonId);
            if (lesson == null)
            {
                return NotFound();
            }

            lesson.SubjectId = subjectId;
            lesson.LessonType = model.LessonType;
            lesson.RecurrenceType = model.RecurrenceType;
            lesson.StartDate = model.StartDate;
            lesson.RepeatsCount = model.RepeatsCount;
            lesson.StartTime = model.StartTime;
            lesson.DurationMinutes = model.DurationMinutes;
            lesson.Location = model.Location;
            lesson.TeacherId = model.TeacherId;

            lessonRepository.Update(lesson);

            return RedirectToAction("Index", new { lessonId = lesson.Id });
        }
    }
}
