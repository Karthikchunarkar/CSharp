using System.Text;
using System.Text.RegularExpressions;

namespace classes
{
    public class EnvironmentHelper
    {
        private static readonly Regex Variables = new Regex(@"\{(.*?)\}");

        public static string GetEnvString(string str)
        {
            StringBuilder sb = new StringBuilder();
            MatchCollection matches = Variables.Matches(str);
            int lastIndex = 0;

            foreach (Match match in matches)
            {
                // Append the text before the match
                sb.Append(str.Substring(lastIndex, match.Index - lastIndex));

                // Extract the variable name
                string variableName = match.Groups[1].Value;

                // Get the environment variable value
                string value = Environment.GetEnvironmentVariable(variableName) ?? variableName;

                // Append the value of the environment variable
                sb.Append(value);

                // Update the last index
                lastIndex = match.Index + match.Length;
            }

            // Append any remaining text after the last match
            sb.Append(str.Substring(lastIndex));

            return sb.ToString();
        }
    }
}
