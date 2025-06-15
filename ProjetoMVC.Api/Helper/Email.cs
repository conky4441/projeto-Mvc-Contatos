using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using ProjetoMVC.Api.Helper;
using ProjetoMVC.Api.Helper.Interfaces;

public class Email : IEmail
{
    private readonly SmtpSettings _emailSettings;

    public Email(IOptions<SmtpSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public void Enviar(string para, string assunto, string mensagem)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
        email.To.Add(MailboxAddress.Parse(para));
        email.Subject = assunto;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = mensagem
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        smtp.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
        smtp.Authenticate(_emailSettings.Username, _emailSettings.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }

}
