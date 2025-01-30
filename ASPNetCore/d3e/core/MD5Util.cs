using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace d3e.core
{
    public class MD5Util
    {
        public static string Md5(List<string> str)
        {
            return Md5(string.Join(",", str));
        }

        public static string Md5Str(string str) 
        {
            return Md5(str);
        }

        public static string Md5(string str)
        {
            try
            {
                {
                    using (var md5 = MD5.Create())
                    {
                        // Compute the hash
                        byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

                        // Convert to hexadecimal string
                        StringBuilder hashText = new StringBuilder();
                        foreach (byte b in hashBytes)
                        {
                            hashText.Append(b.ToString("x2")); // Lowercase hexadecimal format
                        }

                        return hashText.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error generating MD5 hash", e);
            }
        }
        public static string EncodeHmacSHA256(string key, string data)
        {
            try
            {
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
                {
                    // Compute the HMAC
                    byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                    
                    // Convert to hexadecimal string
                    StringBuilder hashText = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        hashText.Append(b.ToString("x2")); // Lowercase hexadecimal format
                    }

                    return hashText.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
