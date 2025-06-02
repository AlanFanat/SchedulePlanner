using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Db.Models;
using SchedulePlanner.Db.Repositories;
using SchedulePlanner.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchedulePlanner.Controllers
{
    public class PeriodController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IPeriodRepository periodRepository;

        public PeriodController(UserManager<User> userManager, IPeriodRepository periodRepository)
        {
            this.userManager = userManager;
            this.periodRepository = periodRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var periods = periodRepository.GetByUserId(userId);
            var periodViewModels = periods.Select(period => PeriodViewModel.FromModel(period)).ToList();
            var user = await userManager.GetUserAsync(User);
            ViewBag.SelectedPeriodId = user.SelectedPeriodId;
            return View(periodViewModels);
        }
        public async Task<IActionResult> Select(Guid periodId)
        {
            var user = await userManager.GetUserAsync(User);
            user.SelectedPeriodId = periodId;
            ViewBag.SelectedPeriodId = periodId;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PeriodViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var period = new Period
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            periodRepository.Add(period);

            // Обновляем выбранный период


            return RedirectToAction("Index", "Period");
        }
        [HttpGet]
        public IActionResult Edit(Guid periodId)
        {
            var period = periodRepository.GetById(periodId);
            var periodViewModel = PeriodViewModel.FromModel(period);
            return View(periodViewModel);
        }
        [HttpPost]
        public IActionResult Edit(PeriodViewModel viewModel)
        {
            periodRepository.Edit(PeriodViewModel.ToModel(viewModel));

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteAsync(Guid periodId)
        {
            var user = await userManager.GetUserAsync(User);
            if(user.SelectedPeriodId == periodId)
            {
                user.SelectedPeriodId = null;
            }
            periodRepository.Delete(periodId);
            return RedirectToAction("Index");
        }
    }
}
