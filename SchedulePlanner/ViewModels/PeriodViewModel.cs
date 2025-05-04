using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePlanner.ViewModels
{
    public class PeriodViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public static PeriodViewModel FromModel(Period period)
        {
            if (period == null)
                return null;

            return new PeriodViewModel
            {
                Id = period.Id,
                UserId = period.UserId,
                Name = period.Name,
                StartDate = period.StartDate,
                EndDate = period.EndDate
            };
        }
    }
}
