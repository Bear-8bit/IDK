using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DigitalStore.Common
{
    public class Common
    {
        private static string password = ConfigurationManager.AppSettings["PasswordEmail"];
        private static string Email = ConfigurationManager.AppSettings["Email"];
        public static bool SendMail(string username, string subject, string content, string toMail)
        {
            bool rs = false;
            try
            {
                MailMessage message = new MailMessage();
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(Email, password);
                    smtp.Timeout = 20000;
                }
                MailAddress fromAddress = new MailAddress(Email, username);
                message.From = fromAddress;
                message.To.Add(toMail);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = content;
                smtp.Send(message);
                rs = true;
            }
            catch (Exception)
            {
                rs = false;
            }
            return rs;
        }
        public static string FormatNumber(object value, int SoSauDauPhay = 2)
        {
            bool isNumber = IsNumberic(value);
            decimal GT = 0;
            if (isNumber)
            {
                GT = Convert.ToDecimal(value);
            }
            string str = "";
            string thapPhan = "";
            for (int i = 0; i < SoSauDauPhay; i++)
            {
                thapPhan += "#";
            }
            if (thapPhan.Length > 0) thapPhan = "." + thapPhan;
            string snumformat = string.Format("0:#,##0{0}", thapPhan);
            str = String.Format("{" + snumformat + "}", GT);

            return str;
        }
        private static bool IsNumberic(object value)
        {
            return value is sbyte
                         || value is byte
                         || value is short
                         || value is ushort
                         || value is int
                         || value is uint
                         || value is long
                         || value is ulong
                         || value is float
                         || value is double
                         || value is decimal;
        }

        public static string HtmlRate(int rate) 
        {
            var str = "";
            if (rate == 1)
            {
                str = @"<li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>";
            }
            if (rate == 2)
            {
                str = @"<li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>";
            }
            if (rate == 3)
            {
                str = @"<li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>";
            }
            if (rate == 4)
            {
                str = @"<li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bx-star' style='color:rgba(214,253,0,0.98)'></i></li>";
            }
            if (rate == 5)
            {
                str = @"<li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>
                        <li><i class='bx bxs-star' style='color:rgba(214,253,0,0.98)'></i></li>";
            }
            return str;
        }
    }
}