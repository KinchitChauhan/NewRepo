using System.ComponentModel.DataAnnotations;

namespace WHRoom.Models
{
    public class BookingCustomer
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Contact { get; set; }
        [Required]
        public string? Email { get; set; }
       
       
        public string? Aadhar { get; set; }
       
        public string? Driving_Licence { get; set; }
        public string? Booking_DateTime { get; set; }
        public string? Retrun_DateTime { get; set; }
        public int Car_Segments { get; set; }
        public int Hatchback { get; set; }
        public int SUV { get; set; }
        public int Sedan { get; set; }
       
    }
    public class fileUpload
    {
        public IFormFile? FormFile { get; set; }
        public string? FileName { get; set; }
       
    }
    
}
