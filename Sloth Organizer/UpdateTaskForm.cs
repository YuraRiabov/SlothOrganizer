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
    public partial class UpdateTaskForm : Form
    {
        private List<TaskState> selectedStates = new List<TaskState>();
        public UpdateTaskForm()
        {
            InitializeComponent();
        }
        private void RefreshTaskList()
        {
            taskListBox.DataSource = null;
            taskListBox.DataSource = SelectTasks(startPicker.Value.Date, endPicker.Value.Date);
            taskListBox.DisplayMember = "Text";
        }

        public List<Assignment> SelectTasks(DateTime start, DateTime end)
        {
            List<Assignment> allTasks = SQLiteConnector.GetAllTasks();
            List<Assignment> tasks = allTasks.Where(x => x.TimeLimits.Start >= start && x.TimeLimits.End <= end && selectedStates.Contains(x.State)).ToList();
            return tasks;
        }

        private void activeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (activeCheckBox.Checked)
            {
                selectedStates.Add(TaskState.Active);
            }
            else
            {
                selectedStates.Remove(TaskState.Active);
            }
            RefreshTaskList();
        }

        private void completedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (completedCheckBox.Checked)
            {
                selectedStates.Add(TaskState.Completed);
            }
            else
            {
                selectedStates.Remove(TaskState.Completed);
            }
            RefreshTaskList();
        }

        private void partiallyCompletedChackBox_CheckedChanged(object sender, EventArgs e)
        {
            if (partiallyCompletedChackBox.Checked)
            {
                selectedStates.Add(TaskState.PartiallyCompleted);
            }
            else
            {
                selectedStates.Remove(TaskState.PartiallyCompleted);
            }
            RefreshTaskList();
        }

        private void failedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (failedCheckBox.Checked)
            {
                selectedStates.Add(TaskState.Failed);
            }
            else
            {
                selectedStates.Remove(TaskState.Failed);
            }
            RefreshTaskList();
        }
        private void inactiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (inactiveCheckBox.Checked)
            {
                selectedStates.Add(TaskState.Inactive);
            }
            else
            {
                selectedStates.Remove(TaskState.Inactive);
            }
            RefreshTaskList();
        }

        private void endPicker_ValueChanged(object sender, EventArgs e)
        {
            if (startPicker.Value.Date <= endPicker.Value.Date)
            {
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show("Start date must be before end date");
            }
        }

        private void changeTermButton_Click(object sender, EventArgs e)
        {
            Assignment selectedTask = (Assignment)taskListBox.SelectedItem;
            if (taskListBox.SelectedIndex >= 0)
            {
                ChangeTermForm changeTermForm = new ChangeTermForm(selectedTask);
                changeTermForm.ShowDialog(); 
            }
        }

        private void changeSubTasksButton_Click(object sender, EventArgs e)
        {
            Assignment selectedTask = (Assignment)taskListBox.SelectedItem;
            if (taskListBox.SelectedIndex >= 0)
            {
                ChangeSubTasksForm changeSubTasksForm = new ChangeSubTasksForm(selectedTask);
                changeSubTasksForm.ShowDialog(); 
            }
        }

        private void markCompletedButton_Click(object sender, EventArgs e)
        {
            Assignment selectedTask = (Assignment)taskListBox.SelectedItem;
            if (taskListBox.SelectedIndex >= 0)
            {
                SQLiteConnector.UpdateTask(selectedTask, TaskState.Completed);
                MarkSubtasksCompleted(selectedTask);
                RefreshTaskList(); 
            }
        }
        private void MarkSubtasksCompleted(Assignment task)
        {
            task.SubTasks = SQLiteConnector.GetSubTasks(task);
            foreach (Assignment subTask in task.SubTasks)
            {
                if (subTask.SubTasks.Count != 0)
                {
                    MarkSubtasksCompleted(subTask);
                }
                SQLiteConnector.UpdateTask(subTask, TaskState.Completed);
            }
        }

        private void startPicker_ValueChanged(object sender, EventArgs e)
        {
            if (startPicker.Value.Date < endPicker.Value.Date)
            {
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show("Start date must be before end date");
            }
        }
    }
}
