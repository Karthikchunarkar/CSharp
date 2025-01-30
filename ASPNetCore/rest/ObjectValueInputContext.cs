using static GraphQL.Parser.Value;
using d3e.core;
using store;
using Newtonsoft.Json.Linq;
using GraphQL;
using static GraphQL.Parser.ParserAST.ValuePrimitive;

namespace rest
{
    public class ObjectValueInputContext : ArgumentInputContext 
    {
        private Dictionary<string, object> _value;

        public ObjectValueInputContext(ObjectValue value, EntityHelperService helperService,
            Dictionary<long, object> inputObjectCache, Dictionary<string, DFile> files, JObject variables) : base(null, helperService, inputObjectCache, files, variables)
        {
            _value = new Dictionary<string, object>();
            foreach (var field in value.Item)
            {
                _value[field.Key] = field.Value;
            }
        }

        public override object ReadAny(string field)
        {
            if (!_value.TryGetValue(field, out var v))
            {
                return null;
            }
            if (v is StringValue stringValue)
                return stringValue.IsStringValue;
            if (v is IntValue intValue)
                return intValue.IsIntValue;
            if (v is FloatValue floatValue)
                return floatValue.IsFloatValue;
            if (v is BooleanValue boolValue)
                return boolValue.IsBooleanValue;

            return v;
        }

        public override bool Has(string field)
        {
            return _value.ContainsKey(field);
        }
    }
}
