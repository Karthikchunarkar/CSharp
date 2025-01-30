using System.Linq;

namespace d3e.core
{
    public class MultiExpression : Criterion
    {
        private List<Criterion> _multi;

        private bool _all;

        public MultiExpression(List<Criterion> multi, bool all)
        {
            this._multi = multi;
            this._all = all;
        }

        public string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public string ToSql(Criteria criteria)
        {
            string join = _all ? "and" : "or";
            var expressions = _multi.Select(m => m.ToSql(criteria));
            return "(" + string.Join($" {join} ", expressions) + ")";
        }
    }
}
