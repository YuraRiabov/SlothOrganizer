namespace Sloth_Organizer
{
    partial class ChangeSubTasksForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeSubTasksForm));
            this.updateTaskButton = new System.Windows.Forms.Button();
            this.deleteSubtaskButton = new System.Windows.Forms.Button();
            this.createSubtaskButton = new System.Windows.Forms.Button();
            this.subtaskListLabel = new System.Windows.Forms.Label();
            this.subTaskListBox = new System.Windows.Forms.ListBox();
            this.changeSubTasksLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // updateTaskButton
            // 
            this.updateTaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateTaskButton.Location = new System.Drawing.Point(135, 275);
            this.updateTaskButton.Name = "updateTaskButton";
            this.updateTaskButton.Size = new System.Drawing.Size(139, 53);
            this.updateTaskButton.TabIndex = 58;
            this.updateTaskButton.Text = "Update task";
            this.updateTaskButton.UseVisualStyleBackColor = true;
            this.updateTaskButton.Click += new System.EventHandler(this.updateTaskButton_Click);
            // 
            // deleteSubtaskButton
            // 
            this.deleteSubtaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteSubtaskButton.Location = new System.Drawing.Point(242, 212);
            this.deleteSubtaskButton.Name = "deleteSubtaskButton";
            this.deleteSubtaskButton.Size = new System.Drawing.Size(150, 37);
            this.deleteSubtaskButton.TabIndex = 57;
            this.deleteSubtaskButton.Text = "Delete subtask";
            this.deleteSubtaskButton.UseVisualStyleBackColor = true;
            this.deleteSubtaskButton.Click += new System.EventHandler(this.deleteSubtaskButton_Click);
            // 
            // createSubtaskButton
            // 
            this.createSubtaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createSubtaskButton.Location = new System.Drawing.Point(18, 212);
            this.createSubtaskButton.Name = "createSubtaskButton";
            this.createSubtaskButton.Size = new System.Drawing.Size(143, 37);
            this.createSubtaskButton.TabIndex = 56;
            this.createSubtaskButton.Text = "Create subtask";
            this.createSubtaskButton.UseVisualStyleBackColor = true;
            this.createSubtaskButton.Click += new System.EventHandler(this.createSubtaskButton_Click);
            // 
            // subtaskListLabel
            // 
            this.subtaskListLabel.AutoSize = true;
            this.subtaskListLabel.Location = new System.Drawing.Point(12, 49);
            this.subtaskListLabel.Name = "subtaskListLabel";
            this.subtaskListLabel.Size = new System.Drawing.Size(122, 30);
            this.subtaskListLabel.TabIndex = 55;
            this.subtaskListLabel.Text = "Subtask list:";
            // 
            // subTaskListBox
            // 
            this.subTaskListBox.FormattingEnabled = true;
            this.subTaskListBox.ItemHeight = 30;
            this.subTaskListBox.Location = new System.Drawing.Point(18, 82);
            this.subTaskListBox.Name = "subTaskListBox";
            this.subTaskListBox.Size = new System.Drawing.Size(374, 124);
            this.subTaskListBox.TabIndex = 54;
            // 
            // changeSubTasksLabel
            // 
            this.changeSubTasksLabel.AutoSize = true;
            this.changeSubTasksLabel.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeSubTasksLabel.Location = new System.Drawing.Point(85, 9);
            this.changeSubTasksLabel.Name = "changeSubTasksLabel";
            this.changeSubTasksLabel.Size = new System.Drawing.Size(230, 40);
            this.changeSubTasksLabel.TabIndex = 59;
            this.changeSubTasksLabel.Text = "Change subtasks";
            // 
            // ChangeSubTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(412, 338);
            this.Controls.Add(this.changeSubTasksLabel);
            this.Controls.Add(this.updateTaskButton);
            this.Controls.Add(this.deleteSubtaskButton);
            this.Controls.Add(this.createSubtaskButton);
            this.Controls.Add(this.subtaskListLabel);
            this.Controls.Add(this.subTaskListBox);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximizeBox = false;
            this.Name = "ChangeSubTasksForm";
            this.Text = "ChangeSubTasksForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button updateTaskButton;
        private System.Windows.Forms.Button deleteSubtaskButton;
        private System.Windows.Forms.Button createSubtaskButton;
        private System.Windows.Forms.Label subtaskListLabel;
        private System.Windows.Forms.ListBox subTaskListBox;
        private System.Windows.Forms.Label changeSubTasksLabel;
    }
}