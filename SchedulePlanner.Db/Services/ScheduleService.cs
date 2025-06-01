using SchedulePlanner.Db.Models;
using SchedulePlanner.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulePlanner.Db.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ILessonRepository lessonRepository;

        public ScheduleService(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        public List<Lesson> GetLessonsForDay(Guid periodId, DateTime targetDate)
        {
            // Получаем все уроки для выбранного периода
            var lessons = lessonRepository.GetByPeriodId(periodId);

            // Фильтруем занятия по дате
            var filteredLessons = lessons
                .Where(l => IsLessonOnDay(l, targetDate))
                .OrderBy(l => l.StartTime)
                .ToList();

            return filteredLessons;
        }
        private bool IsLessonOnDay(Lesson lesson, DateTime targetDate)
        {
            // Проверка, попадает ли занятие в указанную дату
            bool isCorrectDay = lesson.StartDate.Date <= targetDate.Date && lesson.EndDate >= targetDate.Date;

            if (!isCorrectDay)
            {
                return false;
            }
            //проверка повторяемости
            var recurrenceType = (int)lesson.RecurrenceType;
            return (targetDate - lesson.StartDate).Days % recurrenceType == 0;
        }
    }
}
