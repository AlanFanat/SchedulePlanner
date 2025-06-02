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
        public IActionResult Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var periods = periodRepository.GetByUserId(userId);
            var periodViewModels = periods.Select(period => PeriodViewModel.FromModel(period)).ToList();
            return View(periodViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> Select(Guid periodId)
        {
            var user = await userManager.GetUserAsync(User);
            user.SelectedPeriodId = periodId;
            var result = await userManager.UpdateAsync(user);
            if(result.Succeeded)
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
    }
}
