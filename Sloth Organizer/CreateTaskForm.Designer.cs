namespace Sloth_Organizer
{
    partial class CreateTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTaskForm));
            this.addTaskLabel = new System.Windows.Forms.Label();
            this.startSelectionLabel = new System.Windows.Forms.Label();
            this.startingYearDropDown = new System.Windows.Forms.ComboBox();
            this.startingDayDropDown = new System.Windows.Forms.ComboBox();
            this.startingMonthDropDown = new System.Windows.Forms.ComboBox();
            this.endingMonthDropDown = new System.Windows.Forms.ComboBox();
            this.endingDayDropDown = new System.Windows.Forms.ComboBox();
            this.endingYearDropDown = new System.Windows.Forms.ComboBox();
            this.endSelectionLabel = new System.Windows.Forms.Label();
            this.subtaskListBox = new System.Windows.Forms.ListBox();
            this.subtaskListLabel = new System.Windows.Forms.Label();
            this.deleteSubtaskButton = new System.Windows.Forms.Button();
            this.createSubtaskButton = new System.Windows.Forms.Button();
            this.createTaskButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addTaskLabel
            // 
            this.addTaskLabel.AutoSize = true;
            this.addTaskLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addTaskLabel.Location = new System.Drawing.Point(128, 7);
            this.addTaskLabel.Name = "addTaskLabel";
            this.addTaskLabel.Size = new System.Drawing.Size(148, 37);
            this.addTaskLabel.TabIndex = 0;
            this.addTaskLabel.Text = "Create task";
            this.addTaskLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // startSelectionLabel
            // 
            this.startSelectionLabel.AutoSize = true;
            this.startSelectionLabel.Location = new System.Drawing.Point(12, 44);
            this.startSelectionLabel.Name = "startSelectionLabel";
            this.startSelectionLabel.Size = new System.Drawing.Size(210, 30);
            this.startSelectionLabel.TabIndex = 1;
            this.startSelectionLabel.Text = "Choose starting date:";
            this.startSelectionLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // startingYearDropDown
            // 
            this.startingYearDropDown.FormattingEnabled = true;
            this.startingYearDropDown.Location = new System.Drawing.Point(17, 77);
            this.startingYearDropDown.Name = "startingYearDropDown";
            this.startingYearDropDown.Size = new System.Drawing.Size(121, 38);
            this.startingYearDropDown.TabIndex = 2;
            // 
            // startingDayDropDown
            // 
            this.startingDayDropDown.FormattingEnabled = true;
            this.startingDayDropDown.Location = new System.Drawing.Point(271, 77);
            this.startingDayDropDown.Name = "startingDayDropDown";
            this.startingDayDropDown.Size = new System.Drawing.Size(121, 38);
            this.startingDayDropDown.TabIndex = 3;
            // 
            // startingMonthDropDown
            // 
            this.startingMonthDropDown.FormattingEnabled = true;
            this.startingMonthDropDown.Location = new System.Drawing.Point(144, 77);
            this.startingMonthDropDown.Name = "startingMonthDropDown";
            this.startingMonthDropDown.Size = new System.Drawing.Size(121, 38);
            this.startingMonthDropDown.TabIndex = 5;
            // 
            // endingMonthDropDown
            // 
            this.endingMonthDropDown.FormattingEnabled = true;
            this.endingMonthDropDown.Location = new System.Drawing.Point(144, 151);
            this.endingMonthDropDown.Name = "endingMonthDropDown";
            this.endingMonthDropDown.Size = new System.Drawing.Size(121, 38);
            this.endingMonthDropDown.TabIndex = 9;
            // 
            // endingDayDropDown
            // 
            this.endingDayDropDown.FormattingEnabled = true;
            this.endingDayDropDown.Location = new System.Drawing.Point(271, 151);
            this.endingDayDropDown.Name = "endingDayDropDown";
            this.endingDayDropDown.Size = new System.Drawing.Size(121, 38);
            this.endingDayDropDown.TabIndex = 8;
            // 
            // endingYearDropDown
            // 
            this.endingYearDropDown.FormattingEnabled = true;
            this.endingYearDropDown.Location = new System.Drawing.Point(17, 151);
            this.endingYearDropDown.Name = "endingYearDropDown";
            this.endingYearDropDown.Size = new System.Drawing.Size(121, 38);
            this.endingYearDropDown.TabIndex = 7;
            // 
            // endSelectionLabel
            // 
            this.endSelectionLabel.AutoSize = true;
            this.endSelectionLabel.Location = new System.Drawing.Point(12, 118);
            this.endSelectionLabel.Name = "endSelectionLabel";
            this.endSelectionLabel.Size = new System.Drawing.Size(204, 30);
            this.endSelectionLabel.TabIndex = 6;
            this.endSelectionLabel.Text = "Choose ending date:";
            // 
            // subtaskListBox
            // 
            this.subtaskListBox.FormattingEnabled = true;
            this.subtaskListBox.ItemHeight = 30;
            this.subtaskListBox.Location = new System.Drawing.Point(18, 236);
            this.subtaskListBox.Name = "subtaskListBox";
            this.subtaskListBox.Size = new System.Drawing.Size(374, 124);
            this.subtaskListBox.TabIndex = 10;
            // 
            // subtaskListLabel
            // 
            this.subtaskListLabel.AutoSize = true;
            this.subtaskListLabel.Location = new System.Drawing.Point(12, 203);
            this.subtaskListLabel.Name = "subtaskListLabel";
            this.subtaskListLabel.Size = new System.Drawing.Size(122, 30);
            this.subtaskListLabel.TabIndex = 11;
            this.subtaskListLabel.Text = "Subtask list:";
            // 
            // deleteSubtaskButton
            // 
            this.deleteSubtaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteSubtaskButton.Location = new System.Drawing.Point(167, 366);
            this.deleteSubtaskButton.Name = "deleteSubtaskButton";
            this.deleteSubtaskButton.Size = new System.Drawing.Size(150, 37);
            this.deleteSubtaskButton.TabIndex = 52;
            this.deleteSubtaskButton.Text = "Delete subtask";
            this.deleteSubtaskButton.UseVisualStyleBackColor = true;
            // 
            // createSubtaskButton
            // 
            this.createSubtaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createSubtaskButton.Location = new System.Drawing.Point(18, 366);
            this.createSubtaskButton.Name = "createSubtaskButton";
            this.createSubtaskButton.Size = new System.Drawing.Size(143, 37);
            this.createSubtaskButton.TabIndex = 49;
            this.createSubtaskButton.Text = "Create subtask";
            this.createSubtaskButton.UseVisualStyleBackColor = true;
            // 
            // createTaskButton
            // 
            this.createTaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createTaskButton.Location = new System.Drawing.Point(135, 440);
            this.createTaskButton.Name = "createTaskButton";
            this.createTaskButton.Size = new System.Drawing.Size(139, 53);
            this.createTaskButton.TabIndex = 53;
            this.createTaskButton.Text = "Create task";
            this.createTaskButton.UseVisualStyleBackColor = true;
            // 
            // CreateTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(411, 520);
            this.Controls.Add(this.createTaskButton);
            this.Controls.Add(this.deleteSubtaskButton);
            this.Controls.Add(this.createSubtaskButton);
            this.Controls.Add(this.subtaskListLabel);
            this.Controls.Add(this.subtaskListBox);
            this.Controls.Add(this.endingMonthDropDown);
            this.Controls.Add(this.endingDayDropDown);
            this.Controls.Add(this.endingYearDropDown);
            this.Controls.Add(this.endSelectionLabel);
            this.Controls.Add(this.startingMonthDropDown);
            this.Controls.Add(this.startingDayDropDown);
            this.Controls.Add(this.startingYearDropDown);
            this.Controls.Add(this.startSelectionLabel);
            this.Controls.Add(this.addTaskLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "CreateTaskForm";
            this.Text = "Create Task";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label addTaskLabel;
        private System.Windows.Forms.Label startSelectionLabel;
        private System.Windows.Forms.ComboBox startingYearDropDown;
        private System.Windows.Forms.ComboBox startingDayDropDown;
        private System.Windows.Forms.ComboBox startingMonthDropDown;
        private System.Windows.Forms.ComboBox endingMonthDropDown;
        private System.Windows.Forms.ComboBox endingDayDropDown;
        private System.Windows.Forms.ComboBox endingYearDropDown;
        private System.Windows.Forms.Label endSelectionLabel;
        private System.Windows.Forms.Label subtaskListLabel;
        private System.Windows.Forms.Button deleteSubtaskButton;
        private System.Windows.Forms.Button createSubtaskButton;
        private System.Windows.Forms.ListBox subtaskListBox;
        private System.Windows.Forms.Button createTaskButton;
    }
}