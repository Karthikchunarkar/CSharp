using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using gqltosql.schema;
using rest;
using store;

namespace gqltosql
{
    public class DocumentFlatSqlColumn : ISqlColumn
    {
        private Field _field;
        private DFlatField<object, object> _df;
        private Dictionary<string, DocumentSqlColumn> _selectedColumns = new Dictionary<string, DocumentSqlColumn>();

        public DocumentFlatSqlColumn(Field field, DFlatField<object, object> df)
        {
            this._field = field;
            this._df = df;
        }

        public void AddColumn(SqlTable table, SqlQueryContext ctx)
        {
            string[] flatPaths = _df.FlatPaths;
            DModel<object> declType = _df.DeclType;
            foreach (var fn in flatPaths)
            {
                DField<object, object> f = declType.GetField(fn);
                if (f != null)
                {
                    ISqlColumn column = table.GetColumn(f.Name);
                    if (column is DocumentSqlColumn val)
                    {
                        val.DoNotRead();
                        _selectedColumns[column.GetFieldName()] = val;
                    }
                    else
                    {
                        ctx.AddSelection(ctx.From + "." + f.ColumnName, f.Name);
                    }
                }
            }
        }

        public void ExtractDeepFields(IEntityManager em, IModelSchema schema, string type, List<OutObject> rows)
        {
            EntityHelperService instance = EntityHelperService.GetInstance();
            DModel<object> dm = schema.GetType(type);
            EntityHelper<object> helper = instance.Get(type);
            DatabaseObject ins = (DatabaseObject)helper.NewInstance();
            rows.ForEach(o =>
            {
                String[] flatPaths = _df.FlatPaths;
                DModel<object> declType = _df.DeclType;
                foreach (string fn in flatPaths)
                {
                    if (o.Has(fn))
                    {
                        string doc = o.GetString(fn);
                        DatabaseObject obj = (DatabaseObject) JSONInputContext.FromJsonString(doc, o.GetLong("id"), _df.Reference.GetType(), schema);
                        o.Remove(fn);
                        obj.UpdateMasters(c => ins.updateFlat(c));
                        ins.UpdateFlat(obj);
                        o.Remove(fn);
                        if (_selectedColumns.ContainsKey(fn))
                        {
                            DocumentSqlColumn clm = _selectedColumns[fn];
                            clm.Read(schema, o, obj);
                        }
                    }
                }
                SqlOutObjectFetcher fetcher = new SqlOutObjectFetcher(schema);
                List<object> res = _df.GetValue(ins);
                o.Add(_df.Name, fetcher.FetchValue(_field, res, _df.Reference));
            });
        }

        public string GetFieldName()
        {
            return _df.Name;
        }

        public SqlAstNode? GetSubQuery()
        {
            return null;
        }

        public void UpdateSubField(Dictionary<long, OutObject> parents, List<OutObject> all)
        {
            throw new NotImplementedException();
        }
    }
}
