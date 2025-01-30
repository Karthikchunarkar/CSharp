namespace d3e.core
{
    public class DateNowExpression : Criterion
    {
        public string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public string ToSql(Criteria criteria)
        {
            return "now()";
        }
    }
}
