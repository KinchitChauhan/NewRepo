namespace WHRoom.Models.Auth
{
    public class Login : Comman.Comman
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string OTP { get; set; }
        public string OTPres { get; set; }
    }
}
