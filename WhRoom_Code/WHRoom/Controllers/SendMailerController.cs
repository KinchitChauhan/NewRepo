using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace WHRoom.Controllers
{
    public class SendMailerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Index(WHRoom.Models.MailModel _objModelMail)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                System.Net.ServicePointManager.Expect100Continue = false;
                smtp.Credentials = new System.Net.NetworkCredential("Whroom.in@gmail.com", "fmgoavmndteriuml"); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                _objModelMail.Result = "Mail Sent Succesfuly Check your mail box!";
                return View(_objModelMail);
            }
            else
            {
                return View();
            }
        }
    }
}
 