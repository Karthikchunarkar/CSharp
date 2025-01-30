using com.sun.tools.@internal.xjc.model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using gqltosql.schema;
using rest;
using store;

namespace gqltosql
{
    public class DocumentSqlColumn : ISqlColumn
    {
        private Field _field;
        private DField<object, object> _df;
        private bool _doNotRead;
        public string Column;

        public DocumentSqlColumn(Field field, DField<object,object> df, string column)
        {
            this._field = field;
            this._df = df;
            this.Column = column;
        }

        public void AddColumn(SqlTable table, SqlQueryContext ctx)
        {
            ctx.AddSelection(ctx.From + "." + Column, GetFieldName());
        }

        public void ExtractDeepFields(IEntityManager em, IModelSchema schema, string type, List<OutObject> rows)
        {
            if(_doNotRead)
            {
                return;
            }
            rows.ForEach(o =>
            {
                try
                {
                    if (o.Has(GetFieldName()))
                    {
                        string doc = o.GetString(GetFieldName());
                        object obj = JSONInputContext.FromJsonString(doc, o.Id, GetTypeName(), schema);
                        PopulateDocInObj(obj, doc);
                        o.Remove(GetFieldName());
                        Read(schema, o, obj);
                    }
                }
                catch (Exception ex) 
                {
                    
                }
            });
        }

        public override string ToString()
        {
            return GetFieldName();
        }
        public void DoNotRead()
        {
            _doNotRead = true;
        }

        public void Read(IModelSchema schema, OutObject o, object obj)
        {
            SqlOutObjectFetcher fetcher = new SqlOutObjectFetcher(schema);
            object res = obj;
            object value = fetcher.FetchValue(_field, res, GetType());
            AddValueToOut(o, value);
        }

        protected virtual new DModel<object> GetType()
        {
            return _df.Reference;
        }

        protected virtual void AddValueToOut(OutObject o, object value)
        {
            o.Add(_df.Name, value);   
        }

        protected virtual void PopulateDocInObj(object obj, string doc)
        {
            throw new NotImplementedException();
        }

        private string GetTypeName()
        {
            return GetType().GetType();
        }

        public virtual string GetFieldName()
        {
            return _df.Name;
        }

        public SqlAstNode GetSubQuery()
        {
            return null;
        }

        public void UpdateSubField(Dictionary<long, OutObject> parents, List<OutObject> all)
        {
            throw new NotImplementedException();
        }
    }
}
