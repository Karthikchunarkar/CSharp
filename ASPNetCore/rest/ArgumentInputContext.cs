
using GraphQL;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json.Linq;
using d3e.core;
using rest;
using store;
using static GraphQL.Parser.Value;

namespace rest
{
    public class ArgumentInputContext : GraphQLInputContext
    {
        private readonly IResolveFieldContext _context;
        private readonly JObject _variables;

        public ArgumentInputContext(IResolveFieldContext context, EntityHelperService helperService,
            Dictionary<long, object> inputObjectCache, Dictionary<string, DFile> files, JObject variables)
            : base(helperService, inputObjectCache, files)
        {
            _context = context;
            _variables = variables;
        }

        public virtual object ReadAny(string field)
        {
            if (_context.Arguments.TryGetValue(field, out var argumentValue))
            {
                // Return the resolved argument value (variables are already resolved by GraphQL.NET)
                return argumentValue;
            }

            return null;
        }

        public override bool Has(string field)
        {
            return _context.Arguments.ContainsKey(field);
        }

        public override T ReadRef<T>(string field, string type)
        {
            var obj = ReadAny(field);
            if (obj == null)
            {
                return default;
            }

            if (obj is Dictionary<string, object> objectValue)
            {
                var ctx = CreateContext(field);
                return ctx.ReadObject<T>(type, false);
            }
            else
            {
                return (T)ReadRef(HelperService.Get(type), ReadLong(field));
            }
        }

        public override T ReadUnion<T>(string field, string type)
        {
            throw new NotImplementedException();
        }

        public override List<T> ReadUnionColl<T>(string field, string type)
        {
            throw new NotImplementedException();
        }

        public override T ReadChild<T>(string field, string type)
        {
            var ctx = CreateContext(field);
            if (ctx == null)
            {
                return default;
            }

            return ctx.ReadObject<T>(type, true);
        }

        protected override GraphQLInputContext CreateContext(string field)
        {
            var any = ReadAny(field);
            if (any == null)
            {
                return null;
            }

            if (any is ObjectValue val)
            {
                return new ObjectValueInputContext(val, HelperService, InputObjectCache, Files, _variables);
            }
            else if (any is JObject jObject)
            {
                // TODO: schema
                return new JSONInputContext(jObject, HelperService, InputObjectCache, Files, null);
            }
            else
            {
                throw new Exception("Unknown value");
            }
        }

        public override long ReadLong(string field)
        {
            var any = ReadAny(field);
            if (any is long longValue)
            {
                return longValue;
            }

            if (any is int intValue)
            {
                return intValue;
            }

            if (any == null)
            {
                return 0;
            }

            return Convert.ToInt64(any);
        }

        public override string ReadString(string field)
        {
            var any = ReadAny(field);
            if (any == null)
            {
                return null;
            }

            if (any is string stringValue)
            {
                return stringValue;
            }

            if (any is Enum enumValue)
            {
                return enumValue.GetDisplayName();
            }

            return any.ToString();
        }

        public override long ReadInteger(string field)
        {
            var any = ReadAny(field);
            if (any is long longValue)
            {
                return longValue;
            }

            if (any is int intValue)
            {
                return intValue;
            }

            return 0;
        }

        public override bool ReadBoolean(string field)
        {
            var any = ReadAny(field);
            if (any == null)
            {
                return false;
            }

            if (any is bool boolValue)
            {
                return boolValue;
            }

            return Convert.ToBoolean(any);
        }

        public override double ReadDouble(string field)
        {
            var any = ReadAny(field);
            if (any == null)
            {
                return 0.0;
            }

            if (any is double doubleValue)
            {
                return doubleValue;
            }

            if (any is float floatValue)
            {
                return floatValue;
            }

            return Convert.ToDouble(any);
        }

        public override TimeSpan ReadDuration(string field)
        {
            throw new NotImplementedException();
        }

        public override DateTime ReadDateTime(string field)
        {
            throw new NotImplementedException();
        }

        public override DateOnly ReadDate(string field)
        {
            throw new NotImplementedException();
        }

        public override TimeOnly ReadTime(string field)
        {
            throw new NotImplementedException();
        }

        public override List<long> ReadLongColl(string field)
        {
            var any = ReadAny(field);
            if (any is JArray jArray)
            {
                return jArray.Select(token => token.ToObject<long>()).ToList();
            }

            throw new Exception("Unsupported");
        }

        public override List<long> ReadIntegerColl(string field)
        {
            var any = ReadAny(field);
            if (any is JArray jArray)
            {
                return jArray.Select(token => token.ToObject<long>()).ToList();
            }

            throw new Exception("Unsupported");
        }

        public override List<string> ReadStringColl(string field)
        {
            var any = ReadAny(field);
            if (any is JArray jArray)
            {
                return jArray.Select(token => token.ToString()).ToList();
            }

            throw new Exception("Unsupported");
        }

        public override List<T> ReadChildColl<T>(string field, string type)
        {
            throw new NotImplementedException();
        }

        public override List<T> ReadRefColl<T>(string field, string type)
        {
            throw new NotImplementedException();
        }

        public override List<T> ReadEnumColl<T>(string field)
        {
            throw new NotImplementedException();
        }

        public override List<DFile> ReadDFileColl(string field)
        {
            throw new NotImplementedException();
        }
    }
}
