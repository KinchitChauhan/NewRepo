using WHRoom.Models;
using WHRoom.Models.Auth;

namespace WHRoom.DataAccessLayer
{
    public interface IAuthDataAccess
    {
        public Task<Response> Signup(Signup request);
        public Task<Response> Login(Login request);
        public Task<Response> LoginwithOtp(Login request);
    }
}
