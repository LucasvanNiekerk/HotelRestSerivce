using System.Data.SqlClient;

namespace HotelRestService.DBUtil
{
    public class Utils
    {
        private const string connectionString = "Data Source=luca1921server.database.windows.net;Initial Catalog=Luca1921;User ID=luca1921;Password=password1921!;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static SqlConnection _sqlConnection;
        public static SqlConnection GetConnection()
        {
            return _sqlConnection ?? (_sqlConnection = new SqlConnection(connectionString));
        }
    }
}