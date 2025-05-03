using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;

namespace SchedulePlanner.Db.Repositories
{
    public interface ISubjectRepository
    {
        Subject GetById(Guid id);
        IEnumerable<Subject> GetByPeriodId(Guid periodId);
        void Add(Subject subject);
        void Update(Subject subject);
        void Delete(Guid id);
    }
}