using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using d3e.core;
using gqltosql;
using gqltosql.schema;
using store;

namespace gqltosql
{
    public class SqlAstNode
    {
        private Dictionary<int, SqlTable> _tables = new Dictionary<int, SqlTable> ();
        private int _type;
        private string _table;
        private string _path;
        private bool _embedded;
        private IModelSchema _schema;

        public SqlAstNode(IModelSchema schema, string path, int type, string table, bool embedded)
        {
            this._schema = schema;
            this._path = path;
            this._type = type;
            this._table = table;
            this._embedded = embedded;
            AddTable(schema.GetType(type));
        }

        public virtual SqlQueryContext CreateCtx()
        {
            SqlQueryContext ctx = new SqlQueryContext(this, -1);
            ctx.Query.SetFrom(TableName, ctx.From);
            ctx.Query.AddWhere(ctx.From + "._id in (:ids)");
            return ctx;
        }

        public bool Embedded { get { return _embedded; } }

        private void AddTable(DModel<object> dModel)
        {
            throw new NotImplementedException();
        }

        public object Path { get; internal set; }

        public void AddColumn<T>(DModel<T> declType, ISqlColumn column)
        {
            AddTable(declType);
            SqlTable tbl = _tables[declType.GetIndex()];
            tbl.AddColumn(column);
        }

        private void AddTable<T>(DModel<T> type)
        {
            int[] allTypes = type.GetAllTypes();
            foreach (int i in allTypes)
            {
                SqlTable tbl = _tables[i];
                if (tbl == null)
                {
                    DModel <object> temp = _schema.GetType(i);
                    _tables[i] = tbl = new SqlTable(i, temp.GetTableName(), temp.IsEmbedded());
                }
            }
        }

        public string TableName { get { return _table; } }

        public int Type {  get { return _type; } }

        public Dictionary<int, SqlTable> Tables {  get { return _tables; } }

        public string PathVar { get { return _path; } }

        public override string ToString()
        {
            return _table;
        }

        public bool IsEmpty()
        {
            return _tables.Values.All(t => t.Columns.IsNullOrEmpty());
        }

        public virtual void SelectColumns(SqlQueryContext ctx)
        {
            if (IsEmpty())
            {
                ctx.SubType(_type);
                return;
            }
            SqlQueryContext typeCtx = ctx.SubType(Type);
            DModel <object> dModel = _schema.GetType(Type);
            if (!dModel.IsEmbedded() && dModel.GetAllTypes().Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("(case");
                int[] allTypes = FindAllTypes(_type);
                for (int x = allTypes.Length -1; x >= 0; x --)
                {
                    int t = allTypes[x];
                    sb.Append(" when").Append(ctx.GetTableAlias(t.ToString())).Append("._id is not null then").Append(t);
                }
                sb.Append(" else -1 end)");
                typeCtx.AddSelection(sb.ToString(), "__typeindex");
            }
            SqlTable val = Tables[Type];
            val.AddSelections(typeCtx);
            foreach (var item in Tables)
            {
                int tableType = item.Key;
                SqlTable value = item.Value;
                if (tableType == Type)
                {
                    continue;
                }
                SqlQueryContext sub = ctx.SubType(_type);
                string join = sub.From;
                sub.AddJoin(value.GetTableName(), join, join + "._id= " + item.Value.GetIdColumn(typeCtx.From));
                item.Value.AddSelections(sub);
            }
        }

        private int[] FindAllTypes(int type)
        {
            return _schema.GetType(type).GetAllTypes();
        }

        public OutObjectList ExecuteQuery(IEntityManager entityManager, HashSet<long> ids, Dictionary<long, OutObject> dictionary)
        {
            if (ids.IsNullOrEmpty())
            {
                return new OutObjectList();
            }
            SqlQueryContext ctx = CreateCtx();
            ctx.AddSqlColumns(this);
            SqlQuery query = ctx.Query;
            string sql = query.CreateSQL();
            Log.DisplaySql(PathVar, sql, ids);
            Query q = entityManager.CreateNativeQuery(sql);
            q.SetParameter("ids", ids);
            object[] rows = q.GetResultList();
            QueryReader reader = query.Reader;
            OutObjectList result = new OutObjectList();
            foreach (object[] r in rows)
            {
                OutObject obj = reader.Read(r, dictionary);
                result.Add(obj);
            }
            if (!(result.Count == 0))
            {
                ExecuteSubQuery(entityManager, t => result);
            }
            return result;
        }

        public void ExecuteSubQuery(IEntityManager em, Func<int, List<OutObject>> listSupplier)
        {
            foreach(var e in _tables)
            {
                int type = e.Key;
                SqlTable table = e.Value;
                List<OutObject> apply = listSupplier.Invoke(type);
                foreach(ISqlColumn c in table.Columns)
                {
                    c.ExtractDeepFields(em, _schema, type.ToString(), apply);
                    SqlAstNode sub = c.GetSubQuery();
                    if(sub != null)
                    {
                        Dictionary<long, OutObject> objById = new Dictionary<long, OutObject>();
                        apply.ForEach(o =>
                        {
                            try
                            {
                                OutObject row = objById[o.Id];
                                if (row != null)
                                {
                                    row.Duplicate(o);
                                }
                                else
                                {
                                    objById[o.Id] = o;
                                }
                            }
                            catch (Exception ex) { }
                        });
                        OutObjectList array = sub.ExecuteQuery(em, objById.Keys.ToHashSet(), objById);
                        c.UpdateSubField(objById, array);
                    }
                }
            }
        }
    }
}
