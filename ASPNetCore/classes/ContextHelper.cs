using store;

namespace classes
{
    public class ContextHelper
    {
    public static string CreateContext(object context)
        {
            if (context == null)
            {
                return "n:";
            }
            if (context is IList<object> listContext)
            {
                var result = listContext.Select(CreateContext).ToList();
                return "c:" + string.Join(",", result);
            }
            if (context is DatabaseObject dbObject)
            {
                return "m:" + dbObject.GetIdent();
            }
            if (context is string strContext)
            {
                return "s:" + strContext;
            }
            if (context is int intContext)
            {
                return "i:" + intContext;
            }
            if (context is long longContext)
            {
                return "l:" + longContext;
            }
            if (context is double doubleContext)
            {
                return "d:" + doubleContext;
            }
            if (context is bool boolContext)
            {
                return "b:" + boolContext.ToString();
            }

            throw new InvalidOperationException($"Invalid Context: {context.GetType()}");
        }

        public static object ExtractContext(string context)
        {
            if (context.StartsWith("c:"))
            {
                List<String> res = new List<String>();
                String[] split = context.Substring(2).Split(",");
                foreach (String s in split)
                {
                    res.Add(s);
                }
                return res;
            }
            if(context.StartsWith("m:"))
            {
                string ident = context.Substring(0);
                string[] split = ident.Split("-");
                return EntityHelperService.GetInstance().Get(split[0], long.Parse(split[1]));
            }
            if(context.StartsWith("s:"))
            {
                return context.Substring(2);
            }
            if(context.StartsWith("i:"))
            {
                return long.Parse(context.Substring(2));
            }
            if (context.StartsWith("l:"))
            {
                return long.Parse(context.Substring(2));
            }
            if(context.StartsWith("d:"))
            {
                return double.Parse(context.Substring(2));
            }
            if(context.StartsWith("b:"))
            {
                return bool.Parse(context.Substring(2));
            }
            if(context.StartsWith("n:"))
            {
                return null;
            }

            return null;
        }
    }
}
