using MailKit.Security;
using MimeKit;
using ProjetoMVC.Api.Helper;
using ProjetoMVC.Api.Helper.Interfaces;

public class Email : IEmail
{
    private readonly SmtpSettings _smtpSettings;

    public Email(SmtpSettings smtpSettings)
    {
        _smtpSettings = smtpSettings;
    }

    public void Enviar(string para, string assunto, string mensagem)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
        email.To.Add(MailboxAddress.Parse(para));
        email.Subject = assunto;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = mensagem
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        smtp.Connect(_smtpSettings.SmtpServer, _smtpSettings.SmtpPort, SecureSocketOptions.StartTls);
        smtp.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }

}
