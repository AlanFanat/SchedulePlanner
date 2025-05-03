using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulePlanner.Db.Repositories
{
    public class LessonDbRepository : ILessonRepository
    {
        private readonly DatabaseContext _context;

        public LessonDbRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Lesson GetById(Guid id)
        {
            return _context.Lessons
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Lesson> GetAll()
        {
            return _context.Lessons
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .ToList();
        }

        public IEnumerable<Lesson> GetByPeriodId(Guid periodId)
        {
            return _context.Lessons
                .Where(l => l.PeriodId == periodId)
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .ToList();
        }

        public void Add(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            _context.SaveChanges();
        }

        public void Update(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
                _context.SaveChanges();
            }
        }
    }
}
