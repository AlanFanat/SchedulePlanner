using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;

namespace SchedulePlanner.Db.Repositories
{
    public interface ILessonTaskRepository
    {
        LessonTask GetById(Guid id);
        IEnumerable<LessonTask> GetByPeriodId(Guid periodId);
        void Add(LessonTask task);
        void Update(LessonTask task);
        void Delete(Guid id);
    }
}