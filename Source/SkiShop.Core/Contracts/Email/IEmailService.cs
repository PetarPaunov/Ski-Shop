namespace SkiShop.Core.Contracts.Email
{
    using SkiShop.Core.Models.EmailViewModels;

    /// <summary>
    /// Email services
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email to a newly registered user to confirm the account
        /// </summary>
        /// <param name="message">Email message</param>
        void SendEmail(Message message);
    }
}