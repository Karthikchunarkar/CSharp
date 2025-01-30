using classes;

namespace d3e.core
{
    public class AggregateExpression : Criterion
    {

        private ReportAggregateType agg;
        private PropertyExpression field;

        public AggregateExpression(ReportAggregateType agg, PropertyExpression field)
        {
            this.agg = agg;
            this.field = field;
        }

        public string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public string ToSql(Criteria criteria)
        {
            string sql = "agg";
            switch (agg)
            {
                case ReportAggregateType.Array:
                    break;
                case ReportAggregateType.Average:
                    sql = "avg";
                    break;
                case ReportAggregateType.Count:
                    sql = "Count";
                    break;
                case ReportAggregateType.Max:
                    sql = "max";
                    break;
                case ReportAggregateType.Min:
                    sql = "min";
                    break;
                case ReportAggregateType.None:
                    break;
                case ReportAggregateType.Percent:
                    break;
                case ReportAggregateType.Sum:
                    sql = "sum";
                    break;
                default:
                    return sql;

            }
            return sql + "(" + (field == null ? "*" : field.Select(criteria)) + ")";
        }
    }
}
