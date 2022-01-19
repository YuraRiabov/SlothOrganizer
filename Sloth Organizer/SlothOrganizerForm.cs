using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sloth_Organizer
{
    public partial class SlothOrganizerForm : Form
    {
        public SlothOrganizerForm()
        {
            InitializeComponent();
        }

        private void createTaskButton_Click(object sender, EventArgs e)
        {
            CreateTaskForm createTaskForm = new CreateTaskForm();
            createTaskForm.ShowDialog();
        }

        private void viewTaskButton_Click(object sender, EventArgs e)
        {
            ViewTasksForm viewTasksForm = new ViewTasksForm();
            viewTasksForm.ShowDialog();
        }

        private void deleteTaskButton_Click(object sender, EventArgs e)
        {
            DeleteTaskForm deleteTaskForm = new DeleteTaskForm();
            deleteTaskForm.ShowDialog();
        }
    }
}
