using gqltosql.schema;

namespace rest.ws
{
    public class TemplateClazz
    {
        private string _hash;

        private DClazz _clazz;
        private DClazzMethod[] _methods;
        private int[] _mapping;

        public string Hash { get { return _hash; } set { _hash = value; } }

        public DClazz Clazz { get { return _clazz; } set { _clazz = value; } }

        public DClazzMethod[] Methods { get { return _methods; } set { _methods = value; } }

        public int[] Mapping { get { return _mapping; } set { _mapping = value; } }

        public TemplateClazz(DClazz ch, int size)
        {
            Clazz = ch;
            this._methods = new DClazzMethod[size];
            if(ch != null)
            {
                this._mapping = new int[ch.Methods.Length];
                Array.Fill(_mapping, -1);
            }
        }

        public void AddMethod(int idx, DClazzMethod f)
        {
            this._methods[idx] = f;
            if(f != null)
            {
                this._mapping[f.Index] = idx;
            }
        }

        public int GetClientMethodIndex(int serverIdx)
        {
            return _mapping[serverIdx];
        }

        public override string? ToString()
        {
            return _clazz.ToString();
        }
    }
}
