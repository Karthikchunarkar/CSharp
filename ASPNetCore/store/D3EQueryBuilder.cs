using System.Collections;
using System.Text;
using Microsoft.OpenApi.Extensions;
using d3e.core;
using gqltosql;
using gqltosql.schema;
using rest;
using static store.D3EEntityManagerProvider;

namespace store
{
    public class D3EQueryBuilder
    {
        private IModelSchema _schema;

        private D3EResourceHandler _resourceHandler;

        public D3EQueryBuilder(IModelSchema schema, D3EResourceHandler resourceHandler)
        {
            this._schema = schema;
            this._resourceHandler = resourceHandler;
        }

        public D3EQuery GenerateDFileCountUpdate(D3EQuery query, DFile file, int count)
        {
            query.Query = "update _dfile set _count = _count + ? where _id = ?";
            query.Args.Add(count);
            query.Args.Add(file.Id);
            return query;
        }

        public D3EQuery GenerateCreateDFileQuery(D3EQuery query, DFile file)
        {
            query.Query = "insert into _dfile(_id, _name, _size, _mime_type, _count) values (?, ?, ?, ?, 1)";
            query.Args.Add(file.Id);
            query.Args.Add(file.GetName());
            query.Args.Add(file.GetSize());
            query.Args.Add(file.GetMimeType());
            return query;
        }

        public D3EQuery GenerateCreateQuery(D3EQuery query, DModel<object> type, DatabaseObject _this)
        {
            List<string> cols = new List<string>();
            List<string> values = new List<string>();
            List<Object> args = new List<object>();
            query.Obj = _this;

            cols.Add("_id");
            values.Add("?");
            args.Add(_this);

            if (type.GetParent() == null)
            {
                // SaveStatus will not be in inherited entities
                cols.Add("_save_status");
                values.Add("?");
                args.Add(1);
            }

            AddInsertColumns(query, type, _this, cols, values, args, "");
            ICustomFieldProcessor<object> processor = CustomFieldService.Get().GetProcessor(type.GetType());
            if (processor != null)
            {
                processor.Insert(query, type, _this, cols, values, args);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into ").Append(type.GetTableName()).Append(" (").Append(string.Join(", ", cols))
                    .Append(") values (").Append(string.Join(", ", values)).Append(")");
            query.Query = sb.ToString();
            query.Args = args;

            if (type.GetParent() != null)
            {
                GenerateCreateQuery(query.Prev(), type.GetParent(), _this);
            }
            return query;
        }

        private void AddInsertColumns(D3EQuery query, DModel<object> type, object _this, List<string> cols, List<string> data,
            List<object> args, string embeddedPrefix)
        {
            if (IsCreatableDocument(type))
            {
                CreateCreatableDoc(type, _this, cols, data, args);
                return;
            }
            foreach (var field in type.GetFields())
            {
                if (field.IsTransientField() || field.IsDocField() || field.GetType() == FieldType.InverseCollection)
                {
                    continue;
                }
                DModel<object> refer = field.Reference;
                if (refer != null && refer.IsExternal())
                {
                    continue;
                }
                switch (field.GetType())
                {
                    case FieldType.Primitive:
                        cols.Add(embeddedPrefix + field.ColumnName);
                        object fieldValue = field.GetValue(_this);
                        FieldPrimitiveType pt = field.GetPrimitiveType();
                        if (pt == FieldPrimitiveType.Geolocation && fieldValue != null)
                        {
                            Geolocation loc = (Geolocation)fieldValue;
                            data.Add("ST_MakePoint(?, ?)::geography");
                            args.Add(loc.Longitude);
                            args.Add(loc.Latitude);
                        }
                        else
                        {
                            data.Add("?");
                            AddPrimitiveArg(args, field, fieldValue);
                        }
                        break;
                    case FieldType.Reference:
                        if (refer.GetType().Equals("DFile"))
                        {
                            object value = field.GetValue(_this);
                            if (value != null)
                            {
                                cols.Add(embeddedPrefix + field.ColumnName);
                                data.Add("?");
                                DFile df = (DFile)value;
                                CheckAndSaveDFile(query, df);
                                args.Add(df);
                            }
                        }
                        else if (refer.IsDocument() && !refer.IsCreatable())
                        {
                            var dc = (DDocField<object, object>)field;
                            DatabaseObject childDoc = (DatabaseObject)field.GetValue(_this);
                            if (childDoc == null)
                            {
                                continue;
                            }

                            // id
                            cols.Add(embeddedPrefix + dc.IdColumn);
                            data.Add("?");
                            args.Add(_schema.GetDatabaseId(childDoc));

                            // doc
                            cols.Add(embeddedPrefix + field.ColumnName);
                            data.Add("?");
                            string json = JSONInputContext.ToJsonString(childDoc, refer.GetType(), _schema);
                            args.Add(json);
                        }
                        else if (refer.IsEmbedded())
                        {
                            var em = (DEmbField<object, object>)field;
                            AddInsertColumns(query, refer, field.GetValue(_this), cols, data, args, em.Prefix);
                        }
                        else
                        {
                            if (field.IsChild())
                            {
                                object value = field.GetValue(_this);
                                if (value != null)
                                {
                                    GenerateCreateQuery(query.Prev(), refer, (DatabaseObject)value);
                                }
                            }
                            cols.Add(embeddedPrefix + field.ColumnName);
                            data.Add("?");
                            args.Add(field.GetValue(_this));
                        }
                        break;
                    case FieldType.PrimitiveCollection:
                        GeneratePrimitiveCollectionCreateQuery(query.Next(), field, (List<object>)field.GetValue(_this), _this);
                        break;
                    case FieldType.ReferenceCollection:
                        if (refer.IsDocument() && !refer.IsCreatable())
                        {
                            continue;
                        }
                        IList values = (IList)field.GetValue(_this);
                        if (field.IsChild())
                        {
                            foreach (object v in values)
                            {
                                GenerateCreateQuery(query.Prev(), field.Reference, (DatabaseObject)v);
                            }
                        }
                        GenerateReferenceCollectionCreateQuery(query.Next(), field, values, _this);
                        break;
                    default:
                        break;
                }
            }
        }

        private void CreateCreatableDoc(DModel<object> type, object _this, List<string> cols, List<string> values,
            List<object> args)
        {
            string doc = CreatableDocToJson(type, _this);
            string docColumn = GetCreatableDocColumnName(type);
            cols.Add(docColumn);
            values.Add("?");
            args.Add(doc);
        }


        private void UpdateCreatableDoc(DModel<object> type, object _this, List<string> updates, List<object> args)
        {
            string doc = CreatableDocToJson(type, _this);
            string update = GetCreatableDocColumnName(type) + " = ?";
            updates.Add(update);
            args.Add(doc);
        }

        private string GetCreatableDocColumnName(DModel<object> type)
        {
            string docColumn = type.GetTableName() + "_doc";
            return docColumn;
        }

        private string CreatableDocToJson(DModel<object> type, object _this)
        {
            string doc = JSONInputContext.ToJsonString((DatabaseObject)_this, type.GetType(), _schema);
            return doc;
        }

        private bool IsCreatableDocument(DModel<object> type)
        {
            return type.IsCreatable() && type.IsDocument();
        }

        private DFile CheckAndSaveDFile(D3EQuery query, DFile df)
        {
            if (!_resourceHandler.IsSaved(df))
            {
                df = _resourceHandler.Save(df);
                GenerateCreateDFileQuery(query.Prev(), df);
            }
            else
            {
                GenerateDFileCountUpdate(query.Next(), df, 1);
            }
            return df;
        }

        public void AddPrimitiveArg(List<object> args, DField<object, object> field, object value)
        {
            if (value is Enum)
            {
                args.Add(((Enum)value).GetDisplayName());
            }
            else if (value is TimeSpan)
            {
                int millis;
                try
                {
                    millis = ((TimeSpan)value).Milliseconds;
                }
                catch (ArithmeticException e)
                {
                    Log.PrintStackTrace(e);
                    millis = 0;
                }
                args.Add(millis);
            }
            else
            {
                args.Add(value);
            }
        }

        private D3EQuery GenerateReferenceCollectionCreateQuery(D3EQuery query, DField<object, object> field, IList value, object master)
        {
            if (value.Count == 0)
            {
                return null;
            }
            // ref has to exist since this is a reference collection
            DModel<object> refer = field.Reference;
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into ").Append(field.GetCollTableName(null));
            sb.Append(" (").Append(field.DeclType.ToColumnName()).Append(", ").Append(field.ColumnName).Append(", ")
                    .Append(field.ColumnName).Replace("_?_id$", "_order").Append(") values ");

            List<object> args = new List<object>();
            int sz = value.Count;
            for (int i = 0; i < sz; i++)
            {
                if (i != 0)
                {
                    sb.Append(", ");
                }
                args.Add(master);
                object obj = value[i];
                if (refer.GetIndex() == SchemaConstants.DFile)
                {
                    DFile df = (DFile)obj;
                    CheckAndSaveDFile(query, df);
                    obj = df;
                }
                args.Add(obj);
                sb.Append("( ?, ?, ").Append(i).Append(")");
            }
            query.Args = args;
            query.Query = sb.ToString();
            return query;
        }

        private D3EQuery GeneratePrimitiveCollectionCreateQuery(D3EQuery query, DField<object, object> field, List<object> value, object master)
        {
            if (value.Count == 0)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into ").Append(field.GetCollTableName(null));
            sb.Append(" (").Append(field.DeclType.ToColumnName()).Append(", ").Append(field.ColumnName)
                    .Append(") values ");

            List<object> args = new List<object>();
            for (int i = 0; i < value.Count; i++)
            {
                if (i != 0)
                {
                    sb.Append(", ");
                }
                args.Add(master);
                AddPrimitiveArg(args, field, value[i]);
                sb.Append("(?, ?)");
            }
            query.Args = args;
            query.Query = sb.ToString();
            return query;
        }

        public D3EQuery GenerateUpdateQuery(D3EQuery query, DModel<object> type, DatabaseObject _this)
        {
            BitArray _changes = _this._Changes().Changes;
            if (_changes.Count == 0)
            {
                Log.Debug("No Changes found: " + _this.Id);
                return null;
            }
            List<string> updates = new List<string>();
            List<object> args = new List<object>();

            AddUpdateColumns(query, type, _this, updates, args, "");
            ICustomFieldProcessor<object> processor = CustomFieldService.Get().GetProcessor(type.GetType());
            if (processor != null)
            {
                processor.Update(query, type, _this, updates, args);
            }

            if (!(updates.Count == 0))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update ").Append(type.GetTableName()).Append(" set ").Append(string.Join(", ", updates))
                        .Append(" where _id = ").Append(_schema.GetDatabaseId(_this));
                query.Query = sb.ToString();
                query.Args = (args);
            }

            if (type.GetParent() != null)
            {
                GenerateUpdateQuery(query.Prev(), type.GetParent(), _this);
            }
            return query;
        }

        private void AddUpdateColumns(D3EQuery query, DModel<object> type, DBObject _this, List<string> updates, List<object> args,
            string embeddedPrefix)
        {
            DBChange ch = _schema.FindChanges(_this);
            BitArray changes = ch.Changes;
            if (IsCreatableDocument(type) && !(changes.Count == 0))
            {
                UpdateCreatableDoc(type, _this, updates, args);
                return;
            }
            foreach (var field in type.GetFields())
            {
                if (field.IsTransientField() || field.GetType() == FieldType.InverseCollection
                        || !changes.Get(field.Index))
                {
                    continue;
                }
                var refer = field.Reference;
                if (refer != null && refer.IsExternal())
                {
                    continue;
                }
                switch (field.GetType())
                {
                    case FieldType.Primitive:
                        object fieldValue = field.GetValue(_this);
                        FieldPrimitiveType pt = field.GetPrimitiveType();
                        if (pt == FieldPrimitiveType.Geolocation && fieldValue != null)
                        {
                            Geolocation loc = (Geolocation)fieldValue;
                            updates.Add(embeddedPrefix + field.ColumnName + " = ST_MakePoint(?, ?)::geography");
                            args.Add(loc.Longitude);
                            args.Add(loc.Latitude);
                        }
                        else
                        {
                            updates.Add(embeddedPrefix + field.ColumnName + " = ?");
                            AddPrimitiveArg(args, field, fieldValue);
                        }
                        break;
                    case FieldType.Reference:
                        if (refer.GetIndex() == SchemaConstants.DFile)
                        {
                            DFile oldChild = (DFile)ch.OldValues[field.Index];
                            DFile newValue = (DFile)field.GetValue(_this);
                            updates.Add(embeddedPrefix + field.ColumnName + " = ?");
                            if (newValue != null)
                            {
                                DFile df = (DFile)newValue;
                                CheckAndSaveDFile(query, df);
                            }
                            args.Add(newValue);
                            if (oldChild != null)
                            {
                                GenerateDFileCountUpdate(query.Next(), oldChild, -1);
                            }
                        }
                        else if (refer.IsDocument())
                        {
                            // Must be non-creatable, since creatable doc is covered above
                            DatabaseObject childDoc = (DatabaseObject)field.GetValue(_this);
                            if (childDoc == null)
                            {
                                continue;
                            }

                            var dc = (DDocField<object, object>)field;
                            updates.Add(dc.IdColumn + " = ?");
                            args.Add(_schema.GetDatabaseId(childDoc));

                            updates.Add(field.ColumnName + " = ?");
                            string json = JSONInputContext.ToJsonString(childDoc, refer.GetType(), _schema);
                            args.Add(json);
                        }
                        else if (refer.IsEmbedded())
                        {
                            var em = (DEmbField<object, object>)field;
                            AddUpdateColumns(query, refer, (DBObject)field.GetValue(_this), updates, args, em.Prefix);
                        }
                        else
                        {
                            if (field.IsChild())
                            {
                                DatabaseObject oldChild = (DatabaseObject)ch.OldValues[field.Index];
                                DatabaseObject newChild = (DatabaseObject)field.GetValue(_this);
                                if (!ch.OldValues.ContainsKey(field.Index) || oldChild == newChild)
                                {
                                    GenerateUpdateQuery(query.Next(), field.Reference, newChild);
                                    continue;
                                }
                                if (newChild != null)
                                {
                                    GenerateCreateQuery(query.Prev(), refer, (DatabaseObject)newChild);
                                }
                                if (oldChild != null)
                                {
                                    GenerateDeleteQuery(query.Prev(), refer, (DatabaseObject)oldChild);
                                }
                            }
                            updates.Add(embeddedPrefix + field.ColumnName + " = ?");
                            args.Add(field.GetValue(_this));
                        }
                        break;
                    case FieldType.PrimitiveCollection:
                        if (type.IsDocument())
                        {
                            continue;
                        }
                        GeneratePrimitiveCollectionDeleteQuery(query.Prev(), field, new List<object> { _this });
                        GeneratePrimitiveCollectionCreateQuery(query.Next(), field, (List<object>)field.GetValue(_this), _this);
                        break;
                    case FieldType.ReferenceCollection:
                        if (type.IsDocument())
                        {
                            continue;
                        }
                        if (refer.IsDocument())
                        {
                            continue;
                        }
                        GenerateReferenceCollectionDeleteQuery(query.Prev(), field, new List<object> { _this });
                        List<DatabaseObject> values = (List<DatabaseObject>)field.GetValue(_this);
                        List<DatabaseObject> old = (List<DatabaseObject>) ch.OldValues[field.Index];
                        if (field.IsChild())
                        {
                            if (old != null)
                            {
                                List<DatabaseObject> oldCopy = new List<DatabaseObject>((IEnumerable<DatabaseObject>)old);
                                foreach (var v in values)
                                {
                                    if (oldCopy.Contains(v))
                                    {
                                        GenerateUpdateQuery(query.Next(), field.Reference, (DatabaseObject)v);
                                        oldCopy.Remove(v);
                                    }
                                    else
                                    {
                                        GenerateCreateQuery(query.Prev(), field.Reference, (DatabaseObject)v);
                                    }
                                }
                                if (!(oldCopy.Count == 0))
                                {
                                    GenerateMultiDeleteQuery(query.Prev(), oldCopy);
                                }
                            }
                            else
                            {
                                foreach (object v in values)
                                {
                                    GenerateUpdateQuery(query.Next(), field.Reference, (DatabaseObject)v);
                                }
                            }
                        }
                        if (refer.GetIndex() == SchemaConstants.DFile)
                        {
                            foreach (object v in old)
                            {
                                GenerateDFileCountUpdate(query.Next(), (DFile)v, -1);
                            }
                        }
                        GenerateReferenceCollectionCreateQuery(query.Next(), field, values, _this);
                        break;
                    default:
                        break;
                }
            }
        }

        private D3EQuery GenerateReferenceCollectionDeleteQuery(D3EQuery query, DField<object, object> field, IList masterList)
        {
            return GeneratePrimitiveCollectionDeleteQuery(query, field, masterList);
        }

        private D3EQuery GeneratePrimitiveCollectionDeleteQuery(D3EQuery query, DField<object, object> field, IList masterList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from ").Append(field.GetCollTableName(null));
            sb.Append(" where ");
            sb.Append(field.DeclType.ToColumnName()).Append(" in (");
            foreach (var item in masterList)
            {
                sb.Append(string.Join(", ", _schema.GetDatabaseId((DatabaseObject) item)));
            }
            sb.Append(")");
            query.Args = new List<object>();
            query.Query = sb.ToString();
            return query;
        }

        public D3EQuery GenerateDeleteQuery(D3EQuery query, DModel<object> type, DatabaseObject _this)
        {
            AddDeleteColumns(query, type, new List<DatabaseObject> { _this });
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from ").Append(type.GetTableName()).Append(" where _id = ")
                    .Append(_schema.GetDatabaseId(_this));
            query.Query = sb.ToString();
            if (type.GetParent() != null)
            {
                GenerateDeleteQuery(query.Next(), type.GetParent(), _this);
            }
            return query;
        }

        public D3EQuery GenerateMultiDeleteQuery(D3EQuery query, List<DatabaseObject> values)
        {
            if (values.Count == 0)
            {
                return null;
            }
            // Group by types and it's parents;
            Dictionary<int, List<DatabaseObject>> groupByTypes = new Dictionary<int, List<DatabaseObject>>();
            foreach (DatabaseObject v in values)
            {
                DModel<object> type = _schema.Get(v);
                while (type != null)
                {
                    List<DatabaseObject> list = groupByTypes[type.GetIndex()];
                    if (list == null)
                    {
                        list = new List<DatabaseObject>();
                        groupByTypes[type.GetIndex()] = list;
                    }
                    list.Add(v);
                    type = type.GetParent();
                }
            }
            foreach (var (t, list) in groupByTypes)
            {
                var type = _schema.GetType(t);
                AddDeleteColumns(query, type, list);

                var sb = new StringBuilder();
                sb.Append("delete from ").Append(type.GetTableName()).Append(" where _id in (");
                sb.Append(string.Join(", ", list.Select(v => _schema.GetDatabaseId(v))));
                sb.Append(")");

                var q = query.Prev();
                q.Query = (sb.ToString());
            }
            return query.Pre;
        }

        private void AddDeleteColumns(D3EQuery query, DModel<object> type, List<DatabaseObject> multiValues)
        {
            foreach (var field in type.GetFields())
            {
                if (field.IsTransientField() || field.GetType() == FieldType.InverseCollection)
                {
                    continue;
                }

                switch (field.GetType())
                {
                    case FieldType.Reference:
                        if (!field.Reference.IsEmbedded() && field.IsChild() && !field.Reference.IsDocument())
                        {
                            var childs = new List<DatabaseObject>();
                            foreach (var o in multiValues)
                            {
                                var value = field.GetValue(o);
                                if (value != null)
                                {
                                    childs.Add((DatabaseObject) value);
                                }
                            }
                            GenerateMultiDeleteQuery(query.Prev(), childs);
                        }
                        break;

                    case FieldType.PrimitiveCollection:
                        if (type.IsDocument())
                        {
                            continue;
                        }
                        GeneratePrimitiveCollectionDeleteQuery(query.Prev(), field, multiValues);
                        break;

                    case FieldType.ReferenceCollection:
                        if (type.IsDocument())
                        {
                            continue;
                        }
                        if (field.Reference.IsDocument())
                        {
                            continue;
                        }
                        GenerateReferenceCollectionDeleteQuery(query.Prev(), field, multiValues);
                        if (field.IsChild())
                        {
                            var childs = new List<DatabaseObject>();
                            foreach (var o in multiValues)
                            {
                                var value = field.GetValue(o);
                                if (value != null)
                                {
                                    childs.AddRange((List<DatabaseObject>)value);
                                }
                            }
                            GenerateMultiDeleteQuery(query.Prev(), childs);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public string generateSelectAllQuery(DModel<object> type, List<RowField> selectedFields, long id)
        {
            AliasGenerator ag = new AliasGenerator();
            string alias = ag.Next();
            StringBuilder sb = GenerateSelectAllQueryInternal(ag, alias, type, selectedFields);
            sb.Append(" where ").Append(alias).Append("._id = ").Append(id);
            return sb.ToString();
        }

        public string GenerateLoadAllQuery(DModel<object> type, List<RowField> selectedFields, long offset, long limit)
        {
            AliasGenerator ag = new AliasGenerator();
            string alias = ag.Next();
            StringBuilder sb = GenerateSelectAllQueryInternal(ag, alias, type, selectedFields);
            sb.Append(" order by ").Append(alias).Append("._id desc ").Append(" offset ").Append(offset).Append(" limit ")
                    .Append(limit);
            return sb.ToString();
        }

        private StringBuilder GenerateSelectAllQueryInternal(AliasGenerator ag, String alias, DModel<object> type,
                List<RowField> selectedFields)
        {
            StringBuilder sb = new StringBuilder();
            List<string> joins = new List<string>();
            string idColumn = GetTypeAndColumnSql(type, type, alias + "._id", joins, ag);
            sb.Append("select ").Append(idColumn);
            AppendAllColumns(sb, type, selectedFields, joins, ag, alias, "");
            ICustomFieldProcessor<object> processor = CustomFieldService.Get().GetProcessor(type.GetType());
            if (processor != null)
            {
                // TODO processor.selectAll(sb, type, id, selectedFields, joins, ag, alias);
            }
            sb.Append(" from ").Append(type.GetTableName()).Append(" ").Append(alias);
            foreach (string j in joins)
            {
                sb.Append(" left join ").Append(j);
            }
            return sb;
        }

        private void AppendAllColumns(StringBuilder sb, DModel<object> type, List<RowField> selectedFields, List<string> joins,
                AliasGenerator ag, string alias, string embeddedPrefix)
        {
            if (IsCreatableDocument(type))
            {
                sb.Append(", ").Append(alias).Append(".").Append(embeddedPrefix).Append(GetCreatableDocColumnName(type));
                selectedFields.Add(new RowDocField(type));
                return;
            }
            var fields = type.GetFields();
            foreach (var df in fields)
            {
                if (df.IsTransientField() || df.IsDocField())
                {
                    continue;
                }
                FieldType ft = df.GetType();
                switch (ft)
                {
                    case FieldType.Primitive:
                        String column = alias + "." + embeddedPrefix + df.ColumnName;
                        if (df.GetPrimitiveType() == FieldPrimitiveType.Geolocation)
                        {
                            sb.Append(", ").Append("(st_x(" + column + "::geometry) || ',' || st_y(" + column + "::geometry))");
                        }
                        else
                        {
                            sb.Append(", ").Append(column);
                        }
                        selectedFields.Add(new RowField(df));
                        break;
                    case FieldType.Reference:
                        var refer = df.Reference;
                        if (refer != null && refer.IsExternal())
                        {
                            continue;
                        }
                        if (refer.IsDocument() && !refer.IsCreatable())
                        {
                            var dc = (DDocField<object, object>)df;
                            string idCol = alias + '.' + embeddedPrefix + dc.IdColumn;
                            string typeAndIdSql = GetTypeAndColumnSql(type, refer, idCol, joins, ag);
                            sb.Append(", ").Append(typeAndIdSql);
                            sb.Append(", ").Append(alias).Append(".").Append(embeddedPrefix).Append(df.ColumnName);
                            selectedFields.Add(new RowField(df));
                        }
                        else if (refer.IsEmbedded())
                        {
                            List<RowField> subFields = new List<RowField>();
                            var em = (DEmbField<object, object>)df;
                            AppendAllColumns(sb, refer, subFields, joins, ag, alias, em.Prefix);
                            selectedFields.Add(new RowField(df, subFields));
                        }
                        else
                        {
                            sb.Append(", ");
                            sb.Append(CreateRefColumn(type, df.Reference,
                                    alias + "." + embeddedPrefix + df.ColumnName, joins, ag));
                            selectedFields.Add(new RowField(df));
                        }
                        break;
                    case FieldType.PrimitiveCollection:
                    case FieldType.InverseCollection:
                    case FieldType.ReferenceCollection:
                        break;
                    default:
                        break;
                }
            }
            var parent = type.GetParent();
            if (parent != null)
            {
                string ja = ag.Next();
                joins.Add(parent.GetTableName() + " " + ja + " on " + ja + "._id = " + alias + "._id");
                AppendAllColumns(sb, parent, selectedFields, joins, ag, ja, embeddedPrefix);
            }
        }

        public string GenerateSelectCollectionQuery(DModel<object> type, DField<object, object> field, long id)
        {
            StringBuilder b = new StringBuilder();
            List<string> joins = new List<string>();
            AliasGenerator ag = new AliasGenerator();
            b.Append("select ");
            switch (field.GetType())
            {
                case FieldType.InverseCollection:
                    string tableAlias = ag.Next();
                    ag.Store(field.Reference.GetTableName(), "", tableAlias);
                    b.Append(CreateRefColumn(type, field.Reference, tableAlias + "._id", joins, ag));
                    b.Append(" from ");
                    b.Append(field.Reference.GetTableName()).Append(" ").Append(tableAlias);
                    break;
                case FieldType.PrimitiveCollection:
                    b.Append(field.ColumnName);
                    b.Append(" from ");
                    b.Append(field.GetCollTableName(null));
                    break;
                case FieldType.ReferenceCollection:
                    string sql;
                    if (field.Reference.IsDocument() && !field.Reference.IsCreatable())
                    {
                        string docColumn = ((DDocCollField<object, object>) field).DocColumn;
                        sql = CreateDocRefColumn(type, field.Reference, field.ColumnName, docColumn, joins, ag);
                    }
                    else
                    {
                        sql = CreateRefColumn(type, field.Reference, field.ColumnName, joins, ag);
                    }
                    b.Append(sql);
                    b.Append(" from ");
                    b.Append(field.GetCollTableName(null));
                    break;
                default:
                    break;
            }

            if (!(joins.Count == 0))
            {
                // Should never happen
                foreach (string j in joins)
                {
                    b.Append(" left join ").Append(j);
                }
            }

            b.Append(" where ");

            if (field.Reference != null)
            {
                string al = ag.GetAlias(field.Reference.GetTableName(), "");
                if (al != null)
                {
                    b.Append(al).Append(".");
                }
            }
            switch (field.GetType())
            {
                case FieldType.InverseCollection:
                    b.Append(field.ColumnName);
                    break;
                case FieldType.PrimitiveCollection:
                case FieldType.ReferenceCollection:
                    b.Append(type.ToColumnName());
                    break;
                default:
                    break;
            }

            b.Append(" = ").Append(id);
            if (field.GetType() == FieldType.ReferenceCollection)
            {
                b.Append(" order by ");
                b.Append(field.ColumnName.Replace("_?_id$", "_order"));
            }
            return b.ToString();
        }

        private string CreateDocRefColumn(DModel<object> inn, DModel<object> refer, string idColumn, string docColumn,
                List<string> joins, AliasGenerator ag)
        {
            string sql = GetTypeAndColumnSql(inn, refer, idColumn, joins, ag);
            return sql + ", " + docColumn;
        }

        public string CreateRefColumn(DModel<object> inn, DModel<object> refer, string clm, List<string> joins, AliasGenerator ag)
        {
            if (refer.GetIndex() == SchemaConstants.DFile)
            {
                return clm;
            }
            else
            {
                return GetTypeAndColumnSql(inn, refer, clm, joins, ag);
            }
        }

        private string GetTypeAndColumnSql(DModel<object> refIn, DModel<object> refer, string clm, List<string> joins,
                AliasGenerator ag)
        {
            int[] types = refer.GetAllTypes();
            if (types.Length > 1)
            {
                var mp = refer.GetMostParent();
                string mpTableName = mp.GetTableName();
                bool mpAliasCreated = false;
                List<string[]> toJoin = new List<string[]>();
                StringBuilder b = new StringBuilder();
                b.Append("(case");
                for (int x = types.Length - 1; x >= 0; x--)
                {
                    int t = types[x];
                    var type = _schema.GetType(t);
                    string name = type.GetTableName();
                    string tableAlias = ag.GetAlias(name, clm);
                    if (tableAlias == null)
                    {
                        // Never seen this table before. So make a join.
                        tableAlias = ag.Next();
                        ag.Store(name, clm, tableAlias);
                        toJoin.Add(new String[] { name, clm });
                        mpAliasCreated = type == mp;
                    }
                    b.Append(" when ").Append(tableAlias).Append("._id is not null then ").Append(t);
                }
                b.Append(" else -1 end)");

                string mpTableAlias = ag.GetAlias(mpTableName, clm);
                if (mpAliasCreated)
                {
                    string refAlias = ag.GetAlias(refIn.GetTableName(), "");
                    if (refAlias == null)
                    {
                        // Unlikely to be executed
                        refAlias = ag.Next();
                        ag.Store(refIn.GetTableName(), "", refAlias);
                    }
                    joins.Add(mpTableName + " " + mpTableAlias + " on " + mpTableAlias + "._id = " + clm);
                }
                foreach (string[] name in toJoin)
                {
                    if (name[0].Equals(mp.GetTableName()))
                    {
                        continue;
                    }
                    string tableAlias = ag.GetAlias(name[0], name[1]);
                    joins.Add(name[0] + " " + tableAlias + " on " + tableAlias + "._id = " + clm);
                }
                return clm + ", " + b.ToString();
            }
            else
            {
                return clm + ", " + refer.GetIndex();
            }
        }

        public string GenerateSelectDFileQuery(DFile file)
        {
            return "select _id, _name, _size, _mime_type from _dfile where _id = '" + file.Id + "'";
        }

        public string GenerateSelectWhere(DModel<object> dm, string field, object val)
        {
            AliasGenerator ag = new AliasGenerator();
            string alias = ag.Next();
            List<string> joins = new List<string>();
            string clm = alias + "._id";
            StringBuilder sb = new StringBuilder();
            sb.Append("select ").Append(GetTypeAndColumnSql(dm, dm, clm, joins, ag)).Append(" from ")
                    .Append(dm.GetTableName()).Append(" ").Append(alias);
            if (joins.Count != 0)
            {
                foreach (string j in joins)
                {
                    sb.Append(" left join ").Append(j);
                }
            }
            String column;
            if (field.Equals("id"))
            {
                column = "_id";
            }
            else
            {
                var df = dm.GetField(field);
                if (df == null)
                {
                    throw new Exception("Field " + field + " not found in " + dm.GetType());
                }
                column = df.ColumnName;
            }
            sb.Append(" where ").Append(alias).Append(".").Append(column).Append(" = :").Append(field);
            return sb.ToString();
        }
    }
}
