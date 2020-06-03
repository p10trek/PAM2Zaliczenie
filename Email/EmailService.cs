using System.Diagnostics;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace PAM2Zaliczenie.Email
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
    }

    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void Send(EmailMessage emailMessage)
        {
            //message create, adding from and to addresses
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfiguration.SmtpUserName.Remove(_emailConfiguration.SmtpUserName.IndexOf("@"), _emailConfiguration.SmtpUserName.Length - _emailConfiguration.SmtpUserName.IndexOf("@")), _emailConfiguration.SmtpUserName));
            message.To.Add(new MailboxAddress(emailMessage.ToAddress.Name, emailMessage.ToAddress.Address));

            //adding message subject and body
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = emailMessage.Content
            };

            using (var emailClient = new SmtpClient())
            {
                Debug.WriteLine(_emailConfiguration.SmtpServer);
                Debug.WriteLine(_emailConfiguration.SmtpPort);
                Debug.WriteLine(_emailConfiguration.SSL);
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort,
                    _emailConfiguration.SSL);
                emailClient.Authenticate(_emailConfiguration.SmtpUserName, _emailConfiguration.SmtpPassword);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }

        }
    }
}