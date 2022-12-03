using System.Data.SQLite;
using System.Reflection;
using System.Text;

namespace QueryBuilder
{
    public class QueryBuilder : IDisposable
    {
        private SQLiteConnection connection;

        public QueryBuilder(string locationOfDatabase)
        {
            // Create a new database connection:
            connection = new SQLiteConnection("Data Source=" + locationOfDatabase + "; Version = 3; New = True; Compress = True; ");

            // Open the connection:
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            { 
                Console.WriteLine("There was an error opening the SQL database connection.");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error closing the SQL database connection.");
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public T Read<T>(int id) where T : IClassModel, new()
        {
            //Create a command object
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}" +
                                  $" WHERE Id = " + id.ToString();
            var reader = command.ExecuteReader();
            T data = new T();

            while (reader.Read())
            {
                for (int d = 0; d < reader.FieldCount; d++)
                {
                    if (typeof(T).GetProperty(reader.GetName(d)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(d)).SetValue(data, Convert.ToInt32(reader.GetValue(d)));
                    else
                        typeof(T).GetProperty(reader.GetName(d)).SetValue(data, reader.GetValue(d));
                }
            }

            return data;
        }
        public List<T> ReadAll<T>() where T : IClassModel, new()
        {
            //Create a command object
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}";
            var reader = command.ExecuteReader();
            T data;
            var multiData = new List<T>();

            while (reader.Read())
            {
                data = new T();
                for (int d = 0; d < reader.FieldCount; d++)
                {
                    if (typeof(T).GetProperty(reader.GetName(d)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(d)).SetValue(data, Convert.ToInt32(reader.GetValue(d)));
                    else
                        typeof(T).GetProperty(reader.GetName(d)).SetValue(data, reader.GetValue(d));
                }
                multiData.Add(data);
            }

            return multiData;
        }
        public void Create<T>(T obj)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> values = new List<string>();
            List<string> names = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj) + "\"");
                }
                else
                {
                    values.Add(property.GetValue(obj).ToString());
                }
                names.Add(property.Name);
            }

            //Formatting a string to make it work in SQLite as a command
            StringBuilder sbValues = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();

            // string updateString = "";
            for (int i = 0; i < values.Count; i++)
            {
                // updateString += $"{names[i]} = {values[i]},"
                // updateString = updateString.TrimRight(",");

                //we dont want commas for the last value
                if (i == values.Count - 1)
                {
                    sbValues.Append($"{values[i]}");
                    sbNames.Append($"{names[i]}");
                }
                else
                {
                    sbValues.Append($"{values[i]}, ");
                    sbNames.Append($" {names[i]}, ");
                }
            }

            var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO {typeof(T).Name} ({sbNames}) VALUES ({sbValues})";
            command.ExecuteNonQuery();
        }                

        public void Update<T>(T obj) where T : IClassModel
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> values = new List<string>();
            List<string> names = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj) + "\"");
                }
                else
                {
                    values.Add(property.GetValue(obj).ToString());
                }
                names.Add(property.Name);
            }

            //Formatting a string to make it work in SQLite as a command
            string updateString = "";
            for (int i = 0; i < values.Count; i++)
            {
                updateString += $"{names[i]} = {values[i]},";
            }
            updateString = updateString.TrimEnd(',');
            var command = connection.CreateCommand();
            command.CommandText = $"UPDATE {typeof(T).Name} SET {updateString} WHERE Id = {obj.Id}";
            command.ExecuteNonQuery();
        }

        public void Delete<T>(T obj) where T: IClassModel
        {

            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {typeof(T).Name} WHERE Id = {obj.Id}";
            command.ExecuteNonQuery();
        }
    }
}