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
            this.startingMonthDropDown = new System.Windows.Forms.ComboBox();
            this.startingDayDropDown = new System.Windows.Forms.ComboBox();
            this.startingYearDropDown = new System.Windows.Forms.ComboBox();
            this.startSelectionLabel = new System.Windows.Forms.Label();
            this.endingMonthDropDown = new System.Windows.Forms.ComboBox();
            this.endingDayDropDown = new System.Windows.Forms.ComboBox();
            this.endingYearDropDown = new System.Windows.Forms.ComboBox();
            this.endSelectionLabel = new System.Windows.Forms.Label();
            this.changeTermLabel = new System.Windows.Forms.Label();
            this.updateTaskButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startingMonthDropDown
            // 
            this.startingMonthDropDown.FormattingEnabled = true;
            this.startingMonthDropDown.Location = new System.Drawing.Point(144, 79);
            this.startingMonthDropDown.Name = "startingMonthDropDown";
            this.startingMonthDropDown.Size = new System.Drawing.Size(121, 38);
            this.startingMonthDropDown.TabIndex = 9;
            // 
            // startingDayDropDown
            // 
            this.startingDayDropDown.FormattingEnabled = true;
            this.startingDayDropDown.Location = new System.Drawing.Point(271, 79);
            this.startingDayDropDown.Name = "startingDayDropDown";
            this.startingDayDropDown.Size = new System.Drawing.Size(121, 38);
            this.startingDayDropDown.TabIndex = 8;
            // 
            // startingYearDropDown
            // 
            this.startingYearDropDown.FormattingEnabled = true;
            this.startingYearDropDown.Location = new System.Drawing.Point(17, 79);
            this.startingYearDropDown.Name = "startingYearDropDown";
            this.startingYearDropDown.Size = new System.Drawing.Size(121, 38);
            this.startingYearDropDown.TabIndex = 7;
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
            // endingMonthDropDown
            // 
            this.endingMonthDropDown.FormattingEnabled = true;
            this.endingMonthDropDown.Location = new System.Drawing.Point(144, 153);
            this.endingMonthDropDown.Name = "endingMonthDropDown";
            this.endingMonthDropDown.Size = new System.Drawing.Size(121, 38);
            this.endingMonthDropDown.TabIndex = 13;
            // 
            // endingDayDropDown
            // 
            this.endingDayDropDown.FormattingEnabled = true;
            this.endingDayDropDown.Location = new System.Drawing.Point(271, 153);
            this.endingDayDropDown.Name = "endingDayDropDown";
            this.endingDayDropDown.Size = new System.Drawing.Size(121, 38);
            this.endingDayDropDown.TabIndex = 12;
            // 
            // endingYearDropDown
            // 
            this.endingYearDropDown.FormattingEnabled = true;
            this.endingYearDropDown.Location = new System.Drawing.Point(17, 153);
            this.endingYearDropDown.Name = "endingYearDropDown";
            this.endingYearDropDown.Size = new System.Drawing.Size(121, 38);
            this.endingYearDropDown.TabIndex = 11;
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
            // 
            // ChangeTermForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(406, 292);
            this.Controls.Add(this.updateTaskButton);
            this.Controls.Add(this.changeTermLabel);
            this.Controls.Add(this.endingMonthDropDown);
            this.Controls.Add(this.endingDayDropDown);
            this.Controls.Add(this.endingYearDropDown);
            this.Controls.Add(this.endSelectionLabel);
            this.Controls.Add(this.startingMonthDropDown);
            this.Controls.Add(this.startingDayDropDown);
            this.Controls.Add(this.startingYearDropDown);
            this.Controls.Add(this.startSelectionLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ChangeTermForm";
            this.Text = "Change Term";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox startingMonthDropDown;
        private System.Windows.Forms.ComboBox startingDayDropDown;
        private System.Windows.Forms.ComboBox startingYearDropDown;
        private System.Windows.Forms.Label startSelectionLabel;
        private System.Windows.Forms.ComboBox endingMonthDropDown;
        private System.Windows.Forms.ComboBox endingDayDropDown;
        private System.Windows.Forms.ComboBox endingYearDropDown;
        private System.Windows.Forms.Label endSelectionLabel;
        private System.Windows.Forms.Label changeTermLabel;
        private System.Windows.Forms.Button updateTaskButton;
    }
}