using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulePlanner.Db.Repositories
{
    public class LessonTaskDbRepository : ILessonTaskRepository
    {
        private readonly DatabaseContext _context;

        public LessonTaskDbRepository(DatabaseContext context)
        {
            _context = context;
        }

        public LessonTask GetById(Guid id)
        {
            return _context.LessonTasks.Find(id);
        }

        public IEnumerable<LessonTask> GetByPeriodId(Guid periodId)
        {
            return _context.LessonTasks
                .Where(t => t.PeriodId == periodId)
                .ToList();
        }

        public void Add(LessonTask task)
        {
            _context.LessonTasks.Add(task);
            _context.SaveChanges();
        }

        public void Update(LessonTask task)
        {
            _context.LessonTasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var task = _context.LessonTasks.Find(id);
            if (task != null)
            {
                _context.LessonTasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
