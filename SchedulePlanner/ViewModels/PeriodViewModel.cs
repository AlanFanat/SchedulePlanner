using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePlanner.ViewModels
{
    public class PeriodViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Display(Name = "Название периода")]
        public string Name { get; set; }

        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата окончания")]
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
        public static Period ToModel(PeriodViewModel periodViewModel)
        {
            return new Period
            {
                Id = periodViewModel.Id,
                UserId = periodViewModel.UserId,
                Name = periodViewModel.Name,
                StartDate = periodViewModel.StartDate,
                EndDate = periodViewModel.EndDate
            };
        }
    }
}
