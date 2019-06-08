using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.IO;
using WebAPI.Utility;

namespace Utility
{
    public class EmailSender
    {

        public static bool SendMail(string toEmail, string emailSubject, string emailBody, string toName = null)
        {
            bool IsFlag = false;

            var fromEmail = Utilities.FromEmail;
            var fromName = Utilities.FromName;
            var apiKey = Utilities.SendGridAPIKey;

            var client = new SendGridClient(apiKey);
            // Send a Single Email using the Mail Helper
            var from = new EmailAddress(fromEmail, fromName);

            var subject = emailSubject;
            var to = new EmailAddress(toEmail, toName);
            // var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = emailBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            //  EmailAddress Reply = new EmailAddress(txtReplyToEmail.Text.Trim(), txtReplyToName.Text.Trim());
            //  msg.ReplyTo = Reply;
            var response = client.SendEmailAsync(msg);
            var res = response.Result.StatusCode;
            if (HttpStatusCode.Accepted == res)
            {
                IsFlag = true;
            }

            return IsFlag;
        }

        public static bool SendMails(string toEmail, string emailSubject, string emailBody, string emailCC = null, string emailBCC = null, bool isAttachment = false, string mailAttachment = null)
        {
            bool IsFlag = false;

            var fromEmail = Utilities.FromEmail;
            var fromName = Utilities.FromName;
            var apiKey = Utilities.SendGridAPIKey;

            var client = new SendGridClient(apiKey);
            List<EmailAddress> emailAddress = new List<EmailAddress>();
            // Send a Single Email using the Mail Helper
            var from = new EmailAddress(fromEmail, fromName);
            var subject = emailSubject;
            var to = new EmailAddress(toEmail);
            emailAddress.Add(to);
            if (!string.IsNullOrWhiteSpace(emailCC))
            {
                var EmailCC = new EmailAddress(emailCC);
                emailAddress.Add(EmailCC);
            }

            if (!string.IsNullOrWhiteSpace(emailBCC))
            {
                var EmailBCC = new EmailAddress(emailBCC);
                emailAddress.Add(EmailBCC);
            }

            // var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = emailBody;

            if (emailAddress.Count != 0 && emailAddress.Count > 1)
            {
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, emailAddress, subject, "", htmlContent);
                if (isAttachment)
                {
                    byte[] fileArray = System.IO.File.ReadAllBytes(@mailAttachment);
                    string base64ExcelRepresentation = Convert.ToBase64String(fileArray);
                    string fileName = Path.GetFileName(mailAttachment);
                    msg.AddAttachment(fileName, base64ExcelRepresentation);
                }
                var response = client.SendEmailAsync(msg);
                var res = response.Result.StatusCode;
                if (HttpStatusCode.Accepted == res)
                {
                    IsFlag = true;
                }
            }
            else
            {
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
                if (isAttachment)
                {
                    byte[] fileArray = System.IO.File.ReadAllBytes(@mailAttachment);
                    string base64ExcelRepresentation = Convert.ToBase64String(fileArray);
                    string fileName = Path.GetFileName(mailAttachment);
                    msg.AddAttachment(fileName, base64ExcelRepresentation);
                }
                var response = client.SendEmailAsync(msg);
                var res = response.Result.StatusCode;
                if (HttpStatusCode.Accepted == res)
                {
                    IsFlag = true;
                }
            }

            //  EmailAddress Reply = new EmailAddress(txtReplyToEmail.Text.Trim(), txtReplyToName.Text.Trim());
            //  msg.ReplyTo = Reply;



            return IsFlag;
        }




    }//end class
}//end namespace