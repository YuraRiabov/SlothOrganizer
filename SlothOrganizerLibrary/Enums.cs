using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizerLibrary
{
    public enum TaskState
    {
        Inactive,
        Active, // TODO - change all form namings from InProcess to Active
        Completed,
        PartiallyCompleted,
        Failed
    }
}
