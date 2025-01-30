namespace d3e.core
{
    public class SetBuilder<T>
    {
        private readonly HashSet<T> _set = new HashSet<T>();

        public SetBuilder() { }

        public SetBuilder<T> Add(T item)
        {
            _set.Add(item);
            return this;
        }

        public SetBuilder<T> Spread<TE>(IEnumerable<TE> items) where TE : T
        {
            foreach (var item in items)
            {
                _set.Add(item);
            }
            return this;
        }

        public SetBuilder<T> IfThenElse(bool condition, Action<SetBuilder<T>> then, Action<SetBuilder<T>> elseAction)
        {
            if (condition)
            {
                then(this);
            }
            else
            {
                elseAction(this);
            }
            return this;
        }

        public SetBuilder<T> IfThen(bool condition, Action<SetBuilder<T>> then)
        {
            if (condition)
            {
                then(this);
            }
            return this;
        }

        public SetBuilder<T> ForItems<TC>(IEnumerable<TC> items, Action<SetBuilder<T>, TC> then)
        {
            foreach (var item in items)
            {
                then(this, item);
            }
            return this;
        }

        public HashSet<T> Build()
        {
            return _set;
        }
    }
}
