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
    public partial class SlothOrganizerForm : Form
    {
        private List<DateTime> years = new List<DateTime> { new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year + 1, 1, 1) };
        private List<string> months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        private TimePeriod year;
        private bool hasLoaded = false;
        public SlothOrganizerForm()
        {
            InitializeComponent();
            FillYearDropDown();
            FillMonthDropDown();
            RefreshData();
            hasLoaded = true;
        }

        private void createTaskButton_Click(object sender, EventArgs e)
        {
            CreateTaskForm createTaskForm = new CreateTaskForm();
            createTaskForm.ShowDialog();
            RefreshData();
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
            RefreshData();
        }

        private void updateTaskButton_Click(object sender, EventArgs e)
        {
            UpdateTaskForm updateTaskForm = new UpdateTaskForm();
            updateTaskForm.ShowDialog();
            RefreshData();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void CreateYear(int yearNumber)
        {
            DateTime yearBeginning = new DateTime(yearNumber, 1, 1);
            int length = yearNumber % 4 == 0 ? 366 : 365;
            TimePeriod year = new TimePeriod(yearBeginning, length);
            year = SQLiteConnector.CreateTimePeriod(year);
            for (int i = 1; i <= 12; i++)
            {
                DateTime monthBeginning = new DateTime(yearNumber, i, 1);
                TimePeriod month = new TimePeriod(monthBeginning, DateTime.DaysInMonth(yearNumber, i));
                month = SQLiteConnector.CreateTimePeriod(month, year.Id);
                for (int j = 0; j < 6; j++)
                {
                    DateTime weekBeginning = FindWeeksBeginning(month).AddDays(7 * j);
                    TimePeriod week = new TimePeriod(weekBeginning, 7);
                    SQLiteConnector.CreateTimePeriod(week, month.Id);
                }
            }
        }

        private DateTime FindWeeksBeginning(TimePeriod month)
        {
            DateTime beginning = month.Start.AddDays(-7);
            while (beginning.DayOfWeek != DayOfWeek.Monday)
            {
                beginning = beginning.AddDays(1);
            }
            return beginning;
        }

        private void FillYearDropDown()
        {
            yearDropDown.DataSource = null;
            yearDropDown.DataSource = years;
            yearDropDown.DisplayMember = "Year";
            yearDropDown.SelectedIndex = 1;
        }

        private void FillMonthDropDown()
        {
            monthDropDown.DataSource = null;
            monthDropDown.DataSource = months;
            monthDropDown.SelectedIndex = 0;
        }

        private TimePeriod GetYear(DateTime yearBeginning, int monthIndex)
        {   
            TimePeriod year = SQLiteConnector.GetYear(yearBeginning);
            List<TimePeriod> months = SQLiteConnector.GetChildrenTimePeriods(year).OrderBy(x => x.Start).ToList();
            if (months.Count > 0)
            {
                year.ChildrenTimePeriods.Add(months[monthIndex]);
                year.ChildrenTimePeriods[0].ChildrenTimePeriods = SQLiteConnector.GetChildrenTimePeriods(year.ChildrenTimePeriods[0]).OrderBy(x => x.Start).ToList();
                return year; 
            }
            else
            {
                return null;
            }
        }

        private TimePeriod LoadYear(DateTime yearBeginning, int monthIndex)
        {
            TimePeriod year = GetYear(yearBeginning, monthIndex);
            if(year == null)
            {
                CreateYear(yearBeginning.Year);
                LoadYear(yearBeginning, monthIndex);
            }
            return year;
        }

        private void RefreshDaysTable()
        {
            List<List<Control>> days = GetLabels(daysTable, 6, 7);
            DateTime currentDate = FindWeeksBeginning(year.ChildrenTimePeriods[0]);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    days[i][j].Text = $"{(int)currentDate.Day}";
                    if (currentDate == DateTime.Now.Date)
                    {
                        days[i][j].BackColor = Color.Gold;
                    }
                    else if (currentDate.Month != monthDropDown.SelectedIndex + 1)
                    {
                        days[i][j].BackColor = Color.Silver;
                    }
                    else
                    {
                        days[i][j].BackColor = Color.White;
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }
        }

        private List<List<Control>> GetLabels(TableLayoutPanel table, int rows, int columns)
        {
            List<List<Control>> dayLabels = new List<List<Control>>();
            Control[] allDays = new Control[rows * columns];
            table.Controls.CopyTo(allDays, 0);
            for (int i = 0; i < rows; i++)
            {
                dayLabels.Add(allDays.ToList().Where(x => table.GetRow(x) == i).ToList().OrderBy(x => table.GetColumn(x)).ToList());
            }
            return dayLabels;
        }

        private void RefreshIndicatorsTable()
        {
            List<List<Control>> indicators = GetLabels(indicatorsTable, 6, 4);
            for (int i = 0; i < 6; i++)
            {
                indicators[i][0].Text = year.ChildrenTimePeriods[0].ChildrenTimePeriods[i].ActiveNumber.ToString();
                indicators[i][1].Text = year.ChildrenTimePeriods[0].ChildrenTimePeriods[i].CompletedNumber.ToString();
                indicators[i][2].Text = year.ChildrenTimePeriods[0].ChildrenTimePeriods[i].PartiallyCompletedNumber.ToString();
                indicators[i][3].Text = year.ChildrenTimePeriods[0].ChildrenTimePeriods[i].FailedNumber.ToString();
            }
        }

        private void RefreshIndicators()
        {
            yearActiveLabel.Text = year.ActiveNumber.ToString();
            yearCompletedLabel.Text = year.CompletedNumber.ToString();
            yearPartiallyCompletedLabel.Text = year.PartiallyCompletedNumber.ToString();
            yearFailedLabel.Text = year.FailedNumber.ToString();

            monthActiveLabel.Text = year.ChildrenTimePeriods[0].ActiveNumber.ToString();
            monthCompletedLabel.Text = year.ChildrenTimePeriods[0].CompletedNumber.ToString();
            monthPartiallyCompletedLabel.Text = year.ChildrenTimePeriods[0].PartiallyCompletedNumber.ToString();
            monthFailedLabel.Text = year.ChildrenTimePeriods[0].FailedNumber.ToString();

            RefreshIndicatorsTable();
        }

        private void UpdateYear()
        {
            List<Assignment> allTasks = SQLiteConnector.GetAllTasks();
            year.CountStatuses(allTasks);
            SQLiteConnector.UpdateTimePeriod(year);
            year.ChildrenTimePeriods[0].CountStatuses(allTasks);
            SQLiteConnector.UpdateTimePeriod(year.ChildrenTimePeriods[0]);
            for (int i = 0; i < 6; i++)
            {
                TimePeriod week = year.ChildrenTimePeriods[0].ChildrenTimePeriods[i];
                week.CountStatuses(allTasks);
                SQLiteConnector.UpdateTimePeriod(week);
            }
        }

        private void RefreshData()
        {
            UpdateTasks();
            year = LoadYear((DateTime)yearDropDown.SelectedItem, monthDropDown.SelectedIndex);
            UpdateYear();
            RefreshDaysTable();
            RefreshIndicators();
        }

        private void yearDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hasLoaded)
            {
                RefreshData(); 
            }
        }

        private void monthDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hasLoaded)
            {
                RefreshData(); 
            }
        }

        private void UpdateTasks()
        {
            List<Assignment> allTasks = SQLiteConnector.GetAllTasks();
            foreach (Assignment task in allTasks)
            {
                UpdateSubTasks(task);
                task.UpdateState();
                SQLiteConnector.UpdateTask(task, task.State);
            }
        }

        private void UpdateSubTasks(Assignment task)
        {
            task.SubTasks = SQLiteConnector.GetSubTasks(task);
            foreach (Assignment subtask in task.SubTasks)
            {
                UpdateSubTasks(subtask);
                task.UpdateState();
                SQLiteConnector.UpdateTask(task, task.State);
            }
        }
    }
}
