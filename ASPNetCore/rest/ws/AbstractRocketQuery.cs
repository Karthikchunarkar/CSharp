using gqltosql;

namespace rest.ws
{
    public abstract class AbstractRocketQuery
    {
        protected QueryResult SingleResult(string type, bool external, object value)
        {
            return SingleResult(type, external, value, null);
        }

        protected QueryResult SingleResult(string type, bool external, object value, IDisposable changeTracker)
        {
            var r = new QueryResult
            {
                Type = type,
                External = external,
                Value = value,
                ChangeTracker = changeTracker
            };
            return r;
        }

        protected QueryResult ListResult(string type, bool external, object value, IDisposable changeTracker)
        {
            var r = new QueryResult
            {
                Type = type,
                External = external,
                IsList = true,
                Value = value,
                ChangeTracker = changeTracker
            };
            return r;
        }

        protected static Field Inspect(Field field, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return field;
            }
            string[] subFields = path.Split('.');
            return Inspect(field, 0, subFields);
        }

        protected static Field Inspect(Field field, int i, params string[] subFields)
        {
            if (i == subFields.Length)
            {
                return field;
            }
            foreach (var s in field.SelectionSet.Selections)
            {
                if (s is Field f && f.Name == subFields[i])
                {
                    return Inspect(f, i + 1, subFields);
                }
            }
            return null;
        }

        public static Field Inspect2(Field field, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return field;
            }
            string[] subFields = path.Split('.');
            return Inspect2(field, 0, subFields);
        }

        protected static Field Inspect2(Field field, int i, params string[] subFields)
        {
            if (i == subFields.Length)
            {
                return field;
            }
            foreach (var s in field.Selections)
            {
                var fields = s.Fields;
                foreach (var f in fields)
                {
                    if (f.FieldVar.Name == subFields[i])
                    {
                        var res = Inspect2(f, i + 1, subFields);
                        if (res != null)
                        {
                            return res;
                        }
                    }
                }
            }
            return null;
        }
    }
}
