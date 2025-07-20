using Kavenegar;
namespace EasyTime.Utilities.Sender
{
    public static class SmsSender
    {
        public static  async Task SendSms(string receptor,string message)
        {
            string sender = "2000660110";
            var reciver = receptor;
            var bodyMesage = message;
            var api = new KavenegarApi("58445A64466F4F7663796A415A314C716F7363374277534E70526D654D614B2F56393636777457466862593D");
            api.Send(sender, reciver, bodyMesage);
        }
    }
}
