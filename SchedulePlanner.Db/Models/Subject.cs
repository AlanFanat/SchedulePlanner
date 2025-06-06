﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        public Guid PeriodId { get; set; }
        public Period Period { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<LessonTask> LessonTasks { get; set; } = new List<LessonTask>();
    }
}
