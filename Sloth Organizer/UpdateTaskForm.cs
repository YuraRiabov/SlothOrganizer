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
        private TaskSelector taskSelector = new TaskSelector();
        public UpdateTaskForm()
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

        private void changeTermButton_Click(object sender, EventArgs e)
        {
            List<Assignment> tasks = taskSelector.RefreshTaskList(inactiveCheckBox.Checked, activeCheckBox.Checked, completedCheckBox.Checked, partiallyCompletedChackBox.Checked,
                                                                  failedCheckBox.Checked, startPicker.Value.Date, endPicker.Value.Date);
            int index = taskListBox.SelectedIndex;
            if (index >= 0)
            {
                Assignment selectedTask = tasks[index];
                ChangeTermForm changeTermForm = new ChangeTermForm(selectedTask);
                changeTermForm.ShowDialog(); 
            }
        }

        private void changeSubTasksButton_Click(object sender, EventArgs e)
        {
            List<Assignment> tasks = taskSelector.RefreshTaskList(inactiveCheckBox.Checked, activeCheckBox.Checked, completedCheckBox.Checked, partiallyCompletedChackBox.Checked,
                                                                  failedCheckBox.Checked, startPicker.Value.Date, endPicker.Value.Date);
            int index = taskListBox.SelectedIndex;
            if (index >= 0)
            {
                Assignment selectedTask = tasks[index];
                ChangeSubTasksForm changeSubTasksForm = new ChangeSubTasksForm(selectedTask);
                changeSubTasksForm.ShowDialog(); 
            }
        }

        private void markCompletedButton_Click(object sender, EventArgs e)
        {
            List<Assignment> tasks = taskSelector.RefreshTaskList(inactiveCheckBox.Checked, activeCheckBox.Checked, completedCheckBox.Checked, partiallyCompletedChackBox.Checked,
                                                                  failedCheckBox.Checked, startPicker.Value.Date, endPicker.Value.Date);
            int index = taskListBox.SelectedIndex;
            if (index >= 0)
            {
                Assignment selectedTask = tasks[index];
                List<Assignment> subTasks = SQLiteConnector.GetSubTasks(selectedTask);
                SQLiteConnector.UpdateTask(selectedTask, TaskState.Completed);
                MarkSubtasksCompleted(selectedTask);
                RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date); 
            }
        }
        private void MarkSubtasksCompleted(Assignment task)
        {
            task.SubTasks = SQLiteConnector.GetSubTasks(task);
            foreach (Assignment st in task.SubTasks)
            {
                if (st.SubTasks.Count != 0)
                {
                    MarkSubtasksCompleted(st);
                }
                SQLiteConnector.UpdateTask(st, TaskState.Completed);
            }
        }

        private void inactiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTaskList(startPicker.Value.Date, endPicker.Value.Date);
        }
    }
}
