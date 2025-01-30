using System.Data;

namespace store
{
    public class QueryImpl : Query
    {
        private readonly IDbConnection _connection;
        private readonly string _sql;
        private readonly Dictionary<string, object> _parameters;
        private readonly D3EPrimaryCache _cache;

        public QueryImpl(D3EPrimaryCache cache, IDbConnection connection, string sql)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
            _parameters = new Dictionary<string, object>();
        }

        // Returns the first object of type T based on the provided type.
        public T GetObjectFirstResult<T>(int type)
        {
            var result = GetResultList();
            if (result == null || result.Count == 0)
            {
                return default;
            }

            var firstItem = result[0];
            long id = ExtractId(firstItem);
            return _cache.GetOrCreate(type, id);
        }

        // Returns a list of objects of type T based on the provided type.
        public List<T> GetObjectResultList<T>(int type)
        {
            var result = GetResultList();
            if (result == null || result.Count == 0)
            {
                return new List<T>();
            }

            var objectList = new List<T>();
            foreach (var item in result)
            {
                long id = ExtractId(item);
                var obj = _cache.GetOrCreate(type, id);
                if (obj != null)
                {
                    objectList.Add(obj);
                }
            }
            return objectList;
        }

        // Extracts the ID from the result item.
        private long ExtractId(object item)
        {
            if (item is object[] array && array.Length > 0)
            {
                return Convert.ToInt64(array[0]);
            }
            return Convert.ToInt64(item);
        }

        // Executes the query and returns a list of raw results.
        public List<object> GetResultList()
        {
            var results = new List<object>();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = _sql;
                command.CommandType = CommandType.Text;

                // Add parameters to the command
                foreach (var param in _parameters)
                {
                    var dbParam = command.CreateParameter();
                    dbParam.ParameterName = param.Key;
                    dbParam.Value = param.Value;
                    command.Parameters.Add(dbParam);
                }

                // Execute the query
                _connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader.GetValue(i);
                        }
                        results.Add(row.Length == 1 ? row[0] : row);
                    }
                }
                _connection.Close();
            }

            return results;
        }

        // Executes the query and returns a single result.
        public object GetSingleResult()
        {
            var result = GetResultList();
            if (result == null || result.Count == 0)
            {
                throw new InvalidOperationException("No result found.");
            }
            if (result.Count > 1)
            {
                throw new InvalidOperationException("Multiple results found.");
            }
            return result[0];
        }

        // Executes an update query and returns the number of affected rows.
        public int ExecuteUpdate()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = _sql;
                command.CommandType = CommandType.Text;

                // Add parameters to the command
                foreach (var param in _parameters)
                {
                    var dbParam = command.CreateParameter();
                    dbParam.ParameterName = param.Key;
                    dbParam.Value = param.Value;
                    command.Parameters.Add(dbParam);
                }

                _connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                _connection.Close();

                return rowsAffected;
            }
        }

        // Sets a named parameter value.
        public Query SetParameter(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            _parameters[name] = value;
            return this;
        }
    }
}
