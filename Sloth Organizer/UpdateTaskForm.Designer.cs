namespace Sloth_Organizer
{
    partial class UpdateTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateTaskForm));
            this.updateTaskLabel = new System.Windows.Forms.Label();
            this.markCompletedButton = new System.Windows.Forms.Button();
            this.changeTermButton = new System.Windows.Forms.Button();
            this.changeSubTasksButton = new System.Windows.Forms.Button();
            this.endPicker = new System.Windows.Forms.DateTimePicker();
            this.startPicker = new System.Windows.Forms.DateTimePicker();
            this.endSelectionLabel = new System.Windows.Forms.Label();
            this.failedCheckBox = new System.Windows.Forms.CheckBox();
            this.partiallyCompletedChackBox = new System.Windows.Forms.CheckBox();
            this.completedCheckBox = new System.Windows.Forms.CheckBox();
            this.activeCheckBox = new System.Windows.Forms.CheckBox();
            this.taskStatusSelectionLabel = new System.Windows.Forms.Label();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.inactiveCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // updateTaskLabel
            // 
            this.updateTaskLabel.AutoSize = true;
            this.updateTaskLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateTaskLabel.Location = new System.Drawing.Point(143, 9);
            this.updateTaskLabel.Name = "updateTaskLabel";
            this.updateTaskLabel.Size = new System.Drawing.Size(159, 37);
            this.updateTaskLabel.TabIndex = 16;
            this.updateTaskLabel.Text = "Update task";
            // 
            // markCompletedButton
            // 
            this.markCompletedButton.BackColor = System.Drawing.Color.Gainsboro;
            this.markCompletedButton.Location = new System.Drawing.Point(112, 475);
            this.markCompletedButton.Name = "markCompletedButton";
            this.markCompletedButton.Size = new System.Drawing.Size(203, 38);
            this.markCompletedButton.TabIndex = 21;
            this.markCompletedButton.Text = "Mark as completed";
            this.markCompletedButton.UseVisualStyleBackColor = false;
            this.markCompletedButton.Click += new System.EventHandler(this.markCompletedButton_Click);
            // 
            // changeTermButton
            // 
            this.changeTermButton.BackColor = System.Drawing.Color.Gainsboro;
            this.changeTermButton.Location = new System.Drawing.Point(34, 431);
            this.changeTermButton.Name = "changeTermButton";
            this.changeTermButton.Size = new System.Drawing.Size(177, 38);
            this.changeTermButton.TabIndex = 20;
            this.changeTermButton.Text = "Change term";
            this.changeTermButton.UseVisualStyleBackColor = false;
            this.changeTermButton.Click += new System.EventHandler(this.changeTermButton_Click);
            // 
            // changeSubTasksButton
            // 
            this.changeSubTasksButton.BackColor = System.Drawing.Color.Gainsboro;
            this.changeSubTasksButton.Location = new System.Drawing.Point(217, 431);
            this.changeSubTasksButton.Name = "changeSubTasksButton";
            this.changeSubTasksButton.Size = new System.Drawing.Size(177, 38);
            this.changeSubTasksButton.TabIndex = 22;
            this.changeSubTasksButton.Text = "Change subtasks";
            this.changeSubTasksButton.UseVisualStyleBackColor = false;
            this.changeSubTasksButton.Click += new System.EventHandler(this.changeSubTasksButton_Click);
            // 
            // endPicker
            // 
            this.endPicker.Location = new System.Drawing.Point(12, 230);
            this.endPicker.Name = "endPicker";
            this.endPicker.Size = new System.Drawing.Size(375, 35);
            this.endPicker.TabIndex = 78;
            this.endPicker.ValueChanged += new System.EventHandler(this.endPicker_ValueChanged);
            // 
            // startPicker
            // 
            this.startPicker.Location = new System.Drawing.Point(12, 189);
            this.startPicker.Name = "startPicker";
            this.startPicker.Size = new System.Drawing.Size(375, 35);
            this.startPicker.TabIndex = 77;
            this.startPicker.Value = new System.DateTime(2022, 1, 18, 0, 0, 0, 0);
            this.startPicker.ValueChanged += new System.EventHandler(this.startPicker_ValueChanged);
            // 
            // endSelectionLabel
            // 
            this.endSelectionLabel.AutoSize = true;
            this.endSelectionLabel.Location = new System.Drawing.Point(12, 156);
            this.endSelectionLabel.Name = "endSelectionLabel";
            this.endSelectionLabel.Size = new System.Drawing.Size(330, 30);
            this.endSelectionLabel.TabIndex = 76;
            this.endSelectionLabel.Text = "Choose starting and ending dates:";
            // 
            // failedCheckBox
            // 
            this.failedCheckBox.AutoSize = true;
            this.failedCheckBox.Location = new System.Drawing.Point(226, 119);
            this.failedCheckBox.Name = "failedCheckBox";
            this.failedCheckBox.Size = new System.Drawing.Size(85, 34);
            this.failedCheckBox.TabIndex = 75;
            this.failedCheckBox.Text = "Failed";
            this.failedCheckBox.UseVisualStyleBackColor = true;
            this.failedCheckBox.CheckedChanged += new System.EventHandler(this.failedCheckBox_CheckedChanged);
            // 
            // partiallyCompletedChackBox
            // 
            this.partiallyCompletedChackBox.AutoSize = true;
            this.partiallyCompletedChackBox.Location = new System.Drawing.Point(12, 119);
            this.partiallyCompletedChackBox.Name = "partiallyCompletedChackBox";
            this.partiallyCompletedChackBox.Size = new System.Drawing.Size(208, 34);
            this.partiallyCompletedChackBox.TabIndex = 74;
            this.partiallyCompletedChackBox.Text = "Partially completed";
            this.partiallyCompletedChackBox.UseVisualStyleBackColor = true;
            this.partiallyCompletedChackBox.CheckedChanged += new System.EventHandler(this.partiallyCompletedChackBox_CheckedChanged);
            // 
            // completedCheckBox
            // 
            this.completedCheckBox.AutoSize = true;
            this.completedCheckBox.Location = new System.Drawing.Point(217, 79);
            this.completedCheckBox.Name = "completedCheckBox";
            this.completedCheckBox.Size = new System.Drawing.Size(133, 34);
            this.completedCheckBox.TabIndex = 73;
            this.completedCheckBox.Text = "Completed";
            this.completedCheckBox.UseVisualStyleBackColor = true;
            this.completedCheckBox.CheckedChanged += new System.EventHandler(this.completedCheckBox_CheckedChanged);
            // 
            // activeCheckBox
            // 
            this.activeCheckBox.AutoSize = true;
            this.activeCheckBox.Location = new System.Drawing.Point(122, 79);
            this.activeCheckBox.Name = "activeCheckBox";
            this.activeCheckBox.Size = new System.Drawing.Size(89, 34);
            this.activeCheckBox.TabIndex = 72;
            this.activeCheckBox.Text = "Active";
            this.activeCheckBox.UseVisualStyleBackColor = true;
            this.activeCheckBox.CheckedChanged += new System.EventHandler(this.activeCheckBox_CheckedChanged);
            // 
            // taskStatusSelectionLabel
            // 
            this.taskStatusSelectionLabel.AutoSize = true;
            this.taskStatusSelectionLabel.Location = new System.Drawing.Point(12, 46);
            this.taskStatusSelectionLabel.Name = "taskStatusSelectionLabel";
            this.taskStatusSelectionLabel.Size = new System.Drawing.Size(409, 30);
            this.taskStatusSelectionLabel.TabIndex = 71;
            this.taskStatusSelectionLabel.Text = "Choose status of task you want to update :";
            // 
            // taskListBox
            // 
            this.taskListBox.FormattingEnabled = true;
            this.taskListBox.ItemHeight = 30;
            this.taskListBox.Location = new System.Drawing.Point(12, 271);
            this.taskListBox.Name = "taskListBox";
            this.taskListBox.Size = new System.Drawing.Size(409, 154);
            this.taskListBox.TabIndex = 70;
            // 
            // inactiveCheckBox
            // 
            this.inactiveCheckBox.AutoSize = true;
            this.inactiveCheckBox.Location = new System.Drawing.Point(12, 79);
            this.inactiveCheckBox.Name = "inactiveCheckBox";
            this.inactiveCheckBox.Size = new System.Drawing.Size(104, 34);
            this.inactiveCheckBox.TabIndex = 80;
            this.inactiveCheckBox.Text = "Inactive";
            this.inactiveCheckBox.UseVisualStyleBackColor = true;
            this.inactiveCheckBox.CheckedChanged += new System.EventHandler(this.inactiveCheckBox_CheckedChanged);
            // 
            // UpdateTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(433, 522);
            this.Controls.Add(this.inactiveCheckBox);
            this.Controls.Add(this.endPicker);
            this.Controls.Add(this.startPicker);
            this.Controls.Add(this.endSelectionLabel);
            this.Controls.Add(this.failedCheckBox);
            this.Controls.Add(this.partiallyCompletedChackBox);
            this.Controls.Add(this.completedCheckBox);
            this.Controls.Add(this.activeCheckBox);
            this.Controls.Add(this.taskStatusSelectionLabel);
            this.Controls.Add(this.taskListBox);
            this.Controls.Add(this.changeSubTasksButton);
            this.Controls.Add(this.markCompletedButton);
            this.Controls.Add(this.changeTermButton);
            this.Controls.Add(this.updateTaskLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "UpdateTaskForm";
            this.Text = "Update Task";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label updateTaskLabel;
        private System.Windows.Forms.Button markCompletedButton;
        private System.Windows.Forms.Button changeTermButton;
        private System.Windows.Forms.Button changeSubTasksButton;
        private System.Windows.Forms.DateTimePicker endPicker;
        private System.Windows.Forms.DateTimePicker startPicker;
        private System.Windows.Forms.Label endSelectionLabel;
        private System.Windows.Forms.CheckBox failedCheckBox;
        private System.Windows.Forms.CheckBox partiallyCompletedChackBox;
        private System.Windows.Forms.CheckBox completedCheckBox;
        private System.Windows.Forms.CheckBox activeCheckBox;
        private System.Windows.Forms.Label taskStatusSelectionLabel;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.CheckBox inactiveCheckBox;
    }
}