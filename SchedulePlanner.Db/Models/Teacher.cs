using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public Guid PeriodId { get; set; }
        public Period Period { get; set; }

        public string Name { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
