namespace d3e.core
{
    public class IsCriterion : SimpleExpression
    {
        private object _value;

        private bool IsOrNot = true;

        public IsCriterion(Criterion prop, object value, bool isOrNot) : base(prop)
        {
            this._value = value;
            this.IsOrNot = isOrNot;
        }

        public override string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public override string ToSql(Criteria criteria)
        {
            if(IsOrNot)
            {
                return prop.ToSql(criteria) + " is " + CreateArgument(criteria);
            }
            else
            {
                return prop.ToSql(criteria) + " is not " + CreateArgument(criteria);
            }
        }
    }
}
