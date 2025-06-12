using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePlanner.ViewModels
{
    public class SubjectViewModel
    {
        public Guid Id { get; set; }
        public Guid PeriodId { get; set; }

        [Required(ErrorMessage = "Введите название предмета")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите цвет")]
        public string Color { get; set; }
        public static SubjectViewModel FromModel(Subject subject)
        {
            if (subject == null)
                return null;

            return new SubjectViewModel
            {
                Id = subject.Id,
                PeriodId = subject.PeriodId,
                Name = subject.Name,
                Color = subject.Color
            };
        }
        public Subject ToModel()
        {
            return new Subject
            {
                Id = this.Id,
                PeriodId = this.PeriodId,
                Name = this.Name,
                Color = this.Color
            };
        }
    }
}
