namespace Sloth_Organizer
{
    partial class ViewTasksForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTasksForm));
            this.taskSelectionLabel = new System.Windows.Forms.Label();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.chooseWeekDropDown = new System.Windows.Forms.ComboBox();
            this.viewTaskLabel = new System.Windows.Forms.Label();
            this.chooseMonthDropDown = new System.Windows.Forms.ComboBox();
            this.chooseDayDropDown = new System.Windows.Forms.ComboBox();
            this.chooseYearDropDown = new System.Windows.Forms.ComboBox();
            this.startLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.startInfo = new System.Windows.Forms.Label();
            this.endInfo = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusInfo = new System.Windows.Forms.Label();
            this.subtaskListLabel = new System.Windows.Forms.Label();
            this.subtaskListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // taskSelectionLabel
            // 
            this.taskSelectionLabel.AutoSize = true;
            this.taskSelectionLabel.Location = new System.Drawing.Point(12, 46);
            this.taskSelectionLabel.Name = "taskSelectionLabel";
            this.taskSelectionLabel.Size = new System.Drawing.Size(291, 30);
            this.taskSelectionLabel.TabIndex = 19;
            this.taskSelectionLabel.Text = "Choose a task to view info on:";
            // 
            // taskListBox
            // 
            this.taskListBox.FormattingEnabled = true;
            this.taskListBox.ItemHeight = 30;
            this.taskListBox.Location = new System.Drawing.Point(12, 123);
            this.taskListBox.Name = "taskListBox";
            this.taskListBox.Size = new System.Drawing.Size(502, 154);
            this.taskListBox.TabIndex = 18;
            // 
            // chooseWeekDropDown
            // 
            this.chooseWeekDropDown.FormattingEnabled = true;
            this.chooseWeekDropDown.Location = new System.Drawing.Point(266, 79);
            this.chooseWeekDropDown.Name = "chooseWeekDropDown";
            this.chooseWeekDropDown.Size = new System.Drawing.Size(121, 38);
            this.chooseWeekDropDown.TabIndex = 17;
            // 
            // viewTaskLabel
            // 
            this.viewTaskLabel.AutoSize = true;
            this.viewTaskLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewTaskLabel.Location = new System.Drawing.Point(188, 9);
            this.viewTaskLabel.Name = "viewTaskLabel";
            this.viewTaskLabel.Size = new System.Drawing.Size(129, 37);
            this.viewTaskLabel.TabIndex = 16;
            this.viewTaskLabel.Text = "View task";
            // 
            // chooseMonthDropDown
            // 
            this.chooseMonthDropDown.FormattingEnabled = true;
            this.chooseMonthDropDown.Location = new System.Drawing.Point(139, 79);
            this.chooseMonthDropDown.Name = "chooseMonthDropDown";
            this.chooseMonthDropDown.Size = new System.Drawing.Size(121, 38);
            this.chooseMonthDropDown.TabIndex = 15;
            // 
            // chooseDayDropDown
            // 
            this.chooseDayDropDown.FormattingEnabled = true;
            this.chooseDayDropDown.Location = new System.Drawing.Point(393, 79);
            this.chooseDayDropDown.Name = "chooseDayDropDown";
            this.chooseDayDropDown.Size = new System.Drawing.Size(121, 38);
            this.chooseDayDropDown.TabIndex = 14;
            // 
            // chooseYearDropDown
            // 
            this.chooseYearDropDown.FormattingEnabled = true;
            this.chooseYearDropDown.Location = new System.Drawing.Point(12, 79);
            this.chooseYearDropDown.Name = "chooseYearDropDown";
            this.chooseYearDropDown.Size = new System.Drawing.Size(121, 38);
            this.chooseYearDropDown.TabIndex = 13;
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(12, 294);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(107, 30);
            this.startLabel.TabIndex = 20;
            this.startLabel.Text = "Start date:";
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(12, 324);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(100, 30);
            this.endLabel.TabIndex = 21;
            this.endLabel.Text = "End date:";
            // 
            // startInfo
            // 
            this.startInfo.AutoSize = true;
            this.startInfo.Location = new System.Drawing.Point(125, 294);
            this.startInfo.Name = "startInfo";
            this.startInfo.Size = new System.Drawing.Size(82, 30);
            this.startInfo.TabIndex = 22;
            this.startInfo.Text = "<date>";
            // 
            // endInfo
            // 
            this.endInfo.AutoSize = true;
            this.endInfo.Location = new System.Drawing.Point(125, 324);
            this.endInfo.Name = "endInfo";
            this.endInfo.Size = new System.Drawing.Size(82, 30);
            this.endInfo.TabIndex = 23;
            this.endInfo.Text = "<date>";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 354);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(80, 30);
            this.statusLabel.TabIndex = 24;
            this.statusLabel.Text = "Status: ";
            // 
            // statusInfo
            // 
            this.statusInfo.AutoSize = true;
            this.statusInfo.Location = new System.Drawing.Point(125, 354);
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(209, 30);
            this.statusInfo.TabIndex = 25;
            this.statusInfo.Text = "<Completion status>";
            // 
            // subtaskListLabel
            // 
            this.subtaskListLabel.AutoSize = true;
            this.subtaskListLabel.Location = new System.Drawing.Point(11, 384);
            this.subtaskListLabel.Name = "subtaskListLabel";
            this.subtaskListLabel.Size = new System.Drawing.Size(122, 30);
            this.subtaskListLabel.TabIndex = 27;
            this.subtaskListLabel.Text = "Subtask list:";
            // 
            // subtaskListBox
            // 
            this.subtaskListBox.FormattingEnabled = true;
            this.subtaskListBox.ItemHeight = 30;
            this.subtaskListBox.Location = new System.Drawing.Point(12, 417);
            this.subtaskListBox.Name = "subtaskListBox";
            this.subtaskListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.subtaskListBox.Size = new System.Drawing.Size(502, 124);
            this.subtaskListBox.TabIndex = 26;
            this.subtaskListBox.SelectedIndexChanged += new System.EventHandler(this.subtaskListBox_SelectedIndexChanged);
            // 
            // ViewTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(523, 554);
            this.Controls.Add(this.subtaskListLabel);
            this.Controls.Add(this.subtaskListBox);
            this.Controls.Add(this.statusInfo);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.endInfo);
            this.Controls.Add(this.startInfo);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.taskSelectionLabel);
            this.Controls.Add(this.taskListBox);
            this.Controls.Add(this.chooseWeekDropDown);
            this.Controls.Add(this.viewTaskLabel);
            this.Controls.Add(this.chooseMonthDropDown);
            this.Controls.Add(this.chooseDayDropDown);
            this.Controls.Add(this.chooseYearDropDown);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ViewTasksForm";
            this.Text = "View Tasks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label taskSelectionLabel;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.ComboBox chooseWeekDropDown;
        private System.Windows.Forms.Label viewTaskLabel;
        private System.Windows.Forms.ComboBox chooseMonthDropDown;
        private System.Windows.Forms.ComboBox chooseDayDropDown;
        private System.Windows.Forms.ComboBox chooseYearDropDown;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Label startInfo;
        private System.Windows.Forms.Label endInfo;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label statusInfo;
        private System.Windows.Forms.Label subtaskListLabel;
        private System.Windows.Forms.ListBox subtaskListBox;
    }
}