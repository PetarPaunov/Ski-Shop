namespace SkiShop.Core.Services.Email
{
    using MimeKit;
    using MailKit.Net.Smtp;
    using SkiShop.Core.Contracts.Email;
    using SkiShop.Core.Models.EmailViewModels;

    /// <summary>
    /// Email services
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration emailConfiguration;

        public EmailService(EmailConfiguration _emailConfiguration)
        {
            emailConfiguration = _emailConfiguration;
        }

        /// <summary>
        /// Sends an email to a newly registered user to confirm the account
        /// </summary>
        /// <param name="message">Email message</param>
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        /// <summary>
        /// Creates the email message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Message</returns>
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Email Confirmation", emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content};

            return emailMessage;
        }

        /// <summary>
        /// Connect to the webmail service and send the message
        /// </summary>
        /// <param name="emailMessage">Message</param>
        private void Send(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();

            try
            {
                client.Connect(emailConfiguration.SmtpServer, emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(emailConfiguration.UserName, emailConfiguration.Password);

                client.Send(emailMessage);
            }
            catch (Exception)
            {
                throw new ArgumentException("Problem with connection to the client");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}