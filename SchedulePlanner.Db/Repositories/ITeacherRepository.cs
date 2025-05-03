using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;

namespace SchedulePlanner.Db.Repositories
{
    public interface ITeacherRepository
    {
        Teacher GetById(Guid id);
        IEnumerable<Teacher> GetByPeriodId(Guid periodId);
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(Guid id);
    }
}