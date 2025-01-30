using System.Collections;

namespace store
{
    public class DBChange
    {
        public readonly BitArray Changes;
        public readonly Dictionary<int, object> OldValues;
        public readonly Action<bool> OnSet;
        public readonly Action OnUnset;
        public DBObject Old;

        public DBChange(int count, Action<bool> onSet, Action onUnset)
        {
            Changes = new BitArray(count);
            OldValues = new Dictionary<int, object>();
            OnSet = onSet;
            OnUnset = onUnset;
        }

        public void Merge(DBChange otherChanges)
        {
            Changes.Or(otherChanges.Changes);
            foreach (var kvp in otherChanges.OldValues)
            {
                OldValues[kvp.Key] = kvp.Value;
            }
        }

        public void Set(int field, object oldValue)
        {
            if (!Changes.Get(field))
            {
                Changes.Set(field, true);
                OldValues[field] = oldValue;
            }
        }

        public void Unset(int field, bool clear)
        {
            if (clear)
            {
                Changes.Set(field, false);
            }
            OldValues.Remove(field);
        }

        public bool IsEmpty()
        {
            return Changes.Cast<bool>().All(b => !b);
        }

        public void OnFieldChange(int fieldIdx, object oldValue, object newValue)
        {
            if (OldValues.TryGetValue(fieldIdx, out var old))
            {
                if (Equals(newValue, old))
                {
                    Unset(fieldIdx, true);
                    if (IsEmpty())
                    {
                        OnUnset();
                    }
                    return;
                }
            }
            else
            {
                Set(fieldIdx, oldValue);
            }
            OnSet(false);
        }

        public void OnCollFieldChange(int fieldIdx, object oldValue, object newValue)
        {
            if (!(OldValues.TryGetValue(fieldIdx, out var oldList) && oldList is List<object> lc))
            {
                lc = new List<object>((IEnumerable<object>)oldValue);
                Set(fieldIdx, lc);
            }
            else if (newValue != null)
            {
                var newList = (List<object>)newValue;
                if (lc.SequenceEqual(newList))
                {
                    bool hasChanges = CheckListForChanges(newList);
                    Unset(fieldIdx, !hasChanges);
                    if (!hasChanges)
                    {
                        OnUnset();
                    }
                    return;
                }
            }

            if (oldValue is D3EPersistanceList<object> pl && pl.IsInverse())
            {
                OnSet(true);
                return;
            }

            OnSet(false);
        }

        private bool CheckListForChanges(List<object> list)
        {
            return list.Any(x => x is DBObject o && !o._Changes().IsEmpty());
        }

        public void OnInvCollFieldChange(int field, object oldValue)
        {
            if (!OldValues.TryGetValue(field, out var oldList) || !(oldList is List<object> lc))
            {
                lc = new List<object>((IEnumerable<object>)oldValue);
                Set(field, lc);
            }
        }

        public void OnChildFieldChange(int childIdx, bool set)
        {
            if (set)
            {
                Changes.Set(childIdx, true);
                OnSet(false);
            }
            else if (!OldValues.ContainsKey(childIdx))
            {
                Changes.Set(childIdx, false);
                OnUnset();
            }
        }

        public void OnChildCollFieldChange(int childIdx, bool set, List<object> list)
        {
            if (set)
            {
                Changes.Set(childIdx, true);
                OnSet(false);
            }
            else if (!OldValues.TryGetValue(childIdx, out var old))
            {
                bool hasChanges = CheckListForChanges(list);
                if (!hasChanges)
                {
                    Changes.Set(childIdx, false);
                    OnUnset();
                }
            }
        }
    }
}
