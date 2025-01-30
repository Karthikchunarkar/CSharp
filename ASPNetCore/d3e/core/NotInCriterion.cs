namespace d3e.core
{
    public class NotInCriterion : SimpleExpression
    {
        private List<object> _List;

        private bool _InOrNotIn = true;

        public NotInCriterion(PropertyExpression prop, List<object> list) : base(prop) 
        {
             this._List = list;
        }
        public override string Select(Criteria criteria)
        {
            return ToSql(criteria);
        }

        public override string ToSql(Criteria criteria)
        {
            List<string> args = (List<string>) _List.Select(x => CreateArgument(x));
            if(_InOrNotIn)
            {
                return prop.ToSql(criteria) + " in (" + String.Join(", ", args) + ")";
            } 
            else
            {
                return prop.ToSql(criteria) + " not in (" + String.Join(", ", args) + ")";
            }
        }
    }
}
