using gqltosql.schema;
using store;

namespace gqltosql
{
    public class GqlToSql
    {
        private IModelSchema schema;
        private D3EEntityManagerProvider em;

        public GqlToSql(D3EEntityManagerProvider em, IModelSchema schema)
        {
            this.em = em;
            this.schema = schema;
        }

        public OutObjectList Execute(string typeStr, Field field, List<OutObject> objs)
        {
            if (objs.Count == 0)
            {
                return new OutObjectList();
            }

            DModel<object> type = schema.GetType(typeStr);
            SqlAstNode sqlNode = PrepareSqlNode(field.Selections, type);
            HashSet<long> ids = new HashSet<long>();
            Dictionary<long, OutObject> byId = new Dictionary<long, OutObject>();

            foreach (OutObject obj in objs)
            {
                long id = obj.GetLong("id");
                ids.Add(id);
                byId[id] = obj;
            }

            OutObjectList result = sqlNode.ExecuteQuery(em.Get(), ids, byId);
            return result;
        }

        public OutObject Execute(string typeStr, Field field, long id)
        {
            DModel<object> type = schema.GetType(typeStr);
            OutObjectList array = Execute(type, field, new HashSet<long> { id });
            if (array.Count != 0)
            {
                return (OutObject) array[0];
            }
            return null;
        }

        public OutObjectList Execute(DModel<object> type, Field field, HashSet<long> ids)
        {
            try
            {
                SqlAstNode sqlNode = PrepareSqlNode(field.Selections, type);
                OutObjectList result = sqlNode.ExecuteQuery(em.Get(), ids, new Dictionary<long, OutObject>());
                return result;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return new OutObjectList();
            }
        }

        private SqlAstNode PrepareSqlNode(List<Selection> selections, DModel<object> type)
        {
            SqlAstNode node = new SqlAstNode(schema, "this", type.GetIndex(), type.GetTableName(), type.IsEmbedded());
            AddReferenceField(node, selections, "");
            return node;
        }

        private void AddField(SqlAstNode node, Field field, DModel<object> parentType, string embeddedPrefix)
        {
            DField<object, object> df = field.FieldVar;
            if (df is DRefCollField<object, object> coll)
            {
                if (coll.CustomFields)
                {
                    CustomFieldService.Get().GetProcessor(parentType.GetType()).AddGqlToSqlFields(this, node, field, df);
                    return;
                }
            }

            if (df == null || df.IsTransientField())
            {
                return;
            }

            switch (df.GetType())
            {
                case FieldType.Primitive:
                    AddPrimitiveField(node, field, df, embeddedPrefix);
                    break;
                case FieldType.Reference:
                    AddReferenceField(node, field, df, embeddedPrefix);
                    break;
                case FieldType.PrimitiveCollection:
                    AddPrimitiveCollectionField(node, field, df);
                    break;
                case FieldType.ReferenceCollection:
                    AddReferenceCollectionField(node, field, df);
                    break;
                case FieldType.InverseCollection:
                    AddInverseCollectionField(node, field, df);
                    break;
                default:
                    break;
            }
        }

        private void AddPrimitiveCollectionField(SqlAstNode node, Field field, DField<object, object> df)
        {
            DModel<object> dcl = df.DeclType;
            SqlAstNode sub = new SqlPrimitiveCollAstNode(schema, node.PathVar + "." + df.Name,
                df.GetCollTableName(dcl.GetTableName()), dcl.GetTableName() + "_id", df.ColumnName, df.Name);
            AddColumn(node, df, new CollSqlColumn(sub, df.Name));
        }

        private void AddReferenceCollectionField(SqlAstNode node, Field field, DField<object, object> df)
        {
            DModel<object> dm = df.Reference;
            if (dm.IsDocument())
            {
                DModel<object> dcl = df.DeclType;
                if (dm.IsCreatable())
                {
                    SqlCollAstNode sub = new SqlCollAstNode(schema, node.PathVar + "." + df.Name, dm.GetIndex(),
                        dm.GetTableName(), df.GetCollTableName(dcl.GetTableName()), dcl.GetTableName() + "_id",
                        df.ColumnName);

                    sub.AddColumn(dm, new DocumentCreatableSqlColumn(dm, field, null, dm.GetTableName() + "_doc"));
                    AddColumn(node, df, new RefCollSqlColumn(sub, df.Name));
                }
                else
                {
                    SqlAstNode sub = new SqlPrimitiveCollAstNode(schema, node.PathVar + "." + df.Name,
                        df.GetCollTableName(dcl.GetTableName()), dcl.GetTableName() + "_id", df.ColumnName,
                        df.Name);

                    AddColumn(sub, df, new DocumentSqlColumn(field, df, df.ColumnName));
                    AddColumn(node, df, new CollSqlColumn(sub, df.Name));
                }
            }
            else if (dm.GetModelType() == DModelType.MODEL)
            {
                DModel<object> dcl = df.DeclType;
                SqlCollAstNode sub = new SqlCollAstNode(schema, node.PathVar + "." + df.Name, dm.GetIndex(),
                    dm.GetTableName(), df.GetCollTableName(dcl.GetTableName()), dcl.GetTableName() + "_id",
                    df.ColumnName);
                AddReferenceField(sub, field.Selections, "");
                AddColumn(node, df, new RefCollSqlColumn(sub, df.Name));
            }
        }

        private void AddInverseCollectionField(SqlAstNode node, Field field, DField<object, object> df)
        {
            DModel<object> dm = df.Reference;
            if (dm.IsDocument())
            {
                AddColumn(node, df, new DocumentFlatSqlColumn(field, (DFlatField<object, object>) df));
            }
            else if (dm.IsEmbedded())
            {
                SqlInverseCollAstNode sub = new SqlInverseCollAstNode(schema, node.PathVar + "." + df.Name,
                    dm.GetIndex(), dm.GetTableName(), df.ColumnName);
                AddReferenceField(sub, field.Selections, "");
                AddColumn(node, df, new RefCollSqlColumn(sub, df.Name));
            }
            else if (dm.GetModelType() == DModelType.MODEL)
            {
                SqlInverseCollAstNode sub = new SqlInverseCollAstNode(schema, node.PathVar + "." + df.Name,
                    dm.GetIndex(), dm.GetTableName(), df.ColumnName);
                AddReferenceField(sub, field.Selections, "");
                AddColumn(node, df, new RefCollSqlColumn(sub, df.Name));
            }
        }

        private void AddReferenceField(SqlAstNode node, Field field, DField<object, object> df, string embeddedPrefix)
        {
            DModel<object> dm = df.Reference;
            if (dm.IsExternal())
            {
                return;
            }

            if (dm.IsDocument())
            {
                if (dm.IsCreatable())
                {
                    SqlAstNode sub = new SqlAstNode(schema, node.Path + "." + df.Name, dm.GetIndex(),
                        dm.GetTableName(), dm.IsEmbedded());

                    sub.AddColumn(dm, new DocumentCreatableSqlColumn(dm, field, null, embeddedPrefix + dm.GetTableName() + "_doc"));
                    AddColumn(node, df, new RefSqlColumn(sub, embeddedPrefix + df.ColumnName, df.Name, false));
                }
                else
                {
                    AddColumn(node, df, new DocumentSqlColumn(field, df, embeddedPrefix + df.ColumnName));
                }
            }
            else if (dm.IsEmbedded())
            {
                SqlAstNode sub = new SqlAstNode(schema, node.PathVar + "." + df.Name, dm.GetIndex(),
                    dm.GetTableName(), dm.IsEmbedded());
                DEmbField<object, object> emb = (DEmbField<object, object>) df;
                AddReferenceField(sub, field.Selections, emb.Prefix);
                AddColumn(node, df, new RefSqlColumn(sub, df.ColumnName, df.Name, false));
            }
            else if (dm.GetModelType() == DModelType.MODEL)
            {
                SqlAstNode sub = new SqlAstNode(schema, node.Path + "." + df.Name, dm.GetIndex(),
                    dm.GetTableName(), dm.IsEmbedded());
                AddReferenceField(sub, field.Selections, "");
                AddColumn(node, df, new RefSqlColumn(sub, embeddedPrefix + df.ColumnName, df.Name, false));
            }
        }

        public void AddCustomField(DModel<object> declType, SqlAstNode node, Field field, string path, string fieldName,
            DModel<object> dm, string idColumn)
        {
            SqlAstNode sub = new SqlAstNode(schema, node.Path + "." + fieldName, dm.GetIndex(), dm.GetTableName(),
                dm.IsEmbedded());
            AddReferenceField(sub, field.Selections, "");
            node.AddColumn(declType, new RefSqlColumn(sub, idColumn, fieldName, true));
        }

        private void AddColumn(SqlAstNode node, DField<object, object> df, ISqlColumn column)
        {
            node.AddColumn(df.DeclType, column);
        }

        private void AddReferenceField(SqlAstNode node, List<Selection> selections, string embeddedPrefix)
        {
            foreach (Selection selection in selections)
            {
                DModel<object> df = selection.Type;
                foreach (Field field in selection.Fields)
                {
                    AddField(node, field, df, embeddedPrefix);
                }
            }
        }

        private void AddPrimitiveField(SqlAstNode node, Field field, DField<object, object> df, string embeddedPrefix)
        {
            FieldPrimitiveType type = df.GetPrimitiveType();
            if (type == FieldPrimitiveType.Geolocation)
            {
                AddColumn(node, df, new GeoSqlColumn(embeddedPrefix + df.ColumnName, df.Name));
            }
            else
            {
                AddColumn(node, df, new SqlColumn(embeddedPrefix + df.ColumnName, df.Name));
            }
        }
    }
}
