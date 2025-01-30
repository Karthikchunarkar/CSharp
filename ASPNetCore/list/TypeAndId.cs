using store;

namespace list
{
    public class TypeAndId
    {
        public TypeAndId(int type, long id)
        {
            this.Type = type;
            this.Id = id;
        }

        public int Type { get; }
        public long Id { get; }

        public override bool Equals(object obj)
        {
            if (obj is TypeAndId other)
            {
                return other.Type == Type && other.Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Type ^ (int)Id;
        }

        public static TypeAndId From(DBObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            return new TypeAndId(obj._TypeIdx(), obj.Id);
        }

        public static List<TypeAndId> From(List<object> obj)
        {
            var res = new List<TypeAndId>();
            foreach (var a in obj)
            {
                res.Add(From((DBObject)a));
            }
            return res;
        }

        public static List<TypeAndId> FromList(List<DBObject> list, int idx, DBObject master)
        {
            var result = new D3EPersistanceList<TypeAndId>(master, idx);
            var mapped = list.Select(a => new TypeAndId(a.TypeIdx(), a.GetId())).ToList();
            result.SetAll(mapped);
            return result;
        }
    }
}
