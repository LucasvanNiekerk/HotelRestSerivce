using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageRoom
    {
        private SqlConnection connection = Utils.GetConnection();

        public List<Room> GetAllRooms()
        {
            string queryString = "SELECT * FROM Room;";
            List<Room> roomList = new List<Room>();


            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Room room = new Room();
                    room.Room_No = reader.GetInt32(0);
                    room.Hotel_No = reader.GetInt32(1);
                    room.Type = reader.GetString(2).First();
                    room.Price = reader.GetDouble(3);

                    roomList.Add(room);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return roomList;
        }

        public Room GetRoomFromID(int roomNr, int hotelNr)
        {
            string queryString = string.Format("SELECT * FROM Room WHERE Room_No = {0} AND Hotel_No = {1};", roomNr, hotelNr);
            Room room = new Room();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    room.Room_No = reader.GetInt32(0);
                    room.Hotel_No = reader.GetInt32(1);
                    room.Type = reader.GetString(2).First();
                    room.Price = reader.GetDouble(3);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return room;
        }

        public bool CreateRoom(Room room)
        {
            string queryString = string.Format("INSERT INTO Room (Room_No, Hotel_No, Types, Price) VALUES ({0}, {1}, '{2}', {3});", room.Room_No, room.Hotel_No, room.Type, room.Price);

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

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            string queryString = string.Format("UPDATE Room SET Room_No = {0}, Hotel_No = {1}, Types = '{2}', Price = {3} WHERE Room_No = {4} AND hotel_No = {5};", room.Room_No, room.Hotel_No, room.Type, room.Price, roomNr, hotelNr);

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

        public bool DeleteRoom(int roomNr, int hotelNr)
        {
            string queryString = string.Format("DELETE FROM Room WHERE Room_No = {0} AND Hotel_No = {1};", roomNr, hotelNr);

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