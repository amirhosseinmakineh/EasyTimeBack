using Kavenegar;
using Microsoft.Extensions.Configuration;

namespace EasyTime.Utilities.Sender
{
    public class SmsSender
    {
        private readonly string _sender;
        private readonly string _apiKey;

        public SmsSender(IConfiguration configuration)
        {
            _sender = configuration["Sms:Sender"] ?? string.Empty;
            _apiKey = configuration["Sms:ApiKey"] ?? string.Empty;
        }

        public Task SendSms(string receptor, string message)
        {
            var api = new KavenegarApi(_apiKey);
            api.Send(_sender, receptor, message);
            return Task.CompletedTask;
        }
    }
}
