using System.Linq;
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
            message.From.Add(new MailboxAddress(emailMessage.FromAddress.Name, emailMessage.FromAddress.Address));
            message.To.Add(new MailboxAddress(emailMessage.ToAddress.Name, emailMessage.ToAddress.Address));

            //adding message subject and body
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = emailMessage.Content
            };

            using (var emailClient = new SmtpClient())
            {
                //emailClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort,
                //    _emailConfiguration.SSL);
                emailClient.Connect("smtp.wp.pl", 465,
                        true);
                //emailClient.Authenticate(_emailConfiguration.SmtpUserName, _emailConfiguration.SmtpPassword);
                emailClient.Authenticate("zleceniamafia2.0@wp.pl", "qwe12345");
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }

        }
    }
}