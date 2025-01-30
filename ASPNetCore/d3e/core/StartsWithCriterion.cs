namespace d3e.core
{
    public class StartsWithCriterion : SimpleExpression
    {
        private object _value;

        private bool _startOrEnd = true;

        public StartsWithCriterion(Criterion prop, object value, bool startOrEnd) : base(prop)
        {
            this._value = value;
            this._startOrEnd = startOrEnd;
        }

        public override string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public override string ToSql(Criteria criteria)
        {
            if (this._startOrEnd)
            {
                return prop.ToSql(criteria) + " like (" + "'" + _value + "%'" + ")";
            }
            else
            {
                return prop.ToSql(criteria) + " like (" + "'%" + _value + "'" + ")";
            }
        }
    }
}
