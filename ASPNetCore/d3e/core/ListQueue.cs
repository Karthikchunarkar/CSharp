
namespace d3e.core
{
    public class ListQueue<E> : LinkedList<E>, IEnumerable<E>
    {
        public ListQueue() : base() 
        {
            
        }

        public ListQueue(int initialCapacity) : base()
        {
            
        }

        public ListQueue(IEnumerable<E> items): base(items) 
        {
            
        }

        public static ListQueue<E> From(IEnumerable<E> items)
        {
            return new ListQueue<E>(items);
        }

        public static ListQueue<E> Of(IEnumerable<E> items)
        {
            return new ListQueue<E>(items);
        }

        public ListQueue<R> Cast<R>()
        {
            return (ListQueue<R>)(object)this;            
        }

        public long Length()
        {
            return Count;
        }

        public E first()
        {
            return First.Value;
        }

        public E last()
        {
            return Last.Value;
        }

        public E Single()
        {
            if(Count != 1)
                throw new InvalidOperationException("Collection must contain exactly one element");
            return First.Value;
        }

        public E ElementAt(int index) 
        {
            return this.ElementAt(index);   
        }

        public List<E> ToList()
        {
            return ToList(true);
        }

        public List<E> ToList(bool v)
        {
            // TODO Nikhil
            return new List<E>(this);
        }

        public void AddAll(IEnumerable<E> items)
        {
            foreach(E item in items)
            {
                AddLast(item);
            }
        }

        public void RemoveWhere(Func<E, bool> test)
        {
            var nodesToRemove = new List<LinkedListNode<E>>();
            var current = First;

            while (current != null)
            {
                if (test(current.Value))
                    nodesToRemove.Add(current);
                current = current.Next;
            }

            foreach (var node in nodesToRemove)
            {
                Remove(node);
            }
        }

        public void RetainWhere(Func<E, bool> test)
        {
            RemoveWhere(x => !test(x));
        }
    }
}
