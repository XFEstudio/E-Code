using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TranslationAPI
{
    public partial class WinTC : Form
    {
        bool MouseIn = false;
        Color LineColor = MainForm.LineColor;
        Color TextColor = MainForm.TextColor;
        Color BGColor = MainForm.BGColor;
        MainForm.ThemeType themeType = MainForm.themeType;
        float UserTextSize = MainForm.WinTCTextSize;
        int SW = MainForm.SW;
        int SH = MainForm.SH;
        int MaxSize = MainForm.MaxSize;
        private const double PrmSW = 2560d;
        private const double PrmSH = 1440d;
        public WinTC()
        {
            InitializeComponent();
        }
        public static Size SetMaxSize()
        {
            return new Size((int)(MainForm.MaxSize / PrmSW * MainForm.SW), 0);
        }
        private void WinTC_Load(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Label label = new Label()
            {
                Name = "label",
                Text = "",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("等线", UserTextSize),
                AutoSize = true,
                ForeColor = TextColor,
            };
            label.MaximumSize = SetMaxSize();
            label.Click += WinTC_Click;
            label.MouseEnter += WinTC_MouseEnter;
            label.MouseLeave += Label_MouseLeave;
            this.Controls.Add(label);
            this.BackColor = BGColor;
            if (themeType == MainForm.ThemeType.Light)
            {
                this.BackColor = Color.FromArgb(236, 230, 220);
                label.ForeColor = Color.Teal;
            }
            else if(themeType == MainForm.ThemeType.Dark)
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                label.ForeColor = Color.FromArgb(236, 230, 220);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!MouseIn)
            {
                if (this.Opacity >= 0.025)
                {
                    this.Opacity -= 0.025;
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }
        private void WinTC_Paint(object sender, PaintEventArgs e)
        {
            if(themeType == MainForm.ThemeType.Light)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Form).ClientRectangle,
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid, //左边
                Color.FromArgb(69, 83, 113), 3, ButtonBorderStyle.Solid, //上边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid, //右边
                Color.FromArgb(69, 83, 113), 3, ButtonBorderStyle.Solid);//底边
            }
            else if(themeType == MainForm.ThemeType.Dark)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Form).ClientRectangle,
                Color.FromArgb(80, 80, 80), 0, ButtonBorderStyle.Solid, //左边
                Color.FromArgb(80, 80, 80), 3, ButtonBorderStyle.Solid, //上边
                Color.FromArgb(80, 80, 80), 0, ButtonBorderStyle.Solid, //右边
                Color.FromArgb(80, 80, 80), 3, ButtonBorderStyle.Solid);//底边
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Form).ClientRectangle,
                LineColor, 0, ButtonBorderStyle.Solid, //左边
                LineColor, 3, ButtonBorderStyle.Solid, //上边
                LineColor, 0, ButtonBorderStyle.Solid, //右边
                LineColor, 3, ButtonBorderStyle.Solid);//底边
            }
        }
        private void WinTC_Click(object sender, EventArgs e)
        {
            Process.Start($"https://fanyi.baidu.com/#en/zh/{this.Name}");
            this.Close();
        }
        private void WinTC_MouseEnter(object sender, EventArgs e)
        {
            MouseIn = true;
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            this.Opacity = 1;
        }
        private void Label_MouseEnter(object sender, EventArgs e)
        {
            MouseIn = true;
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            this.Opacity = 1;
        }

        private void WinTC_MouseLeave(object sender, EventArgs e)
        {
            MouseIn = false;
            if (!timer1.Enabled)
            {
                timer1.Start();
            }
        }
        private void Label_MouseLeave(object sender, EventArgs e)
        {
            MouseIn = false;
            if (!timer1.Enabled)
            {
                timer1.Start();
            }
        }
    }
}
