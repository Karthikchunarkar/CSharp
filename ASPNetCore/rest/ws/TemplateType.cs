using d3e.core;
using gqltosql.schema;

namespace rest.ws
{
    public class TemplateType
    {
        private DModel<object> _model;
        private string _unknownName;
        private int _clientParent;
        private DField<object, object>[] _fields;
        private int[] _mapping;
        private String _hash;
        private int _parentServerCount;
        private int _parentClientCount;
        private TemplateType _parentType;
        public bool _valid = true;
        private bool _parentFieldCountDone = false;

        public TemplateType(DModel<object> model, int length)
        {
            this._model = model;
            this._fields = new DField<object, object>[length];
            if(_model != null)
            {
                this._mapping = new int[model.GetFields().Length];
                for(int x =0; x < this._mapping.Length; x++)
                {
                    this._mapping[x] = -1;
                }
                this._parentServerCount = model.GetParentCount();
            }
        }

        public string UnknowName { get { return _unknownName; } set { _unknownName = value; } }

        public int ParentClientCount
        {
            get { return _parentClientCount; }
            set { _parentClientCount = value; }
        }

        public string Hash { get { return _hash; } set { _hash = value; } }

        public void AddField(int idx, DField<object, object> field)
        {
            this._fields[idx] = field;
            if(_valid)
            {
                _mapping[field.Index - _parentServerCount] = idx;

            }
        }

        public DModel<object> Model
        {
            get { return _model; }

        }

        public DField<object, object>[] Fields { get { return _fields; } }

        public DField<object, object> GetField(int idx)
        {
            if (idx < _parentServerCount)
            {
                return  _parentType.GetField(idx);
            }
            int index = idx - _parentClientCount;
            if(index >= _fields.Length)
            {
                Log.Info("Invalid field index in " + this._model.GetType() + " index: " + idx);
            }
            return _fields[index];
        }

        public int ToClientIdx(int serverIdx)
        {
            if (serverIdx < _parentServerCount)
            {
                return _parentType.ToClientIdx(serverIdx);
            }
            return _mapping[serverIdx - _parentServerCount] + _parentClientCount;
        }

        public void SetParent(TemplateType parentType)
        {
            this._parentType = parentType;
            this._valid = parentType._valid;
        }

        public void ComputeParentFieldsCount()
        {
            if (_parentType == null)
            {
                return;
            }
            _parentType.ComputeParentFieldsCount();
            if (_parentFieldCountDone)
            {
                return;
            }
            this._parentClientCount = _parentType._fields.Length + _parentType._parentClientCount;
            _parentFieldCountDone = true;
        }

        public TemplateType ParentType { get { return _parentType; } }

        public override string ToString()
        {
            return this._model.ToString();
        }

        public void computeHash()
        {
            if (_hash != null)
            {
                return;
            }
            List<string> md5 = new List<string>();
            TemplateType parent = ParentType;
            if (parent != null)
            {
                parent.computeHash();
                md5.Add(parent.Hash);
            }
            if (_valid)
            {
                md5.Add(Model.GetType());
            }
            else
            {
                md5.Add(_unknownName);
            }
            foreach (var f in _fields)
            {
                md5.Add(f.Name);
            }
            String hash = MD5Util.Md5(md5);
            Hash = hash;
        }
    }
}
