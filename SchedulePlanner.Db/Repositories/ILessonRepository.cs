using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db.Repositories
{
    public interface ILessonRepository
    {
        Lesson GetById(Guid id);
        IEnumerable<Lesson> GetAll();
        IEnumerable<Lesson> GetByPeriodId(Guid periodId);
        void Add(Lesson lesson);
        void Update(Lesson lesson);
        void Delete(Guid id);
        bool HasSubject(Guid subjectId);
    }
}
