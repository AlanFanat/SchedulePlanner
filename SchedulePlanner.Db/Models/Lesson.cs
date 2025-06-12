using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchedulePlanner.Db.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public Guid PeriodId { get; set; }
        public Period Period { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public LessonType LessonType { get; set; }

        public RecurrenceType RecurrenceType { get; set; }
        public DateTime StartDate { get; set; }
        public int RepeatsCount { get; set; }

        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public string Location { get; set; }
        public Guid? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [NotMapped]
        public DateTime EndDate => StartDate.AddDays( (int)RecurrenceType * (RepeatsCount-1));    
        [NotMapped]
        public TimeSpan EndTime => StartTime + TimeSpan.FromMinutes(DurationMinutes);
        [NotMapped]
        public string SubjectName => Subject.Name;
        [NotMapped]
        public string TeacherName => Teacher.Name;
    }
    public enum LessonType
    {
        [Display(Name = "Лекция")]
        Lecture,

        [Display(Name = "Практика")]
        Practice,

        [Display(Name = "Лабораторная работа")]
        Laboratory,

        [Display(Name = "Семинар")]
        Seminar,

        [Display(Name = "Консультация")]
        Consultation,

        [Display(Name = "Контрольная работа")]
        Test,

        [Display(Name = "Экзамен")]
        Exam,

        [Display(Name = "Зачёт")]
        Credit
    }
    public enum RecurrenceType
    {
        [Display(Name = "Каждый день")]
        Everyday = 1,

        [Display(Name = "Каждую неделю")]
        Weekly = 7,

        [Display(Name = "Каждые 2 недели")]
        BiWeekly = 14
    }
}
