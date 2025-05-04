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
    }
}
