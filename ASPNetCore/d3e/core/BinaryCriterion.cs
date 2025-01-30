namespace d3e.core
{
    public class BinaryCriterion : SimpleExpression
    {
        private string Op;
        private object Rval;


        public BinaryCriterion(string op, Criterion prop, object rval) : base(prop)
        {
            this.Op = op;
            this.Rval = rval;
        }

        public override string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public override string ToSql(Criteria criteria)
        {
            return prop.ToSql(criteria) + " " + Op + " " + CreateArgument(Rval);
        }
    }
}
