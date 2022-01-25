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
            this.taskStatusSelectionLabel = new System.Windows.Forms.Label();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.viewTaskLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.startInfo = new System.Windows.Forms.Label();
            this.endInfo = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusInfo = new System.Windows.Forms.Label();
            this.subtaskListLabel = new System.Windows.Forms.Label();
            this.subtaskListBox = new System.Windows.Forms.ListBox();
            this.activeCheckBox = new System.Windows.Forms.CheckBox();
            this.completedCheckBox = new System.Windows.Forms.CheckBox();
            this.partiallyCompletedChackBox = new System.Windows.Forms.CheckBox();
            this.failedCheckBox = new System.Windows.Forms.CheckBox();
            this.endPicker = new System.Windows.Forms.DateTimePicker();
            this.endSelectionLabel = new System.Windows.Forms.Label();
            this.inactiveCheckBox = new System.Windows.Forms.CheckBox();
            this.startPicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // taskStatusSelectionLabel
            // 
            this.taskStatusSelectionLabel.AutoSize = true;
            this.taskStatusSelectionLabel.Location = new System.Drawing.Point(12, 46);
            this.taskStatusSelectionLabel.Name = "taskStatusSelectionLabel";
            this.taskStatusSelectionLabel.Size = new System.Drawing.Size(414, 30);
            this.taskStatusSelectionLabel.TabIndex = 19;
            this.taskStatusSelectionLabel.Text = "Choose statuses of tasks you want to view :";
            // 
            // taskListBox
            // 
            this.taskListBox.FormattingEnabled = true;
            this.taskListBox.ItemHeight = 30;
            this.taskListBox.Location = new System.Drawing.Point(12, 271);
            this.taskListBox.Name = "taskListBox";
            this.taskListBox.Size = new System.Drawing.Size(502, 154);
            this.taskListBox.TabIndex = 18;
            this.taskListBox.SelectedIndexChanged += new System.EventHandler(this.taskListBox_SelectedIndexChanged);
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
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(12, 442);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(107, 30);
            this.startLabel.TabIndex = 20;
            this.startLabel.Text = "Start date:";
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(12, 472);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(100, 30);
            this.endLabel.TabIndex = 21;
            this.endLabel.Text = "End date:";
            // 
            // startInfo
            // 
            this.startInfo.Location = new System.Drawing.Point(125, 442);
            this.startInfo.Name = "startInfo";
            this.startInfo.Size = new System.Drawing.Size(113, 30);
            this.startInfo.TabIndex = 22;
            // 
            // endInfo
            // 
            this.endInfo.Location = new System.Drawing.Point(125, 472);
            this.endInfo.Name = "endInfo";
            this.endInfo.Size = new System.Drawing.Size(113, 30);
            this.endInfo.TabIndex = 23;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 502);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(80, 30);
            this.statusLabel.TabIndex = 24;
            this.statusLabel.Text = "Status: ";
            // 
            // statusInfo
            // 
            this.statusInfo.AutoSize = true;
            this.statusInfo.Location = new System.Drawing.Point(125, 502);
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(0, 30);
            this.statusInfo.TabIndex = 25;
            // 
            // subtaskListLabel
            // 
            this.subtaskListLabel.AutoSize = true;
            this.subtaskListLabel.Location = new System.Drawing.Point(11, 532);
            this.subtaskListLabel.Name = "subtaskListLabel";
            this.subtaskListLabel.Size = new System.Drawing.Size(122, 30);
            this.subtaskListLabel.TabIndex = 27;
            this.subtaskListLabel.Text = "Subtask list:";
            // 
            // subtaskListBox
            // 
            this.subtaskListBox.FormattingEnabled = true;
            this.subtaskListBox.ItemHeight = 30;
            this.subtaskListBox.Location = new System.Drawing.Point(12, 565);
            this.subtaskListBox.Name = "subtaskListBox";
            this.subtaskListBox.Size = new System.Drawing.Size(502, 124);
            this.subtaskListBox.TabIndex = 26;
            // 
            // activeCheckBox
            // 
            this.activeCheckBox.AutoSize = true;
            this.activeCheckBox.Location = new System.Drawing.Point(122, 79);
            this.activeCheckBox.Name = "activeCheckBox";
            this.activeCheckBox.Size = new System.Drawing.Size(89, 34);
            this.activeCheckBox.TabIndex = 28;
            this.activeCheckBox.Text = "Active";
            this.activeCheckBox.UseVisualStyleBackColor = true;
            this.activeCheckBox.CheckedChanged += new System.EventHandler(this.activeCheckBox_CheckedChanged);
            // 
            // completedCheckBox
            // 
            this.completedCheckBox.AutoSize = true;
            this.completedCheckBox.Location = new System.Drawing.Point(217, 79);
            this.completedCheckBox.Name = "completedCheckBox";
            this.completedCheckBox.Size = new System.Drawing.Size(133, 34);
            this.completedCheckBox.TabIndex = 29;
            this.completedCheckBox.Text = "Completed";
            this.completedCheckBox.UseVisualStyleBackColor = true;
            this.completedCheckBox.CheckedChanged += new System.EventHandler(this.completedCheckBox_CheckedChanged);
            // 
            // partiallyCompletedChackBox
            // 
            this.partiallyCompletedChackBox.AutoSize = true;
            this.partiallyCompletedChackBox.Location = new System.Drawing.Point(12, 119);
            this.partiallyCompletedChackBox.Name = "partiallyCompletedChackBox";
            this.partiallyCompletedChackBox.Size = new System.Drawing.Size(208, 34);
            this.partiallyCompletedChackBox.TabIndex = 30;
            this.partiallyCompletedChackBox.Text = "Partially completed";
            this.partiallyCompletedChackBox.UseVisualStyleBackColor = true;
            this.partiallyCompletedChackBox.CheckedChanged += new System.EventHandler(this.partiallyCompletedChackBox_CheckedChanged);
            // 
            // failedCheckBox
            // 
            this.failedCheckBox.AutoSize = true;
            this.failedCheckBox.Location = new System.Drawing.Point(226, 119);
            this.failedCheckBox.Name = "failedCheckBox";
            this.failedCheckBox.Size = new System.Drawing.Size(85, 34);
            this.failedCheckBox.TabIndex = 31;
            this.failedCheckBox.Text = "Failed";
            this.failedCheckBox.UseVisualStyleBackColor = true;
            this.failedCheckBox.CheckedChanged += new System.EventHandler(this.failedCheckBox_CheckedChanged);
            // 
            // endPicker
            // 
            this.endPicker.Location = new System.Drawing.Point(12, 230);
            this.endPicker.Name = "endPicker";
            this.endPicker.Size = new System.Drawing.Size(375, 35);
            this.endPicker.TabIndex = 58;
            this.endPicker.ValueChanged += new System.EventHandler(this.endPicker_ValueChanged);
            // 
            // endSelectionLabel
            // 
            this.endSelectionLabel.AutoSize = true;
            this.endSelectionLabel.Location = new System.Drawing.Point(12, 156);
            this.endSelectionLabel.Name = "endSelectionLabel";
            this.endSelectionLabel.Size = new System.Drawing.Size(330, 30);
            this.endSelectionLabel.TabIndex = 56;
            this.endSelectionLabel.Text = "Choose starting and ending dates:";
            // 
            // inactiveCheckBox
            // 
            this.inactiveCheckBox.AutoSize = true;
            this.inactiveCheckBox.Location = new System.Drawing.Point(12, 79);
            this.inactiveCheckBox.Name = "inactiveCheckBox";
            this.inactiveCheckBox.Size = new System.Drawing.Size(104, 34);
            this.inactiveCheckBox.TabIndex = 81;
            this.inactiveCheckBox.Text = "Inactive";
            this.inactiveCheckBox.UseVisualStyleBackColor = true;
            this.inactiveCheckBox.CheckedChanged += new System.EventHandler(this.inactiveCheckBox_CheckedChanged);
            // 
            // startPicker
            // 
            this.startPicker.Location = new System.Drawing.Point(12, 189);
            this.startPicker.Name = "startPicker";
            this.startPicker.Size = new System.Drawing.Size(374, 35);
            this.startPicker.TabIndex = 82;
            this.startPicker.ValueChanged += new System.EventHandler(this.startPicker_ValueChanged);
            // 
            // ViewTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(523, 703);
            this.Controls.Add(this.startPicker);
            this.Controls.Add(this.inactiveCheckBox);
            this.Controls.Add(this.endPicker);
            this.Controls.Add(this.endSelectionLabel);
            this.Controls.Add(this.failedCheckBox);
            this.Controls.Add(this.partiallyCompletedChackBox);
            this.Controls.Add(this.completedCheckBox);
            this.Controls.Add(this.activeCheckBox);
            this.Controls.Add(this.subtaskListLabel);
            this.Controls.Add(this.subtaskListBox);
            this.Controls.Add(this.statusInfo);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.endInfo);
            this.Controls.Add(this.startInfo);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.taskStatusSelectionLabel);
            this.Controls.Add(this.taskListBox);
            this.Controls.Add(this.viewTaskLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximizeBox = false;
            this.Name = "ViewTasksForm";
            this.Text = "View Tasks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label taskStatusSelectionLabel;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.Label viewTaskLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Label startInfo;
        private System.Windows.Forms.Label endInfo;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label statusInfo;
        private System.Windows.Forms.Label subtaskListLabel;
        private System.Windows.Forms.ListBox subtaskListBox;
        private System.Windows.Forms.CheckBox activeCheckBox;
        private System.Windows.Forms.CheckBox completedCheckBox;
        private System.Windows.Forms.CheckBox partiallyCompletedChackBox;
        private System.Windows.Forms.CheckBox failedCheckBox;
        private System.Windows.Forms.DateTimePicker endPicker;
        private System.Windows.Forms.Label endSelectionLabel;
        private System.Windows.Forms.CheckBox inactiveCheckBox;
        private System.Windows.Forms.DateTimePicker startPicker;
    }
}