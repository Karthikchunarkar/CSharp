namespace d3e.core
{
    public class MethodType : Type
    {
        public Type _on {  get => _on; set => _on = value; }

        public Type _gen { get => _gen; set => _gen = value; }

        public MethodType(Type on, string name, Type gen) : base(name)
        {
            this._on = on;
            this._gen = gen;
        }
    }
}
