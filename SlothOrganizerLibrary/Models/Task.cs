using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizerLibrary.Models
{
    public class Task
    {
        public string Text { get; set; }
        public List<Task> SubTasks { get; set; } = new List<Task>();
        public Term TimeLimits { get; set; }
        public TaskState State { get; set; }
        public bool IsSubTask { get; set; }
    }
}
