
namespace store
{
    public static class QueryImplUtil
    {
        // Set an integer parameter
        public static void SetIntegerParameter(Query query, string param, long value)
        {
            SetParameter(query, param, value);
        }

        // Set a list of integer parameters
        public static void SetIntegerListParameter(Query query, string param, List<long> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<long> { 0L });
            }
            else
            {
                query.SetParameter(param, values);
            }
        }

        // Set a double parameter
        public static void SetDoubleParameter(Query query, string param, double value)
        {
            SetParameter(query, param, value);
        }

        // Set a list of double parameters
        public static void SetDoubleListParameter(Query query, string param, List<double> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<double> { 0.0 });
            }
            else
            {
                query.SetParameter(param, values);
            }
        }

        // Set an enum parameter
        public static void SetEnumParameter(Query query, string param, Enum value)
        {
            SetParameter(query, param, value);
        }

        // Set a list of enum parameters
        public static void SetEnumListParameter(Query query, string param, List<Enum> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<string> { "" });
            }
            else
            {
                var names = values.Select(v => v.ToString()).ToList();
                query.SetParameter(param, names);
            }
        }

        // Set a date parameter
        public static void SetDateParameter(Query query, string param, DateOnly value)
        {
            SetParameter(query, param, value == default ? DateTime.MinValue : value);
        }

        // Set a list of date parameters
        public static void SetDateListParameter(Query query, string param, List<DateOnly> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<string> { "" });
            }
            else
            {
                query.SetParameter(param, values);
            }
        }

        // Set a date-time parameter
        public static void SetDateTimeParameter(Query query, string param, DateTime value)
        {
            SetParameter(query, param, value);
        }

        // Set a list of date-time parameters
        public static void SetDateTimeListParameter(Query query, string param, List<DateTime> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<string> { "" });
            }
            else
            {
                query.SetParameter(param, values);
            }
        }

        // Set a string parameter
        public static void SetStringParameter(Query query, string param, string value)
        {
            SetParameter(query, param, value);
        }

        // Set a list of string parameters
        public static void SetStringListParameter(Query query, string param, List<string> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<string> { "" });
            }
            else
            {
                query.SetParameter(param, values);
            }
        }

        // Set a boolean parameter
        public static void SetBooleanParameter(Query query, string param, bool value)
        {
            SetParameter(query, param, value);
        }

        // Set a list of boolean parameters
        public static void SetBooleanListParameter(Query query, string param, List<bool> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<bool> { false });
            }
            else
            {
                query.SetParameter(param, values);
            }
        }

        // Set a database object parameter
        public static void SetDatabaseObjectParameter(Query query, string param, DatabaseObject value)
        {
            SetParameter(query, param, value);
        }

        // Set a list of database object parameters
        public static void SetDatabaseObjectListParameter(Query query, string param, List<DatabaseObject> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<long> { 0L });
            }
            else
            {
                var ids = values.Select(v => v.Id).ToList();
                query.SetParameter(param, ids);
            }
        }

        // Set a list of object parameters
        public static void SetObjectListParameter(Query query, string param, List<DatabaseObject> values)
        {
            if (values == null || !values.Any())
            {
                query.SetParameter(param, new List<long> { 0L });
            }
            else
            {
                var ids = values.Select(v => v.Id).ToList();
                query.SetParameter(param, ids);
            }
        }

        // Set a parameter for a database object
        public static void SetParameter(Query query, string name, DatabaseObject value)
        {
            if (value == null)
            {
                query.SetParameter(name, 0L);
            }
            else
            {
                query.SetParameter(name, value.Id);
            }
        }

        // Set a parameter for an enum
        public static void SetParameter(Query query, string name, Enum value)
        {
            if (value == null)
            {
                query.SetParameter(name, "");
            }
            else
            {
                query.SetParameter(name, value.ToString());
            }
        }

        // Set a parameter for a generic object
        public static void SetParameter(Query query, string name, object value)
        {
            if (value is TimeSpan duration)
            {
                value = duration.TotalMilliseconds;
            }
            query.SetParameter(name, value);
        }

        // Set a parameter for a date
        public static void SetParameter(Query query, string name, DateTime value)
        {
            query.SetParameter(name, value);
        }

        // Set a parameter for a string
        public static void SetParameter(Query query, string name, string value)
        {
            if (value == null)
            {
                query.SetParameter(name, "");
            }
            else
            {
                query.SetParameter(name, value);
            }
        }
    }
}