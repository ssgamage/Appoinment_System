namespace DBConTest1
{
    partial class Form8
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
            this.button1pay = new System.Windows.Forms.Button();
            this.button2docpay = new System.Windows.Forms.Button();
            this.button3test = new System.Windows.Forms.Button();
            this.button4test = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1pay
            // 
            this.button1pay.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1pay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1pay.FlatAppearance.BorderSize = 0;
            this.button1pay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1pay.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1pay.ForeColor = System.Drawing.Color.White;
            this.button1pay.Location = new System.Drawing.Point(142, 217);
            this.button1pay.Name = "button1pay";
            this.button1pay.Size = new System.Drawing.Size(240, 105);
            this.button1pay.TabIndex = 11;
            this.button1pay.Text = "PAYMENT DETAILS";
            this.button1pay.UseVisualStyleBackColor = false;
            this.button1pay.Click += new System.EventHandler(this.button1pay_Click);
            // 
            // button2docpay
            // 
            this.button2docpay.BackColor = System.Drawing.Color.RoyalBlue;
            this.button2docpay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2docpay.FlatAppearance.BorderSize = 0;
            this.button2docpay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2docpay.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2docpay.ForeColor = System.Drawing.Color.White;
            this.button2docpay.Location = new System.Drawing.Point(492, 217);
            this.button2docpay.Name = "button2docpay";
            this.button2docpay.Size = new System.Drawing.Size(240, 105);
            this.button2docpay.TabIndex = 12;
            this.button2docpay.Text = "DOCTOR PAYMENTS";
            this.button2docpay.UseVisualStyleBackColor = false;
            this.button2docpay.Click += new System.EventHandler(this.button2docpay_Click);
            // 
            // button3test
            // 
            this.button3test.BackColor = System.Drawing.Color.RoyalBlue;
            this.button3test.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3test.FlatAppearance.BorderSize = 0;
            this.button3test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3test.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3test.ForeColor = System.Drawing.Color.White;
            this.button3test.Location = new System.Drawing.Point(142, 377);
            this.button3test.Name = "button3test";
            this.button3test.Size = new System.Drawing.Size(240, 105);
            this.button3test.TabIndex = 13;
            this.button3test.Text = "PREDICTION SECTION";
            this.button3test.UseVisualStyleBackColor = false;
            this.button3test.Click += new System.EventHandler(this.button3test_Click);
            // 
            // button4test
            // 
            this.button4test.BackColor = System.Drawing.Color.RoyalBlue;
            this.button4test.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4test.FlatAppearance.BorderSize = 0;
            this.button4test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4test.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4test.ForeColor = System.Drawing.Color.White;
            this.button4test.Location = new System.Drawing.Point(492, 377);
            this.button4test.Name = "button4test";
            this.button4test.Size = new System.Drawing.Size(240, 105);
            this.button4test.TabIndex = 14;
            this.button4test.Text = "FIND NEXT CLINIC DATE";
            this.button4test.UseVisualStyleBackColor = false;
            this.button4test.Click += new System.EventHandler(this.button4test_Click);
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::DBConTest1.Properties.Resources.bgdash1;
            this.ClientSize = new System.Drawing.Size(880, 690);
            this.Controls.Add(this.button4test);
            this.Controls.Add(this.button3test);
            this.Controls.Add(this.button2docpay);
            this.Controls.Add(this.button1pay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form8";
            this.Text = "Form8";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1pay;
        private System.Windows.Forms.Button button2docpay;
        private System.Windows.Forms.Button button3test;
        private System.Windows.Forms.Button button4test;
    }
}