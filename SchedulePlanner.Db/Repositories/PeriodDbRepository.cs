using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulePlanner.Db.Repositories
{
    public class PeriodDbRepository : IPeriodRepository
    {
        private readonly DatabaseContext _context;

        public PeriodDbRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Period GetById(Guid id)
        {
            return _context.Periods
                .Include(p => p.Lessons)
                .Include(p => p.LessonTasks)
                .Include(p => p.Subjects)
                .Include(p => p.Teachers)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Period> GetAll()
        {
            return _context.Periods.ToList();
        }

        public List<Period> GetByUserId(Guid userId)
        {
            return _context.Periods
                .Where(p => p.UserId == userId)
                .ToList();
        }

        public void Add(Period period)
        {
            _context.Periods.Add(period);
            _context.SaveChanges();
        }

        public void Update(Period period)
        {
            _context.Periods.Update(period);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var period = _context.Periods.Find(id);
            if (period != null)
            {
                _context.Periods.Remove(period);
                _context.SaveChanges();
            }
        }
    }
}
