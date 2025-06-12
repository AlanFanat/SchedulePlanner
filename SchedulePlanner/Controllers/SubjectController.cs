using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SchedulePlanner.Db.Models;
using SchedulePlanner.Db.Repositories;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using SchedulePlanner.ViewModels;

namespace SchedulePlanner.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILessonRepository _lessonRepository;

        public SubjectController(ISubjectRepository subjectRepository, UserManager<User> userManager, ILessonRepository lessonRepository)
        {
            _subjectRepository = subjectRepository;
            _userManager = userManager;
            _lessonRepository = lessonRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var periodId = user.SelectedPeriodId.GetValueOrDefault(); //Выбранный период
                if (periodId == Guid.Empty)
                {
                    return RedirectToAction("Index", "Period");
                }
                var subjects = _subjectRepository.GetByPeriodId(periodId).Select(subject => SubjectViewModel.FromModel(subject));
                return View(subjects);
            }
            return NotFound();
        }

        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var periodId = user.SelectedPeriodId.GetValueOrDefault();
            
            var subject = new Subject
            {
                PeriodId = periodId
            };
            
            return View(SubjectViewModel.FromModel(subject));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubjectViewModel subjectViewModel)
        {
            if (ModelState.IsValid)
            {
                var subject = subjectViewModel.ToModel();
                subject.Id = Guid.NewGuid();
                _subjectRepository.Add(subject);
                return RedirectToAction("Index");
            }
            return View(subjectViewModel);
        }

        public IActionResult Edit(Guid id)
        {
            var subject = _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(SubjectViewModel.FromModel(subject));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, SubjectViewModel subjectViewModel)
        {
            if (id != subjectViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var subject = subjectViewModel.ToModel();
                _subjectRepository.Update(subject);
                return RedirectToAction("Index");
            }
            return View(subjectViewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var subject = _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(SubjectViewModel.FromModel(subject));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            bool hasLesson = _lessonRepository.HasSubject(id);
            if(hasLesson)
            {
                ModelState.AddModelError("", "Удаление невозможно, так как данный предмет используется в каком-то занятии");
                var subject = _subjectRepository.GetById(id);
                return View(SubjectViewModel.FromModel(subject));
            }    
            _subjectRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
} 