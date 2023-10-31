using SWBiblioteca.Models;

namespace SWBiblioteca.Services.Contract
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
