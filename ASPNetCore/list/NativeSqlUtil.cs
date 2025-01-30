using Newtonsoft.Json.Linq;
using gqltosql;
using store;

namespace list
{
    public class NativeSqlUtil
    {
        public static HashSet<long> GetAllIds(IEnumerable<NativeObj> rows)
        {
            HashSet<long> ids = new HashSet<long>();
            foreach (var s in rows)
            {
                ids.Add(s.Id);
            }
            return ids;
        }

        public static List<T> Sort<T>(List<T> objs, List<NativeObj> rows) where T : DatabaseObject
        {
            var objById = objs.ToDictionary(s => s.Id, s => s);
            return rows.Select(r => objById[r.Id]).ToList();
        }

        public static JArray Sort(JArray objs, List<NativeObj> rows)
        {
            var objById = new Dictionary<long, JObject>();
            foreach (var o in objs)
            {
                var jobj = (JObject)o;
                objById[jobj["id"].ToObject<long>()] = jobj;
            }
            var result = new JArray();
            foreach (var o in rows)
            {
                result.Add(objById[o.Id]);
            }
            return result;
        }

        public static List<NativeObj> CreateNativeObj(List<object> list, int id)
        {
            return list.Select(o => new NativeObj(o, id)).ToList();
        }

        public static JArray GetJsonArray(List<NativeObj> listRef, List<SqlRow> sqlDecl1)
        {
            var list = new JArray();
            foreach (var obj in listRef)
            {
                var row = new SqlRow();
                row["id"] = obj.Id;
                sqlDecl1.Add(row);
                list.Add(row);
            }
            return list;
        }

        public static JObject GetJsonObject(NativeObj refObj, List<SqlRow> sqlDecl0)
        {
            if (refObj == null)
            {
                return null;
            }
            var row = new SqlRow();
            row["id"] = refObj.Id;
            sqlDecl0.Add(row);
            return row;
        }

        public static OutObject GetOutObject(NativeObj refObj, int type, List<OutObject> sqlDecl0)
        {
            if (refObj == null)
            {
                return null;
            }
            var row = new OutObject();
            row.AddType(type);
            row.Id = refObj.Id;
            sqlDecl0.Add(row);
            return row;
        }

        public static OutObjectList GetOutObjectList(List<NativeObj> listRef, int type, List<OutObject> sqlDecl1)
        {
            var list = new OutObjectList();
            foreach (var obj in listRef)
            {
                var row = new OutObject();
                row.Id = obj.Id;
                row.AddType(type);
                sqlDecl1.Add(row);
                list.Add(row);
            }
            return list;
        }

        public static List<T> GetList<T>(IEntityManager em, List<NativeObj> listRef, int type)
        {
            return listRef.Select(r => em.Find<T>(type, r.Id)).ToList();
        }

        public static T Get<T>(IEntityManager em, NativeObj refObj, int type)
        {
            if (refObj == null)
            {
                return default;
            }
            return em.Find<T>(type, refObj.Id);
        }

        public static List<NativeObj> GroupBy<R>(List<NativeObj> rows, Func<NativeObj, R> groupBy,
            Func<NativeObj, NativeObj> group, Func<NativeObj, NativeObj, NativeObj> map)
        {
            var groups = rows.GroupBy(groupBy).ToDictionary(g => g.Key, g => g.Select(group).ToList());
            var result = new List<NativeObj>();
            foreach (var kvp in groups)
            {
                result.Add(map(kvp.Value[0], Combine(kvp.Value)));
            }
            return result;
        }

        public static NativeObj Combine(List<NativeObj> rows)
        {
            if (rows.Count == 0)
            {
                return null;
            }
            var row = new List<object>[rows[0].Size()];
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = new List<object>();
            }
            foreach (var o in rows)
            {
                var one = o.Row;
                for (int j = 0; j < one.Length; j++)
                {
                    row[j].Add(one[j]);
                }
            }
            return new NativeObj(row, -1);
        }
    }
}
