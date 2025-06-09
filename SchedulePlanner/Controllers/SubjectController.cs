using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SchedulePlanner.Db.Models;
using SchedulePlanner.Db.Repositories;
using System.Threading.Tasks;
using System;

namespace SchedulePlanner.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly UserManager<User> _userManager;

        public SubjectController(ISubjectRepository subjectRepository, UserManager<User> userManager)
        {
            _subjectRepository = subjectRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var periodId = user.SelectedPeriodId.GetValueOrDefault();
            var subjects = _subjectRepository.GetByPeriodId(periodId);
            return View(subjects);
        }

        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var periodId = user.SelectedPeriodId.GetValueOrDefault();
            
            var subject = new Subject
            {
                PeriodId = periodId
            };
            
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                subject.Id = Guid.NewGuid();
                _subjectRepository.Add(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public IActionResult Edit(Guid id)
        {
            var subject = _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _subjectRepository.Update(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public IActionResult Delete(Guid id)
        {
            var subject = _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _subjectRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 