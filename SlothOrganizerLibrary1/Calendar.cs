using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizerLibrary
{
    public class Calendar
    {
        public List<Assignment> Tasks { get; set; } = new List<Assignment>();
        public List<TimePeriod> Years { get; set; } = new List<TimePeriod>();
        public DateTime CurrentDate { get; set; }
    }
}
