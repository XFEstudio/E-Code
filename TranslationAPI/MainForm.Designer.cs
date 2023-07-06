namespace TranslationAPI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_Switch = new System.Windows.Forms.Button();
            this.label_Title = new System.Windows.Forms.Label();
            this.btn_Option = new System.Windows.Forms.Button();
            this.btn_Help = new System.Windows.Forms.Button();
            this.btn_Small = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.timerAddShadow = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btn_Switch
            // 
            this.btn_Switch.BackColor = System.Drawing.Color.Teal;
            this.btn_Switch.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Switch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(83)))), ((int)(((byte)(113)))));
            this.btn_Switch.FlatAppearance.BorderSize = 2;
            this.btn_Switch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(95)))));
            this.btn_Switch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(125)))));
            this.btn_Switch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Switch.Font = new System.Drawing.Font("等线", 22F, System.Drawing.FontStyle.Bold);
            this.btn_Switch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.btn_Switch.Location = new System.Drawing.Point(13, 88);
            this.btn_Switch.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Switch.Name = "btn_Switch";
            this.btn_Switch.Size = new System.Drawing.Size(680, 100);
            this.btn_Switch.TabIndex = 0;
            this.btn_Switch.Text = "划词翻译模式";
            this.btn_Switch.UseVisualStyleBackColor = false;
            this.btn_Switch.Click += new System.EventHandler(this.btn_Switch_Click);
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.BackColor = System.Drawing.Color.Transparent;
            this.label_Title.Font = new System.Drawing.Font("等线", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(125)))));
            this.label_Title.Location = new System.Drawing.Point(0, 0);
            this.label_Title.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(486, 84);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "XFE划词翻译";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Title.Paint += new System.Windows.Forms.PaintEventHandler(this.label_Title_Paint);
            // 
            // btn_Option
            // 
            this.btn_Option.BackColor = System.Drawing.Color.Teal;
            this.btn_Option.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Option.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(83)))), ((int)(((byte)(113)))));
            this.btn_Option.FlatAppearance.BorderSize = 2;
            this.btn_Option.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(95)))));
            this.btn_Option.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(125)))));
            this.btn_Option.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Option.Font = new System.Drawing.Font("等线", 22F, System.Drawing.FontStyle.Bold);
            this.btn_Option.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.btn_Option.Location = new System.Drawing.Point(13, 220);
            this.btn_Option.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Option.Name = "btn_Option";
            this.btn_Option.Size = new System.Drawing.Size(680, 100);
            this.btn_Option.TabIndex = 2;
            this.btn_Option.Text = "选项设置";
            this.btn_Option.UseVisualStyleBackColor = false;
            this.btn_Option.Click += new System.EventHandler(this.btn_Option_Click);
            // 
            // btn_Help
            // 
            this.btn_Help.BackColor = System.Drawing.Color.Teal;
            this.btn_Help.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Help.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(83)))), ((int)(((byte)(113)))));
            this.btn_Help.FlatAppearance.BorderSize = 2;
            this.btn_Help.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(95)))));
            this.btn_Help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(125)))));
            this.btn_Help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Help.Font = new System.Drawing.Font("等线", 22F, System.Drawing.FontStyle.Bold);
            this.btn_Help.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.btn_Help.Location = new System.Drawing.Point(13, 349);
            this.btn_Help.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.Size = new System.Drawing.Size(680, 100);
            this.btn_Help.TabIndex = 3;
            this.btn_Help.Text = "说明帮助";
            this.btn_Help.UseVisualStyleBackColor = false;
            this.btn_Help.Click += new System.EventHandler(this.btn_Help_Click);
            // 
            // btn_Small
            // 
            this.btn_Small.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(83)))), ((int)(((byte)(113)))));
            this.btn_Small.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Small.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(103)))));
            this.btn_Small.FlatAppearance.BorderSize = 2;
            this.btn_Small.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(53)))), ((int)(((byte)(83)))));
            this.btn_Small.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(103)))));
            this.btn_Small.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Small.Font = new System.Drawing.Font("等线", 20F, System.Drawing.FontStyle.Bold);
            this.btn_Small.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.btn_Small.Location = new System.Drawing.Point(1000, 0);
            this.btn_Small.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Small.Name = "btn_Small";
            this.btn_Small.Size = new System.Drawing.Size(80, 411);
            this.btn_Small.TabIndex = 4;
            this.btn_Small.Text = "最小化";
            this.btn_Small.UseVisualStyleBackColor = false;
            this.btn_Small.Click += new System.EventHandler(this.btn_Small_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(83)))), ((int)(((byte)(113)))));
            this.btn_Close.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(103)))));
            this.btn_Close.FlatAppearance.BorderSize = 2;
            this.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(53)))), ((int)(((byte)(83)))));
            this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(103)))));
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("等线", 20F, System.Drawing.FontStyle.Bold);
            this.btn_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.btn_Close.Location = new System.Drawing.Point(1000, 411);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(80, 411);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.Text = "关闭";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 1;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "XFE划词翻译";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.ShowHelp = true;
            // 
            // timerAddShadow
            // 
            this.timerAddShadow.Interval = 500;
            this.timerAddShadow.Tick += new System.EventHandler(this.timerAddShadow_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1080, 820);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Small);
            this.Controls.Add(this.btn_Help);
            this.Controls.Add(this.btn_Option);
            this.Controls.Add(this.label_Title);
            this.Controls.Add(this.btn_Switch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XFE翻译工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Switch;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Button btn_Option;
        private System.Windows.Forms.Button btn_Help;
        private System.Windows.Forms.Button btn_Small;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Timer timerAddShadow;
    }
}

