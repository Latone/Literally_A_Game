namespace WindowsFormsDelicate
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Player_HP = new System.Windows.Forms.Label();
            this.Enemy_HP = new System.Windows.Forms.Label();
            this.Hero_Stats = new System.Windows.Forms.TextBox();
            this.Enemy_Stats = new System.Windows.Forms.TextBox();
            this.Attack_Button = new System.Windows.Forms.Button();
            this.STOIKA_BUTTON = new System.Windows.Forms.Button();
            this.chck_box = new System.Windows.Forms.CheckBox();
            this.info_label = new System.Windows.Forms.Label();
            this.hodiki = new System.Windows.Forms.Label();
            this.WinScreen = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(239, 152);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(165, 94);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.Visible = false;
            this.checkedListBox1.SelectedValueChanged += new System.EventHandler(this.CheckedListBox1_SelectedValueChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(0, 255);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(526, 268);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox2_Paint);
            // 
            // Player_HP
            // 
            this.Player_HP.AutoSize = true;
            this.Player_HP.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Player_HP.Location = new System.Drawing.Point(54, 317);
            this.Player_HP.Name = "Player_HP";
            this.Player_HP.Size = new System.Drawing.Size(81, 19);
            this.Player_HP.TabIndex = 3;
            this.Player_HP.Text = "HP: 100/100";
            this.Player_HP.Visible = false;
            // 
            // Enemy_HP
            // 
            this.Enemy_HP.AutoSize = true;
            this.Enemy_HP.Font = new System.Drawing.Font("Impact", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Enemy_HP.Location = new System.Drawing.Point(419, 359);
            this.Enemy_HP.Name = "Enemy_HP";
            this.Enemy_HP.Size = new System.Drawing.Size(49, 19);
            this.Enemy_HP.TabIndex = 3;
            this.Enemy_HP.Text = "HP: 7/7";
            this.Enemy_HP.Visible = false;
            // 
            // Hero_Stats
            // 
            this.Hero_Stats.Enabled = false;
            this.Hero_Stats.Location = new System.Drawing.Point(142, 265);
            this.Hero_Stats.Multiline = true;
            this.Hero_Stats.Name = "Hero_Stats";
            this.Hero_Stats.Size = new System.Drawing.Size(124, 85);
            this.Hero_Stats.TabIndex = 4;
            this.Hero_Stats.Visible = false;
            // 
            // Enemy_Stats
            // 
            this.Enemy_Stats.Enabled = false;
            this.Enemy_Stats.Location = new System.Drawing.Point(290, 265);
            this.Enemy_Stats.Multiline = true;
            this.Enemy_Stats.Name = "Enemy_Stats";
            this.Enemy_Stats.Size = new System.Drawing.Size(124, 85);
            this.Enemy_Stats.TabIndex = 4;
            this.Enemy_Stats.Visible = false;
            // 
            // Attack_Button
            // 
            this.Attack_Button.Font = new System.Drawing.Font("Book Antiqua", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Attack_Button.Location = new System.Drawing.Point(535, 383);
            this.Attack_Button.Name = "Attack_Button";
            this.Attack_Button.Size = new System.Drawing.Size(136, 42);
            this.Attack_Button.TabIndex = 5;
            this.Attack_Button.Text = "Атаковать!";
            this.Attack_Button.UseVisualStyleBackColor = true;
            this.Attack_Button.Visible = false;
            this.Attack_Button.Click += new System.EventHandler(this.Attack_Button_Click);
            // 
            // STOIKA_BUTTON
            // 
            this.STOIKA_BUTTON.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STOIKA_BUTTON.Location = new System.Drawing.Point(535, 431);
            this.STOIKA_BUTTON.Name = "STOIKA_BUTTON";
            this.STOIKA_BUTTON.Size = new System.Drawing.Size(136, 56);
            this.STOIKA_BUTTON.TabIndex = 5;
            this.STOIKA_BUTTON.Text = "Встать в стойку";
            this.STOIKA_BUTTON.UseVisualStyleBackColor = true;
            this.STOIKA_BUTTON.Visible = false;
            this.STOIKA_BUTTON.Click += new System.EventHandler(this.STOIKA_button_Click);
            // 
            // chck_box
            // 
            this.chck_box.AutoSize = true;
            this.chck_box.Location = new System.Drawing.Point(704, 456);
            this.chck_box.Name = "chck_box";
            this.chck_box.Size = new System.Drawing.Size(84, 17);
            this.chck_box.TabIndex = 6;
            this.chck_box.Text = "Читы/коды";
            this.chck_box.UseVisualStyleBackColor = true;
            this.chck_box.Visible = false;
            this.chck_box.CheckedChanged += new System.EventHandler(this.Chck_box_CheckedChanged);
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.Font = new System.Drawing.Font("Candara", 10.25F);
            this.info_label.Location = new System.Drawing.Point(532, 55);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(194, 221);
            this.info_label.TabIndex = 7;
            this.info_label.Text = resources.GetString("info_label.Text");
            // 
            // hodiki
            // 
            this.hodiki.AutoSize = true;
            this.hodiki.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hodiki.Location = new System.Drawing.Point(535, 298);
            this.hodiki.Name = "hodiki";
            this.hodiki.Size = new System.Drawing.Size(107, 25);
            this.hodiki.TabIndex = 8;
            this.hodiki.Text = "<--Ваш ход";
            this.hodiki.Visible = false;
            // 
            // WinScreen
            // 
            this.WinScreen.AutoSize = true;
            this.WinScreen.Font = new System.Drawing.Font("Palatino Linotype", 22F, System.Drawing.FontStyle.Bold);
            this.WinScreen.ForeColor = System.Drawing.Color.LimeGreen;
            this.WinScreen.Location = new System.Drawing.Point(149, 97);
            this.WinScreen.Name = "WinScreen";
            this.WinScreen.Size = new System.Drawing.Size(244, 41);
            this.WinScreen.TabIndex = 9;
            this.WinScreen.Text = "ПОБЕДИТЕЛЬ!";
            this.WinScreen.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 523);
            this.Controls.Add(this.WinScreen);
            this.Controls.Add(this.hodiki);
            this.Controls.Add(this.info_label);
            this.Controls.Add(this.chck_box);
            this.Controls.Add(this.STOIKA_BUTTON);
            this.Controls.Add(this.Attack_Button);
            this.Controls.Add(this.Enemy_Stats);
            this.Controls.Add(this.Hero_Stats);
            this.Controls.Add(this.Enemy_HP);
            this.Controls.Add(this.Player_HP);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Игра";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label Player_HP;
        private System.Windows.Forms.Label Enemy_HP;
        private System.Windows.Forms.TextBox Hero_Stats;
        private System.Windows.Forms.TextBox Enemy_Stats;
        private System.Windows.Forms.Button Attack_Button;
        private System.Windows.Forms.Button STOIKA_BUTTON;
        private System.Windows.Forms.CheckBox chck_box;
        private System.Windows.Forms.Label info_label;
        private System.Windows.Forms.Label hodiki;
        private System.Windows.Forms.Label WinScreen;
    }
}