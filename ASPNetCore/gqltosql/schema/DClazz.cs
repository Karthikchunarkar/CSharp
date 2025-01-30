namespace gqltosql.schema
{
    public class DClazz
    {
        private string _name;
        private int _index;
        private DClazzMethod[] _methods;
        private Dictionary<string, DClazzMethod> _methodsByName;

        public DClazz(string name, int index, int msgCount)
        {
            this._name = name;
            this._index = index;
            this._methods = new DClazzMethod[msgCount];
            this._methodsByName = new Dictionary<string, DClazzMethod>();
        }

        public string Name
        { 
            get { return _name; }
            set { _name = value; }
        }

        public int Index
        { 
            get { return _index; }
            set { _index = value; }
        }

        public DClazzMethod[] Methods 
        {
            get { return this._methods; }
            set { this._methods = value; _methodsByName = value.ToDictionary(m => m.Name, m => m);
            }
        }

        public DClazzMethod GetMethod(int idx)
        {
            return _methods[idx];
        }

        public DClazzMethod GetMethod(string name)
        {
            return _methodsByName[name];
        }

        public void AddMethod(int index, DClazzMethod method)
        {
            this._methods[index] = method;
            this._methodsByName[method.Name] = method;
        }
    }
}
