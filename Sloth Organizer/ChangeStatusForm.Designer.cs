namespace Sloth_Organizer
{
    partial class ChangeStatusForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeStatusForm));
            this.changeStatusLabel = new System.Windows.Forms.Label();
            this.chooseStatusCheckBox = new System.Windows.Forms.CheckedListBox();
            this.updateTaskButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // changeStatusLabel
            // 
            this.changeStatusLabel.AutoSize = true;
            this.changeStatusLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeStatusLabel.Location = new System.Drawing.Point(77, 9);
            this.changeStatusLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.changeStatusLabel.Name = "changeStatusLabel";
            this.changeStatusLabel.Size = new System.Drawing.Size(184, 37);
            this.changeStatusLabel.TabIndex = 18;
            this.changeStatusLabel.Text = "Change status";
            this.changeStatusLabel.Click += new System.EventHandler(this.changeTermLabel_Click);
            // 
            // chooseStatusCheckBox
            // 
            this.chooseStatusCheckBox.FormattingEnabled = true;
            this.chooseStatusCheckBox.Location = new System.Drawing.Point(12, 59);
            this.chooseStatusCheckBox.Name = "chooseStatusCheckBox";
            this.chooseStatusCheckBox.Size = new System.Drawing.Size(249, 64);
            this.chooseStatusCheckBox.TabIndex = 19;
            // 
            // updateTaskButton
            // 
            this.updateTaskButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateTaskButton.Location = new System.Drawing.Point(95, 143);
            this.updateTaskButton.Name = "updateTaskButton";
            this.updateTaskButton.Size = new System.Drawing.Size(147, 61);
            this.updateTaskButton.TabIndex = 49;
            this.updateTaskButton.Text = "Update task";
            this.updateTaskButton.UseVisualStyleBackColor = true;
            // 
            // ChangeStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(324, 216);
            this.Controls.Add(this.updateTaskButton);
            this.Controls.Add(this.chooseStatusCheckBox);
            this.Controls.Add(this.changeStatusLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ChangeStatusForm";
            this.Text = "Change Status";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label changeStatusLabel;
        private System.Windows.Forms.CheckedListBox chooseStatusCheckBox;
        private System.Windows.Forms.Button updateTaskButton;
    }
}