using System;
using System.Collections.Generic;
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
        public int LessonsCount { get; set; }

        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Location { get; set; }

        public Guid? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public TimeSpan EndTime => StartTime + TimeSpan.FromMinutes(DurationMinutes);
        public string SubjectName => Subject.Name;
        public string TeacherName => Teacher.Name;
    }
    public enum LessonType
    {
        Lecture,
        Practice,
        Laboratory,
        Seminar,
        Consultation,
        Test,
        Exam,
        Credit
    }
    public enum RecurrenceType
    {
        None = 0,           // Без повторения
        Weekly = 7,         // Каждую неделю
        BiWeekly = 14       // Каждые 2 недели
    }
}
