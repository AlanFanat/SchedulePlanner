using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;

namespace SchedulePlanner.Db.Repositories
{
    public interface IPeriodRepository
    {
        Period GetById(Guid id);
        List<Period> GetAll();
        List<Period> GetByUserId(Guid userId);
        void Add(Period period);
        void Update(Period period);
        void Delete(Guid id);
    }
}