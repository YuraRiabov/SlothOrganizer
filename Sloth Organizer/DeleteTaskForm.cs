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
        private List<Assignment> allTasks = SQLiteConnector.GetAllTasks();
        private readonly List<TaskState> allStates = new List<TaskState> { TaskState.Inactive, TaskState.Active, TaskState.Completed, TaskState.PartiallyCompleted, TaskState.Failed };
        private List<TaskState> states = new List<TaskState> { };
        public DeleteTaskForm()
        {
            InitializeComponent();
        }
        private void RefreshTaskList(DateTime start, DateTime end)
        {
            taskListBox.DataSource = null;
            taskListBox.DataSource = ChooseTasks(start, end);
            taskListBox.DisplayMember = "Text";
        }
        private List<Assignment> ChooseTasks(DateTime start, DateTime end)
        {
            List<Assignment> result = new List<Assignment>();
            foreach (Assignment task in allTasks)
            {
                if (task.TimeLimits.Start >= start && task.TimeLimits.End <= end && IsInStates(task.State))
                {
                    result.Add(task);
                }
            }
            return result;
        }
        private bool IsInStates(TaskState state)
        {
            foreach (TaskState st in states)
            {
                if (state == st)
                {
                    return true;
                }
            }
            return false;
        }

        private void allCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allCheckBox.Checked)
            {
                states = allStates;
                activeCheckBox.Checked = true;
                completedCheckBox.Checked = true;
                partiallyCompletedChackBox.Checked = true;
                failedCheckBox.Checked = true;
                allCheckBox.Checked = true;
            }
            else
            {
                states.Clear();
                if (activeCheckBox.Checked)
                {
                    states.Add(TaskState.Active);
                }
                if (completedCheckBox.Checked)
                {
                    states.Add(TaskState.Completed);
                }
                if (partiallyCompletedChackBox.Checked)
                {
                    states.Add(TaskState.PartiallyCompleted);
                }
                if (failedCheckBox.Checked)
                {
                    states.Add(TaskState.Failed);
                }
            }
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }

        private void activeCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            allCheckBox.Checked = false;
            if (activeCheckBox.Checked)
            {
                states.Add(TaskState.Active);
            }
            else
            {
                states.Remove(TaskState.Active);
            }
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }

        private void completedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            allCheckBox.Checked = false;
            if (completedCheckBox.Checked)
            {
                states.Add(TaskState.Completed);
            }
            else
            {
                states.Remove(TaskState.Completed);
            }
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }

        private void partiallyCompletedChackBox_CheckedChanged(object sender, EventArgs e)
        {
            allCheckBox.Checked = false;
            if (partiallyCompletedChackBox.Checked)
            {
                states.Add(TaskState.PartiallyCompleted);
            }
            else
            {
                states.Remove(TaskState.PartiallyCompleted);
            }
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }

        private void failedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            allCheckBox.Checked = false;
            if (failedCheckBox.Checked)
            {
                states.Add(TaskState.Failed);
            }
            else
            {
                states.Remove(TaskState.Failed);
            }
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }

        private void startPicker_ValueChanged(object sender, EventArgs e)
        {
            if (startPicker.Value.Date < endPicker.Value.Date)
            {
                RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
            }
            else
            {
                MessageBox.Show("Start date must be before end date");
            }
        }

        private void endPicker_ValueChanged(object sender, EventArgs e)
        {
            if (startPicker.Value.Date < endPicker.Value.Date)
            {
                RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
            }
            else
            {
                MessageBox.Show("Start date must be before end date");
            }
        }

        private void deleteWithSubTaskButton_Click(object sender, EventArgs e)
        {
            List<Assignment> tasks = ChooseTasks(startPicker.Value.Date, endPicker.Value.Date);
            Assignment selectedTask = tasks[taskListBox.SelectedIndex];
            selectedTask.SubTasks = SQLiteConnector.GetSubTasks(selectedTask);
            SQLiteConnector.DeleteSubTasks(selectedTask);
            DeleteTask(selectedTask);
        }

        private void deleteWithoutSubTaskButton_Click(object sender, EventArgs e)
        {
            List<Assignment> tasks = ChooseTasks(startPicker.Value.Date, endPicker.Value.Date);
            Assignment selectedTask = tasks[taskListBox.SelectedIndex];
            DeleteTask(selectedTask);
        }
        private void DeleteTask(Assignment task)
        {
            SQLiteConnector.DeleteTask(task);
            allTasks = SQLiteConnector.GetAllTasks();
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }
    }
}
