

using Newtonsoft.Json.Linq;

namespace gqltosql
{
    public class SqlRow : JObject
    {
        private HashSet<string> types = new HashSet<string>();
        private SqlRow dup;

        public SqlRow() : base()
        {
        }

        public void AddType(string type)
        {
            types.Add(type);
        }

        public bool IsOfType(string type)
        {
            return types.Contains(type);
        }

        public void Duplicate(SqlRow dup)
        {
            if (this.dup == dup)
            {
                return;
            }
            if (this.dup != null)
            {
                this.dup.Duplicate(dup);
            }
            else
            {
                this.dup = dup;
            }
        }

        public SqlRow GetDuplicate()
        {
            return dup;
        }

        public void AddCollectionField(string field, JArray val)
        {
            this[field] = val;
            if (dup != null)
            {
                dup.AddCollectionField(field, val);
            }
        }
    }
}
