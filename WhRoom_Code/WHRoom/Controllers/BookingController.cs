using Microsoft.AspNetCore.Mvc;
using WHRoom.DataAccessLayer;
using WHRoom.Models;

namespace WHRoom.Controllers
{
    //Git Check
    public class BookingController : Controller
    {
        public readonly IBookingDataAccess _bookingDataAccess;

        public BookingController(IBookingDataAccess bookingDataAccess)
        {
            _bookingDataAccess = bookingDataAccess;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitCustomerBooking(BookingCustomer request)
        {
            APIResponceModel response = new APIResponceModel();
            try
            {
                response=await _bookingDataAccess.SubmitCustomerBooking(request);
            }
            catch
            {

            }
            return Ok(response);
        }

     
    }
}
