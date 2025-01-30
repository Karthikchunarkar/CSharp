using d3e.core;
using store;
using Store;

namespace list
{
    public abstract class AbsDataQueryImpl
    {
        protected void SetIntegerParameter(Query query, string param, long value)
        {
            QueryImplUtil.SetIntegerParameter(query, param, value);
        }

        protected void SetIntegerListParameter(Query query, string param, List<long> value)
        {
            QueryImplUtil.SetIntegerListParameter(query, param, value);
        }

        protected void SetDoubleParameter(Query query, string param, double value)
        {
            QueryImplUtil.SetDoubleParameter(query, param, value);
        }

        protected void SetDoubleListParameter(Query query, string param, List<double> value)
        {
            QueryImplUtil.SetDoubleListParameter(query, param, value);
        }


        protected void SetEnumParameter(Query query, string param, Enum value)
        {
            QueryImplUtil.SetEnumParameter(query, param, value);
        }

        protected void SetEnumListParameter(Query query, string param, List<Enum> value)
        {
            QueryImplUtil.SetEnumListParameter(query, param, value);
        }

        protected void SetDateParameter(Query query, string param, DateOnly value)
        {
            QueryImplUtil.SetDateParameter(query, param, value);
        }

        protected void SetDateListParameter(Query query, string param, List<DateOnly> value)
        {
            QueryImplUtil.SetDateListParameter(query, param, value);
        }

        protected void SetDateTimeParameter(Query query, string param, DateTime value)
        {
            QueryImplUtil.SetDateTimeParameter(query, param, value);
        }

        protected void SetDateTimeListParameter(Query query, string param, List<DateTime> value)
        {
            QueryImplUtil.SetDateTimeListParameter(query, param, value);
        }

        protected void SetStringParameter(Query query, string param, string value)
        {
            QueryImplUtil.SetStringParameter(query, param, value);
        }

        protected void SetStringListParameter(Query query, string param, List<string> value)
        {
            QueryImplUtil.SetStringListParameter(query, param, value);
        }

        protected void SetBooleanParameter(Query query, string param, bool value)
        {
            QueryImplUtil.SetBooleanParameter(query, param, value);
        }

        protected void SetBooleanListParameter(Query query, string param, List<bool> value)
        {
            QueryImplUtil.SetBooleanListParameter(query, param, value);
        }

        protected void SetTimeParameter(Query query, string param, TimeSpan value)
        {
            QueryImplUtil.SetParameter(query, param, value);
        }

        protected void SetGeolocationParameter(Query query, string param, Geolocation value)
        {
            if (value == null)
            {
                QueryImplUtil.SetDoubleParameter(query, param + "_lat", 0);
                QueryImplUtil.SetDoubleParameter(query, param + "_lng", 0);
            }
            else
            {
                QueryImplUtil.SetDoubleParameter(query, param + "_lat", value.Latitude);
                QueryImplUtil.SetDoubleParameter(query, param + "_lng", value.Longitude);
            }
        }

        protected void SetDatabaseObjectParameter(Query query, string param, DatabaseObject value)
        {
            QueryImplUtil.SetDatabaseObjectParameter(query, param, value);
        }

        protected void SetDatabaseObjectListParameter(Query query, string param, List<DatabaseObject> value)
        {
            QueryImplUtil.SetDatabaseObjectListParameter(query, param, value);
        }

        protected void SetObjectListParameter(Query query, string param, List<DatabaseObject> value)
        {
            QueryImplUtil.SetObjectListParameter(query, param, value);
        }

        protected string Like(string val)
        {
            if (val == null)
            {
                return "%%";
            }
            else
            {
                return "%" + val + "%";
            }
        }

        protected void AssertLimitNotNegative(long limit)
        {
            if (limit < 0)
            {
                throw new Exception("Limit is negative.");
            }
        }

        protected void LogQuery(string sql, Query query)
        {
            Log.Query(sql, query);
        }

        protected long GetCountResult(Query query)
        {
            try
            {
                object result = query.GetSingleResult();
                return (long)result;
            }
            catch (Exception e)
            {
                Log.PrintStackTrace(e);
                return 0;
            }
        }
    }
}
