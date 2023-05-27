using Microsoft.AspNetCore.Mvc;
using Vonage;
using Vonage.Request;
using WHRoom.Models;


namespace WHRoom.Controllers
{
    public class SMSMessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMessage(Message message)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
    "d600a9da",
    "KQYUHFcE0d4OHgMG"
    );
            var VonageClient = new VonageClient(credentials);
            var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
            {
                To = message.To,
                From = "Support@Whroom.in",
                Text = message.ContentMsg
            });
            if (response != null && Convert.ToInt32(response.MessageCount) > 0 && response.Messages[0].StatusCode.ToString() == "Success")
            {
                message.Result = "Otp sent successfully!Plz Check your mail Box";
            }
            else
            {
                message.Result = "Message Failure. Please try your request again. ";
            }
          
            return View(message);
        }
    }
}
