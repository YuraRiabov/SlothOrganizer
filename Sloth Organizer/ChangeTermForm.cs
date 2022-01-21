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
    public partial class ChangeTermForm : Form
    {
        Assignment task;
        public ChangeTermForm(Assignment assignment)
        {
            InitializeComponent();
            task = assignment;
        }

        private void updateTaskButton_Click(object sender, EventArgs e)
        {
            if(startPicker.Value.Date < endPicker.Value.Date)
            {
                SQLiteConnector.UpdateTask(task, startPicker.Value.Date, endPicker.Value.Date);
            }
            else
            {
                MessageBox.Show("Enter valid start and end");
            }
            Close();
        }
    }
}
