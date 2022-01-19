using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizerLibrary
{
    public class TimePeriod
    {
        public DateTime Start { get; set; }
        public DateTime End { get; private set; }
        public int Length { get; set; }
        public List<TimePeriod> ChildrenTimePeriods { get; set; } = new List<TimePeriod>();
        public int ActiveNumber { get; set; } = 0;
        public int CompletedNumber { get; set; } = 0;
        public int PartiallyCompletedNumber { get; set; } = 0;
        public int FailedNumber { get; set; } = 0;
    }
}
