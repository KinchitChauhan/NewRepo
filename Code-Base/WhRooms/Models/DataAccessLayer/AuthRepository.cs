using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WhRooms.DBContext;

namespace WhRooms.Models.DataAccessLayer
{
    public class AuthRepository: IAuthRepository
    {
        WHRoomDevEntities objCon = new WHRoomDevEntities();
        
        public bool IsEmailExists(string eMail)
        {
            var IsCheck = objCon.Tbl_Users.Where(email => email.Email == eMail).FirstOrDefault();
            return IsCheck != null;
        }
    }
}