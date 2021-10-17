using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.IO;
using System.Security.Authentication;
using System.Threading;

namespace EmailSender
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("Loading Settings");
        Settings settings = Settings.Load(AppDomain.CurrentDomain.BaseDirectory + "\\" + args[0]);
        Console.WriteLine("Creating Message");
        MimeMessage message1 = new MimeMessage();
        message1.From.Add((InternetAddress) new MailboxAddress("", settings.From.Email));
        message1.To.Add((InternetAddress) new MailboxAddress("", settings.To));
        message1.Subject = settings.Subject;
        message1.Headers.Add("X-Confirm-Reading-To", settings.From.Email);
        message1.Headers.Add("Disposition-Notification-To", settings.From.Email);
        message1.Body = (MimeEntity) new TextPart("html")
        {
          Text = settings.Body
        };
        Console.WriteLine("Connecting To Server");
        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + args[0] + ".log"))
          File.Delete(AppDomain.CurrentDomain.BaseDirectory + args[0] + ".log");
        using (SmtpClient smtpClient = settings.Log ? new SmtpClient((IProtocolLogger) new ProtocolLogger(AppDomain.CurrentDomain.BaseDirectory + args[0] + ".log", false)) : new SmtpClient())
        {
          if (settings.From.useSSL)
          {
            smtpClient.SslProtocols = SslProtocols.Default | SslProtocols.Tls11 | SslProtocols.Tls12;
            smtpClient.Connect(settings.From.Host, settings.From.Port, SecureSocketOptions.SslOnConnect, new CancellationToken());
          }
          else
            smtpClient.Connect(settings.From.Host, settings.From.Port, SecureSocketOptions.StartTlsWhenAvailable, new CancellationToken());
          smtpClient.Authenticate(settings.From.User, settings.From.Pass, new CancellationToken());
          Console.WriteLine("Sending Message");
          smtpClient.Send(message1, new CancellationToken(), (ITransferProgress) null);
          Console.WriteLine("Message Sent");
          if (settings.MessageSentConfirmation)
          {
            Console.WriteLine("Creating MessageSentConfirmation");
            MimeMessage message2 = new MimeMessage();
            message2.From.Add((InternetAddress) new MailboxAddress("", settings.From.Email));
            message2.To.Add((InternetAddress) new MailboxAddress("", settings.From.Email));
            message2.Subject = settings.Subject + " - Message Sent";
            message2.Body = (MimeEntity) new TextPart("html")
            {
              Text = ("Message Body: <br /><br />" + settings.Body)
            };
            Console.WriteLine("Sending MessageSentConfirmation");
            smtpClient.Send(message2, new CancellationToken(), (ITransferProgress) null);
            Console.WriteLine("Sent MessageSentConfirmation");
          }
          smtpClient.Disconnect(true, new CancellationToken());
        }
      }
      catch
      {
      }
    }
  }
}
