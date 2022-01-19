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
            this.chooseMonthDropDown = new System.Windows.Forms.ComboBox();
            this.chooseDayDropDown = new System.Windows.Forms.ComboBox();
            this.chooseYearDropDown = new System.Windows.Forms.ComboBox();
            this.deleteTaskLabel = new System.Windows.Forms.Label();
            this.chooseWeekDropDown = new System.Windows.Forms.ComboBox();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.taskSelectionLabel = new System.Windows.Forms.Label();
            this.deleteTaskButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chooseMonthDropDown
            // 
            this.chooseMonthDropDown.FormattingEnabled = true;
            this.chooseMonthDropDown.Location = new System.Drawing.Point(139, 79);
            this.chooseMonthDropDown.Name = "chooseMonthDropDown";
            this.chooseMonthDropDown.Size = new System.Drawing.Size(121, 38);
            this.chooseMonthDropDown.TabIndex = 8;
            // 
            // chooseDayDropDown
            // 
            this.chooseDayDropDown.FormattingEnabled = true;
            this.chooseDayDropDown.Location = new System.Drawing.Point(393, 79);
            this.chooseDayDropDown.Name = "chooseDayDropDown";
            this.chooseDayDropDown.Size = new System.Drawing.Size(121, 38);
            this.chooseDayDropDown.TabIndex = 7;
            // 
            // chooseYearDropDown
            // 
            this.chooseYearDropDown.FormattingEnabled = true;
            this.chooseYearDropDown.Location = new System.Drawing.Point(12, 79);
            this.chooseYearDropDown.Name = "chooseYearDropDown";
            this.chooseYearDropDown.Size = new System.Drawing.Size(121, 38);
            this.chooseYearDropDown.TabIndex = 6;
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
            // chooseWeekDropDown
            // 
            this.chooseWeekDropDown.FormattingEnabled = true;
            this.chooseWeekDropDown.Location = new System.Drawing.Point(266, 79);
            this.chooseWeekDropDown.Name = "chooseWeekDropDown";
            this.chooseWeekDropDown.Size = new System.Drawing.Size(121, 38);
            this.chooseWeekDropDown.TabIndex = 10;
            // 
            // taskListBox
            // 
            this.taskListBox.FormattingEnabled = true;
            this.taskListBox.ItemHeight = 30;
            this.taskListBox.Location = new System.Drawing.Point(12, 123);
            this.taskListBox.Name = "taskListBox";
            this.taskListBox.Size = new System.Drawing.Size(502, 154);
            this.taskListBox.TabIndex = 11;
            // 
            // taskSelectionLabel
            // 
            this.taskSelectionLabel.AutoSize = true;
            this.taskSelectionLabel.Location = new System.Drawing.Point(12, 46);
            this.taskSelectionLabel.Name = "taskSelectionLabel";
            this.taskSelectionLabel.Size = new System.Drawing.Size(235, 30);
            this.taskSelectionLabel.TabIndex = 12;
            this.taskSelectionLabel.Text = "Choose a task to delete:";
            // 
            // deleteTaskButton
            // 
            this.deleteTaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteTaskButton.Location = new System.Drawing.Point(173, 302);
            this.deleteTaskButton.Name = "deleteTaskButton";
            this.deleteTaskButton.Size = new System.Drawing.Size(163, 54);
            this.deleteTaskButton.TabIndex = 49;
            this.deleteTaskButton.Text = "Delete task";
            this.deleteTaskButton.UseVisualStyleBackColor = true;
            // 
            // DeleteTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(526, 368);
            this.Controls.Add(this.deleteTaskButton);
            this.Controls.Add(this.taskSelectionLabel);
            this.Controls.Add(this.taskListBox);
            this.Controls.Add(this.chooseWeekDropDown);
            this.Controls.Add(this.deleteTaskLabel);
            this.Controls.Add(this.chooseMonthDropDown);
            this.Controls.Add(this.chooseDayDropDown);
            this.Controls.Add(this.chooseYearDropDown);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "DeleteTaskForm";
            this.Text = "Delete Task";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox chooseMonthDropDown;
        private System.Windows.Forms.ComboBox chooseDayDropDown;
        private System.Windows.Forms.ComboBox chooseYearDropDown;
        private System.Windows.Forms.Label deleteTaskLabel;
        private System.Windows.Forms.ComboBox chooseWeekDropDown;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.Label taskSelectionLabel;
        private System.Windows.Forms.Button deleteTaskButton;
    }
}