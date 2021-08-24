namespace Demo.User
{
    partial class Profile
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.magv_tb = new System.Windows.Forms.TextBox();
            this.ten_tb = new System.Windows.Forms.TextBox();
            this.email_tb = new System.Windows.Forms.TextBox();
            this.sdt_tb = new System.Windows.Forms.TextBox();
            this.khoa_cb = new System.Windows.Forms.ComboBox();
            this.save_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.insprivkey_lb = new System.Windows.Forms.Label();
            this.insprivkey_tb = new System.Windows.Forms.TextBox();
            this.cv_cb = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.makhoa_tb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Họ và tên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã GV";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Email";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Số điện thoại";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Chức vụ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(281, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Khoa";
            // 
            // magv_tb
            // 
            this.magv_tb.Location = new System.Drawing.Point(113, 47);
            this.magv_tb.Name = "magv_tb";
            this.magv_tb.Size = new System.Drawing.Size(118, 20);
            this.magv_tb.TabIndex = 0;
            // 
            // ten_tb
            // 
            this.ten_tb.Location = new System.Drawing.Point(113, 73);
            this.ten_tb.Name = "ten_tb";
            this.ten_tb.Size = new System.Drawing.Size(118, 20);
            this.ten_tb.TabIndex = 1;
            // 
            // email_tb
            // 
            this.email_tb.Location = new System.Drawing.Point(113, 100);
            this.email_tb.Name = "email_tb";
            this.email_tb.Size = new System.Drawing.Size(118, 20);
            this.email_tb.TabIndex = 2;
            // 
            // sdt_tb
            // 
            this.sdt_tb.Location = new System.Drawing.Point(113, 126);
            this.sdt_tb.Name = "sdt_tb";
            this.sdt_tb.Size = new System.Drawing.Size(118, 20);
            this.sdt_tb.TabIndex = 3;
            // 
            // khoa_cb
            // 
            this.khoa_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.khoa_cb.FormattingEnabled = true;
            this.khoa_cb.Location = new System.Drawing.Point(334, 73);
            this.khoa_cb.Name = "khoa_cb";
            this.khoa_cb.Size = new System.Drawing.Size(157, 21);
            this.khoa_cb.TabIndex = 5;
            this.khoa_cb.SelectedIndexChanged += new System.EventHandler(this.khoa_cb_SelectedIndexChanged);
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(180, 190);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(75, 23);
            this.save_btn.TabIndex = 8;
            this.save_btn.Text = "Lưu";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(66, 190);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 9;
            this.cancel_btn.Text = "Hủy bỏ";
            this.cancel_btn.UseVisualStyleBackColor = true;
            // 
            // insprivkey_lb
            // 
            this.insprivkey_lb.Location = new System.Drawing.Point(281, 125);
            this.insprivkey_lb.Name = "insprivkey_lb";
            this.insprivkey_lb.Size = new System.Drawing.Size(57, 32);
            this.insprivkey_lb.TabIndex = 0;
            this.insprivkey_lb.Text = "Tùy chọn Pubkey";
            // 
            // insprivkey_tb
            // 
            this.insprivkey_tb.Location = new System.Drawing.Point(334, 127);
            this.insprivkey_tb.Name = "insprivkey_tb";
            this.insprivkey_tb.Size = new System.Drawing.Size(157, 20);
            this.insprivkey_tb.TabIndex = 6;
            this.insprivkey_tb.TextChanged += new System.EventHandler(this.insprivkey_tb_TextChanged);
            // 
            // cv_cb
            // 
            this.cv_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cv_cb.FormattingEnabled = true;
            this.cv_cb.Location = new System.Drawing.Point(334, 46);
            this.cv_cb.Name = "cv_cb";
            this.cv_cb.Size = new System.Drawing.Size(157, 21);
            this.cv_cb.TabIndex = 4;
            this.cv_cb.SelectedIndexChanged += new System.EventHandler(this.cv_cb_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(281, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Mã khoa";
            // 
            // makhoa_tb
            // 
            this.makhoa_tb.Location = new System.Drawing.Point(336, 100);
            this.makhoa_tb.Name = "makhoa_tb";
            this.makhoa_tb.ReadOnly = true;
            this.makhoa_tb.Size = new System.Drawing.Size(155, 20);
            this.makhoa_tb.TabIndex = 10;
            // 
            // Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(557, 225);
            this.Controls.Add(this.makhoa_tb);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.cv_cb);
            this.Controls.Add(this.khoa_cb);
            this.Controls.Add(this.sdt_tb);
            this.Controls.Add(this.email_tb);
            this.Controls.Add(this.ten_tb);
            this.Controls.Add(this.insprivkey_tb);
            this.Controls.Add(this.magv_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.insprivkey_lb);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Profile";
            this.Text = "User";
            this.Load += new System.EventHandler(this.Profile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox magv_tb;
        private System.Windows.Forms.TextBox ten_tb;
        private System.Windows.Forms.TextBox email_tb;
        private System.Windows.Forms.TextBox sdt_tb;
        private System.Windows.Forms.ComboBox khoa_cb;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Label insprivkey_lb;
        private System.Windows.Forms.TextBox insprivkey_tb;
        private System.Windows.Forms.ComboBox cv_cb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox makhoa_tb;
    }
}