using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizerLibrary
{
    public class TimePeriod
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End => Start.AddDays(Length - 1);
        public int Length { get; set; }
        public List<TimePeriod> ChildrenTimePeriods { get; set; } = new List<TimePeriod>();
        public int ActiveNumber { get; set; } = 0;
        public int CompletedNumber { get; set; } = 0;
        public int PartiallyCompletedNumber { get; set; } = 0;
        public int FailedNumber { get; set; } = 0;

        public TimePeriod(int id, DateTime start, int length, int active, int completed, int partiallyCompleted, int failed)
        {
            Id = id;
            Start = start;
            Length = length;
            ActiveNumber = active;
            CompletedNumber = completed;
            PartiallyCompletedNumber = partiallyCompleted;
            FailedNumber = failed;
        }
        public TimePeriod(DateTime start, int length, int active, int completed, int partiallyCompleted, int failed)
        {
            Start = start;
            Length = length;
            ActiveNumber = active;
            CompletedNumber = completed;
            PartiallyCompletedNumber = partiallyCompleted;
            FailedNumber = failed;
        }
        public TimePeriod(DateTime start, int length)
        {
            Start = start;
            Length = length;
        }
        public TimePeriod()
        {
            Start = DateTime.Now.Date;
            Length = 1;
        }

        public void CountStatuses(List<Assignment> tasks)
        {
            ActiveNumber = tasks.Where(x => x.State == TaskState.Active && IncludesTask(x)).ToList().Count;
            CompletedNumber = tasks.Where(x => x.State == TaskState.Completed && IncludesTask(x)).ToList().Count;
            PartiallyCompletedNumber = tasks.Where(x => x.State == TaskState.PartiallyCompleted && IncludesTask(x)).ToList().Count;
            FailedNumber = tasks.Where(x => x.State == TaskState.Failed && IncludesTask(x)).ToList().Count;
        }

        public bool IncludesTask(Assignment task)
        {
            return Start <= task.TimeLimits.Start && End >= task.TimeLimits.End;
        }
    }
}
