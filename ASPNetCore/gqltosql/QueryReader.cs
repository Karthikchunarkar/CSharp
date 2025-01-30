

using com.sun.tools.@internal.ws.wsdl.document.schema;

namespace gqltosql
{
    public class QueryReader
    {

        private List<QueryTypeReader> _byType = new List<QueryTypeReader>();
        private int _id;
        private bool _embedded;
        private bool _file;

        public QueryReader(int index, bool embedded)
        {
            this._id = index;
            this._embedded = embedded;
        }

        public OutObject Read(object[] val, Dictionary<long, OutObject> byId) 
        {
            object[] row;
            if (val.GetType().IsArray)
            {
                row = val; 
            }
            else
            {
                row = new object[] {val};
            }
            long rowId = ReadId(row, this._id);
            if(rowId == null)
            {
                foreach (QueryTypeReader item in _byType)
                {
                    long id = ReadId(row, item.Id);
                    if(id != null)
                    {
                        rowId = id;
                        break;
                    }
                }
            }
            OutObject obj = null;
            if(rowId != null)
            {
                obj = byId[rowId];
            }
            if(obj == null)
            {
                obj = new OutObject();
            }
            if(rowId != null)
            {
                obj.Id = rowId;
            }
            ReadIntoObj(row, obj);
            if(_embedded)
            {
                if(obj.Length == 0)
                {
                    return null;
                }
            } 
            else
            {
                if(_file && rowId == null && !IsCollection())
                {
                    return null;
                }
            }
            return obj;
        }

        private bool IsCollection()
        {
            foreach (QueryTypeReader item in _byType)
            {
                if(item.Type == -1)
                {
                    return true;
                }
            }
            return false;   
        }

        private void ReadIntoObj(object[] row, OutObject obj)
        {
            foreach (QueryTypeReader item in row)
            {
                item.Read(row, obj);
            }
            OutObject dup = obj.GetDuplicate();
            if(dup != null)
            {
                ReadIntoObj(row, dup);
            }
        }

        private long ReadId(object[] row, int id)
        {
            if(id == -1)
            {
                return 0;
            }
            if (!(row[id] is long val))
            {
                return 0;
            }
            return (long)row[id];
        }

        public QueryTypeReader GetTypeReader(int type)
        {
            foreach (QueryTypeReader item in _byType)
            {
                if (item.Type == type)
                {
                    return item;
                }
            }
            //if (type == .DFile)
            //{
            //    file = true;
            //}
            //TODO Nikhil
            QueryTypeReader tr = new QueryTypeReader(type);
            _byType.Add(tr);
            return tr;
        }
    }
}
