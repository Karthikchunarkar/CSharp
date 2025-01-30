namespace d3e.core
{
    public class InCriterion : SimpleExpression
    {
        private List<object> _list;

        public InCriterion(PropertyExpression prop, List<object> list) : base(prop) 
        {
            this._list = list;
        }

        public override string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public override string ToSql(Criteria criteria)
        {
            List<string> strings = _list.Select(a => CreateArgument(a)).ToList();
            return prop.ToSql(criteria) + " in (" + String.Join(", ", strings) + ")";
        }
    }
}
