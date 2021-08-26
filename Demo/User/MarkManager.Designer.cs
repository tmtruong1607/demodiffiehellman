namespace Demo.User
{
    partial class MarkManager
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
            this.transcript_dgv = new System.Windows.Forms.DataGridView();
            this.malop_cb = new System.Windows.Forms.ComboBox();
            this.tenlop_tb = new System.Windows.Forms.TextBox();
            this.magvpt_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.save_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.tengvpt_tb = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.transcript_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // transcript_dgv
            // 
            this.transcript_dgv.AllowUserToAddRows = false;
            this.transcript_dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.transcript_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transcript_dgv.Location = new System.Drawing.Point(12, 113);
            this.transcript_dgv.Name = "transcript_dgv";
            this.transcript_dgv.RowHeadersVisible = false;
            this.transcript_dgv.Size = new System.Drawing.Size(438, 268);
            this.transcript_dgv.TabIndex = 0;
            this.transcript_dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.transcript_dgv_CellContentClick);
            // 
            // malop_cb
            // 
            this.malop_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.malop_cb.FormattingEnabled = true;
            this.malop_cb.Location = new System.Drawing.Point(101, 61);
            this.malop_cb.Name = "malop_cb";
            this.malop_cb.Size = new System.Drawing.Size(121, 21);
            this.malop_cb.TabIndex = 1;
            this.malop_cb.SelectedIndexChanged += new System.EventHandler(this.malop_cb_SelectedIndexChanged);
            // 
            // tenlop_tb
            // 
            this.tenlop_tb.Location = new System.Drawing.Point(347, 61);
            this.tenlop_tb.Name = "tenlop_tb";
            this.tenlop_tb.ReadOnly = true;
            this.tenlop_tb.Size = new System.Drawing.Size(146, 20);
            this.tenlop_tb.TabIndex = 2;
            // 
            // magvpt_tb
            // 
            this.magvpt_tb.Location = new System.Drawing.Point(347, 87);
            this.magvpt_tb.Name = "magvpt_tb";
            this.magvpt_tb.ReadOnly = true;
            this.magvpt_tb.Size = new System.Drawing.Size(68, 20);
            this.magvpt_tb.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã lớp học phần";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên lớp học phần";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Giáo viên phụ trách";
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(475, 151);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(75, 23);
            this.save_btn.TabIndex = 4;
            this.save_btn.Text = "Lưu";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(475, 213);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 4;
            this.cancel_btn.Text = "Hủy";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // tengvpt_tb
            // 
            this.tengvpt_tb.Location = new System.Drawing.Point(421, 87);
            this.tengvpt_tb.Name = "tengvpt_tb";
            this.tengvpt_tb.ReadOnly = true;
            this.tengvpt_tb.Size = new System.Drawing.Size(129, 20);
            this.tengvpt_tb.TabIndex = 2;
            // 
            // MarkManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(566, 393);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tengvpt_tb);
            this.Controls.Add(this.magvpt_tb);
            this.Controls.Add(this.tenlop_tb);
            this.Controls.Add(this.malop_cb);
            this.Controls.Add(this.transcript_dgv);
            this.Name = "MarkManager";
            this.Text = "MarkManager";
            this.Load += new System.EventHandler(this.MarkManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.transcript_dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView transcript_dgv;
        private System.Windows.Forms.ComboBox malop_cb;
        private System.Windows.Forms.TextBox tenlop_tb;
        private System.Windows.Forms.TextBox magvpt_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.TextBox tengvpt_tb;
    }
}