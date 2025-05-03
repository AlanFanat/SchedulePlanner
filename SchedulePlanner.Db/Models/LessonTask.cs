using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db.Models
{
    public class LessonTask
    {
        public Guid Id { get; set; }
        public Guid PeriodId { get; set; }
        public Period Period { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public Guid? SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime Date { get; set; }
    }
}
