namespace d3e.core
{
    public class WrappedType : Type
    {
        private List<Type> _subs;

        private Type _outer;

        public WrappedType(Type outer, List<Type> subs) : base(outer.GetName())
        {
            this._subs = subs;
            this._outer = outer;
        }

        public List<Type> GetSubs() 
        {
            return _subs;
        }

        public void SetSubs(List<Type> subs)
        {
            _subs = subs;
        }

        public Type GetOuter()
        {
            return _outer;   
        }

        public void SetOuter(Type outer) 
        {
            _outer = outer;
        }
    }
}
