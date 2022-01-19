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
            this.taskSelectionLabel = new System.Windows.Forms.Label();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.chooseWeekDropDown = new System.Windows.Forms.ComboBox();
            this.updateTaskLabel = new System.Windows.Forms.Label();
            this.chooseMonthDropDown = new System.Windows.Forms.ComboBox();
            this.chooseDayDropDown = new System.Windows.Forms.ComboBox();
            this.chooseYearDropDown = new System.Windows.Forms.ComboBox();
            this.changeStatusButton = new System.Windows.Forms.Button();
            this.changeTermButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // taskSelectionLabel
            // 
            this.taskSelectionLabel.AutoSize = true;
            this.taskSelectionLabel.Location = new System.Drawing.Point(12, 46);
            this.taskSelectionLabel.Name = "taskSelectionLabel";
            this.taskSelectionLabel.Size = new System.Drawing.Size(243, 30);
            this.taskSelectionLabel.TabIndex = 19;
            this.taskSelectionLabel.Text = "Choose a task to update:";
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
            // updateTaskLabel
            // 
            this.updateTaskLabel.AutoSize = true;
            this.updateTaskLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateTaskLabel.Location = new System.Drawing.Point(186, 9);
            this.updateTaskLabel.Name = "updateTaskLabel";
            this.updateTaskLabel.Size = new System.Drawing.Size(159, 37);
            this.updateTaskLabel.TabIndex = 16;
            this.updateTaskLabel.Text = "Update task";
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
            // changeStatusButton
            // 
            this.changeStatusButton.BackColor = System.Drawing.Color.Gainsboro;
            this.changeStatusButton.Location = new System.Drawing.Point(293, 296);
            this.changeStatusButton.Name = "changeStatusButton";
            this.changeStatusButton.Size = new System.Drawing.Size(160, 38);
            this.changeStatusButton.TabIndex = 21;
            this.changeStatusButton.Text = "Change status";
            this.changeStatusButton.UseVisualStyleBackColor = false;
            // 
            // changeTermButton
            // 
            this.changeTermButton.BackColor = System.Drawing.Color.Gainsboro;
            this.changeTermButton.Location = new System.Drawing.Point(64, 296);
            this.changeTermButton.Name = "changeTermButton";
            this.changeTermButton.Size = new System.Drawing.Size(160, 38);
            this.changeTermButton.TabIndex = 20;
            this.changeTermButton.Text = "Change term";
            this.changeTermButton.UseVisualStyleBackColor = false;
            // 
            // UpdateTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(526, 357);
            this.Controls.Add(this.changeStatusButton);
            this.Controls.Add(this.changeTermButton);
            this.Controls.Add(this.taskSelectionLabel);
            this.Controls.Add(this.taskListBox);
            this.Controls.Add(this.chooseWeekDropDown);
            this.Controls.Add(this.updateTaskLabel);
            this.Controls.Add(this.chooseMonthDropDown);
            this.Controls.Add(this.chooseDayDropDown);
            this.Controls.Add(this.chooseYearDropDown);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "UpdateTaskForm";
            this.Text = "Update Task";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label taskSelectionLabel;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.ComboBox chooseWeekDropDown;
        private System.Windows.Forms.Label updateTaskLabel;
        private System.Windows.Forms.ComboBox chooseMonthDropDown;
        private System.Windows.Forms.ComboBox chooseDayDropDown;
        private System.Windows.Forms.ComboBox chooseYearDropDown;
        private System.Windows.Forms.Button changeStatusButton;
        private System.Windows.Forms.Button changeTermButton;
    }
}