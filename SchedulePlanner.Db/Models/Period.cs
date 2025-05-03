using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db.Models
{
    public class Period
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Lesson> Lessons { get; set; }
        public List<LessonTask> LessonTasks { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
