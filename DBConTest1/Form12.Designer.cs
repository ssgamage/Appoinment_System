namespace DBConTest1
{
    partial class Form12
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblFutureDate = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.btnCalculateFutureDate = new System.Windows.Forms.Button();
            this.txtDays = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(263, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 41;
            this.label1.Text = "Enter date frame Here :";
            // 
            // lblFutureDate
            // 
            this.lblFutureDate.AutoSize = true;
            this.lblFutureDate.BackColor = System.Drawing.Color.Transparent;
            this.lblFutureDate.Location = new System.Drawing.Point(428, 386);
            this.lblFutureDate.Name = "lblFutureDate";
            this.lblFutureDate.Size = new System.Drawing.Size(126, 16);
            this.lblFutureDate.TabIndex = 40;
            this.lblFutureDate.Text = "\"result show in here\"";
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.ForeColor = System.Drawing.Color.White;
            this.buttonBack.Location = new System.Drawing.Point(137, 24);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(172, 58);
            this.buttonBack.TabIndex = 39;
            this.buttonBack.Text = "BACK";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // btnCalculateFutureDate
            // 
            this.btnCalculateFutureDate.BackColor = System.Drawing.Color.Yellow;
            this.btnCalculateFutureDate.Location = new System.Drawing.Point(443, 284);
            this.btnCalculateFutureDate.Name = "btnCalculateFutureDate";
            this.btnCalculateFutureDate.Size = new System.Drawing.Size(142, 23);
            this.btnCalculateFutureDate.TabIndex = 38;
            this.btnCalculateFutureDate.Text = "Calculate Next Date\r\n\r\n";
            this.btnCalculateFutureDate.UseVisualStyleBackColor = false;
            this.btnCalculateFutureDate.Click += new System.EventHandler(this.btnCalculateFutureDate_Click);
            // 
            // txtDays
            // 
            this.txtDays.Location = new System.Drawing.Point(446, 256);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(139, 22);
            this.txtDays.TabIndex = 37;
            // 
            // Form12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DBConTest1.Properties.Resources.bgdash1;
            this.ClientSize = new System.Drawing.Size(880, 690);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFutureDate);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.btnCalculateFutureDate);
            this.Controls.Add(this.txtDays);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form12";
            this.Text = "Form12";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFutureDate;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button btnCalculateFutureDate;
        private System.Windows.Forms.TextBox txtDays;
    }
}