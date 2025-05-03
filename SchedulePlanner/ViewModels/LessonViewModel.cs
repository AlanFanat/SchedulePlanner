using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePlanner.ViewModels
{
    public class LessonViewModel
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
        public LessonType LessonType { get; set; }

        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public string TeacherName { get; set; }
        public string Location { get; set; }

        // Вычисляемое свойство
        public TimeSpan EndTime => StartTime + TimeSpan.FromMinutes(DurationMinutes);
        public static LessonViewModel FromModel(Lesson lesson)
        {
            if (lesson == null)
                return null;

            return new LessonViewModel
            {
                Id = lesson.Id,
                SubjectName = lesson.Subject?.Name ?? "Без предмета",
                LessonType = lesson.LessonType,
                StartDate = lesson.StartDate,
                StartTime = lesson.StartTime,
                DurationMinutes = lesson.DurationMinutes,
                TeacherName = lesson.Teacher?.Name,
                Location = lesson.Location
            };
        }
    }
}
