using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RentACar.Infrastructure.Interfaces;
using RentACar.Infrastructure.Options;

namespace RentACar.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions emailOptions;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            this.emailOptions = emailOptions.Value;
        }

        public void SendPasswordRecoveryToken(string tokenRecovery, string toEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailOptions.displayName, emailOptions.username));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Password recovery";

            message.Body = new TextPart("html")
            {
                Text = $@"
                    <p>You have requested to recover your password.</p>
                    <a href=""{emailOptions.recoveryUrl + tokenRecovery}/"" >Click here to recover</a>
                "
            };

            using (var client = new SmtpClient())
            {
                client.Connect(emailOptions.host, emailOptions.port, SecureSocketOptions.StartTls);

                client.Authenticate(emailOptions.username, emailOptions.password);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
