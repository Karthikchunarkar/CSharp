using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace rest
{
    [ApiController]
    [Route("api/native")]
    public class NativeGraphQL : AbstractQueryService
    {
        private readonly NativeQuery _query;
        private readonly NativeMutation _mutation;

        public NativeGraphQL(NativeQuery query, NativeMutation mutation)
        {
            _query = query;
            _mutation = mutation;
        }

        [HttpPost("graphql")]
        [Produces("application/json")]
        public async Task<string> Run([FromBody] string body)
        {
            var req = JObject.Parse(body);
            var operation = ParseOperation(req);
            var variables = req["variables"] as JObject;
            var fields = GetFields(operation);
            var operationName = operation.OperationDefinition.Operation.ToString().ToLower();

            string queryStr = req["query"]?.ToString();

            return operationName switch
            {
                "query" => await _query.ExecuteFields(queryStr, fields, variables),
                "mutation" => await _mutation.ExecuteFields(queryStr, fields, variables),
                _ => throw new InvalidOperationException($"Unsupported operation: {operationName}")
            };
        }
    }
}
