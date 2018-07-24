using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Portfoglio.Models
{
    public class EmailSendService : ISendService
    {
        public async Task SendAsync(IMessage message)
        {
            var emailMessage = new MimeMessage
            {
                Subject = message.Title,
                Body = new TextPart(TextFormat.Html)
                {
                    Text = message.Text
                }
            };

            emailMessage.From.AddRange(message.From.Select(item => new MailboxAddress(Encoding.UTF8, message.Name, item)).ToList());
            emailMessage.To.AddRange(message.To.Select(item => new MailboxAddress(Encoding.UTF8, "", item)).ToList());

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp address server", 0, SecureSocketOptions.Auto);
                await client.AuthenticateAsync("login email", "password email");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }

        }
    }
}