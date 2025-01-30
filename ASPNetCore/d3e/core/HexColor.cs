using System.Drawing;
using System.Globalization;
using System.Text;

namespace d3e.core
{
    public class HexColor
    {
        public static Color FromHexStr(string hexString)
        {
            string hexcode = hexString != null && hexString.Length > 0 ? hexString : "00000000";
            StringBuilder sb = new StringBuilder();
            if (hexcode.Length == 6 || hexcode.Length == 7)
            {
                sb.Append("ff");
            }
            sb.Append(hexcode.Replace("#", ""));
            try
            {
                return Color.FromArgb(int.Parse(sb.ToString()), 0, 0, 0);
            }
            catch (FormatException e)
            {
                return Color.FromArgb(255, 255, 255, 0);
            }
        }

        public static Color FromHexInt(long hex1)
        {
            int hex = (int)hex1;
            int alpha = (hex >> 24) & 0xFF;
            int red = (hex >> 16) & 0xFF;
            int green = (hex >> 8) & 0xFF;
            int blue = hex & 0xFF;
            return Color.FromArgb(red, green, blue, alpha);
        }

        public static string toHexStr(Color color, bool leadingHasSign)
        {
            try
            {
                return (leadingHasSign ? "#" : "") + String.Format("%02X", color.A) +
                    String.Format("%02X", color.R) +
                    String.Format("%02X", color.G) +
                    String.Format("%02X", color.B);
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

        public static int ToHexInt(Color color) 
        {
            return int.Parse(color.ToString());
        }
    }
}
