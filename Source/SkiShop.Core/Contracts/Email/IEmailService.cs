namespace SkiShop.Core.Contracts.Email
{
    using SkiShop.Core.Models.EmailViewModels;

    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
