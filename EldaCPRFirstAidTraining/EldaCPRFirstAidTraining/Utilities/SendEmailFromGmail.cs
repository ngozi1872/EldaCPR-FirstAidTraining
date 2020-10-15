using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EldaCPRFirstAidTraining.Utilities
{
    public class SendEmailFromGmail
    {
        public void SendEmail(string toEmail, string toName, string subject, string body, string path = null)
        {
            var fromAddress = new MailAddress("stringbuild1@gmail.com", "Elda CPR and First Aid Training");
            var toAddress = new MailAddress(toEmail, toName);
            var copy = new MailAddress("stringbuild1@gmail.com");
            const string fromPassword = "Trinity@2016";

            var smtp = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };

            string html = @"<html><body><img src='cid:NHLogo' style='display: block; margin-left: auto; margin-right: auto; width:75px; height:75px'><hr/><br/>" + body + "</body></html>";
            AlternateView altView = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);

            if (path != null)
            {
                LinkedResource yourPictureRes = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
                yourPictureRes.ContentId = "NHLogo";
                altView.LinkedResources.Add(yourPictureRes);
            }

            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            message.AlternateViews.Add(altView);
            {
                //await smtp.SendMailAsync(message);
                message.CC.Add(copy);
                message.IsBodyHtml = true;
                smtp.Send(message);
            }

        }
    }
}
