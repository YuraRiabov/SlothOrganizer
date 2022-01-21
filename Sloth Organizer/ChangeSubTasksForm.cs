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
    public partial class ChangeSubTasksForm : Form
    {
        public Assignment Task { get; private set; }
        public ChangeSubTasksForm(Assignment assignment)
        {
            InitializeComponent();
            Task = assignment;
            Task.SubTasks = SQLiteConnector.GetSubTasks(Task);
            RefreshSubTaskList();
        }

        private void RefreshSubTaskList()
        {
            subTaskListBox.DataSource = null;
            subTaskListBox.DataSource = Task.SubTasks;
            subTaskListBox.DisplayMember = "Text";
        }

        private void createSubtaskButton_Click(object sender, EventArgs e)
        {
            CreateTaskForm createTaskForm = new CreateTaskForm(true, Task);
            createTaskForm.ShowDialog();
            RefreshSubTaskList();
        }

        private void deleteSubtaskButton_Click(object sender, EventArgs e)
        {
            SQLiteConnector.DeleteTask(Task.SubTasks[subTaskListBox.SelectedIndex]);
            Task.SubTasks.RemoveAt(subTaskListBox.SelectedIndex);
            RefreshSubTaskList();
        }

        private bool IsNewSubtask(Assignment task)
        {
            if (task.Id == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void AddSubTasksToDB(Assignment task)
        {
            Assignment subTask;
            foreach (Assignment st in task.SubTasks)
            {
                subTask = st;
                subTask = SQLiteConnector.CreateTask(subTask, task.Id);
                if (subTask.SubTasks.Count != 0)
                {
                    AddSubTasksToDB(subTask);
                }
            }
        }

        private void updateTaskButton_Click(object sender, EventArgs e)
        {
            foreach (Assignment subTask in Task.SubTasks)
            {
                if (IsNewSubtask(subTask))
                {
                    SQLiteConnector.CreateTask(subTask, Task.Id);
                    AddSubTasksToDB(subTask);
                }
            }
            Close();
        }
    }
}
