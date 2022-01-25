namespace Sloth_Organizer
{
    partial class ChangeTermForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeTermForm));
            this.startSelectionLabel = new System.Windows.Forms.Label();
            this.endSelectionLabel = new System.Windows.Forms.Label();
            this.changeTermLabel = new System.Windows.Forms.Label();
            this.updateTaskButton = new System.Windows.Forms.Button();
            this.startPicker = new System.Windows.Forms.DateTimePicker();
            this.endPicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // startSelectionLabel
            // 
            this.startSelectionLabel.AutoSize = true;
            this.startSelectionLabel.Location = new System.Drawing.Point(12, 46);
            this.startSelectionLabel.Name = "startSelectionLabel";
            this.startSelectionLabel.Size = new System.Drawing.Size(210, 30);
            this.startSelectionLabel.TabIndex = 6;
            this.startSelectionLabel.Text = "Choose starting date:";
            // 
            // endSelectionLabel
            // 
            this.endSelectionLabel.AutoSize = true;
            this.endSelectionLabel.Location = new System.Drawing.Point(12, 120);
            this.endSelectionLabel.Name = "endSelectionLabel";
            this.endSelectionLabel.Size = new System.Drawing.Size(204, 30);
            this.endSelectionLabel.TabIndex = 10;
            this.endSelectionLabel.Text = "Choose ending date:";
            // 
            // changeTermLabel
            // 
            this.changeTermLabel.AutoSize = true;
            this.changeTermLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeTermLabel.Location = new System.Drawing.Point(120, 9);
            this.changeTermLabel.Name = "changeTermLabel";
            this.changeTermLabel.Size = new System.Drawing.Size(170, 37);
            this.changeTermLabel.TabIndex = 17;
            this.changeTermLabel.Text = "Change term";
            // 
            // updateTaskButton
            // 
            this.updateTaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateTaskButton.Location = new System.Drawing.Point(118, 219);
            this.updateTaskButton.Name = "updateTaskButton";
            this.updateTaskButton.Size = new System.Drawing.Size(147, 61);
            this.updateTaskButton.TabIndex = 48;
            this.updateTaskButton.Text = "Update task";
            this.updateTaskButton.UseVisualStyleBackColor = true;
            this.updateTaskButton.Click += new System.EventHandler(this.updateTaskButton_Click);
            // 
            // startPicker
            // 
            this.startPicker.Location = new System.Drawing.Point(17, 82);
            this.startPicker.Name = "startPicker";
            this.startPicker.Size = new System.Drawing.Size(375, 35);
            this.startPicker.TabIndex = 55;
            // 
            // endPicker
            // 
            this.endPicker.Location = new System.Drawing.Point(17, 153);
            this.endPicker.Name = "endPicker";
            this.endPicker.Size = new System.Drawing.Size(374, 35);
            this.endPicker.TabIndex = 56;
            // 
            // ChangeTermForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(406, 292);
            this.Controls.Add(this.endPicker);
            this.Controls.Add(this.startPicker);
            this.Controls.Add(this.updateTaskButton);
            this.Controls.Add(this.changeTermLabel);
            this.Controls.Add(this.endSelectionLabel);
            this.Controls.Add(this.startSelectionLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximizeBox = false;
            this.Name = "ChangeTermForm";
            this.Text = "Change Term";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label startSelectionLabel;
        private System.Windows.Forms.Label endSelectionLabel;
        private System.Windows.Forms.Label changeTermLabel;
        private System.Windows.Forms.Button updateTaskButton;
        private System.Windows.Forms.DateTimePicker startPicker;
        private System.Windows.Forms.DateTimePicker endPicker;
    }
}