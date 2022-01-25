namespace Sloth_Organizer
{
    partial class DeleteTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteTaskForm));
            this.deleteTaskLabel = new System.Windows.Forms.Label();
            this.deleteWithSubTaskButton = new System.Windows.Forms.Button();
            this.endPicker = new System.Windows.Forms.DateTimePicker();
            this.endSelectionLabel = new System.Windows.Forms.Label();
            this.failedCheckBox = new System.Windows.Forms.CheckBox();
            this.partiallyCompletedChackBox = new System.Windows.Forms.CheckBox();
            this.completedCheckBox = new System.Windows.Forms.CheckBox();
            this.activeCheckBox = new System.Windows.Forms.CheckBox();
            this.taskStatusSelectionLabel = new System.Windows.Forms.Label();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.deleteWithoutSubTaskButton = new System.Windows.Forms.Button();
            this.inactiveCheckBox = new System.Windows.Forms.CheckBox();
            this.startPicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // deleteTaskLabel
            // 
            this.deleteTaskLabel.AutoSize = true;
            this.deleteTaskLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteTaskLabel.Location = new System.Drawing.Point(188, 9);
            this.deleteTaskLabel.Name = "deleteTaskLabel";
            this.deleteTaskLabel.Size = new System.Drawing.Size(148, 37);
            this.deleteTaskLabel.TabIndex = 9;
            this.deleteTaskLabel.Text = "Delete task";
            // 
            // deleteWithSubTaskButton
            // 
            this.deleteWithSubTaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteWithSubTaskButton.Location = new System.Drawing.Point(12, 431);
            this.deleteWithSubTaskButton.Name = "deleteWithSubTaskButton";
            this.deleteWithSubTaskButton.Size = new System.Drawing.Size(228, 54);
            this.deleteWithSubTaskButton.TabIndex = 49;
            this.deleteWithSubTaskButton.Text = "Delete with subtasks";
            this.deleteWithSubTaskButton.UseVisualStyleBackColor = true;
            this.deleteWithSubTaskButton.Click += new System.EventHandler(this.deleteWithSubTaskButton_Click);
            // 
            // endPicker
            // 
            this.endPicker.Location = new System.Drawing.Point(12, 230);
            this.endPicker.Name = "endPicker";
            this.endPicker.Size = new System.Drawing.Size(375, 35);
            this.endPicker.TabIndex = 68;
            this.endPicker.ValueChanged += new System.EventHandler(this.endPicker_ValueChanged);
            // 
            // endSelectionLabel
            // 
            this.endSelectionLabel.AutoSize = true;
            this.endSelectionLabel.Location = new System.Drawing.Point(12, 156);
            this.endSelectionLabel.Name = "endSelectionLabel";
            this.endSelectionLabel.Size = new System.Drawing.Size(330, 30);
            this.endSelectionLabel.TabIndex = 66;
            this.endSelectionLabel.Text = "Choose starting and ending dates:";
            // 
            // failedCheckBox
            // 
            this.failedCheckBox.AutoSize = true;
            this.failedCheckBox.Location = new System.Drawing.Point(226, 119);
            this.failedCheckBox.Name = "failedCheckBox";
            this.failedCheckBox.Size = new System.Drawing.Size(85, 34);
            this.failedCheckBox.TabIndex = 65;
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
            this.partiallyCompletedChackBox.TabIndex = 64;
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
            this.completedCheckBox.TabIndex = 63;
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
            this.activeCheckBox.TabIndex = 62;
            this.activeCheckBox.Text = "Active";
            this.activeCheckBox.UseVisualStyleBackColor = true;
            this.activeCheckBox.CheckedChanged += new System.EventHandler(this.activeCheckBox_CheckedChanged);
            // 
            // taskStatusSelectionLabel
            // 
            this.taskStatusSelectionLabel.AutoSize = true;
            this.taskStatusSelectionLabel.Location = new System.Drawing.Point(12, 46);
            this.taskStatusSelectionLabel.Name = "taskStatusSelectionLabel";
            this.taskStatusSelectionLabel.Size = new System.Drawing.Size(401, 30);
            this.taskStatusSelectionLabel.TabIndex = 61;
            this.taskStatusSelectionLabel.Text = "Choose status of task you want to delete :";
            // 
            // taskListBox
            // 
            this.taskListBox.FormattingEnabled = true;
            this.taskListBox.ItemHeight = 30;
            this.taskListBox.Location = new System.Drawing.Point(12, 271);
            this.taskListBox.Name = "taskListBox";
            this.taskListBox.Size = new System.Drawing.Size(502, 154);
            this.taskListBox.TabIndex = 60;
            // 
            // deleteWithoutSubTaskButton
            // 
            this.deleteWithoutSubTaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteWithoutSubTaskButton.Location = new System.Drawing.Point(286, 431);
            this.deleteWithoutSubTaskButton.Name = "deleteWithoutSubTaskButton";
            this.deleteWithoutSubTaskButton.Size = new System.Drawing.Size(228, 54);
            this.deleteWithoutSubTaskButton.TabIndex = 70;
            this.deleteWithoutSubTaskButton.Text = "Delete without subtasks";
            this.deleteWithoutSubTaskButton.UseVisualStyleBackColor = true;
            this.deleteWithoutSubTaskButton.Click += new System.EventHandler(this.deleteWithoutSubTaskButton_Click);
            // 
            // inactiveCheckBox
            // 
            this.inactiveCheckBox.AutoSize = true;
            this.inactiveCheckBox.Location = new System.Drawing.Point(12, 79);
            this.inactiveCheckBox.Name = "inactiveCheckBox";
            this.inactiveCheckBox.Size = new System.Drawing.Size(104, 34);
            this.inactiveCheckBox.TabIndex = 82;
            this.inactiveCheckBox.Text = "Inactive";
            this.inactiveCheckBox.UseVisualStyleBackColor = true;
            this.inactiveCheckBox.CheckedChanged += new System.EventHandler(this.inactiveCheckBox_CheckedChanged);
            // 
            // startPicker
            // 
            this.startPicker.Location = new System.Drawing.Point(12, 189);
            this.startPicker.Name = "startPicker";
            this.startPicker.Size = new System.Drawing.Size(374, 35);
            this.startPicker.TabIndex = 83;
            this.startPicker.ValueChanged += new System.EventHandler(this.startPicker_ValueChanged);
            // 
            // DeleteTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(526, 496);
            this.Controls.Add(this.startPicker);
            this.Controls.Add(this.inactiveCheckBox);
            this.Controls.Add(this.deleteWithoutSubTaskButton);
            this.Controls.Add(this.endPicker);
            this.Controls.Add(this.endSelectionLabel);
            this.Controls.Add(this.failedCheckBox);
            this.Controls.Add(this.partiallyCompletedChackBox);
            this.Controls.Add(this.completedCheckBox);
            this.Controls.Add(this.activeCheckBox);
            this.Controls.Add(this.taskStatusSelectionLabel);
            this.Controls.Add(this.taskListBox);
            this.Controls.Add(this.deleteWithSubTaskButton);
            this.Controls.Add(this.deleteTaskLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximizeBox = false;
            this.Name = "DeleteTaskForm";
            this.Text = "Delete Task";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label deleteTaskLabel;
        private System.Windows.Forms.Button deleteWithSubTaskButton;
        private System.Windows.Forms.DateTimePicker endPicker;
        private System.Windows.Forms.Label endSelectionLabel;
        private System.Windows.Forms.CheckBox failedCheckBox;
        private System.Windows.Forms.CheckBox partiallyCompletedChackBox;
        private System.Windows.Forms.CheckBox completedCheckBox;
        private System.Windows.Forms.CheckBox activeCheckBox;
        private System.Windows.Forms.Label taskStatusSelectionLabel;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.Button deleteWithoutSubTaskButton;
        private System.Windows.Forms.CheckBox inactiveCheckBox;
        private System.Windows.Forms.DateTimePicker startPicker;
    }
}