using System;
using System.Net.Mail;

namespace WPFAuthorization
{
    internal sealed class MailManager
    {

        internal void GetDataFromMail(string firstName, string lastName, string email)
        {
            try
            {
                SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru");
                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.EnableSsl = true;

                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential
                ("b.koljabai@taimas-group.kz", "1Je5jDjhGCP4WukqrDVJ");
                mySmtpClient.Credentials = basicAuthenticationInfo;

                MailAddress fromAddress = new MailAddress("b.koljabai@taimas-group.kz", "Erkebulan");
                MailAddress toAddress = new MailAddress("b.koljabai@taimas-group.kz");
                MailMessage message = new MailMessage(fromAddress, toAddress);

                string regValues = $"FirstName is {firstName}, LastName is {lastName}, Email is {email}";
                message.Body = regValues;

                message.Subject = "New User Data";
                mySmtpClient.Send(message);
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException
                ("Error SmtpException: " + ex.Message);
            }

        }
    }
}
