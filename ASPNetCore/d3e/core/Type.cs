namespace d3e.core
{
    public class Type
    {
        private string _name;

        public Type(string name)
        {
            this._name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public static Type Find(string name)
        {
            return new Type(name == null ? "Object" : name);
        }

        public static Type Wrap(Type outer, List<Type> args)
        {
            return new WrappedType(outer, args);
        }

        public static Type MethodType(Type on, string name, Type gen)
        {
             return new MethodType(on, name, gen);
        }
    }
}
