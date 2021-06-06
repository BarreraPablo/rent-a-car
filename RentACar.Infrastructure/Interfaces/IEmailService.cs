namespace RentACar.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        void SendPasswordRecoveryToken(string tokenRecovery, string toEmail);
    }
}