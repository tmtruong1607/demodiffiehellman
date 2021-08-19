namespace Demo
{
    partial class Login
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
            this.id_tb = new System.Windows.Forms.TextBox();
            this.pw_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lg_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // id_tb
            // 
            this.id_tb.Location = new System.Drawing.Point(112, 34);
            this.id_tb.Name = "id_tb";
            this.id_tb.Size = new System.Drawing.Size(133, 20);
            this.id_tb.TabIndex = 0;
            // 
            // pw_tb
            // 
            this.pw_tb.Location = new System.Drawing.Point(112, 70);
            this.pw_tb.Name = "pw_tb";
            this.pw_tb.Size = new System.Drawing.Size(133, 20);
            this.pw_tb.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên đăng nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mật khẩu";
            // 
            // lg_btn
            // 
            this.lg_btn.Location = new System.Drawing.Point(170, 117);
            this.lg_btn.Name = "lg_btn";
            this.lg_btn.Size = new System.Drawing.Size(75, 23);
            this.lg_btn.TabIndex = 4;
            this.lg_btn.Text = "Đăng nhập";
            this.lg_btn.UseVisualStyleBackColor = true;
            this.lg_btn.Click += new System.EventHandler(this.lg_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 158);
            this.Controls.Add(this.lg_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pw_tb);
            this.Controls.Add(this.id_tb);
            this.Name = "Form1";
            this.Text = "Đăng nhập";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox id_tb;
        private System.Windows.Forms.TextBox pw_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button lg_btn;
    }
}

