using System.Text.RegularExpressions;

namespace d3e.core
{
    public class FieldValidation
    {
        public static bool IsEmail(string email)
        {
            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            Regex pattern = new Regex(emailRegex, RegexOptions.IgnoreCase);
            return pattern.IsMatch(email);
        }
    }
}
