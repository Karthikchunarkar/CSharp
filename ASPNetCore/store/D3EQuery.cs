namespace store
{
    public class D3EQuery
    {
        public List<D3EQuery> Queries { get; private set; }
        public string Query { get { return Query; } set { Query = value; } }
        public List<object> Args { get; set; } = new List<object>();
        private D3EQuery _pre;
        private D3EQuery _next;
        private DatabaseObject _obj;

        public D3EQuery()
        {
        }

        public static D3EQuery Create()
        {
            var query = new D3EQuery
            {
                Queries = new List<D3EQuery>()
            };
            query.Queries.Add(query);
            return query;
        }

        public DatabaseObject Obj { get { return _obj; } set { _obj = value; } }

        public D3EQuery Pre { get => _pre; set => _pre = value; }
        public D3EQuery Next1 { get => _next; set => _next = value; }

        public D3EQuery Prev()
        {
            var q = new D3EQuery
            {
                Queries = this.Queries
            };

            var p = this;
            while (p.Pre != null)
            {
                p = p.Pre;
            }
            p.Pre = q;

            int idx = Queries.IndexOf(p);
            Queries.Insert(idx, q);
            return q;
        }

        public D3EQuery Next()
        {
            var q = new D3EQuery
            {
                Queries = this.Queries
            };

            var n = this;
            while (n.Next1 != null)
            {
                n = n.Next1;
            }
            n.Next1 = q;

            int idx = Queries.IndexOf(n);
            Queries.Insert(idx + 1, q);
            return q;
        }

        public void SetArgs(List<object> args)
        {
            this.Args = args;
        }
    }
}
