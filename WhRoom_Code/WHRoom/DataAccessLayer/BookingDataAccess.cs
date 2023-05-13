using System.Data.SqlClient;
using WHRoom.Models;

namespace WHRoom.DataAccessLayer
{
    public class BookingDataAccess : IBookingDataAccess
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _mySqlConnection;

        public BookingDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);

        }

        public async Task<APIResponceModel> SubmitCustomerBooking(BookingCustomer request)
        {
            APIResponceModel response = new APIResponceModel();
            response.success = "SuccessFul";
            try
            {
                
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

               
                using (SqlCommand sqlCommand = new SqlCommand("addbookings", _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Name", request.Name);
                    sqlCommand.Parameters.AddWithValue("@Contact", request.Contact);
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@Aadhar", request.Aadhar);
                    sqlCommand.Parameters.AddWithValue("@Driving_Licence", request.Driving_Licence);
                    sqlCommand.Parameters.AddWithValue("@Booking_DateTime", request.Booking_DateTime);
                    sqlCommand.Parameters.AddWithValue("@Retrun_DateTime", request.Retrun_DateTime);
                    sqlCommand.Parameters.AddWithValue("@Car_Segments", request.Car_Segments);
                    sqlCommand.Parameters.AddWithValue("@Hatchback", request.Hatchback);
                    sqlCommand.Parameters.AddWithValue("@SUV", request.SUV);
                    sqlCommand.Parameters.AddWithValue("@Sedan", request.Sedan);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                       
                        response.success = "Error--Register Query Not Executed";
                        return response;
                    }
                    else
                    {
                        response.success = "Succesfully added";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
              
                response.success = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }


     



    }
}
