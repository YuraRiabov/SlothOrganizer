using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizerLibrary
{
    public class Assignment
    {
        public int Id { get; set; } = -1;
        public string Text { get; set; }
        public List<Assignment> SubTasks { get; set; } = new List<Assignment>();
        public Term TimeLimits { get; set; } = new Term();
        public TaskState State { get; set; } = TaskState.Inactive;
        public bool IsSubTask { get; set; }

        public Assignment()
        {
            Text = "";
            TimeLimits = new Term();
            IsSubTask = false;
        }
        public Assignment(bool isSubTask)
        {
            Text = "";
            TimeLimits = new Term();
            IsSubTask = isSubTask;
        }
        public Assignment(int id, string text, DateTime start, DateTime end, int state)
        {
            Text = text;
            TimeLimits.Start = start;
            TimeLimits.End = end;
            State = (TaskState)state;
            Id = id;
        }
        public void UpdateState()
        {
            DateTime currentDate = DateTime.Now.Date;
            int completed = 0;
            int uncompleted = 0;
            foreach (Assignment subTask in SubTasks)
            {
                if (subTask.State == TaskState.PartiallyCompleted || subTask.State == TaskState.Completed)
                {
                    completed++;
                }
                else
                {
                    uncompleted++;
                }
            }
            if (State != TaskState.Completed)
            {
                if (currentDate < TimeLimits.Start)
                {
                    if (SubTasks.Count != 0 && completed != 0)
                    {
                        State = TaskState.PartiallyCompleted;
                    }
                    else
                    {
                        State = TaskState.Inactive;
                    }
                }
                else if (currentDate <= TimeLimits.End)
                {
                    if (SubTasks.Count != 0 && completed != 0)
                    {
                        State = TaskState.PartiallyCompleted;
                    }
                    else
                    {
                        State = TaskState.Active;
                    }
                }
                else if (currentDate >= TimeLimits.End && State != TaskState.Completed && State != TaskState.PartiallyCompleted)
                {
                    if (SubTasks.Count != 0 && completed != 0)
                    {
                        State = TaskState.PartiallyCompleted;
                    }
                    else
                    {
                        State = TaskState.Failed;
                    }
                } 
            }
        }
    }
}
