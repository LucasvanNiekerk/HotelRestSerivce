using System.Collections.Generic;
using System.Data.SqlClient;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageGuest
    {
        private SqlConnection connection = Utils.GetConnection();

        public List<Guest> GetAllGuest()
        {
            string queryString = "SELECT * FROM Guest;";
            List<Guest> allGuests = new List<Guest>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Guest guest = new Guest();
                    guest.Guest_No = reader.GetInt32(0);
                    guest.Name = reader.GetString(1);
                    guest.Address = reader.GetString(2);
                    allGuests.Add(guest);
                }
            }
            finally
            {
                connection.Close();
                reader.Close();
            }

            return allGuests;
        }

        public Guest GetGuestFromId(int guestNr)
        {
            string queryString = string.Format("SELECT * FROM Guest WHERE Guest_No = {0};", guestNr);
            Guest guest = new Guest();

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    guest.Guest_No = reader.GetInt32(0);
                    guest.Name = reader.GetString(1);
                    guest.Address = reader.GetString(2);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return guest;
        }

        public bool CreateGuest(Guest guest)
        {
            string queryString = string.Format("INSERT INTO Guest (Guest_No, Name, Address) VALUES ({0}, '{1}', '{2}');", guest.Guest_No, guest.Name, guest.Address);

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
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

        public bool UpdateGuest(Guest guest, int guestNr)
        {
            string queryString = string.Format("UPDATE Guest SET Guest_No = {0}, Name = '{1}', Address = '{2}' WHERE Guest_No = {3};", guest.Guest_No, guest.Name, guest.Address, guestNr);

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

        public bool DeleteGuest(int guestNr)
        {
            string queryString = string.Format("DELETE FROM Guest WHERE Guest_No = {0};", guestNr);

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            try
            {
                return command.ExecuteNonQuery() == 0 ? false : true;
            } //ToDo delete all bookings related to guest being deleted.
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