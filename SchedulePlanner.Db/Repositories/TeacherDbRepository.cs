using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulePlanner.Db.Repositories
{
    public class TeacherDbRepository : ITeacherRepository
    {
        private readonly DatabaseContext _context;

        public TeacherDbRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Teacher GetById(Guid id)
        {
            return _context.Teachers.Find(id);
        }

        public IEnumerable<Teacher> GetByPeriodId(Guid periodId)
        {
            return _context.Teachers
                .Where(t => t.PeriodId == periodId)
                .ToList();
        }

        public void Add(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        public void Update(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }
        }
    }
}
