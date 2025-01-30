namespace d3e.core
{
    public class ParamExpression : Criterion
    {
        private readonly string _param;

        public ParamExpression(string param)
        {
            this._param = param;
        }

        public string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public string ToSql(Criteria criteria)
        {
            return "?";
        }
    }
}
