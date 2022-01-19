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
        private Assignment task;
        private Assignment parent;
        public CreateTaskForm(bool isSubTask = false, Assignment parentTask = null)
        {
            InitializeComponent();
            task = new Assignment(isSubTask);
            parent = parentTask;
        }
        private void FillSubTaskListBox()
        {
            subTaskListBox.DataSource = null;
            subTaskListBox.DataSource = task.SubTasks;
            subTaskListBox.DisplayMember = "Text";
        }
        private void createSubtaskButton_Click(object sender, EventArgs e)
        {
            CreateTaskForm createSubTaskForm = new CreateTaskForm(true, task);
            createSubTaskForm.ShowDialog();
            FillSubTaskListBox();
        }
        private void deleteSubtaskButton_Click(object sender, EventArgs e)
        {
            if (subTaskListBox.SelectedItem != null)
            {
                task.SubTasks.RemoveAt(subTaskListBox.SelectedIndex);
            }
            FillSubTaskListBox();
        }

        private void createTaskButton_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                task.Text = textTextBox.Text;
                task.TimeLimits.Start = startPicker.Value;
                task.TimeLimits.End = endPicker.Value;
                task.UpdateState();
                if (task.IsSubTask)
                {
                    parent.SubTasks.Add(task);
                } 
                else
                {
                    task = SQLiteConnector.CreateTask(task);
                    AddSubTasksToDB(task);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please, enter valid data");
            }
        }
        private bool IsValid()
        {
            if (textTextBox.Text == null || startPicker.Value > endPicker.Value)
            {
                return false;
            }
            else
            {
                return true;
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
    }
}
