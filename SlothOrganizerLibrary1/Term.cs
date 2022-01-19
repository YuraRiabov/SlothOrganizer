using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizerLibrary
{
    public class Term
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Term()
        {
            Start = DateTime.Now.Date;
            End = DateTime.Now.Date;
        }
        public Term(DateTime start, DateTime end)
        {
            Start = start.Date;
            End = end.Date;
        }
    }
}
