using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlothOrganizerLibrary;

namespace Sloth_Organizer
{
    public partial class CreateTaskForm : Form
    {
        public CreateTaskForm(bool isSubTask = false)
        {
            InitializeComponent();
            Assignment assignment = new Assignment();
        }

        private void createTaskButton_Click(object sender, EventArgs e)
        {

        }
    }
}
