namespace d3e.core
{
    public abstract class SimpleExpression : Criterion
    {
        protected Criterion prop;

        protected SimpleExpression(Criterion prop)
        {
            this.prop = prop;
        }

        protected string CreateArgument(object arg)
        {
            if (arg == null)
            {
                return "null";
            }
            if (arg is String val)
            {
                return "" + val + "'";
            }
            return arg.ToString() ?? "";
        }

        public abstract string Select(Criteria criteria);

        public abstract string ToSql(Criteria criteria);
    }
}
