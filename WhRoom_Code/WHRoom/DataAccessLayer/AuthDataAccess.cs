using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using WHRoom.Models;
using WHRoom.Models.Auth;

namespace WHRoom.DataAccessLayer
{
    public class AuthDataAccess : IAuthDataAccess
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _mySqlConnection;
        public AuthDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);

        }
        public async Task<Response> Signup(Signup request)
        {
            Response response = new Response();
            response.res = "SuccessFul";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }


                using (SqlCommand sqlCommand = new SqlCommand("addUsers", _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                    sqlCommand.Parameters.AddWithValue("@ConfirmPassword", request.ConfirmPassword);
                    sqlCommand.Parameters.AddWithValue("@MachineName", Environment.MachineName);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status == -1)
                    {
                        response.res = "EmailID Already exists";
                        return response;
                    }
                    else
                    {

                        response.res = "Succesfully added and Email send please check your email.";
                        MailMessage mail = new MailMessage();
                        mail.To.Add(request.Email);
                        mail.From = new MailAddress("Support@Whroom.in");
                        mail.Subject = "New Account Created";
                        string Body = "Your Account Created and Your Cred are" + "" + request.Email + "" + request.Password;
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
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {

                response.res = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }
        public async Task<Response> Login(Login request)
        {
            Response response = new Response();
            response.res = "SuccessFul";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (SqlCommand sqlCommand = new SqlCommand("getPassword", _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                    SqlParameter outputParam = sqlCommand.Parameters.Add("@out", SqlDbType.VarChar,255);
                    outputParam.Direction = ParameterDirection.Output;


                    int Status= await sqlCommand.ExecuteNonQueryAsync();
                   
                    string? Password = sqlCommand.Parameters["@out"].Value.ToString();
                    if (Password == request.Password)
                    {
                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                        request.OTP = GenerateRandomOTP(6, saAllowedCharacters);
                        using (SqlCommand sqlCommand1 = new SqlCommand("otpGenerateSave", _mySqlConnection))
                        {
                            sqlCommand1.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand1.CommandTimeout = 180;
                            sqlCommand1.Parameters.AddWithValue("@Email", request.Email);
                            sqlCommand1.Parameters.AddWithValue("@OTP", request.OTP);
                            int Status1 = await sqlCommand1.ExecuteNonQueryAsync();
                            MailMessage mail = new MailMessage();
                            mail.To.Add(request.Email);
                            mail.From = new MailAddress("Support@Whroom.in");
                            mail.Subject = "OTP";
                            string Body = "Your One Time Password is" + "" + request.OTP;
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
                            response.res = "OTP send succesfully please check your email box";
                            return response;

                        }

                    }
                    else
                    {
                        response.res = "Password not valid!";
                        return response;
                    }
                }

                
                ///method for generate otp and save in db
               
                



            }
            catch(Exception ex)
            {

            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }
    
        public async Task<Response> LoginwithOtp(Login request)
        {
            Response response = new Response();
            response.res = "SuccessFul";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }


                using (SqlCommand sqlCommand = new SqlCommand("checkUserOtp", _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@OTP", request.OTP);
                    SqlParameter outputParam = sqlCommand.Parameters.Add("@otpout", SqlDbType.VarChar, 255);
                    outputParam.Direction = ParameterDirection.Output;
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    string? otpout = sqlCommand.Parameters["@otpout"].Value.ToString();
                    if (otpout == "otp ok")
                    {
                        ///method for generate otp and save in db



                        MailMessage mail = new MailMessage();
                        mail.To.Add(request.Email);
                        mail.From = new MailAddress("Support@Whroom.in");
                        mail.Subject = "OTP";
                        string Body = "Login Ok";
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
                        response.res = "Login  succesfully please check your email box";
                        return response;
                    }
                    else if (otpout == "otp not ok")
                    {
                        response.res = "OTP not  Match want to resend!";
                        return response;

                    }
                }
                

                





            }

            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }
        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)

        {

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
    }
}
