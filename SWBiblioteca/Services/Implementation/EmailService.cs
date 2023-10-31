using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SWBiblioteca.Models;
using SWBiblioteca.Services.Contract;

namespace SWBiblioteca.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            //Indicamos el correo emisor
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:UserName").Value));
            //Indicamos al correo receptor
            email.To.Add(MailboxAddress.Parse(request.For));
            //Indicamos el asunto
            email.Subject = request.Affair;
            //Indicamos el contenido
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Content
            };
            //Conectamos con nuestro servidor
            using var smtp = new SmtpClient();
            smtp.Connect(
                _configuration.GetSection("Email:Host").Value,
                Convert.ToInt32(_configuration.GetSection("Email:Port").Value),
                //Seguridad
                SecureSocketOptions.StartTls
                );

            //Nos Autenticamos
            smtp.Authenticate(
                _configuration.GetSection("Email:UserName").Value,
                _configuration.GetSection("Email:PassWord").Value
                );

            //Enviar Correo
            smtp.Send(email);

            //Desconectamos de nuestro servidor
            smtp.Disconnect(true);
        }
    }
}
