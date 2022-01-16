namespace Sloth_Organizer
{
    partial class ConfirmDeletionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmDeletionForm));
            this.confirmDeletionLabel = new System.Windows.Forms.Label();
            this.taskLabel = new System.Windows.Forms.Label();
            this.noButton = new System.Windows.Forms.Button();
            this.yesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // confirmDeletionLabel
            // 
            this.confirmDeletionLabel.AutoSize = true;
            this.confirmDeletionLabel.Location = new System.Drawing.Point(15, 9);
            this.confirmDeletionLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.confirmDeletionLabel.Name = "confirmDeletionLabel";
            this.confirmDeletionLabel.Size = new System.Drawing.Size(400, 30);
            this.confirmDeletionLabel.TabIndex = 0;
            this.confirmDeletionLabel.Text = "Are you sure you want to delete this task?";
            // 
            // taskLabel
            // 
            this.taskLabel.AutoSize = true;
            this.taskLabel.Location = new System.Drawing.Point(15, 39);
            this.taskLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.taskLabel.MaximumSize = new System.Drawing.Size(400, 0);
            this.taskLabel.Name = "taskLabel";
            this.taskLabel.Size = new System.Drawing.Size(78, 30);
            this.taskLabel.TabIndex = 1;
            this.taskLabel.Text = "<task>";
            // 
            // noButton
            // 
            this.noButton.BackColor = System.Drawing.Color.Gainsboro;
            this.noButton.Location = new System.Drawing.Point(90, 114);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(61, 38);
            this.noButton.TabIndex = 2;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = false;
            // 
            // yesButton
            // 
            this.yesButton.BackColor = System.Drawing.Color.Gainsboro;
            this.yesButton.Location = new System.Drawing.Point(274, 114);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(61, 38);
            this.yesButton.TabIndex = 3;
            this.yesButton.Text = "Yes";
            this.yesButton.UseVisualStyleBackColor = false;
            this.yesButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // ConfirmDeletionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(429, 176);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.taskLabel);
            this.Controls.Add(this.confirmDeletionLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ConfirmDeletionForm";
            this.Text = "ConfirmDeletionForm";
            this.Load += new System.EventHandler(this.ConfirmDeletionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label confirmDeletionLabel;
        private System.Windows.Forms.Label taskLabel;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Button yesButton;
    }
}