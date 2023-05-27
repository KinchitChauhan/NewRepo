using System;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WhRooms.DBContext;
using WhRooms.Models;
using WhRooms.Models.Comman;
using WhRooms.Models.DataAccessLayer;

namespace WhRooms.Controllers
{
    public class AuthController : Controller
    {
        public IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        
        WHRoomDevEntities objCon = new WHRoomDevEntities();
        // GET: Auth
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        #region Registration post method for data save    
        [HttpPost]
        public ActionResult Signup(UserRegistration request)
        {
            Response response = new Response();
            try
            {
               
                var IsExists= _authRepository.IsEmailExists(request.Email);
                if (IsExists)
                {
                    ModelState.AddModelError("Email Exists", "Email already Exists");
                    return View();
                }

            }
            catch
            {

            }
            return View(request);
            //// email not verified on registration time    
            //objUsr.EmailVerification = false;
            //var IsExists= IsEmailExists(objUsr.Email);
            //if (IsExists)
            //{
            //    ModelState.AddModelError("Email Exists", "Email already Exists");
            //    return View();
            //}
            ////it generate unique code       
            //objUsr.ActivetionCode = Guid.NewGuid();
            ////password convert    
            //objUsr.Password = encryptPassword.textToEncrypt(objUsr.Password);
            //objCon.Tbl_Users.Add(objUsr);
            //objCon.SaveChanges();
            //SendEmailToUser(objUsr.Email, objUsr.ActivetionCode.ToString());
            //var message = "Registration Complete.Please Check your Email:" + objUsr.Email;
            //ViewBag.Message = message;  
            //return View();
        }
        #endregion
        #region Check Email Exists or not in DB    
        public bool IsEmailExists(string eMail)
        {
            var IsCheck = objCon.Tbl_Users.Where(email => email.Email == eMail).FirstOrDefault();
            return IsCheck != null;
        }
        #endregion
        public void SendEmailToUser(string emailId, string activationCode)
        {
            var GenarateUserVerificationLink = "/Auth/UserVerification/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var fromMail = new MailAddress("Support@Whroom.in","WHRoom"); // set your email    
            //var fromEmailpassword = "fmgoavmndteriuml"; // Set your password     
            var toEmail = new MailAddress(emailId);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            System.Net.ServicePointManager.Expect100Continue = false;
            smtp.Credentials = new System.Net.NetworkCredential("Whroom.in@gmail.com", "fmgoavmndteriuml"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Registration Completed";
            Message.Body = "<br/> Your registration completed succesfully." +
                           "<br/> please click on the below link for account verification" +
                           "<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            smtp.Send(Message);

        }

        #region Verification from Email Account.    
        public ActionResult UserVerification(string id)
        {
          
            objCon.Configuration.ValidateOnSaveEnabled = false; // Ignor to password confirmation     
            var IsVerify = objCon.Tbl_Users.Where(u => u.ActivetionCode == new Guid(id)).FirstOrDefault();

            if (IsVerify != null)
            {
                IsVerify.EmailVerification = true;
                objCon.SaveChanges();
                ViewBag.Message = "Email Verification completed";
            }
            else
            {
                ViewBag.Message = "Invalid Request...Email not verify";
                ViewBag.Status = false;
            }

            return View();
        }
        #endregion
        [HttpPost]
        public ActionResult Login(UserLogin LgnUsr)
        {
            var _passWord = encryptPassword.textToEncrypt(LgnUsr.Password);
            bool Isvalid = objCon.Tbl_Users.Any(x => x.Email == LgnUsr.EmailId && x.EmailVerification == true &&
            x.Password == _passWord);
            if (Isvalid)
            {
                int timeout = LgnUsr.Rememberme ? 60 : 5; // Timeout in minutes, 60 = 1 hour.    
                var ticket = new FormsAuthenticationTicket(LgnUsr.EmailId, false, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Information... Please try again!");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword pass)
        {
            var IsExists = IsEmailExists(pass.EmailId);
            if (!IsExists)
            {
                ModelState.AddModelError("EmailNotExists", "This email is not exists");
                return View();
            }
            var objUsr = objCon.Tbl_Users.Where(x => x.Email == pass.EmailId).FirstOrDefault();

            // Genrate OTP     
            string OTP = GeneratePassword();

            objUsr.ActivetionCode = Guid.NewGuid();
            objUsr.OTP = OTP;
            objCon.Entry(objUsr).State = System.Data.Entity.EntityState.Modified;
            objCon.SaveChanges();

            //ForgetPasswordEmailToUser(objUsr.Email, objUsr.ActivetionCode.ToString(), objUsr.OTP);
            return View();
        }
        public string GeneratePassword()
        {
            string OTPLength = "4";
            string OTP = string.Empty;

            string Chars = string.Empty;
            Chars = "1,2,3,4,5,6,7,8,9,0";

            char[] seplitChar = { ',' };
            string[] arr = Chars.Split(seplitChar);
            string NewOTP = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                NewOTP += temp;
                OTP = NewOTP;
            }
            return OTP;
        }
    }
}