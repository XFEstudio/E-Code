using IWshRuntimeLibrary;
using System;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static TranslationAPI.MainForm;
using System.Windows.Forms;

namespace TranslationAPI.Method
{
    internal class AllMethod
    {
        //-----------------以下方法----------------
        public static void EmailDebug(string subject, string body)
        {
            try
            {
                // 配置邮件发送的参数
                string smtpServer = "smtp.exmail.qq.com";
                int smtpPort = 587;
                string smtpUsername = "mail@xfegzs.com";
                string smtpPassword = "XFE016346616ddaa736";
                string fromName = "XFE划词翻译";
                string fromAddress = "mail@xfegzs.com";
                string toAddress = "xfe18827675401@126.com";

                // 创建 SmtpClient 实例
                SmtpClient client = new SmtpClient(smtpServer, smtpPort);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                // 创建发件人的 MailAddress 对象，指定显示名称和邮箱地址
                MailAddress fromMailAddress = new MailAddress(fromAddress, fromName);

                // 创建 MailMessage 实例，并设置发件人、收件人、主题和正文内容
                MailMessage message = new MailMessage();
                message.From = fromMailAddress;
                message.To.Add(toAddress);
                message.Subject = subject;
                message.Body = body;

                // 发送邮件
                client.Send(message);

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }//通过邮件发送错误报告
        public static async Task<string> SendAndGetGptMessage(string message)
        {
            string receivedMessage = string.Empty;
            try
            {
                ClientWebSocket webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri("ws://api.xfegzs.com/"), CancellationToken.None);
                Console.WriteLine("Connected to WebSocket server.");

                await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine("Message sent: " + message);
                byte[] buffer = new byte[1024];

                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine("Received: " + receivedMessage);

                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed.", CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return receivedMessage;
        }//从WS服务器中发送并获取消息
        public static int HttpGet(string url, out string reslut)
        {
            reslut = "";
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Proxy = null;
                wbRequest.Method = "GET";
                wbRequest.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        reslut = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                reslut = e.Message;     //输出捕获到的异常，用OUT关键字输出
                return -1;              //出现异常，函数的返回值为-1
            }
            return 0;
        }//http的GET方法请求
        public static string GetMD5WithString(string input)
        {
            MD5 md5Hash = MD5.Create();
            // 将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // 创建一个 Stringbuilder 来收集字节并创建字符串  
            StringBuilder str = new StringBuilder();
            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)
            {
                str.Append(data[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }
            // 返回十六进制字符串  
            return str.ToString();
        }//将string加密为MD5
        public static string SummonLongRandomCode()
        {
            Random r = new Random();
            return $"{r.Next(1000, 1000000)}{r.Next(1000, 1000000)}";
        }//以string生成随机的长随机数
        public static string ConvertUnicodeToUtf8(string unicode)
        {
            if (string.IsNullOrEmpty(unicode))
            {
                return string.Empty;
            }
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(unicode, p => Convert.ToChar(Convert.ToUInt16(p.Result("$1"), 16)).ToString());
        }//将Unicode编码转为UTF-8编码
        public static string GetClipBoardContent()
        {
            if (Clipboard.ContainsText())
            {
                return Clipboard.GetText();
            }
            else
            {
                return string.Empty;
            }
        }//获取剪切板内容
        public static void WriteIn(string Name, string txt)
        {
            FileInfo rd = new FileInfo(Name);
            StreamWriter sw = rd.CreateText();
            sw.WriteLine(txt);
            sw.Flush();
            sw.Close();
        }//写入文件
        public static bool ReadIn(string Name, out string Content)
        {
            FileInfo rd = new FileInfo(Name);
            if (rd.Exists)
            {
                StreamReader sr = rd.OpenText();
                Content = sr.ReadLine();
                sr.Close();
                return true;
            }
            else
            {
                Content = string.Empty;
                return false;
            }
        }//读取文件
        public static void CheckAppDate()
        {
            if (!Directory.Exists(Application.UserAppDataPath.Substring(0, Application.UserAppDataPath.LastIndexOf(@"\")) + @"\UserSelfEditData"))
            {
                Directory.CreateDirectory(Application.UserAppDataPath.Substring(0, Application.UserAppDataPath.LastIndexOf(@"\")) + @"\UserSelfEditData");
            }
            if (!Directory.Exists(Application.UserAppDataPath))
            {
                Directory.CreateDirectory(Application.UserAppDataPath);
            }
        }//检测是否存在配置文件夹否则创建
        public static Color BringStringToColor(string stringColor)
        {
            Color color = new Color();
            string[] Sstring = stringColor.Split(new char[] { ',' });
            color = Color.FromArgb(int.Parse(Sstring[0]), int.Parse(Sstring[1]), int.Parse(Sstring[2]));
            return color;
        }//将String数组颜色值转换为颜色类型
        public static string BringColorToString(Color color)
        {
            return $"{color.R},{color.G},{color.B}";
        }//将颜色值转换为String类型的字符串
        public static string OutPutThemeColor(ThemeType themeType)
        {
            if (themeType == ThemeType.Light)
            {
                return "0";
            }
            else if (themeType == ThemeType.Dark)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }//根据枚举类型ThemeType输出对应的String类型的值
        public static string OutPutBoolInString(bool Bo)
        {
            if (Bo)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }//将bool值类型转换为string类型字符串输出
        public static void CreateShortCut(string Path, string Argument)
        {
            WshShell wshShell = new WshShell();
            IWshShortcut wshShortcut = (IWshShortcut)wshShell.CreateShortcut(Path + "\\XFE划词翻译.lnk");
            //设置快捷方式的目标所在的位置(源程序完整路径)
            wshShortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //当用户没有指定一个具体的目录时，快捷方式的目标应用程序将使用该属性所指定的目录来装载或保存文件。
            wshShortcut.WorkingDirectory = System.Environment.CurrentDirectory;
            //目标应用程序窗口类型(1.Normal window普通窗口,3.Maximized最大化窗口,7.Minimized最小化)
            wshShortcut.WindowStyle = 1;
            //快捷方式的描述
            wshShortcut.Description = "划词翻译软件-XFEstudio";
            //设置应用程序的启动参数
            wshShortcut.Arguments = Argument;
            //保存快捷方式
            wshShortcut.Save();
        }//在Path目录下创建本程序的快捷方式
    }
}
