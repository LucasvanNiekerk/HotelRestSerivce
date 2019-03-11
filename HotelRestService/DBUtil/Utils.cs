using System.Data.SqlClient;

namespace HotelRestService.DBUtil
{
    public class Utils
    {
        //ToDo insert Connectionstring here:
        private const string ConnectionString = "";
        private static SqlConnection _sqlConnection;
        public static SqlConnection GetConnection()
        { //If SqlConnection is null return new SqlConnection else return sqlConnection
            return _sqlConnection == null ? new SqlConnection(ConnectionString) : _sqlConnection;
        }
    }
}