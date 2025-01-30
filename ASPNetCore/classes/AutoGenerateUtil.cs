using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace classes
{
    public class AutoGenerateUtil
    {
        public AutoGenerateUtil()
        {
            
        }

        private static readonly string REPLACE_REGEX = "\\W+";

        public static string GenerateIdentity(string src, string sanitizeWith, string prefix, string suffix)
        {
            return GenerateIdentity(src, sanitizeWith, prefix, suffix, null);
        }

        public static string GenerateIdentityLowerCase(string src, string sanitizeWith, string prefix, string suffix)
        {
            return GenerateIdentity(src, sanitizeWith, prefix, suffix, (str) => str.ToLower());
        }

        public static string GenerateIdentityUpperCase(string src, string sanitizeWith, string prefix, string suffix)
        {
            return GenerateIdentity(src, sanitizeWith, prefix, suffix, (str) => str.ToUpper());
        }

        public static string GenerateIdentityCamelCaseStartLower(String src, String sanitizeWith, String prefix, String suffix)
        {
            return uncapitalize(GenerateIdentityCamelCaseStartUpper(src, sanitizeWith, prefix, suffix));
        }

        private static string uncapitalize(string str)
        {
            int strLen;
            if (str == null || (strLen = str.Length) == 0)
            {
                return str;
            }

            StringBuilder stringBuilder = new StringBuilder(strLen).Append(char.ToLower(str[0])).Append(str.Substring(1));
            return stringBuilder.ToString();
        }

        private static string? GenerateIdentityCamelCaseStartUpper(string src, string sanitizeWith, string prefix, string suffix)
        {
            if(string.IsNullOrEmpty(src) || string.IsNullOrEmpty(suffix))
            {
                return null;
            }

            string joined = join(prefix, suffix, src);

            return string.Join(sanitizeWith,
                Regex.Split(joined, REPLACE_REGEX)
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => char.ToUpper(s[0]) + s.Substring(1).ToLower()));
        }

        private static string join(string prefix, string suffix, string payload)
        {
            StringBuilder sb = new StringBuilder();
            if(prefix != null)
            {
                sb.Append(prefix);
            }
            if (Regex.IsMatch(payload, "^[0-9]+$") && string.IsNullOrEmpty(prefix))
            {
                sb.Append("n");
            }
            sb.Append(payload);

            if(suffix != null)
            { sb.Append(suffix); }

            return sb.ToString();
        }

        private static string GenerateIdentity(string src, string sanitizeWith, string prefix, string suffix, Func<string, string> modResult)
        {
            if(src == null || sanitizeWith == null)
            {
                return null;
            }

            string sanitized = sanitize(src, sanitizeWith);

            string? sanitizedPrefix = null;

            if(prefix !=  null)
            {
                sanitizedPrefix = sanitize(prefix, sanitizeWith);
            }

            string? sanitizedSuffix = null;

            if (suffix != null)
            {
                sanitizedSuffix = sanitize(suffix, sanitizeWith);
            }

            string joined = join(sanitizedPrefix, sanitizedSuffix, sanitized);

            string modifed = modResult == null ? joined : modResult(joined);

            return modifed;
        }

        private static string sanitize(string str, string sanitizewith) 
        {   
               return str.Replace(REPLACE_REGEX, sanitizewith);
        }

        public static string GenerateNextSequenceString(long startsFrom, long step, string prefix, string suffix, string old)
        {
            if(prefix == null)
            {
                prefix = "";
            }
            if(suffix == null)
            {
                suffix = "";
            }
            if(old == null)
            {
                return startsFrom.ToString();
            }
            string stripped = old.Substring(prefix.Length);
            stripped = stripped.Substring(0, stripped.Length - suffix.Length);

            long oldInt = long.Parse(old);
            long newInt = GenerateNextSequence(startsFrom, step, oldInt, false);

            return new StringBuilder(prefix).Append(newInt).Append(suffix).ToString();
        }

        public static long GenerateNextSequence(long startsFrom, long step, long old, bool hasOld)
        {
            if(!hasOld)
            {
                return startsFrom;
            }
            return old + step;
        }

        public static string toName(string str)
        {
            string[] pieces = SplitCamelCaseString(str);
            if (pieces.Length > 0)
            {
                pieces[0] = char.ToUpper(pieces[0][0]) + pieces[0].Substring(1);
                for (int i = 1; i < pieces.Length; i++)
                {
                    pieces[i] = pieces[i].ToLower();
                }
            }
            return string.Join(" ", pieces);
        }

        private static string[] SplitCamelCaseString(string str)
        {
            var pieces = new List<string>();
            int i = 0;
            int len = str.Length;
            int prevType = -1;
            var sb = new StringBuilder();

            while (i < len)
            {
                char currentChar = str[i];
                var type = char.GetUnicodeCategory(currentChar);
                bool upperNextToLower = prevType == (int)UnicodeCategory.LowercaseLetter &&
                                      type == UnicodeCategory.UppercaseLetter;

                if (prevType == -1 || type == (UnicodeCategory)prevType || !upperNextToLower)
                {
                    sb.Append(currentChar);
                }
                else if (upperNextToLower)
                {
                    pieces.Add(sb.ToString());
                    sb.Clear();
                    sb.Append(currentChar);
                }

                prevType = (int)type;
                i++;
            }

            if (sb.Length != 0)
            {
                pieces.Add(sb.ToString());
            }

            return pieces.ToArray();
        }

        public static string GenerateUUID(string prefix)
        {
            return prefix + Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
