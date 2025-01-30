namespace list
{
    public abstract class BaseObjectList
    {

        protected List<long> CollectIds(List<object[]> objects, int groupStart, int groupEnd, int index)
        {
            List<long> group = new List<long>();
            if (groupStart == groupEnd)
            {
                return group;
            }
            for (int x = groupStart; x < groupEnd; x++)
            {
                string ids = (string)objects[x][index];
                List<long> allIds = ids.Substring(1, ids.Length - 2)
                    .Split(',')
                    .Select(i => long.Parse(i.Trim()))
                    .ToList();
                group.AddRange(allIds);
            }
            return group;
        }

        protected List<T> CollectGroupByItems<T>(List<object[]> objects, int groupStart, int groupEnd, int groupColumn,
            GroupByInputMapper<T> itemMapper, object[] percentTotals)
        {
            List<T> group = new List<T>();
            if (groupStart == groupEnd)
            {
                return group;
            }
            object currentValue = objects[groupStart][groupColumn];
            int newStartX = groupStart;
            int newEndX = groupStart;
            for (int x = groupStart; x < groupEnd; x++, newEndX++)
            {
                object[] objArray = objects[x];
                // If prev and current are same, then continue
                if (objArray[groupColumn].Equals(currentValue))
                {
                    continue;
                }
                // Now read these items.
                group.Add(itemMapper.Map(objects, newStartX, newEndX, groupColumn + 1, currentValue, percentTotals));
                newStartX = newEndX;
                currentValue = objArray[groupColumn];
            }
            if (newStartX != newEndX)
            {
                group.Add(itemMapper.Map(objects, newStartX, newEndX, groupColumn + 1, currentValue, percentTotals));
            }
            return group;
        }

        public interface GroupByInputMapper<T>
        {
            T Map(List<object[]> objects, int groupStart, int groupEnd, int groupColumn, object currentValue, object[] percentTotals);
        }

    }
}
