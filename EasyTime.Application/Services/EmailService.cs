using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace EasyTime.Application.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _port = 587;
        private readonly string _senderEmail = "amirhosseinmakineh1379@gmail.com";
        //private readonly string _password = "wlzvkgwryllfiyk";
        private readonly string _password = "osbz qvom ddni nyhg";

        public async Task SendEmailAsync(string toEmail, string subject, string resetPasswordUrl)
        {
            try
            {
                string emailBody = $@"
<!DOCTYPE html>
<html lang='en'>
  <head>
    <meta charset='UTF-8' />
    <title>Reset Password</title>
  </head>
  <body style='font-family: Arial, sans-serif; background-color: #f2f2f2; padding: 30px;'>
    <div style='max-width: 500px; margin: auto; background-color: #ffffff; border-radius: 10px; padding: 20px; box-shadow: 0 0 10px rgba(0,0,0,0.1);'>
      <h2 style='text-align: center; color: #4CAF50;'>🔒 Reset Your Password</h2>
      <p>Hello,</p>
      <p>Click the button below to reset your password:</p>
      
      <div style='text-align: center; margin: 30px 0;'>
        <a href='{resetPasswordUrl}' style='background-color: #4CAF50; color: white; text-decoration: none; padding: 12px 24px; border-radius: 5px; display: inline-block;'>
          Reset Password
        </a>
      </div>

      <p>If you didn’t request this, you can ignore this email.</p>
      <p style='margin-top: 40px;'>Thanks,<br><strong>EasyTime Team</strong></p>
    </div>
  </body>
</html>";

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_senderEmail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = emailBody };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_senderEmail, _password); // باید App Password باشد
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Email Error] {ex.Message}");
                // یا اگر لاگر دارید:
                // _logger.LogError(ex, "Failed to send email.");
            }
        }

    }

}
