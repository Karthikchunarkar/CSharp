namespace d3e.core
{
    public class ListBuilder<T>
    {
        public List<T> _list = new List<T>(0);
        public ListBuilder() { }

        public ListBuilder<T> add(T item)
        {
            this._list.Add(item);
            return this;
        }

        public ListBuilder<T> Spread<TElement>(IEnumerable<T> items) where TElement : T
        {
            foreach (var item in items) 
            {
                this._list.Add(item);
            }
            return this;
        }

        public ListBuilder<T> IfThenElse(bool value, Action<ListBuilder<T>> then, Action<ListBuilder<T>> andElse)
        {
            if(value)
            {
                then.Invoke(this);
            } else
            {
                andElse.Invoke(this);
            }
            return this;
        }

        public ListBuilder<T> IfThen(bool value , Action<ListBuilder<T>> then)
        {
            if(value)
            {
                then.Invoke(this);
            }
            return this;
        }

        public ListBuilder<T> ForItems<C>(IEnumerable<C> items, Action<ListBuilder<T>, C> then)
        {
            foreach (var item in items)
            {
                then(this, item);
            }
            return this;
        }

        public List<T> Build()
        {
            return _list;
        }
    }
}
