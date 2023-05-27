using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhRooms.Models.DataAccessLayer
{
    public interface IAuthRepository
    {
         bool IsEmailExists(string eMail);


    }
}
