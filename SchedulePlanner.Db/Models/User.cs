using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db.Models
{
    public class User : IdentityUser
    {
        public Guid? SelectedPeriodId { get; set; }
    }
}
