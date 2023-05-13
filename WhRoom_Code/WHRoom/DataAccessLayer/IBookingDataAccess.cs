using Microsoft.AspNetCore.Mvc;
using WHRoom.Models;

namespace WHRoom.DataAccessLayer
{
    public interface IBookingDataAccess
    {
        public  Task<APIResponceModel> SubmitCustomerBooking(BookingCustomer request);

     
    }
    
}
