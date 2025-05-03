using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db.Services
{
    public interface IScheduleService
    {
        List<Lesson> GetLessonsForDay(Guid periodId, DateTime targetDate);
    }
}
