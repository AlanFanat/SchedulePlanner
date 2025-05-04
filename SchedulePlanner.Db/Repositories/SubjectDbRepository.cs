using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulePlanner.Db.Repositories
{
    public class SubjectDbRepository : ISubjectRepository
    {
        private readonly DatabaseContext _context;

        public SubjectDbRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Subject GetById(Guid id)
        {
            return _context.Subjects.Find(id);
        }

        public IEnumerable<Subject> GetByPeriodId(Guid periodId)
        {
            return _context.Subjects
                .Where(s => s.PeriodId == periodId);
        }

        public void Add(Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }

        public void Update(Subject subject)
        {
            _context.Subjects.Update(subject);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                _context.SaveChanges();
            }
        }
    }
}
