using gqltosql;

namespace rest.ws
{
    public class TemplateUsage
    {
        private string _hash;
        private UsageType[] _types;
        private Field _field;

        public TemplateUsage(UsageType[] types)
        {
            this._types = types;
        }

        public string Hash { get { return _hash; } set { _hash = value; } }

        public UsageType[] Types { get { return _types; } }

        public Field Field { get { return _field; } set { _field = value; } }
    }
}
