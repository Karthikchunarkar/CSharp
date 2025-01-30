namespace gqltosql.schema
{
    public class DClazzMethod
    {
        private string _name;
        private DParam[] _params;
        private int _index;
        private int _returnType = -1;
        private bool _returnColl;

        public DClazzMethod(string name, int index, DParam[] parameters)
        {
            _name = name;
            _index = index;
            _params = parameters;
        }

        public DClazzMethod(string name, int index, int numParams)
        {
            _name = name;
            _index = index;
            _params = new DParam[numParams];
        }

        public DClazzMethod(string name, int index, int returnType, bool returnColl, DParam[] parameters)
            : this(name, index, parameters)
        {
            _returnType = returnType;
            _returnColl = returnColl;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public DParam[] Params
        {
            get => _params;
            set => _params = value;
        }

        public void AddParam(int index, DParam param)
        {
            if (index >= 0 && index < _params.Length)
            {
                _params[index] = param;
            }
        }

        public int Index
        {
            get => _index;
            set => _index = value;
        }

        public bool HasReturnType => _returnType != -1;

        public int ReturnType
        {
            get => _returnType;
            set => _returnType = value;
        }

        public void SetReturnType(int returnType, bool returnColl)
        {
            _returnType = returnType;
            _returnColl = returnColl;
        }

        public bool IsReturnColl => _returnColl;
    }
}
