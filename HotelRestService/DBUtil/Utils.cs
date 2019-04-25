using System;
using System.Data.SqlClient;
using System.Reflection;

namespace HotelRestService.DBUtil
{
    public class Utils
    {
        //ToDo insert Connectionstring here:
        private const string ConnectionString = "Data Source=luca1921server.database.windows.net;Initial Catalog=Luca1921;User ID=luca1921;Password=password1921!;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static SqlConnection _sqlConnection;
        public static SqlConnection GetConnection()
        { //If SqlConnection is null return new SqlConnection else return sqlConnection
            return _sqlConnection == null ? new SqlConnection(ConnectionString) : _sqlConnection;
        }

        public static class GenericPropertyFinder<TModel>
        {
            public static PropertyInfo[] PrintTModelPropertyAndValue<T>(T type)
            {
                //Getting Type of Generic Class Model
                Type tModelType = type.GetType();

                //We will be defining a PropertyInfo Object which contains details about the class property 
                PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();


                return arrayPropertyInfos;
            }
        }

        public static void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
            }
        }
    }
}