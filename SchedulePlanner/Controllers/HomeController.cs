using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchedulePlanner.Db.Models;
using SchedulePlanner.Db.Services;
using SchedulePlanner.Models;
using SchedulePlanner.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePlanner.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> userManager;
        private readonly IScheduleService scheduleService;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IScheduleService scheduleService)
        {
            _logger = logger;
            this.userManager = userManager;
            this.scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index(DateTime? selectedDate)
        {
            //если выбран какой-то день, то будут загружены его данные,
            //иначе будут загружены данные сегодняшего дня
            var date = selectedDate ?? DateTime.Today;
            ViewBag.SelectedDate = date;

            var user = await userManager.GetUserAsync(User); // Получаем текущего пользователя
            if (user != null)
            {
                //var userId = user.Id;       // ID пользователя
                //var userName = user.UserName; // Имя пользователя
                var periodId = user.SelectedPeriodId.GetValueOrDefault(); //Выбранный период
                if (periodId == Guid.Empty)
                {
                    return RedirectToAction("Index", "Period");
                }
                var lessons = scheduleService.GetLessonsForDay(periodId, date);
                var lessonViewModels = lessons.Select(lesson => LessonViewModel.FromModel(lesson)).ToList();
                return View(lessonViewModels);
            }
            _logger.LogWarning("Пользователь не найден");
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
