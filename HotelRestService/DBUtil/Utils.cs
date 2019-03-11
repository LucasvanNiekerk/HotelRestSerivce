using System.Data.SqlClient;

namespace HotelRestService.DBUtil
{
    public class Utils
    {
        private static SqlConnection _sqlConnection;
        public static SqlConnection GetConnection(string connectionString)
        {
            return _sqlConnection ?? (_sqlConnection = new SqlConnection(connectionString));
        }
    }
}