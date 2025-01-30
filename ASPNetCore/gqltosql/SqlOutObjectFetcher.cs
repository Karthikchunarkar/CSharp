using d3e.core;
using gqltosql.schema;
using store;

namespace gqltosql
{
    public class SqlOutObjectFetcher
    {
        private IModelSchema schema;

        public SqlOutObjectFetcher(IModelSchema schema)
        {
            this.schema = schema;
        }

        public object FetchValue<T>(Field field, object value, DModel<T> type)
        {
            if (value == null)
            {
                return null;
            }

            if (value is DFile dFile)
            {
                return FetchDFile(field, dFile);
            }
            if (value is DatabaseObject dbObj)
            {
                return FetchReference(field, dbObj);
            }
            if (value is ICollection<object> collection)
            {
                DField<object, object> df = field.FieldVar;
                IList list = df.GetType() == FieldType.PrimitiveCollection
                    ? new OutPrimitiveList()
                    : new OutObjectList();

                foreach (var v in collection)
                {
                    list.Add(FetchValue(field, v, type));
                }
                return list;
            }
            return value;
        }

        public OutObject FetchReference(Field field, DatabaseObject value)
        {
            OutObject res = new OutObject();
            DModel<object> parent = schema.GetType(value._TypeIdx());
            res.Id = value.Id;
            res.AddType(parent.GetIndex());
            while (parent != null)
            {
                DModel<object> type = parent;
                Selection selec = field.Selections.FirstOrDefault(s => s.Type == type);
                if (selec != null)
                {
                    FetchReferenceInternal(selec, res, type, value);
                }
                parent = parent.GetParent();
            }
            return res;
        }

        private OutObject FetchDFile(Field field, DFile value)
        {
            OutObject res = new OutObject();
            List<Selection> selections = field.Selections;
            Selection selec = selections[0];
            foreach (var f in selec.Fields)
            {
                try
                {
                    DField<object, object> df = f.FieldVar;
                    if (df.Name == "id")
                    {
                        res.Add("id", value.Id);
                    }
                    else if (df.Name == "name")
                    {
                        res.Add("name", value.GetName());
                    }
                    else if (df.Name == "size")
                    {
                        res.Add("size", value.GetSize());
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return res;
        }

        private void FetchReferenceInternal(Selection set, OutObject res, DModel<object> type, object value)
        {
            foreach (var s in set.Fields)
            {
                DField<object, object> df = s.FieldVar;
                try
                {
                    object val = df.FetchValue(value, new DataFetcherImpl(s, df));
                    res.Add(df.Name, val);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
        }

        private class DataFetcherImpl : IDataFetcher
        {
            private readonly Field field;

            public DataFetcherImpl(Field field, DField<object, object> df)
            {
                this.field = field;
            }

            public object OnEmbeddedValue(object value)
            {
                return OnReferenceValue(value);
            }

            public object OnFlatValue<R>(List<R> value)
            {
                throw new NotImplementedException();
            }

            public object OnInverseValue<R>(List<R> value)
            {
                return OnReferenceList(value);
            }

            public object OnPrimitiveList<T, R>(List<R> value, DField<T, List<R>> df)
            {
                OutPrimitiveList list = new OutPrimitiveList();
                value.ForEach(v =>list.Add(OnPrimitiveValue(v, field)));
                return list;
            }

            public object OnPrimitiveValue<T, R>(object value, DField<T, R> df)
            {
                if (value == null)
                {
                    return null;
                }
                if (value is DFile dFile)
                {
                    return FetchDFile(field, dFile);
                }
                return value;
            }

            public object OnReferenceList<R>(List<R> value)
            {
                OutObjectList list = new OutObjectList();
                value.ForEach(v =>list.Add(OnPrimitiveValue(v)));
                return list;
            }

            public object OnReferenceValue(object value)
            {
                if(value == null)
                {
                    return null;
                }
                return FetchReference(field, (DatabaseObject) value);
            }
        }
    }
}
