using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageBooking
    {
        private SqlConnection connection = Utils.GetConnection();
        
        public List<Booking> GetAllBookings()
        {
            string queryString = "SELECT * FROM Booking;";
            List<Booking> bookingList = new List<Booking>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Booking booking = new Booking();
                    booking.BookingID = reader.GetInt32(0);
                    booking.Hotel_No = reader.GetInt32(1);
                    booking.Guest_No = reader.GetInt32(2);
                    booking.Date_From = reader.GetDateTime(3);
                    booking.Date_To = reader.GetDateTime(4);
                    booking.Room_No = reader.GetInt32(5);

                    bookingList.Add(booking);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return bookingList;
        }

        public Booking GetBookingFromID(int bookingId)
        {
            string queryString = string.Format("SELECT * FROM Booking WHERE Booking_Id = {0};", bookingId);
            Booking booking = new Booking();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    booking.BookingID = reader.GetInt32(0);
                    booking.Hotel_No = reader.GetInt32(1);
                    booking.Guest_No = reader.GetInt32(2);
                    booking.Date_From = reader.GetDateTime(3);
                    booking.Date_To = reader.GetDateTime(4);
                    booking.Room_No = reader.GetInt32(5);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return booking;

        }

        public bool CreateBooking(Booking booking)
        {
            string queryString = string.Format("INSERT INTO Booking (Hotel_No, Guest_No, Date_From, Date_To, Room_No) VALUES ({0}, {1}, cast('{2}' AS DATE), cast('{3}' AS DATE), {4});", booking.Hotel_No, booking.Guest_No, booking.Date_From.ToString("yyyy/MM/dd"), booking.Date_To.ToString("yyyy/MM/dd"), booking.Room_No);

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                command.Connection.Close();
            }

        }

        public bool UpdateBooking(Booking booking, int bookingId)
        {
            string queryString = string.Format("UPDATE Booking SET Hotel_No = {0}, Guest_No = {1}, Date_From = cast('{2}' AS DATE), Date_To = cast('{3}' AS DATE), Room_No = {4} WHERE Booking_id = {5};", booking.Hotel_No, booking.Guest_No, booking.Date_From.ToString("yyyy/MM/dd"), booking.Date_To.ToString("yyyy/MM/dd"), booking.Room_No, bookingId);

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        public bool DeleteBooking(int bookingId)
        {
            string queryString = string.Format("DELETE FROM Booking WHERE Booking_id = {0};", bookingId);

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            try
            { //Hvis intet blev slette, fordi det ikke eksiterede, returner vi false ellers hvis vi slettede noget returner vi true
                return command.ExecuteNonQuery() == 0 ? false : true;
            }
            catch
            {//Hvis vi fejler returner vi false.
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}