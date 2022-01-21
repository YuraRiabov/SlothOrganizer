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
        private TaskSelector taskSelector = new TaskSelector();
        public DeleteTaskForm()
        {
            InitializeComponent();
        }
        private void RefreshTaskList(DateTime start, DateTime end)
        {
            taskListBox.DataSource = null;
            taskListBox.DataSource = taskSelector.RefreshTaskList(inactiveCheckBox.Checked, activeCheckBox.Checked, completedCheckBox.Checked, partiallyCompletedChackBox.Checked,
                                                                  failedCheckBox.Checked, start, end);
            taskListBox.DisplayMember = "Text";
        }

        private void activeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }

        private void completedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }

        private void partiallyCompletedChackBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }

        private void failedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
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
            if (startPicker.Value.Date <= endPicker.Value.Date)
            {
                RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
            }
            else
            {
                MessageBox.Show("Start date must be before end date");
            }
        }
        private void inactiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }
        private void deleteWithSubTaskButton_Click(object sender, EventArgs e)
        {
            List<Assignment> tasks = taskSelector.RefreshTaskList(inactiveCheckBox.Checked, activeCheckBox.Checked, completedCheckBox.Checked, partiallyCompletedChackBox.Checked,
                                                                  failedCheckBox.Checked, startPicker.Value.Date, endPicker.Value.Date);
            Assignment selectedTask = tasks[taskListBox.SelectedIndex];
            selectedTask.SubTasks = SQLiteConnector.GetSubTasks(selectedTask);
            SQLiteConnector.DeleteSubTasks(selectedTask);
            DeleteTask(selectedTask);
        }

        private void deleteWithoutSubTaskButton_Click(object sender, EventArgs e)
        {
            List<Assignment> tasks = taskSelector.RefreshTaskList(inactiveCheckBox.Checked, activeCheckBox.Checked, completedCheckBox.Checked, partiallyCompletedChackBox.Checked,
                                                                  failedCheckBox.Checked, startPicker.Value.Date, endPicker.Value.Date);
            Assignment selectedTask = tasks[taskListBox.SelectedIndex];
            DeleteTask(selectedTask);
        }
        private void DeleteTask(Assignment task)
        {
            SQLiteConnector.DeleteTask(task);
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }
    }
}
