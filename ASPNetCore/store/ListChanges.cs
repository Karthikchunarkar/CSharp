using System.Collections;
using System.Linq;

namespace store
{
    public class ListChanges
    {
        public enum ChangeType
        {
            Added,
            Removed
        }

        public class Change
        {
            public ChangeType Type { get; }
            public object Obj { get; }
            public int Index { get; }

            public Change(ChangeType type, int index, object obj)
            {
                Type = type;
                Index = index;
                Obj = obj;
            }

            public override string ToString()
            {
                return $"{Type} at {Index} : {Obj}";
            }
        }

        private IList Old { get; }
        private List<Change> CompiledResult { get; set; }

        public ListChanges(IList old)
        {
            Old = old;
        }

        public List<Change> Compile(IList list)
        {
            if (CompiledResult != null)
            {
                return CompiledResult;
            }

            CompiledResult = new List<Change>();
            int x = 0;
            int xCount = list.Count;
            int y = 0;
            int yCount = Old.Count;
            int lookAhead = 1;

            while (x < xCount)
            {
                object xobj = list[x];
                if (y == yCount)
                {
                    CompiledResult.Add(new Change(ChangeType.Added, x, xobj));
                }
                else
                {
                    int temp = 0;
                    bool found = false;
                    while (temp <= lookAhead && (y + temp) < yCount)
                    {
                        object yobj = Old[y + temp];
                        if (Equals(xobj, yobj))
                        {
                            found = true;
                            while (temp > 0)
                            {
                                temp--;
                                yobj = Old[y + temp];
                                CompiledResult.Add(new Change(ChangeType.Removed, x, yobj));
                            }
                            y++;
                            break;
                        }
                        temp++;
                    }
                    if (!found)
                    {
                        CompiledResult.Add(new Change(ChangeType.Added, x, xobj));
                    }
                }
                x++;
            }

            while (y < yCount)
            {
                object yobj = Old[y];
                CompiledResult.Add(new Change(ChangeType.Removed, x, yobj));
                y++;
            }

            return CompiledResult;
        }

        public IList GetOld()
        {
            return Old;
        }
    }
}
