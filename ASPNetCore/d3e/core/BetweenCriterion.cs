namespace d3e.core
{
    public class BetweenCriterion : SimpleExpression
    {
        private object lo;
        private object hi;

        public BetweenCriterion(PropertyExpression prop, object lo, object hi) : base(prop)
        {
            this.lo = lo;
            this.hi = hi;
        }

        
        public override string ToSql(Criteria criteria)
        {
            return prop.ToSql(criteria) + " between" + CreateArgument(lo) + " and" + CreateArgument(hi);
        }

        public override string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }
    }
}
