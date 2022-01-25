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
    public partial class DeleteTaskForm : Form
    {
        private List<TaskState> selectedStates = new List<TaskState>();
        public DeleteTaskForm()
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
        private void deleteWithSubTaskButton_Click(object sender, EventArgs e)
        {
            Assignment selectedTask = (Assignment)taskListBox.SelectedItem;

            int index = taskListBox.SelectedIndex;
            if (index >= 0)
            {
                selectedTask.SubTasks = SQLiteConnector.GetSubTasks(selectedTask);
                SQLiteConnector.DeleteSubTasks(selectedTask);
                DeleteTask(selectedTask); 
            }
        }

        private void deleteWithoutSubTaskButton_Click(object sender, EventArgs e)
        {
            Assignment selectedTask = (Assignment)taskListBox.SelectedItem;
            int index = taskListBox.SelectedIndex;
            if (index >= 0)
            {
                SQLiteConnector.DisconnectSubTasks(selectedTask);
                DeleteTask(selectedTask); 
            }
        }
        private void DeleteTask(Assignment task)
        {
            SQLiteConnector.DeleteTask(task);
            RefreshTaskList();
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
