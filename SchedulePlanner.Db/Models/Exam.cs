using System;

namespace SchedulePlanner.Db.Models
{
    public class Exam
    {
        public Guid Id { get; set; }
        public Guid PeriodId { get; set; }
        public Period Period { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public string Location { get; set; }
    }
}
