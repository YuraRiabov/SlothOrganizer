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
    public partial class ViewTasksForm : Form
    {
        private TaskSelector taskSelector = new TaskSelector();
        public ViewTasksForm()
        {
            InitializeComponent();
        }

        private void taskListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Assignment> tasks = taskSelector.RefreshTaskList(inactiveCheckBox.Checked, activeCheckBox.Checked, completedCheckBox.Checked, partiallyCompletedChackBox.Checked,
                                                                  failedCheckBox.Checked, startPicker.Value.Date, endPicker.Value.Date);
            int selectedIndex = Math.Max(0, taskListBox.SelectedIndex);
            startInfo.Text = tasks[selectedIndex].TimeLimits.Start.Date.ToString();
            endInfo.Text = tasks[selectedIndex].TimeLimits.End.Date.ToString();
            statusInfo.Text = tasks[selectedIndex].State.ToString();
            RefreshSubTaskList(tasks[selectedIndex]);
        }
        private void RefreshSubTaskList(Assignment task)
        {
            subtaskListBox.DataSource = null;
            List<Assignment> subtasks = SQLiteConnector.GetSubTasks(task);
            subtaskListBox.DataSource = subtasks;
            subtaskListBox.DisplayMember = "Text";
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
    }
}
