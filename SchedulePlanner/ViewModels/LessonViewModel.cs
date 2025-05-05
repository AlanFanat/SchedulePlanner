using Microsoft.AspNetCore.Mvc.Rendering;
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

        public Guid PeriodId { get; set; }
        public Period Period { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
       
        public List<SelectListItem> Subjects { get; set; } // Для выпадающего списка
        public string SubjectIdRaw { get; set; }
        public LessonType LessonType { get; set; }

        public RecurrenceType RecurrenceType { get; set; }
        public DateTime StartDate { get; set; }
        public int RepeatsCount { get; set; }

        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public string Location { get; set; }
        public Guid? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<SelectListItem> Teachers { get; set; } // Для выпадающего списка

        public DateTime EndDate => StartDate.AddDays((int)RecurrenceType * RepeatsCount);
        public TimeSpan EndTime => StartTime + TimeSpan.FromMinutes(DurationMinutes);

        public string SubjectName => Subject?.Name; // Для отображения выбранного предмета
        public string TeacherName => Teacher?.Name;
        public static LessonViewModel FromModel(Lesson lesson)
        {
            if (lesson == null)
                return null;

            return new LessonViewModel
            {
                Id = lesson.Id,
                PeriodId = lesson.PeriodId,
                Subject = lesson.Subject,
                SubjectId = lesson.SubjectId,
                LessonType = lesson.LessonType,
                RecurrenceType = lesson.RecurrenceType,
                RepeatsCount = lesson.RepeatsCount,
                StartDate = lesson.StartDate,
                StartTime = lesson.StartTime,
                DurationMinutes = lesson.DurationMinutes,
                Teacher = lesson.Teacher,
                Location = lesson.Location
            };
        }

    }
    
}
