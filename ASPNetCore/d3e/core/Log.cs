
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using store;

namespace d3e.core
{
    public class Log
    {

        private static ILoggerFactory LOG = LoggerFactory.Create(b => b.AddConsole());

        private static bool DISTABLE_LOG;

        public static bool _showSql { get => _showSql; set => _showSql = value; }

        public static bool _showGraphql { get => _showGraphql; set => _showGraphql = value; }

        public static void Info(string msg)
        {
            if(DISTABLE_LOG)
            {
                Console.Error.WriteLine(msg);
            }
            else
            {
                Log.Info(msg);
            }
        }

        public static void Debug(string msg)
        {
            if(DISTABLE_LOG)
            {
                Console.Error.WriteLine(msg);
            }
            else
            {
                Log.Debug(msg);
            }
        }

        public static void Error(string msg)
        {
            if(DISTABLE_LOG)
            {
                Console.Error.WriteLine(msg);
            } 
            else
            {
                Log.Error(msg);
            }
        }

        public static void PrintStackTrace(Exception e)
        {
            if(DISTABLE_LOG)
            {
                Console.Error.WriteLine(e.GetType().FullName + ": " + e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }
            else
            {
                Log.Error(e.Message);
            }
        }

        public static void DisplaySql(string path, string sql, HashSet<long> ids)
        {
            if(!Log._showSql )
            {
                return;
            }

            bool hasPath = !String.IsNullOrEmpty(path);
            bool hasSql = !String.IsNullOrEmpty(sql);
            bool hasIds = ids != null && !ids.IsNullOrEmpty();
            
            if(!hasPath && !hasSql && !hasIds)
            {
                return ;
            }
            StringBuilder sb = new StringBuilder("GQL TO SQL: \n");
            Console.WriteLine();
            Console.WriteLine("*** SQL ***");
            if(hasPath)
            {
                sb.Append("Path: " + path);
            }
            if(hasSql)
            {
                sb.Append("Execute SQL:" + sql);
            }
            if(hasIds)
            {
                sb.Append("Ids:" + ids);
            }
            SQL(sb.ToString());
        }

        private static void SQL(string sql)
        {
            if(!Log._showSql)
            {
                return;
            }
            if(DISTABLE_LOG)
            {
                Console.Error.WriteLine(sql);
            }
            else
            {
                Log.Info(sql);
            }
        }

        public static void DisplaySql(string sql, Dictionary<string, object> values)
        {
            if(!Log._showSql)
            {
                return;
            }

            bool hasSql = !String.IsNullOrEmpty(sql);
            bool hasParams = !values.IsNullOrEmpty();
            if(!hasSql && !hasParams)
            {
                return;
            }
            StringBuilder b = new StringBuilder("DQ SQL: ");
            if (hasSql)
            {
                b.Append("\nExecute SQL: " + sql);
            }
            if (hasParams)
            {
                b.Append("\nParameters: " + values);
            }
            SQL(b.ToString());
        }

        public static void DisplayGraphQL(String field, String op, JsonObject variables)
        {
            if (!Log._showGraphql)
            {
                return;
            }
            bool hasOp = !String.IsNullOrEmpty(op);
            bool hasVars = variables != null && variables.Count > 0;
            if (!hasOp && !hasVars)
            {
                return;
            }
            StringBuilder b = new StringBuilder("GraphQl: \n");
            if (hasOp)
            {
                b.Append("Operation: " + op);
            }
            if (hasVars)
            {
                b.Append("Variables: " + variables);
            }
            SQL(b.ToString());
        }

        public static void Query(string sql)
        {
            // TODO Nikhil
        }

        internal static void Query(string sql, Query query)
        {
            throw new NotImplementedException();
        }
    }
}
