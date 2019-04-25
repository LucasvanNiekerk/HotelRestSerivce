using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace HotelRestService.DBUtil
{
    public class ManageFacilityTest //Shitty namegiving
    {
        private SqlConnection connection = Utils.GetConnection();
        private static Dictionary<string, PropertyInfo[]> _propertyOfDifferentClasses = new Dictionary<string, PropertyInfo[]>();

        public List<T> GetAllOfType<T>(T type, string databaseTableName)
        {
            string queryString = string.Format("SELECT * FROM {0};", databaseTableName);

            List<T> resultList = new List<T>();

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            PropertyInfo[] arrayPropertyInfos = GetArrayOfPropeties(type);
            try
            {
                while (reader.Read())
                {
                    T result = GetObjectAndSetObjectFromType<T>(reader, arrayPropertyInfos);
                    resultList.Add(result);
                }
            }
            finally
            {
                connection.Close();
                reader.Close();
            }

            return resultList;
        }

        public T GetOneFromId<T>(T type, string databaseTableName, int id)
        {
            string queryString = string.Format("SELECT * FROM {0} WHERE Hotel_No = {1};", databaseTableName, id);
            var result = type;

            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            PropertyInfo[] arrayPropertyInfos = GetArrayOfPropeties(type);
            try
            {
                while (reader.Read())
                {
                    result = GetObjectAndSetObjectFromType<T>(reader, arrayPropertyInfos);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
            return result;
        }

        public bool Create<T>(T type, string databseTableName)
        {
            PropertyInfo[] arrayPropertyInfos = GetArrayOfPropeties(type);

            string queryString = GenerateCreateQueryString(databseTableName, arrayPropertyInfos);

            SqlCommand command = new SqlCommand(queryString, connection);

            AddWithValues(type, arrayPropertyInfos, command);

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

        public bool Update<T>(T type, string databseTableName, string primaryKeyName, int id)
        {
            PropertyInfo[] arrayPropertyInfos = GetArrayOfPropeties(type);

            string queryString = GenerateUpdateQueryString(databseTableName, arrayPropertyInfos, primaryKeyName, id);

            SqlCommand command = new SqlCommand(queryString, connection);

            AddWithValues(type, arrayPropertyInfos, command);

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

        public bool Delete(string databaseName, string primaryKeyName, int ID)
        {
            string queryString = string.Format("DELETE FROM {0} WHERE {1} = {2};", databaseName, primaryKeyName, ID);

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

        #region Help Methods
        private static T GetObjectAndSetObjectFromType<T>(SqlDataReader reader, PropertyInfo[] arrayPropertyInfos)
        {
            var result = (T)Activator.CreateInstance(typeof(T));
            for (int i = 0; i < arrayPropertyInfos.Length; i++)
            {
                if (arrayPropertyInfos[i].PropertyType == typeof(string))
                {
                    TrySetProperty(result, arrayPropertyInfos[i].Name, reader.GetString(i));
                }
                else if (arrayPropertyInfos[i].PropertyType == typeof(int))
                {
                    TrySetProperty(result, arrayPropertyInfos[i].Name, reader.GetInt32(i));
                }
                else if (arrayPropertyInfos[i].PropertyType == typeof(char))
                {
                    TrySetProperty(result, arrayPropertyInfos[i].Name, reader.GetString(i).First());
                }
                else if (arrayPropertyInfos[i].PropertyType == typeof(double))
                {
                    TrySetProperty(result, arrayPropertyInfos[i].Name, reader.GetDouble(i));
                }
                else if (arrayPropertyInfos[i].PropertyType == typeof(DateTime))
                {
                    TrySetProperty(result, arrayPropertyInfos[i].Name, reader.GetDateTime(i));
                }
                else if (arrayPropertyInfos[i].PropertyType == typeof(byte))
                {
                    TrySetProperty(result, arrayPropertyInfos[i].Name, reader.GetByte(i));
                }
                else if (arrayPropertyInfos[i].PropertyType == typeof(float))
                {
                    TrySetProperty(result, arrayPropertyInfos[i].Name, reader.GetFloat(i));
                }
                else if (arrayPropertyInfos[i].PropertyType == typeof(bool))
                {
                    TrySetProperty(result, arrayPropertyInfos[i].Name, reader.GetBoolean(i));
                }
            }

            return result;
        }

        private static void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
            }
        }
        
        private static PropertyInfo[] GetArrayOfPropeties<T>(T type)
        {
            Type tModelType = type.GetType();
            string tModelTypeName = tModelType.ToString();
            PropertyInfo[] arrayPropertyInfos;
            if (_propertyOfDifferentClasses.ContainsKey(tModelTypeName))
            {
                arrayPropertyInfos = _propertyOfDifferentClasses[tModelTypeName];
            }
            else
            {
                arrayPropertyInfos = tModelType.GetProperties();
                _propertyOfDifferentClasses.Add(tModelTypeName, arrayPropertyInfos);
            }

            return arrayPropertyInfos;
        }

        private static string GenerateCreateQueryString(string databseTableName, PropertyInfo[] arrayPropertyInfos)
        {
            string queryString = string.Format("INSERT INTO {0} (", databseTableName);
            for (int i = 0; i < arrayPropertyInfos.Length; i++)
            {
                queryString += arrayPropertyInfos[i].Name + ", ";
            }

            queryString = queryString.Remove(queryString.Length - 2);
            queryString += ") VALUES (";

            for (int i = 0; i < arrayPropertyInfos.Length; i++)
            {
                queryString += "@" + arrayPropertyInfos[i].Name + ", ";
            }

            queryString = queryString.Remove(queryString.Length - 2);
            queryString += ")";

            return queryString;
        }

        private static string GenerateUpdateQueryString(string databaseTableName, PropertyInfo[] arrayPropertyInfos, string primaryKeyName, int id)
        {
            string queryString = string.Format("UPDATE {0} SET ", databaseTableName);

            for (int i = 0; i < arrayPropertyInfos.Length; i++)
            {
                queryString += arrayPropertyInfos[i].Name + " = @" + arrayPropertyInfos[i].Name + ", ";
            }

            queryString = queryString.Remove(queryString.Length - 2);
            queryString += "WHERE " + primaryKeyName + " = " + id;

            return queryString;
        }

        private static void AddWithValues<T>(T type, PropertyInfo[] arrayPropertyInfos, SqlCommand command)
        {
            for (int i = 0; i < arrayPropertyInfos.Length; i++)
            {
                command.Parameters.AddWithValue(("@" + arrayPropertyInfos[i].Name), arrayPropertyInfos[i].GetValue(type));
            }
        }
        #endregion
    }
}