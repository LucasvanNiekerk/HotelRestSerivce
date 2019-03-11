using System.Data.SqlClient;

namespace HotelRestService.DBUtil
{
    public class Utils
    {
        //ToDo insert Connectionstring here:
        private const string ConnectionString = "";
        private static SqlConnection _sqlConnection;
        public static SqlConnection GetConnection()
        {
            return _sqlConnection ?? (_sqlConnection = new SqlConnection(ConnectionString));
        }
    }
}