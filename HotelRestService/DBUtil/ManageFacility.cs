using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageFacility : IManageFacility
    {
        private const string connectionString = "Data Source=luca1921server.database.windows.net;Initial Catalog=Luca1921;User ID=luca1921;Password=password1921!;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection connection = Utils.GetConnection(connectionString);

        public List<Facility> GetAllFacilities()
        {
            string queryString = "SELECT * FROM Facility;";
            List<Facility> facilityList = new List<Facility>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Facility facility = new Facility();
                    facility.Hotel_No = reader.GetInt32(0);
                    facility.Bar = reader.GetString(1).First();
                    facility.TableTennis = reader.GetString(2).First();
                    facility.PoolTable = reader.GetString(3).First();
                    facility.Restaurant = reader.GetString(4).First();
                    facility.SwimmingPool = reader.GetString(5).First();
                    facilityList.Add(facility);
                }
            }
            finally
            {
                connection.Close();
                reader.Close();
            }

            return facilityList;
        }
        public Facility GetFacilityFromId(int hotel_No)
        {
            string queryString = string.Format("SELECT * FROM Facility WHERE Hotel_No = {0};", hotel_No);
            Facility facility = new Facility();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    facility.Hotel_No = reader.GetInt32(0);
                    facility.Bar = reader.GetString(1).First();
                    facility.TableTennis = reader.GetString(2).First();
                    facility.PoolTable = reader.GetString(3).First();
                    facility.Restaurant = reader.GetString(4).First();
                    facility.SwimmingPool = reader.GetString(5).First();
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
            return facility;
        }

        public bool CreateFacility(Facility facility)
        {
            string queryString = string.Format("INSERT INTO Facility (Hotel_No, Swimming_Pool, Bar, Table_Tennis, Pool_Table, Restaurant) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}');", facility.Hotel_No, facility.SwimmingPool, facility.Bar, facility.TableTennis, facility.PoolTable, facility.Restaurant);

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

        public bool UpdateFacility(Facility facility, int hotel_No)
        {
            string queryString = string.Format("UPDATE Facility SET Hotel_No = {0}, Swimming_Pool = '{1}', Bar = '{2}', Table_Tennis = '{3}', Pool_Table = '{4}', Restaurant = '{5}'  WHERE Hotel_No = {6}", facility.Hotel_No, facility.SwimmingPool, facility.Bar, facility.TableTennis, facility.PoolTable, facility.Restaurant, hotel_No);

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

        public bool DeleteFacility(int hotel_No)
        {
            string queryString = string.Format("DELETE FROM Facility WHERE Hotel_No = {0};", hotel_No);

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