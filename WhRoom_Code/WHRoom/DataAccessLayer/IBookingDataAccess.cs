using Microsoft.AspNetCore.Mvc;
using WHRoom.Models;
using WHRoom.Models.Auth;

namespace WHRoom.DataAccessLayer
{
    public interface IBookingDataAccess
    {
        public Task<Response> SubmitCustomerBooking(BookingCustomer request);
       

     
    }
    
}
