using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizerLibrary;

namespace Sloth_Organizer
{
    public class TaskSelector
    {
        private List<Assignment> allTasks;

        public List<Assignment> RefreshTaskList(bool isInactive, bool isActive, bool isCompleted, bool isPartiallyCompleted, bool isFailed, DateTime start, DateTime end)
        {
            RefreshAllTasks();
            List<Assignment> tasks = new List<Assignment>();
            if (isInactive)
            {
                foreach (Assignment task in ChooseTasks(TaskState.Inactive, start, end))
                {
                    tasks.Add(task);
                }
            }
            if (isActive)
            {
                foreach (Assignment task in ChooseTasks(TaskState.Active, start, end))
                {
                    tasks.Add(task);
                }
            }
            if (isCompleted)
            {
                foreach (Assignment task in ChooseTasks(TaskState.Completed, start, end))
                {
                    tasks.Add(task);
                }
            }
            if (isPartiallyCompleted)
            {
                foreach (Assignment task in ChooseTasks(TaskState.PartiallyCompleted, start, end))
                {
                    tasks.Add(task);
                }
            }
            if (isFailed)
            {
                foreach (Assignment task in ChooseTasks(TaskState.Failed, start, end))
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        private List<Assignment> ChooseTasks(TaskState state, DateTime start, DateTime end)
        {
            List<Assignment> tasks = new List<Assignment>();
            foreach(Assignment task in allTasks)
            {
                if(task.State == state && task.TimeLimits.Start >= start && task.TimeLimits.End <= end)
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        private void RefreshAllTasks()
        {
            allTasks = SQLiteConnector.GetAllTasks();
        }
    }
}
