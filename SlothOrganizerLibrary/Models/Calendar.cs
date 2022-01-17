using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizerLibrary.Models
{
    public class Calendar
    {
        public List<Task> Tasks { get; set; } = new List<Task>();
        public List<TimePeriod> Years { get; set; } = new List<TimePeriod>();
        public DateOnly CurrentDate { get; set; }
    }
}
