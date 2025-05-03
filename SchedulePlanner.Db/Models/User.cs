using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db.Models
{
    public class User : IdentityUser
    {
        public List<Period> Periods { get; set; } = new List<Period>();
        public Period SelectedPeriod { get; set; }
        public Guid SelectedPeriodId { get; set; }
    }
}
