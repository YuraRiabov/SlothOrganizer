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
            this.createTaskLabel = new System.Windows.Forms.Label();
            this.startSelectionLabel = new System.Windows.Forms.Label();
            this.endSelectionLabel = new System.Windows.Forms.Label();
            this.subtaskListBox = new System.Windows.Forms.ListBox();
            this.subtaskListLabel = new System.Windows.Forms.Label();
            this.deleteSubtaskButton = new System.Windows.Forms.Button();
            this.createSubtaskButton = new System.Windows.Forms.Button();
            this.createTaskButton = new System.Windows.Forms.Button();
            this.startPicker = new System.Windows.Forms.DateTimePicker();
            this.endPicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // createTaskLabel
            // 
            this.createTaskLabel.AutoSize = true;
            this.createTaskLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createTaskLabel.Location = new System.Drawing.Point(128, 7);
            this.createTaskLabel.Name = "createTaskLabel";
            this.createTaskLabel.Size = new System.Drawing.Size(148, 37);
            this.createTaskLabel.TabIndex = 0;
            this.createTaskLabel.Text = "Create task";
            this.createTaskLabel.Click += new System.EventHandler(this.label1_Click);
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
            this.deleteSubtaskButton.Location = new System.Drawing.Point(242, 366);
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
            // startPicker
            // 
            this.startPicker.Location = new System.Drawing.Point(17, 80);
            this.startPicker.Name = "startPicker";
            this.startPicker.Size = new System.Drawing.Size(375, 35);
            this.startPicker.TabIndex = 54;
            this.startPicker.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // endPicker
            // 
            this.endPicker.Location = new System.Drawing.Point(18, 165);
            this.endPicker.Name = "endPicker";
            this.endPicker.Size = new System.Drawing.Size(374, 35);
            this.endPicker.TabIndex = 55;
            // 
            // CreateTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(411, 520);
            this.Controls.Add(this.endPicker);
            this.Controls.Add(this.startPicker);
            this.Controls.Add(this.createTaskButton);
            this.Controls.Add(this.deleteSubtaskButton);
            this.Controls.Add(this.createSubtaskButton);
            this.Controls.Add(this.subtaskListLabel);
            this.Controls.Add(this.subtaskListBox);
            this.Controls.Add(this.endSelectionLabel);
            this.Controls.Add(this.startSelectionLabel);
            this.Controls.Add(this.createTaskLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "CreateTaskForm";
            this.Text = "Create Task";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label createTaskLabel;
        private System.Windows.Forms.Label startSelectionLabel;
        private System.Windows.Forms.Label endSelectionLabel;
        private System.Windows.Forms.Label subtaskListLabel;
        private System.Windows.Forms.Button deleteSubtaskButton;
        private System.Windows.Forms.Button createSubtaskButton;
        private System.Windows.Forms.ListBox subtaskListBox;
        private System.Windows.Forms.Button createTaskButton;
        private System.Windows.Forms.DateTimePicker startPicker;
        private System.Windows.Forms.DateTimePicker endPicker;
    }
}