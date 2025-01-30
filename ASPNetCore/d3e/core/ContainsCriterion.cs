namespace d3e.core
{
    public class ContainsCriterion : SimpleExpression
    {
        private object value;

        private bool ContainsOrNot = true;

        public ContainsCriterion(Criterion prop, Object value, bool containsOrNot) : base(prop) 
        {
            this.value = value;
            this.ContainsOrNot = containsOrNot;
        }

        public override string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public override string ToSql(Criteria criteria)
        {
            if(ContainsOrNot)
            {
                return prop.ToSql(criteria) + " contains (" + CreateArgument(value) + ")";
            }
            else
            {
                return prop.ToSql(criteria) + " not contains (" + CreateArgument(value) + ")";
            }
        }
    }
}
