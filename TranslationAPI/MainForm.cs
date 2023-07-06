using System;
using TranslationAPI.Class;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using static TranslationAPI.Method.AllMethod;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics;
using File = System.IO.File;
using TranslationAPI.Properties;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TranslationAPI
{
    public enum CatchWordsMode
    {
        AskGPT,
        Translate,
        Closed
    }
    public partial class MainForm : Form
    {
        #region Private变量
        private static WinTC winTC;
        private static Thread GetClipBD;
        private static Thread Download;
        private static Thread ThreadbackConnect;
        private static Font ButtonTextFont;
        private static Font SmallButtonTextFont;
        private static Button btn_SwitchColor;
        private static Button btn_ChangeSize;
        private static Button btn_BackToMenu;
        private static Button btn_LineColor;
        private static Button btn_TextColor;
        private static Button btn_BGColor;
        private static Button btn_StartUp;
        private static Socket backConnect;
        private static Point mouseLocation;
        private static bool isDragging;
        private static CatchWordsMode CatchWordsMode = CatchWordsMode.Translate;
        private static bool StartUp;
        private static string ClipBoardContent;
        private static string ClipHtmlContent;
        private static string APPID = string.Empty;
        private static string DeCode = string.Empty;
        private static string CommonPath = Application.UserAppDataPath.Substring(0, Application.UserAppDataPath.LastIndexOf(@"\"));
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        private const uint CF_TEXT = 1;
        private const double PrmSW = 2560d;
        private const double PrmSH = 1440d;
        private const double PrmW = 540d;
        private const double PrmH = 410d;
        #endregion
        #region Public变量
        public static Color LineColor;
        public static Color TextColor;
        public static Color BGColor;
        public enum ThemeType
        {
            Light,
            Dark,
            SelfEdit
        }
        public static ThemeType themeType;
        public static int SW;
        public static int SH;
        public static float WinTCTextSize = 13;
        public static int MaxSize = 155;
        #endregion
        public MainForm()
        {
            InitializeComponent();
        }
        //-----------------以下方法----------------
        #region 各类非静态方法
        public void ChangeButtonColor(Color Text, Color Back, Color MDown, Color MOver, Color Line)
        {
            //----------切换开关-----------
            btn_Switch.ForeColor = Text;
            btn_Switch.BackColor = Back;
            btn_Switch.FlatAppearance.MouseDownBackColor = MDown;
            btn_Switch.FlatAppearance.MouseOverBackColor = MOver;
            btn_Switch.FlatAppearance.BorderColor = Line;
            //----------设置选项-----------
            btn_Option.ForeColor = Text;
            btn_Option.BackColor = Back;
            btn_Option.FlatAppearance.MouseDownBackColor = MDown;
            btn_Option.FlatAppearance.MouseOverBackColor = MOver;
            btn_Option.FlatAppearance.BorderColor = Line;
            //----------帮助说明-----------
            btn_Help.ForeColor = Text;
            btn_Help.BackColor = Back;
            btn_Help.FlatAppearance.MouseDownBackColor = MDown;
            btn_Help.FlatAppearance.MouseOverBackColor = MOver;
            btn_Help.FlatAppearance.BorderColor = Line;
        }//改变所有按钮颜色
        public void VersionTC(string Text)
        {
            Panel pl = new Panel()
            {
                Size = new Size((int)(this.Size.Width / 1.5), (int)(this.Size.Height / 1.5)),
                BackColor = Color.FromArgb(240, 230, 220),
                ForeColor = Color.FromArgb(1, 106, 125),
            };
            Label lbltitle = new Label
            {
                Text = "发现新版本",
                ForeColor = Color.FromArgb(1, 106, 125),
                Font = SmallButtonTextFont,
                AutoSize = true,
            };
            lbltitle.Location = new Point((pl.Size.Width - lbltitle.Size.Width) / 2, (pl.Size.Height - lbltitle.Size.Height) / 16);
            Label lbl = new Label
            {
                Text = Text,
                AutoSize = true,
                ForeColor = Color.FromArgb(1, 106, 125),
                Font = SmallButtonTextFont
            };
            lbl.MaximumSize = new Size(pl.Size.Width - (pl.Size.Width - lbl.Size.Width) / 8, 0);
            lbl.Location = new Point((pl.Size.Width - lbl.Size.Width) / 16, (pl.Size.Height - lbl.Size.Height) / 5);
            Button button = new Button()
            {
                Text = "下载",
                BackColor = Color.Teal,
                Size = new Size(pl.Size.Width / 4, pl.Size.Height / 5),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = SmallButtonTextFont,
                ForeColor = Color.FromArgb(240, 230, 220),
                FlatStyle = FlatStyle.Flat
            };
            button.Location = new Point((pl.Size.Width - button.Size.Width) / 6, (pl.Size.Height - button.Size.Height) - (pl.Size.Height - button.Size.Height) / 10);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            button.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            button.FlatAppearance.BorderSize = 2;
            button.Click += btn_VersionTC_Click;
            Button button_Cancel = new Button()
            {
                Text = "取消",
                BackColor = Color.Teal,
                Size = new Size(pl.Size.Width / 4, pl.Size.Height / 5),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = SmallButtonTextFont,
                ForeColor = Color.FromArgb(240, 230, 220),
                FlatStyle = FlatStyle.Flat
            };
            button_Cancel.Location = new Point((pl.Size.Width - button_Cancel.Size.Width) - (pl.Size.Width - button_Cancel.Size.Width) / 6, (pl.Size.Height - button_Cancel.Size.Height) - (pl.Size.Height - button_Cancel.Size.Height) / 10);
            button_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            button_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            button_Cancel.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            button_Cancel.FlatAppearance.BorderSize = 2;
            button_Cancel.Click += btn_VersionTC_Cancel_Click;
            pl.Paint += VersionTC_Paint;
            this.Invoke(new Action(() =>
            {
                pl.Location = new Point((this.Size.Width - pl.Size.Width) / 2, (this.Size.Height - pl.Size.Height) / 2);
                this.Controls.Add(pl);
                pl.BringToFront();
                pl.Controls.Add(lbltitle);
                pl.Controls.Add(button);
                pl.Controls.Add(button_Cancel);
                pl.Controls.Add(lbl);
            }));
        }//更新版本时的弹窗方法
        public void NormalTC(string Title, string Text)
        {
            Panel pl = new Panel()
            {
                Size = new Size((int)(this.Size.Width / 1.5), (int)(this.Size.Height / 1.5)),
                BackColor = Color.FromArgb(240, 230, 220),
                ForeColor = Color.FromArgb(1, 106, 125),
            };
            Label lbltitle = new Label
            {
                Text = Title,
                ForeColor = Color.FromArgb(1, 106, 125),
                Font = SmallButtonTextFont,
                AutoSize = true,
            };
            lbltitle.Location = new Point((pl.Size.Width - lbltitle.Size.Width) / 2, (pl.Size.Height - lbltitle.Size.Height) / 16);
            Label lbl = new Label
            {
                Text = Text,
                AutoSize = true,
                ForeColor = Color.FromArgb(1, 106, 125),
                Font = SmallButtonTextFont
            };
            lbl.MaximumSize = new Size(pl.Size.Width - (pl.Size.Width - lbl.Size.Width) / 8, 0);
            lbl.Location = new Point((pl.Size.Width - lbl.Size.Width) / 16, (pl.Size.Height - lbl.Size.Height) / 5);
            Button button_Cancel = new Button()
            {
                Text = "确认",
                BackColor = Color.Teal,
                Size = new Size(pl.Size.Width / 4, pl.Size.Height / 5),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = SmallButtonTextFont,
                ForeColor = Color.FromArgb(240, 230, 220),
                FlatStyle = FlatStyle.Flat
            };
            button_Cancel.Location = new Point((pl.Size.Width - button_Cancel.Size.Width) / 2, (pl.Size.Height - button_Cancel.Size.Height) - (pl.Size.Height - button_Cancel.Size.Height) / 10);
            button_Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            button_Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            button_Cancel.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            button_Cancel.FlatAppearance.BorderSize = 2;
            button_Cancel.Click += btn_VersionTC_Cancel_Click;
            pl.Paint += VersionTC_Paint;
            this.Invoke(new Action(() =>
            {
                pl.Location = new Point((this.Size.Width - pl.Size.Width) / 2, (this.Size.Height - pl.Size.Height) / 2);
                this.Controls.Add(pl);
                pl.BringToFront();
                pl.Controls.Add(lbltitle);
                pl.Controls.Add(button_Cancel);
                pl.Controls.Add(lbl);
            }));
        }//普通的弹窗方法
        public void SendBugTC(string Title, string Text, string BugContent)
        {
            Panel pl = new Panel()
            {
                Size = new Size((int)(this.Size.Width / 1.5), (int)(this.Size.Height / 1.5)),
                BackColor = Color.FromArgb(240, 230, 220),
                ForeColor = Color.FromArgb(1, 106, 125),
            };
            Label lbltitle = new Label
            {
                Text = Title,
                ForeColor = Color.FromArgb(1, 106, 125),
                Font = SmallButtonTextFont,
                AutoSize = true,
            };
            lbltitle.Location = new Point((pl.Size.Width - lbltitle.Size.Width) / 2, (pl.Size.Height - lbltitle.Size.Height) / 16);
            Label lbl = new Label
            {
                Text = Text,
                AutoSize = true,
                ForeColor = Color.FromArgb(1, 106, 125),
                Font = SmallButtonTextFont
            };
            lbl.MaximumSize = new Size(pl.Size.Width - (pl.Size.Width - lbl.Size.Width) / 8, 0);
            lbl.Location = new Point((pl.Size.Width - lbl.Size.Width) / 16, (pl.Size.Height - lbl.Size.Height) / 5);
            Button button_SendDebugEmail = new Button()
            {
                Text = "发送错误报告",
                BackColor = Color.Teal,
                Size = new Size((int)(pl.Size.Width / 1.2), pl.Size.Height / 5),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = SmallButtonTextFont,
                ForeColor = Color.FromArgb(240, 230, 220),
                FlatStyle = FlatStyle.Flat,
                Tag = new TCTransmission(Title, BugContent)
            };
            button_SendDebugEmail.Location = new Point((pl.Size.Width - button_SendDebugEmail.Size.Width) / 2, (pl.Size.Height - button_SendDebugEmail.Size.Height) - (pl.Size.Height - button_SendDebugEmail.Size.Height) / 10);
            button_SendDebugEmail.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            button_SendDebugEmail.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            button_SendDebugEmail.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            button_SendDebugEmail.FlatAppearance.BorderSize = 2;
            button_SendDebugEmail.Click += btn_SendBugTC_Send_Click;
            pl.Paint += VersionTC_Paint;
            this.Invoke(new Action(() =>
            {
                pl.Location = new Point((this.Size.Width - pl.Size.Width) / 2, (this.Size.Height - pl.Size.Height) / 2);
                this.Controls.Add(pl);
                pl.BringToFront();
                pl.Controls.Add(lbltitle);
                pl.Controls.Add(button_SendDebugEmail);
                pl.Controls.Add(lbl);
            }));
        }
        private void btn_VersionTC_Click(object sender, EventArgs e)
        {
            WebClientPro webClient = new WebClientPro(60000);
            webClient.DownloadFileCompleted += UpdateByCMD;
            webClient.DownloadFileAsync(new Uri("https://www.xfegzs.com/XFETranslate/TRapp"), Application.StartupPath + "\\XFETranslate_bin.exe");
            this.Controls.Remove((sender as Button).Parent);
        }//版本更新弹窗按钮的确认事件
        private void btn_VersionTC_Cancel_Click(object sender, EventArgs e)
        {
            this.Controls.Remove((sender as Button).Parent);
        }//版本更新弹窗的按钮取消事件
        private void btn_SendBugTC_Send_Click(object sender, EventArgs e)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            string AddressList = string.Empty;
            foreach (IPAddress ipAddress in hostEntry.AddressList)
            {
                AddressList += ipAddress.ToString() + "\n";
            }
            var Transmission = (sender as Button).Tag as TCTransmission;
            EmailDebug($"XFE划词翻译:来自用户的错误报告 {Transmission.Title}", $"用户IP地址：{AddressList}\n详细错误信息：{Transmission.Text}");
            this.Controls.Remove((sender as Button).Parent);
        }//发送错误报告弹窗按钮的事件
        private static void UpdateByCMD(object sender, AsyncCompletedEventArgs e)
        {
            WriteIn(CommonPath + @"\ReWrite", "ReWrite");
            ProcessStartInfo psi;
            if (StartUp)
            {
                psi = new ProcessStartInfo("cmd.exe", $"/C Echo on & ping /n 2 127.0.0.1>nul & Del {Application.ExecutablePath} & Rename {Application.StartupPath}\\XFETranslate_bin.exe XFETranslate.exe & ping /n 2 127.0.0.1>nul & powershell -Command \"Start-Process 'XFETranslate.exe' -Verb runAs\"");
            }
            else
            {
                psi = new ProcessStartInfo("cmd.exe", $"/C Echo on & ping /n 2 127.0.0.1>nul & Del {Application.ExecutablePath} & Rename {Application.StartupPath}\\XFETranslate_bin.exe XFETranslate.exe & ping /n 2 127.0.0.1>nul & XFETranslate.exe");
            }
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process.Start(psi);
            Application.Exit();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GetClipBD.Abort();
            ThreadbackConnect.Abort();
            Download.Abort();
            if (!timer2.Enabled)
            {
                timer2.Start();
            }
            WriteIn(CommonPath + @"\UserSelfEditData\Color", $"{BringColorToString(LineColor)},|{BringColorToString(TextColor)}|{BringColorToString(BGColor)}|{OutPutThemeColor(themeType)}|{WinTCTextSize}|{CatchWordsMode}");
            backConnect.Close();
            Application.Exit();
        }
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Form)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Form).ClientRectangle,
                Color.FromArgb(69, 83, 113), 16, ButtonBorderStyle.Solid, //左边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid, //上边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid, //右边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid);//底边
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Panel).ClientRectangle,
                Color.FromArgb(69, 83, 113), 16, ButtonBorderStyle.Solid, //左边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid, //上边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid, //右边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid);//底边
            }
        }
        private void VersionTC_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, (sender as Panel).ClientRectangle,
            Color.FromArgb(69, 83, 113), 2, ButtonBorderStyle.Solid, //左边
            Color.FromArgb(69, 83, 113), 2, ButtonBorderStyle.Solid, //上边
            Color.FromArgb(69, 83, 113), 2, ButtonBorderStyle.Solid, //右边
            Color.FromArgb(69, 83, 113), 2, ButtonBorderStyle.Solid);//底边
        }
        private void label_Title_Paint(object sender, PaintEventArgs e)
        {
            if (themeType == ThemeType.Light || themeType == ThemeType.SelfEdit)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Label).ClientRectangle,
                Color.FromArgb(69, 83, 113), 3, ButtonBorderStyle.Solid, //左边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid, //上边
                Color.FromArgb(69, 83, 113), 3, ButtonBorderStyle.Solid, //右边
                Color.FromArgb(69, 83, 113), 0, ButtonBorderStyle.Solid);//底边
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Label).ClientRectangle,
                Color.FromArgb(236, 230, 220), 3, ButtonBorderStyle.Solid, //左边
                Color.FromArgb(236, 230, 220), 0, ButtonBorderStyle.Solid, //上边
                Color.FromArgb(236, 230, 220), 3, ButtonBorderStyle.Solid, //右边
                Color.FromArgb(236, 230, 220), 0, ButtonBorderStyle.Solid);//底边
            }
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (!timer2.Enabled)
            {
                timer2.Start();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 0.025)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                timer2.Stop();
                this.Close();
            }
        }
        private void btn_Small_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.Visible = true;
        }
        private void btn_Switch_Click(object sender, EventArgs e)
        {
            switch (CatchWordsMode)
            {
                case CatchWordsMode.AskGPT:
                    if (themeType == ThemeType.Light || themeType == ThemeType.SelfEdit)
                    {
                        btn_Switch.Text = "功能已关闭";
                        btn_Switch.BackColor = Color.FromArgb(205, 77, 77);
                        btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(154, 16, 35);
                        btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 36, 55);
                        btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                    }
                    else
                    {
                        btn_Switch.Text = "功能已关闭";
                        btn_Switch.BackColor = Color.FromArgb(40, 40, 40);
                        btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                        btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 59, 73, 103);
                        btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                    }
                    CatchWordsMode = CatchWordsMode.Closed;
                    break;

                case CatchWordsMode.Closed:
                    if (themeType == ThemeType.Light || themeType == ThemeType.SelfEdit)
                    {
                        btn_Switch.Text = "划词翻译模式";
                        btn_Switch.BackColor = Color.Teal;
                        btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                        btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                        btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                    }
                    else
                    {
                        btn_Switch.Text = "划词翻译模式";
                        btn_Switch.BackColor = Color.FromArgb(69, 83, 113);
                        btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                        btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                        btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                    }
                    CatchWordsMode = CatchWordsMode.Translate;
                    break;

                case CatchWordsMode.Translate:
                    if (themeType == ThemeType.Light || themeType == ThemeType.SelfEdit)
                    {
                        btn_Switch.Text = "代码解析模式";
                        btn_Switch.BackColor = Color.Teal;
                        btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                        btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                        btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                    }
                    else
                    {
                        btn_Switch.Text = "代码解析模式";
                        btn_Switch.BackColor = Color.FromArgb(69, 83, 113);
                        btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                        btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                        btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                    }
                    CatchWordsMode = CatchWordsMode.AskGPT;
                    break;
            }
        }
        private void btn_SwitchColor_Click(object sender, EventArgs e)
        {
            //-------------普通控件重新着色--------------
            if (themeType == ThemeType.Light)
            {
                ChangeButtonColor(Color.FromArgb(236, 230, 220), Color.FromArgb(69, 83, 113), Color.FromArgb(39, 53, 83), Color.FromArgb(59, 73, 103), Color.FromArgb(59, 73, 103));
                ((sender as Button).Parent as Panel).BackColor = Color.FromArgb(30, 30, 30);
                //-----------------颜色按钮-----------------
                (sender as Button).Text = "深色主题";
                (sender as Button).ForeColor = Color.FromArgb(236, 230, 220);
                (sender as Button).BackColor = Color.FromArgb(69, 83, 113);
                (sender as Button).FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                (sender as Button).FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                (sender as Button).FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                //-----------------字体按钮-----------------
                btn_ChangeSize.ForeColor = Color.FromArgb(236, 230, 220);
                btn_ChangeSize.BackColor = Color.FromArgb(69, 83, 113);
                btn_ChangeSize.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                btn_ChangeSize.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                btn_ChangeSize.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                //-----------------返回按钮-----------------
                btn_BackToMenu.ForeColor = Color.FromArgb(236, 230, 220);
                btn_BackToMenu.BackColor = Color.FromArgb(69, 83, 113);
                btn_BackToMenu.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                btn_BackToMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                btn_BackToMenu.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                //-----------------自启按钮-----------------
                btn_StartUp.ForeColor = Color.FromArgb(236, 230, 220);
                btn_StartUp.BackColor = Color.FromArgb(69, 83, 113);
                btn_StartUp.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                btn_StartUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                btn_StartUp.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                //----------------更新主界面-----------------
                this.BackColor = Color.FromArgb(30, 30, 30);
                label_Title.ForeColor = Color.FromArgb(236, 230, 220);
                //-----------------设置变量-----------------
                themeType = ThemeType.Dark;
            }
            else if (themeType == ThemeType.Dark)
            {
                ChangeButtonColor(Color.FromArgb(236, 230, 220), Color.Teal, Color.FromArgb(0, 70, 95), Color.FromArgb(1, 106, 125), Color.FromArgb(69, 83, 113));
                ((sender as Button).Parent as Panel).BackColor = Color.FromArgb(236, 230, 220);
                //-----------------颜色按钮-----------------
                (sender as Button).Text = "自定义";
                (sender as Button).ForeColor = TextColor;
                (sender as Button).BackColor = BGColor;
                (sender as Button).FlatAppearance.MouseDownBackColor = Color.FromArgb(50, BGColor.R - BGColor.R / 3, BGColor.G - BGColor.G / 3, BGColor.B - BGColor.B / 3);
                (sender as Button).FlatAppearance.MouseOverBackColor = Color.FromArgb(20, BGColor.R - BGColor.R / 2, BGColor.G - BGColor.G / 2, BGColor.B - BGColor.B / 2);
                (sender as Button).FlatAppearance.BorderColor = LineColor;
                //-----------------字体按钮-----------------
                btn_ChangeSize.ForeColor = Color.FromArgb(236, 230, 220);
                btn_ChangeSize.BackColor = Color.Teal;
                btn_ChangeSize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                btn_ChangeSize.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                btn_ChangeSize.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                //-----------------返回按钮-----------------
                btn_BackToMenu.ForeColor = Color.FromArgb(236, 230, 220);
                btn_BackToMenu.BackColor = Color.Teal;
                btn_BackToMenu.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                btn_BackToMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                btn_BackToMenu.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                //-----------------返回按钮-----------------
                btn_StartUp.ForeColor = Color.FromArgb(236, 230, 220);
                btn_StartUp.BackColor = Color.Teal;
                btn_StartUp.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                btn_StartUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                btn_StartUp.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                //--------------线条颜色按钮---------------
                btn_LineColor = new Button()
                {
                    Text = "线条颜色",
                    ForeColor = Color.FromArgb(255 - LineColor.R, 255 - LineColor.G, 255 - LineColor.B),
                    BackColor = LineColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = SmallButtonTextFont,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size((int)(140 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                    Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(140 / PrmSW * SW)) / 5, (int)((((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) / 4.1))
                };
                btn_LineColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, LineColor.R - LineColor.R / 3, LineColor.G - LineColor.G / 3, LineColor.B - LineColor.B / 3);
                btn_LineColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, LineColor.R - LineColor.R / 2, LineColor.G - LineColor.G / 2, LineColor.B - LineColor.B / 2);
                btn_LineColor.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                btn_LineColor.FlatAppearance.BorderSize = 2;
                btn_LineColor.Click += btn_ChangeSelfColor_Click;
                ((sender as Button).Parent as Panel).Controls.Add(btn_LineColor);
                //--------------文字颜色按钮---------------
                btn_TextColor = new Button()
                {
                    Text = "文字颜色",
                    ForeColor = Color.FromArgb(255 - TextColor.R, 255 - TextColor.G, 255 - TextColor.B),
                    BackColor = TextColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = SmallButtonTextFont,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size((int)(140 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                    Location = new Point(((sender as Button).Parent as Panel).Size.Width - (int)(140 / PrmSW * SW) - (((sender as Button).Parent as Panel).Size.Width - (int)(140 / PrmSW * SW)) / 5, (int)((((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) / 4.1))
                };
                btn_TextColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, TextColor.R - TextColor.R / 3, TextColor.G - TextColor.G / 3, TextColor.B - TextColor.B / 3);
                btn_TextColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, TextColor.R - TextColor.R / 2, TextColor.G - TextColor.G / 2, TextColor.B - TextColor.B / 2);
                btn_TextColor.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                btn_TextColor.FlatAppearance.BorderSize = 2;
                btn_TextColor.Click += btn_ChangeSelfColor_Click;
                ((sender as Button).Parent as Panel).Controls.Add(btn_TextColor);
                //--------------背景颜色按钮---------------
                btn_BGColor = new Button()
                {
                    Text = "背景颜色",
                    ForeColor = Color.FromArgb(255 - BGColor.R, 255 - BGColor.G, 255 - BGColor.B),
                    BackColor = BGColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = SmallButtonTextFont,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size((int)(180 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                    Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(180 / PrmSW * SW)) / 2, (int)((((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) / 2.4))
                };
                btn_BGColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, BGColor.R - BGColor.R / 3, BGColor.G - BGColor.G / 3, BGColor.B - BGColor.B / 3);
                btn_BGColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, BGColor.R - BGColor.R / 2, BGColor.G - BGColor.G / 2, BGColor.B - BGColor.B / 2);
                btn_BGColor.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                btn_BGColor.FlatAppearance.BorderSize = 2;
                btn_BGColor.Click += btn_ChangeSelfColor_Click;
                ((sender as Button).Parent as Panel).Controls.Add(btn_BGColor);
                //----------------更新主界面-----------------
                this.BackColor = Color.FromArgb(236, 230, 220);
                label_Title.ForeColor = Color.FromArgb(1, 106, 125);
                (sender as Button).Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(340 / PrmSW * SW)) / 2, (((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) / 16);
                btn_BackToMenu.Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(340 / PrmSW * SW)) / 2, (((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) - (((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) / 16);
                btn_ChangeSize.Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(340 / PrmSW * SW)) / 2, (((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) - (int)((((sender as Button).Parent as Panel).Size.Height - (50 / PrmSW * SW)) / 2.4));
                btn_StartUp.Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(340 / PrmSW * SW)) / 2, (((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) - (int)((((sender as Button).Parent as Panel).Size.Height - (50 / PrmSW * SW)) / 4.2));
                //-----------------设置变量-----------------
                themeType = ThemeType.SelfEdit;
            }
            else
            {
                ChangeButtonColor(Color.FromArgb(236, 230, 220), Color.Teal, Color.FromArgb(0, 70, 95), Color.FromArgb(1, 106, 125), Color.FromArgb(69, 83, 113));
                ((sender as Button).Parent as Panel).BackColor = Color.FromArgb(236, 230, 220);
                //-----------------颜色按钮-----------------
                (sender as Button).Text = "浅色主题";
                (sender as Button).ForeColor = Color.FromArgb(236, 230, 220);
                (sender as Button).BackColor = Color.Teal;
                (sender as Button).FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                (sender as Button).FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                (sender as Button).FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                //-----------------字体按钮-----------------
                btn_ChangeSize.ForeColor = Color.FromArgb(236, 230, 220);
                btn_ChangeSize.BackColor = Color.Teal;
                btn_ChangeSize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                btn_ChangeSize.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                btn_ChangeSize.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                //-----------------返回按钮-----------------
                btn_BackToMenu.ForeColor = Color.FromArgb(236, 230, 220);
                btn_BackToMenu.BackColor = Color.Teal;
                btn_BackToMenu.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                btn_BackToMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                btn_BackToMenu.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                //-----------------返回按钮-----------------
                btn_StartUp.ForeColor = Color.FromArgb(236, 230, 220);
                btn_StartUp.BackColor = Color.Teal;
                btn_StartUp.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                btn_StartUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                btn_StartUp.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                //----------------更新主界面-----------------
                this.BackColor = Color.FromArgb(236, 230, 220);
                label_Title.ForeColor = Color.FromArgb(1, 106, 125);
                (sender as Button).Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(340 / PrmSW * SW)) / 2, (((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) / 7);
                btn_BackToMenu.Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(340 / PrmSW * SW)) / 2, (((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) - (((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW)) / 7);
                btn_ChangeSize.Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(340 / PrmSW * SW)) / 2, (int)((((sender as Button).Parent as Panel).Size.Height - (50 / PrmSW * SW)) / 2.6));
                btn_StartUp.Location = new Point((((sender as Button).Parent as Panel).Size.Width - (int)(340 / PrmSW * SW)) / 2, ((sender as Button).Parent as Panel).Size.Height - (int)(50 / PrmSW * SW) - (int)((((sender as Button).Parent as Panel).Size.Height - (50 / PrmSW * SW)) / 2.6));
                ((sender as Button).Parent as Panel).Controls.Remove(btn_LineColor);
                ((sender as Button).Parent as Panel).Controls.Remove(btn_TextColor);
                ((sender as Button).Parent as Panel).Controls.Remove(btn_BGColor);
                //-----------------设置变量-----------------
                themeType = ThemeType.Light;
            }
            //------------切换开关特殊重着色-------------
            if (CatchWordsMode == CatchWordsMode.Closed)
            {
                if (themeType == ThemeType.Light || themeType == ThemeType.SelfEdit)
                {
                    btn_Switch.BackColor = Color.FromArgb(205, 77, 77);
                    btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(154, 16, 35);
                    btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 36, 55);
                    btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                }
                else
                {
                    btn_Switch.BackColor = Color.FromArgb(40, 40, 40);
                    btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                    btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 59, 73, 103);
                    btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                }
            }
            else
            {
                if (themeType == ThemeType.Light || themeType == ThemeType.SelfEdit)
                {
                    btn_Switch.BackColor = Color.Teal;
                    btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
                    btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
                    btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                }
                else
                {
                    btn_Switch.BackColor = Color.FromArgb(69, 83, 113);
                    btn_Switch.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                    btn_Switch.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                    btn_Switch.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                }
            }
        }
        private void colorDialog1_HelpRequest(object sender, EventArgs e)
        {
            MessageBox.Show($"选择自定义你的{(sender as ColorDialog).Tag}颜色");
        }
        private void btn_BackToMenu_Click(object sender, EventArgs e)
        {
            WriteIn(CommonPath + @"\UserSelfEditData\Color", $"{BringColorToString(LineColor)},|{BringColorToString(TextColor)}|{BringColorToString(BGColor)}|{OutPutThemeColor(themeType)}|{WinTCTextSize}");
            ((sender as Button).Parent as Panel).Dispose();
        }
        private void btn_BackToMenuHelp_Click(object sender, EventArgs e)
        {
            ((sender as Button).Parent as Panel).Dispose();
        }
        private void btn_ChangeSelfColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Tag = (sender as Button).Text;
            colorDialog1.HelpRequest += colorDialog1_HelpRequest;
            if ((sender as Button).Text == "线条颜色")
            {
                colorDialog1.Color = LineColor;
                colorDialog1.ShowDialog();
                LineColor = colorDialog1.Color;
                (sender as Button).ForeColor = Color.FromArgb(255 - LineColor.R, 255 - LineColor.G, 255 - LineColor.B);
                (sender as Button).BackColor = LineColor;
                (sender as Button).FlatAppearance.MouseDownBackColor = Color.FromArgb(180, LineColor.R - LineColor.R / 3, LineColor.G - LineColor.G / 3, LineColor.B - LineColor.B / 3);
                (sender as Button).FlatAppearance.MouseOverBackColor = Color.FromArgb(100, LineColor.R - LineColor.R / 2, LineColor.G - LineColor.G / 2, LineColor.B - LineColor.B / 2);
                btn_SwitchColor.FlatAppearance.BorderColor = LineColor;
            }
            else if ((sender as Button).Text == "文字颜色")
            {
                colorDialog1.Color = TextColor;
                colorDialog1.ShowDialog();
                TextColor = colorDialog1.Color;
                (sender as Button).ForeColor = Color.FromArgb(255 - TextColor.R, 255 - TextColor.G, 255 - TextColor.B);
                (sender as Button).BackColor = TextColor;
                (sender as Button).FlatAppearance.MouseDownBackColor = Color.FromArgb(180, TextColor.R - TextColor.R / 3, TextColor.G - TextColor.G / 3, TextColor.B - TextColor.B / 3);
                (sender as Button).FlatAppearance.MouseOverBackColor = Color.FromArgb(100, TextColor.R - TextColor.R / 2, TextColor.G - TextColor.G / 2, TextColor.B - TextColor.B / 2);
                btn_SwitchColor.ForeColor = TextColor;
            }
            else
            {
                colorDialog1.Color = BGColor;
                colorDialog1.ShowDialog();
                BGColor = colorDialog1.Color;
                (sender as Button).ForeColor = Color.FromArgb(255 - BGColor.R, 255 - BGColor.G, 255 - BGColor.B);
                (sender as Button).BackColor = BGColor;
                (sender as Button).FlatAppearance.MouseDownBackColor = Color.FromArgb(180, BGColor.R - BGColor.R / 3, BGColor.G - BGColor.G / 3, BGColor.B - BGColor.B / 3);
                (sender as Button).FlatAppearance.MouseOverBackColor = Color.FromArgb(100, BGColor.R - BGColor.R / 2, BGColor.G - BGColor.G / 2, BGColor.B - BGColor.B / 2);
                btn_SwitchColor.BackColor = BGColor;
            }
        }
        private void btn_ChangeSize_Click(object sender, EventArgs e)
        {
            if (WinTCTextSize == 7)
            {
                WinTCTextSize = 11;
                btn_ChangeSize.Text = "字体大小：小";
            }
            else if (WinTCTextSize == 11)
            {
                WinTCTextSize = 13;
                btn_ChangeSize.Text = "字体大小：中";
            }
            else if (WinTCTextSize == 13)
            {
                WinTCTextSize = 15;
                btn_ChangeSize.Text = "字体大小：大";
            }
            else if (WinTCTextSize == 15)
            {
                WinTCTextSize = 19;
                btn_ChangeSize.Text = "字体大小：超大";
            }
            else if (WinTCTextSize == 19)
            {
                WinTCTextSize = 7;
                btn_ChangeSize.Text = "字体大小：超小";
            }
        }
        private void btn_StartUp_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Application.ExecutablePath;
            psi.Verb = "runas";
            try
            {
                WriteIn(CommonPath + @"\GetAdmin", "Getting...");
                Process.Start(psi);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.Activate();
                notifyIcon1.Visible = false;
            }
        }
        private void btn_Option_Click(object sender, EventArgs e)
        {
            //--------------Panel控件---------------
            #region Panel控件(panel)
            Panel panel = new Panel()
            {
                Width = this.Size.Width - btn_Close.Width,
                Height = this.Size.Height,
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(236, 230, 220),
            };
            panel.Paint += MainForm_Paint;
            this.Controls.Add(panel);
            panel.BringToFront();
            #endregion
            //--------------主题按钮---------------
            #region 主题按钮(btn_SwitchColor)
            btn_SwitchColor = new Button()
            {
                Text = "浅色主题",
                ForeColor = Color.FromArgb(236, 230, 220),
                BackColor = Color.Teal,
                FlatStyle = FlatStyle.Flat,
                Font = ButtonTextFont,
                Size = new Size((int)(340 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, (panel.Size.Height - (int)(50 / PrmSW * SW)) / 7)
            };
            btn_SwitchColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            btn_SwitchColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            btn_SwitchColor.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            btn_SwitchColor.FlatAppearance.BorderSize = 2;
            btn_SwitchColor.Click += btn_SwitchColor_Click;
            panel.Controls.Add(btn_SwitchColor);
            #endregion
            //--------------返回按钮---------------
            #region 返回按钮(btn_BackToMenu)
            btn_BackToMenu = new Button()
            {
                Text = "应用并返回",
                ForeColor = Color.FromArgb(236, 230, 220),
                BackColor = Color.Teal,
                FlatStyle = FlatStyle.Flat,
                Font = ButtonTextFont,
                Size = new Size((int)(340 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, (panel.Size.Height - (int)(50 / PrmSW * SW)) - (panel.Size.Height - (int)(50 / PrmSW * SW)) / 7)
            };
            btn_BackToMenu.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            btn_BackToMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            btn_BackToMenu.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            btn_BackToMenu.FlatAppearance.BorderSize = 2;
            btn_BackToMenu.Click += btn_BackToMenu_Click;
            panel.Controls.Add(btn_BackToMenu);
            #endregion
            //--------------字体按钮---------------
            #region 字体按钮(btn_ChangeSize)
            btn_ChangeSize = new Button()
            {
                Text = "字体大小：中",
                ForeColor = Color.FromArgb(236, 230, 220),
                BackColor = Color.Teal,
                FlatStyle = FlatStyle.Flat,
                Font = ButtonTextFont,
                Size = new Size((int)(340 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, (int)((panel.Size.Height - (50 / PrmSW * SW)) / 2.6))
            };
            btn_ChangeSize.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            btn_ChangeSize.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            btn_ChangeSize.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            btn_ChangeSize.FlatAppearance.BorderSize = 2;
            btn_ChangeSize.Click += btn_ChangeSize_Click;
            panel.Controls.Add(btn_ChangeSize);
            #endregion
            //--------------自启按钮---------------
            #region 自启按钮(btn_StartUp)
            btn_StartUp = new Button()
            {
                Text = "开机自启已关闭",
                ForeColor = Color.FromArgb(236, 230, 220),
                BackColor = Color.Teal,
                FlatStyle = FlatStyle.Flat,
                Font = ButtonTextFont,
                Size = new Size((int)(340 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, panel.Size.Height - (int)(50 / PrmSW * SW) - (int)((panel.Size.Height - (50 / PrmSW * SW)) / 2.6))
            };
            btn_StartUp.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            btn_StartUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            btn_StartUp.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            btn_StartUp.FlatAppearance.BorderSize = 2;
            btn_StartUp.Click += btn_StartUp_Click;
            panel.Controls.Add(btn_StartUp);
            #endregion
            //--------------各类检测---------------
            #region 检测主题
            //--------------检测主题---------------
            if (themeType == ThemeType.Dark)
            {
                panel.BackColor = Color.FromArgb(30, 30, 30);
                //--------------主题按钮---------------
                btn_SwitchColor.Text = "深色主题";
                btn_SwitchColor.ForeColor = Color.FromArgb(236, 230, 220);
                btn_SwitchColor.BackColor = Color.FromArgb(69, 83, 113);
                btn_SwitchColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                btn_SwitchColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                btn_SwitchColor.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                //--------------返回按钮---------------
                btn_BackToMenu.ForeColor = Color.FromArgb(236, 230, 220);
                btn_BackToMenu.BackColor = Color.FromArgb(69, 83, 113);
                btn_BackToMenu.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                btn_BackToMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                btn_BackToMenu.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                //--------------字体按钮---------------
                btn_ChangeSize.ForeColor = Color.FromArgb(236, 230, 220);
                btn_ChangeSize.BackColor = Color.FromArgb(69, 83, 113);
                btn_ChangeSize.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                btn_ChangeSize.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                btn_ChangeSize.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
                //--------------自启按钮---------------
                btn_StartUp.ForeColor = Color.FromArgb(236, 230, 220);
                btn_StartUp.BackColor = Color.FromArgb(69, 83, 113);
                btn_StartUp.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                btn_StartUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                btn_StartUp.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
            }
            else if (themeType == ThemeType.SelfEdit)
            {
                btn_SwitchColor.Text = "自定义";
                btn_SwitchColor.ForeColor = TextColor;
                btn_SwitchColor.BackColor = BGColor;
                btn_SwitchColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, BGColor.R - BGColor.R / 3, BGColor.G - BGColor.G / 3, BGColor.B - BGColor.B / 3);
                btn_SwitchColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, BGColor.R - BGColor.R / 2, BGColor.G - BGColor.G / 2, BGColor.B - BGColor.B / 2);
                btn_SwitchColor.FlatAppearance.BorderColor = LineColor;
                //--------------线条颜色按钮---------------
                btn_LineColor = new Button()
                {
                    Text = "线条颜色",
                    ForeColor = Color.FromArgb(255 - LineColor.R, 255 - LineColor.G, 255 - LineColor.B),
                    BackColor = LineColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = SmallButtonTextFont,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size((int)(140 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                    Location = new Point((panel.Size.Width - (int)(140 / PrmSW * SW)) / 5, (int)((panel.Size.Height - (int)(50 / PrmSW * SW)) / 4.1))
                };
                btn_LineColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, LineColor.R - LineColor.R / 3, LineColor.G - LineColor.G / 3, LineColor.B - LineColor.B / 3);
                btn_LineColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, LineColor.R - LineColor.R / 2, LineColor.G - LineColor.G / 2, LineColor.B - LineColor.B / 2);
                btn_LineColor.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                btn_LineColor.FlatAppearance.BorderSize = 2;
                btn_LineColor.Click += btn_ChangeSelfColor_Click;
                panel.Controls.Add(btn_LineColor);
                //--------------文字颜色按钮---------------
                btn_TextColor = new Button()
                {
                    Text = "文字颜色",
                    ForeColor = Color.FromArgb(255 - TextColor.R, 255 - TextColor.G, 255 - TextColor.B),
                    BackColor = TextColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = SmallButtonTextFont,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size((int)(140 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                    Location = new Point(panel.Size.Width - (int)(140 / PrmSW * SW) - (panel.Size.Width - (int)(140 / PrmSW * SW)) / 5, (int)((panel.Size.Height - (int)(50 / PrmSW * SW)) / 4.1))
                };
                btn_TextColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, TextColor.R - TextColor.R / 3, TextColor.G - TextColor.G / 3, TextColor.B - TextColor.B / 3);
                btn_TextColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, TextColor.R - TextColor.R / 2, TextColor.G - TextColor.G / 2, TextColor.B - TextColor.B / 2);
                btn_TextColor.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                btn_TextColor.FlatAppearance.BorderSize = 2;
                btn_TextColor.Click += btn_ChangeSelfColor_Click;
                panel.Controls.Add(btn_TextColor);
                //--------------背景颜色按钮---------------
                btn_BGColor = new Button()
                {
                    Text = "背景颜色",
                    ForeColor = Color.FromArgb(255 - BGColor.R, 255 - BGColor.G, 255 - BGColor.B),
                    BackColor = BGColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = SmallButtonTextFont,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size((int)(180 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                    Location = new Point((panel.Size.Width - (int)(180 / PrmSW * SW)) / 2, (int)((panel.Size.Height - (int)(50 / PrmSW * SW)) / 2.4))
                };
                btn_BGColor.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, BGColor.R - BGColor.R / 3, BGColor.G - BGColor.G / 3, BGColor.B - BGColor.B / 3);
                btn_BGColor.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, BGColor.R - BGColor.R / 2, BGColor.G - BGColor.G / 2, BGColor.B - BGColor.B / 2);
                btn_BGColor.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
                btn_BGColor.FlatAppearance.BorderSize = 2;
                btn_BGColor.Click += btn_ChangeSelfColor_Click;
                panel.Controls.Add(btn_BGColor);
                //--------------调整控件位置---------------
                btn_SwitchColor.Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, (panel.Size.Height - (int)(50 / PrmSW * SW)) / 16);
                btn_BackToMenu.Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, (panel.Size.Height - (int)(50 / PrmSW * SW)) - (panel.Size.Height - (int)(50 / PrmSW * SW)) / 16);
                btn_ChangeSize.Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, (panel.Size.Height - (int)(50 / PrmSW * SW)) - (int)((panel.Size.Height - (50 / PrmSW * SW)) / 2.4));
                btn_StartUp.Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, (panel.Size.Height - (int)(50 / PrmSW * SW)) - (int)((panel.Size.Height - (50 / PrmSW * SW)) / 4.2));
            }
            //--------------检测字体---------------
            #region 检测字体
            if (WinTCTextSize == 7)
            {
                btn_ChangeSize.Text = "字体大小：超小";
            }
            else if (WinTCTextSize == 11)
            {
                btn_ChangeSize.Text = "字体大小：小";
            }
            else if (WinTCTextSize == 13)
            {
                btn_ChangeSize.Text = "字体大小：中";
            }
            else if (WinTCTextSize == 15)
            {
                btn_ChangeSize.Text = "字体大小：大";
            }
            else if (WinTCTextSize == 19)
            {
                btn_ChangeSize.Text = "字体大小：超大";
            }
            #endregion
            //--------------检测自启---------------
            if (StartUp)
            {
                btn_StartUp.Text = "开机自启已开启";
            }
            #endregion
            #region 检测防止控件长宽过小
            var PrefferedSize = TextRenderer.MeasureText(btn_SwitchColor.Text, btn_SwitchColor.Font);
            btn_SwitchColor.Width = PrefferedSize.Width > btn_SwitchColor.Width ? PrefferedSize.Width : btn_SwitchColor.Width;
            btn_SwitchColor.Height = PrefferedSize.Height > btn_SwitchColor.Height ? PrefferedSize.Height : btn_SwitchColor.Height;
            btn_BackToMenu.Width = PrefferedSize.Width > btn_BackToMenu.Width ? PrefferedSize.Width : btn_BackToMenu.Width;
            btn_BackToMenu.Height = PrefferedSize.Height > btn_BackToMenu.Height ? PrefferedSize.Height : btn_BackToMenu.Height;
            btn_ChangeSize.Width = PrefferedSize.Width > btn_ChangeSize.Width ? PrefferedSize.Width : btn_ChangeSize.Width;
            btn_ChangeSize.Height = PrefferedSize.Height > btn_ChangeSize.Height ? PrefferedSize.Height : btn_ChangeSize.Height;
            btn_StartUp.Width = PrefferedSize.Width > btn_StartUp.Width ? PrefferedSize.Width : btn_StartUp.Width;
            btn_StartUp.Height = PrefferedSize.Height > btn_StartUp.Height ? PrefferedSize.Height : btn_StartUp.Height;
            #endregion
        }
        private void btn_HelpLabel_LinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"http://xfegzs.com/TRsp.mp4");
        }
        private void btn_Help_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel()
            {
                Width = this.Size.Width - btn_Close.Width,
                Height = this.Size.Height,
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(236, 230, 220),
            };
            panel.Paint += MainForm_Paint;
            this.Controls.Add(panel);
            panel.BringToFront();
            //--------------说明标签---------------
            LinkLabel HelpLabel = new LinkLabel()
            {
                Text = "如何使用：在需要翻译的地方划线，接着按Ctrl+C即可弹出翻译后的内容（请确保主菜单界面的开关按钮为开启状态）。点击翻译框即可跳转至百度翻译的详细界面，查看读音和详细释义~\n如若还有不懂的可以点此观看教学视频",
                LinkBehavior = LinkBehavior.HoverUnderline,
                LinkColor = Color.FromArgb(80, 80, 80),
                ActiveLinkColor = Color.FromArgb(60, 60, 60),
                Font = SmallButtonTextFont,
                ForeColor = Color.FromArgb(1, 106, 125),
                AutoSize = true,
                Location = new Point(16, 3),
                MaximumSize = new Size(panel.Size.Width - 16, 0)
            };
            HelpLabel.LinkArea = new LinkArea() { Start = HelpLabel.Text.Length - 8, Length = 8 };
            HelpLabel.LinkClicked += btn_HelpLabel_LinkClick;
            panel.Controls.Add(HelpLabel);
            //--------------返回按钮---------------
            Button btn_BackToMenuHelp = new Button()
            {
                Text = "返回主界面",
                ForeColor = Color.FromArgb(236, 230, 220),
                BackColor = Color.Teal,
                FlatStyle = FlatStyle.Flat,
                Font = ButtonTextFont,
                Size = new Size((int)(340 / PrmSW * SW), (int)(50 / PrmSW * SW)),
                Location = new Point((panel.Size.Width - (int)(340 / PrmSW * SW)) / 2, (panel.Size.Height - (int)(50 / PrmSW * SW)) - (panel.Size.Height - (int)(50 / PrmSW * SW)) / 16)
            };
            btn_BackToMenuHelp.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 70, 95);
            btn_BackToMenuHelp.FlatAppearance.MouseOverBackColor = Color.FromArgb(1, 106, 125);
            btn_BackToMenuHelp.FlatAppearance.BorderColor = Color.FromArgb(69, 83, 113);
            btn_BackToMenuHelp.FlatAppearance.BorderSize = 2;
            btn_BackToMenuHelp.Click += btn_BackToMenuHelp_Click;
            panel.Controls.Add(btn_BackToMenuHelp);
            //--------------打赏图片---------------
            Panel WeChatGetImagePanel = new Panel()
            {
                Size = new Size((int)(panel.Size.Height / 2.5), (int)(panel.Size.Height / 2.5)),
                BackgroundImage = Resources.Charge,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            WeChatGetImagePanel.Location = new Point((panel.Size.Width - WeChatGetImagePanel.Size.Width) / 2, panel.Size.Height - WeChatGetImagePanel.Size.Height - (panel.Size.Height - WeChatGetImagePanel.Size.Height) / 3);
            panel.Controls.Add(WeChatGetImagePanel);
            //-------------检测并设置主题-------------
            if (themeType == ThemeType.Dark)
            {
                panel.BackColor = Color.FromArgb(30, 30, 30);
                HelpLabel.LinkColor = Color.FromArgb(220, 220, 220);
                HelpLabel.ActiveLinkColor = Color.FromArgb(100, 100, 100);
                HelpLabel.ForeColor = Color.FromArgb(240, 230, 220);
                btn_BackToMenuHelp.ForeColor = Color.FromArgb(236, 230, 220);
                btn_BackToMenuHelp.BackColor = Color.FromArgb(69, 83, 113);
                btn_BackToMenuHelp.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 53, 83);
                btn_BackToMenuHelp.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 103);
                btn_BackToMenuHelp.FlatAppearance.BorderColor = Color.FromArgb(59, 73, 103);
            }
        }
        private void timerAddShadow_Tick(object sender, EventArgs e)
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);//添加窗体阴影
            timerAddShadow.Dispose();
        }
        #endregion
        #region 线程方法
        //----------------后台线程-------------
        private void ConnectToServer()
        {
            //-----------------连接后台------------------
            IPAddress iPAddress = IPAddress.Parse("175.178.121.98");
            backConnect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                backConnect.Connect(new IPEndPoint(iPAddress, 9019));
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法连接至服务器：\n{0}\n{1}", ex.Message, ex.ToString());
            }
        }
        private void GetBoard()
        {
            string StatueContent;
            ClipBoardContent = GetClipBoardContent();
            if (Clipboard.ContainsData(DataFormats.Html))
            {
                ClipHtmlContent = Clipboard.GetDataObject().GetData(DataFormats.Html).ToString();
            }
            else
            {
                ClipHtmlContent = string.Empty;
            }
            while (true)
            {
                //------------判断用户是否又打开了此程序------------
                if (ReadIn(Application.UserAppDataPath + @"\Statue.xfe", out StatueContent))
                {
                    if (StatueContent == "1")
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.Show();
                            this.Activate();
                        }));
                        notifyIcon1.Visible = false;
                        System.Media.SystemSounds.Exclamation.Play();
                        WriteIn(Application.UserAppDataPath + @"\Statue.xfe", "0");
                    }
                }
                #region 获取秘钥
                if (APPID == string.Empty)
                {
                    WebClient GetID = new WebClient();
                    try
                    {
                        APPID = GetID.DownloadString("https://www.xfegzs.com/XFETranslate/APPID");
                        //APPID = "20230214001562006";
                    }
                    catch (Exception ex)
                    {
                        APPID = "0000000000000000";
                        SendBugTC("无法连接至服务器", ex.Message, ex.ToString());
                    }
                }
                if (DeCode == string.Empty)
                {
                    WebClient GetCode = new WebClient();
                    try
                    {
                        DeCode = GetCode.DownloadString("https://www.xfegzs.com/XFETranslate/DeCode");
                        //DeCode = "XARI_HUduJ0WmMo4GSfH";
                    }
                    catch (Exception ex)
                    {
                        DeCode = "0x000000000000000";
                        SendBugTC("无法连接至服务器", ex.Message, ex.ToString());
                    }
                }
                #endregion
                //------------------------间隔----------------------
                Thread.Sleep(50);
            }
        }
        private void Down()
        {
            if (!File.Exists(CommonPath + @"\ReWrite"))
            {
                WebClientPro versionDow = new WebClientPro(10000);
                versionDow.DownloadFileCompleted += VersionDetected;
                versionDow.DownloadFileAsync(new Uri("https://www.xfegzs.com/XFETranslate/Version"), Application.UserAppDataPath + @"\version");
            }
        }
        private void VersionDetected(object sender, AsyncCompletedEventArgs e)
        {
            string versionAndPost = File.ReadAllText(Application.UserAppDataPath + @"\version");
            if (versionAndPost.Length > 0)
            {
                string version = versionAndPost.Split('$')[0];
                string Post = versionAndPost.Split('$')[1];
                if (version != Application.ProductVersion)
                {
                    VersionTC(Post);
                    this.Invoke(new Action(() =>
                    {
                        this.Show();
                        this.Activate();
                    }));
                    notifyIcon1.Visible = false;
                }
                File.Delete(Application.UserAppDataPath + @"\version");
            }
            else
            {
                SendBugTC("无法获取更新信息", "无法连接至更新服务器，请检查网络设置", $"无法获取更新信息：{versionAndPost}");
            }
        }
        #endregion
        //------------------主窗体-----------------
        private void MainForm_Load(object sender, EventArgs e)
        {
            string Colorstring;
            #region 自适应屏幕
            SH = Screen.PrimaryScreen.Bounds.Height;
            SW = Screen.PrimaryScreen.Bounds.Width;
            double SWP = PrmW / PrmSW;//屏幕宽比例
            double SHP = PrmH / PrmSH;//屏幕长比例
            this.Width = (int)(SW * SWP);
            this.Height = (int)(SH * SHP);
            btn_Switch.Size = new Size((int)(btn_Switch.Size.Width / PrmSW * SW), (int)(btn_Switch.Size.Height / PrmSH * SH));
            btn_Option.Size = new Size((int)(btn_Option.Size.Width / PrmSW * SW), (int)(btn_Option.Size.Height / PrmSH * SH));
            btn_Help.Size = new Size((int)(btn_Help.Size.Width / PrmSW * SW), (int)(btn_Help.Size.Height / PrmSH * SH));
            btn_Small.Size = new Size((int)(btn_Small.Size.Width / PrmSW * SW), (int)(btn_Small.Size.Height / PrmSH * SH));
            btn_Close.Size = new Size((int)(btn_Close.Size.Width / PrmSW * SW), (int)(btn_Close.Size.Height / PrmSH * SH));
            #endregion
            #region 设置字体
            ButtonTextFont = new Font("等线", (float)(22 / PrmSW * SW), FontStyle.Bold);
            SmallButtonTextFont = new Font("等线", (float)(18 / PrmSW * SW), FontStyle.Bold);
            btn_Switch.Font = new Font("等线", (float)(btn_Switch.Font.Size / PrmSW * SW), FontStyle.Bold);
            btn_Option.Font = new Font("等线", (float)(btn_Option.Font.Size / PrmSW * SW), FontStyle.Bold);
            btn_Help.Font = new Font("等线", (float)(btn_Help.Font.Size / PrmSW * SW), FontStyle.Bold);
            btn_Small.Font = new Font("等线", (float)(btn_Small.Font.Size / PrmSW * SW), FontStyle.Bold);
            btn_Close.Font = new Font("等线", (float)(btn_Close.Font.Size / PrmSW * SW), FontStyle.Bold);
            label_Title.Font = new Font("等线", (float)(label_Title.Font.Size / PrmSW * SW), FontStyle.Bold);
            #endregion
            #region 调整位置
            int SwitchWidthLoc = this.Width - btn_Switch.Size.Width;
            int SwitchHeightLoc = this.Height - btn_Switch.Size.Height;
            btn_Switch.Location = new Point(SwitchWidthLoc / 2, SwitchHeightLoc / 4);
            btn_Option.Location = new Point(SwitchWidthLoc / 2, SwitchHeightLoc / 2);
            btn_Help.Location = new Point(SwitchWidthLoc / 2, (SwitchHeightLoc / 4) * 3);
            btn_Small.Location = new Point(this.Width - btn_Small.Size.Width, 0);
            btn_Close.Location = new Point(this.Width - btn_Close.Size.Width, this.Height - btn_Small.Size.Height);
            label_Title.Location = new Point((this.Size.Width - label_Title.Size.Width) / 2, (this.Size.Height - label_Title.Size.Height) / 16);
            #region 防止控件长宽过小
            var PrefferedSize = TextRenderer.MeasureText(btn_Switch.Text, btn_Switch.Font);
            btn_Switch.Width = PrefferedSize.Width > btn_Switch.Width ? PrefferedSize.Width : btn_Switch.Width;
            btn_Switch.Height = PrefferedSize.Height > btn_Switch.Height ? PrefferedSize.Height : btn_Switch.Height;
            btn_Option.Width = PrefferedSize.Width > btn_Option.Width ? PrefferedSize.Width : btn_Option.Width;
            btn_Option.Height = PrefferedSize.Height > btn_Option.Height ? PrefferedSize.Height : btn_Option.Height;
            btn_Help.Width = PrefferedSize.Width > btn_Help.Width ? PrefferedSize.Width : btn_Help.Width;
            btn_Help.Height = PrefferedSize.Height > btn_Help.Height ? PrefferedSize.Height : btn_Help.Height;
            #endregion
            #endregion
            #region 运行线程
            GetClipBD = new Thread(GetBoard);
            GetClipBD.SetApartmentState(ApartmentState.STA);
            GetClipBD.Start();
            Download = new Thread(Down);
            Download.Start();
            ThreadbackConnect = new Thread(ConnectToServer);
            ThreadbackConnect.Start();
            AddClipboardFormatListener(this.Handle);
            WebClient GetID = new WebClient();
            #endregion
            #region 检测自启动
            if (!Program.RunInStartUp)
            {
                SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);//添加窗体阴影
            }
            else
            {
                this.Activated += MainForm_Activated;
            }
            notifyIcon1.Visible = false;
            #endregion
            #region 读取用户文件
            CheckAppDate();//检测用户文件夹
            if (!File.Exists(CommonPath + @"\IsSetUp.xfe"))
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\XFE划词翻译.lnk"))
                {
                    CreateShortCut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), string.Empty);
                    WriteIn(CommonPath + @"\IsSetUp.xfe", "TRSetUp");
                }
            }//是否初始化
            if (File.Exists(CommonPath + @"\StartUp.xfe"))
            {
                StartUp = true;
            }
            else
            {
                StartUp = false;
            }
            if (File.Exists(CommonPath + @"\ReWrite"))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\XFE划词翻译.lnk"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\XFE划词翻译.lnk");
                    CreateShortCut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), string.Empty);
                }//重写桌面快捷方式
                if (File.Exists(CommonPath + @"\StartUp.xfe"))
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup) + @"\XFE划词翻译.lnk");
                        CreateShortCut(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup), "StartUp");
                        StartUp = true;
                    }
                    else
                    {
                        File.Delete(CommonPath + @"\StartUp.xfe");
                        StartUp = false;
                    }
                }
                else
                {
                    StartUp = false;
                }
                File.Delete(CommonPath + @"\ReWrite");
            }
            else
            {
                if (File.Exists(CommonPath + @"\GetAdmin"))
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                    {
                        if (File.Exists(CommonPath + @"\StartUp.xfe"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup) + @"\XFE划词翻译.lnk");
                            File.Delete(CommonPath + @"\StartUp.xfe");
                            File.Delete(CommonPath + @"\GetAdmin");
                            StartUp = false;
                        }
                        else
                        {
                            CreateShortCut(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup), "StartUp");
                            WriteIn(CommonPath + @"\StartUp.xfe", "IsStartUp");
                            File.Delete(CommonPath + @"\GetAdmin");
                            StartUp = true;
                        }
                    }//检测是否获取到了管理员权限
                    else
                    {
                        File.Delete(CommonPath + @"\GetAdmin");
                        StartUp = false;
                    }
                }
            }
            #region 拆分读取用户配置
            if (ReadIn(CommonPath + @"\UserSelfEditData\Color", out Colorstring))
            {
                string[] ColorGroup = Colorstring.Split(new char[] { '|' });
                LineColor = BringStringToColor(ColorGroup[0]);
                TextColor = BringStringToColor(ColorGroup[1]);
                BGColor = BringStringToColor(ColorGroup[2]);
                if (ColorGroup.Length == 6)
                {
                    if (ColorGroup[3] == "0")
                    {
                        themeType = ThemeType.Light;
                    }
                    else if (ColorGroup[3] == "1")
                    {
                        themeType = ThemeType.Dark;
                    }
                    else
                    {
                        themeType = ThemeType.SelfEdit;
                    }
                    WinTCTextSize = float.Parse(ColorGroup[4]);
                    if (ColorGroup[5] == "AskGPT")
                    {
                        btn_Switch_Click(sender, e);
                    }
                    else if (ColorGroup[5] == "Closed")
                    {
                        btn_Switch_Click(sender, e);
                        btn_Switch_Click(sender, e);
                    }
                }
                else
                {
                    WriteIn(CommonPath + @"\UserSelfEditData\Color", $"69,83,113|1,106,125|240,230,220|0|{WinTCTextSize}|{CatchWordsMode}");
                }
            }
            else
            {
                LineColor = Color.FromArgb(69, 83, 113);
                TextColor = Color.FromArgb(1, 106, 125);
                BGColor = Color.FromArgb(240, 230, 220);
                themeType = ThemeType.Light;
                WriteIn(CommonPath + @"\UserSelfEditData\Color", $"69,83,113|1,106,125|240,230,220|0|{WinTCTextSize}|{CatchWordsMode}");
            }
            #endregion
            #endregion
            #region 设置颜色
            if (themeType == ThemeType.Dark)
            {
                ChangeButtonColor(Color.FromArgb(236, 230, 220), Color.FromArgb(69, 83, 113), Color.FromArgb(39, 53, 83), Color.FromArgb(59, 73, 103), Color.FromArgb(59, 73, 103));
                this.BackColor = Color.FromArgb(30, 30, 30);
                label_Title.ForeColor = Color.FromArgb(236, 230, 220);
            }
            #endregion
        }
        #region 拖动窗体
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLocation = new Point(-e.X, -e.Y);
                //表示鼠标当前位置相对于窗口左上角的坐标，
                //并取负数,这里的e是参数，
                //可以获取鼠标位置
                isDragging = true;//标识鼠标已经按下
            }
        }
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newMouseLocation = MousePosition;
                //获取鼠标当前位置
                newMouseLocation.Offset(mouseLocation.X, mouseLocation.Y);
                //用鼠标当前位置加上鼠标相较于窗体左上角的
                //坐标的负数，也就获取到了新的窗体左上角位置
                if (sender is Form)
                {
                    Location = newMouseLocation;
                }

            }
        }
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;//鼠标已抬起，标识为false
            }
        }
        #endregion
        //-----------------加载窗体----------------
        private void MainForm_Activated(object sender, EventArgs e)
        {
            btn_Small_Click(null, null);
            timerAddShadow.Start();
            this.Activated -= MainForm_Activated;
        }
        //-----------持续监听剪切板是否变化--------
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x031D && Clipboard.ContainsText() && CatchWordsMode != CatchWordsMode.Closed) // WM_CLIPBOARDUPDATE
            {
                ClipBoardContent = Clipboard.GetText();
                if (ClipBoardContent != string.Empty)
                {
                    string TranslatedText = ClipBoardContent;
                    Label label;
                    MaxSize += (int)(TranslatedText.Length / 1.5);
                    winTC = new WinTC();
                    winTC.Show();
                    winTC.Location = new Point(MousePosition.X - winTC.Width / 6, MousePosition.Y - winTC.Height / 6);
                    label = winTC.Controls.Find("label", false)[0] as Label;
                    label.Text = "正在分析，请稍后...";
                    winTC.Size = new Size(label.Size.Width, label.Size.Height + 8);
                    label.Location = new Point((winTC.Width - label.Width) / 2, (winTC.Height - label.Height) / 2);
                    int WinTCX = winTC.Location.X;
                    int WinTCY = winTC.Location.Y;
                    if (Cursor.Position.X + winTC.Width > SW)
                    {
                        winTC.Location = new Point(SW - winTC.Width - 10, WinTCY);
                    }
                    if (Cursor.Position.Y + winTC.Height > SH)
                    {
                        winTC.Location = new Point(WinTCX, SH - winTC.Width - 10);
                    }
                    winTC.Activate();
                    if (CatchWordsMode == CatchWordsMode.AskGPT)
                    {
                        var task = Task.Run(() => SendAndGetGptMessage(ClipBoardContent));
                        task.Wait();
                        TranslatedText = task.Result;
                    }
                    else
                    {
                        string qury = ClipBoardContent;
                        string salt = SummonLongRandomCode();
                        string GETALL = $"{APPID}{qury}{salt}{DeCode}";
                        string sign = GetMD5WithString(GETALL);
                        string Url = $"https://fanyi-api.baidu.com/api/trans/vip/translate?q={qury}&from=auto&to=zh&appid={APPID}&salt={salt}&sign={sign}&dict=1";
                        string result;
                        try
                        {
                            HttpGet(Url, out result);
                        }
                        catch (Exception ex)
                        {
                            SendBugTC("出现网络问题", ex.Message, ex.ToString());
                            result = ex.Message;
                        }
                        string OutPut = ConvertUnicodeToUtf8(result);
                        int a = 0;
                        var DevGroup = OutPut.Split(new char[1] { '\"' });
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < Regex.Matches(OutPut, "dst").Count; i++)
                        {
                            for (; a < DevGroup.Count(); a++)
                            {
                                if (DevGroup[a] == "dst")
                                {
                                    a += 2;
                                    sb.Append(DevGroup[a]);
                                    break;
                                }
                            }
                            a++;
                        }
                        TranslatedText = sb.ToString();
                        Console.WriteLine(result);
                    }
                    if (TranslatedText != string.Empty)
                    {
                        MaxSize += (int)(TranslatedText.Length);
                        winTC.Invoke(new Action(() => { label.MaximumSize = WinTC.SetMaxSize(); }));
                        winTC.Name = TranslatedText;
                    }
                    else
                    {
                        label.Text = "返回出错！";
                    }
                    winTC.Text = TranslatedText;
                    winTC.Invoke(new Action(() =>
                    {
                        if (TranslatedText != "")
                        {
                            if (ClipBoardContent.Length <= 2000)
                            {
                                if (TranslatedText.Length <= 1000)
                                {
                                    label.Text = TranslatedText;
                                }
                                else
                                {
                                    label.Text = TranslatedText;
                                    MaxSize = 1400;
                                }
                                winTC.Size = new Size(label.Size.Width, label.Size.Height + 8);
                                label.Location = new Point((winTC.Width - label.Width) / 2, (winTC.Height - label.Height) / 2);
                            }
                            else
                            {
                                label.Text = "翻译文本长度超长，请点此查看详情";
                                winTC.Size = new Size(label.Size.Width, label.Size.Height + 8);
                                label.Location = new Point((winTC.Width - label.Width) / 2, (winTC.Height - label.Height) / 2);
                            }
                        }
                    }));
                }
            }
            base.WndProc(ref m);
        }
        #region DLL调用
        //------------------调用阴影DLL------------------
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        //-----------------调用剪切板DLL-----------------
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetClipboardData(uint uFormat);
        #endregion
    }
}
