using WHRoom.Models.Comman;

namespace WHRoom.Models.Auth
{
    public class Signup : Comman.Comman
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
    }
}
