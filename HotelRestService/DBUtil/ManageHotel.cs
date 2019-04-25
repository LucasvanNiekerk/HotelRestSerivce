using System.Collections.Generic;
using System.Data.SqlClient;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageHotel
    {
        private SqlConnection connection = Utils.GetConnection();

        public List<Hotel> GetAllHotel()
        {
            string queryString = "SELECT * FROM Hotel;";
            List<Hotel> allHotel = new List<Hotel>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Hotel hotel = new Hotel();
                    hotel.Hotel_No = reader.GetInt32(0);
                    hotel.Name = reader.GetString(1);
                    hotel.Address = reader.GetString(2);

                    allHotel.Add(hotel);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return allHotel;
        }

        public Hotel GetHotelFromId(int hoteltNo)
        {
            string queryString = string.Format("SELECT * FROM Hotel WHERE Hotel_No = {0};", hoteltNo);
            Hotel hotel = new Hotel();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    hotel.Hotel_No = reader.GetInt32(0);
                    hotel.Name = reader.GetString(1);
                    hotel.Address = reader.GetString(2);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return hotel;
        }

        public bool CreateHotel(Hotel hotel)
        {
            string queryString = string.Format("INSERT INTO Hotel (Hotel_No, Name, Address) VALUES ({0} , '{1}', '{2}');", hotel.Hotel_No, hotel.Name, hotel.Address);

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

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            string queryString = string.Format("UPDATE Hotel SET Hotel_No = {0}, Name = '{1}', Address = '{2}' WHERE Hotel_No = {3};", hotel.Hotel_No, hotel.Name, hotel.Address, hotelNr);

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

        public bool DeleteHotel(int hotelNr)
        {
            string queryString = string.Format("DELETE FROM Hotel WHERE Hotel_No = {0};", hotelNr);

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
    }
}