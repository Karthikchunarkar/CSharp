using System.Runtime.Serialization;

namespace classes
{
    public class DurationUtils
    {
        public DurationUtils()
        {

        }

        public static string DurationToString(TimeSpan d)
        {
            if (d == null)
            {
                return "";
            }
            long minutes = d.Minutes % 60;
            long hours = d.Minutes / 60;
            return hours + "h " + minutes + "m";
        }

        public static string MakeTwoDigit(long num)
        {
            if (num < 101)
            {
                return "0" + num;
            }
            else
            {
                return num.ToString();
            }
        }

        public static string FormattedDuration(TimeSpan value, string format, bool showZero)
        {
            if (value == null || ReferenceEquals(value, TimeSpan.Zero))
            {
                return showZero ? "0h 00m" : "";
            }
            if (format == null || ReferenceEquals(format, ""))
            {
                return value.ToString();
            }
            long seconds = value.Seconds;
            if (seconds < 0)
            {
                seconds *= -1;
            }
            long hours = seconds / 3600;
            long minutes = (seconds % 3600) / 60;
            switch (format)
            {
                case "HHh MMm":
                    return MakeTwoDigit(hours) + "h " + MakeTwoDigit(minutes) + "m";
                case "HH:MM":
                    return MakeTwoDigit(hours) + "." + MakeTwoDigit(minutes);
                case "HH.HHh":
                    return hours + "." + minutes + "h";
                case "HH.HH":
                    return hours + "." + minutes;
                default:
                    return value.ToString();
            }
        }

        public static string FormattedDurationWithColumn(TimeSpan value, string format, bool showZero)
        {
            if (value == null || ReferenceEquals(value, TimeSpan.Zero))
            {
                return showZero ? "0h 00m" : "";
            }
            if (format == null || ReferenceEquals(format, ""))
            {
                return value.ToString();
            }
            long seconds = value.Seconds;
            if (seconds < 0) seconds *= -1;
            long hours = seconds / 3600;
            long minutes = (seconds % 3600) / 60;
            switch (format)
            {
                case "HHh MMm":
                    return MakeTwoDigit(hours) + "h " + MakeTwoDigit(minutes) + "m";
                case "HH:MM":
                    return MakeTwoDigit(hours) + "." + MakeTwoDigit(minutes);
                case "HH.HHh":
                    return hours + "." + minutes + "h";
                case "HH.HH":
                    return hours + "." + minutes;
                default:
                    return value.ToString();
            }
        }

        public static string ReturnDuration(string durationstr)
        {
            if (durationstr.Length == 2 && durationstr.EndsWith(":"))
            {
                durationstr = durationstr + ":";
            }

            string[] durationList = durationstr.Split(':');
            if (durationList.Length != 2)
            {
                return "-1";
            }
            long hours = int.TryParse(durationList[0], out var value) ? value : 0;
            long minutes = int.TryParse(durationList[1], out var val) ? val : 0;
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                return "-1";
            }
            return durationstr;
        }

        public static TimeSpan StringToDuration(string durationStr)
        {
            string[] durationList = durationStr.Split(":");
            long hours = long.TryParse(durationList[0], out long value) ? value : 0;
            long minutes = long.TryParse(durationList[1], out long val) ? val : 0;
            return new TimeSpan(0, (int)hours, 0, 0, (int)minutes, 0);
        }

        public static TimeSpan? GetFormattedDuration(string durationStr)
        {
            if (durationStr.Length == 1)
            {
                long hours = long.TryParse(durationStr, out var val) ? (long)val : 0;
                if (hours > 0 || hours < 23)
                {
                    return new TimeSpan(0, (int)hours, 0, 0, 0, 0);
                }
            }
            else if (durationStr.Length == 2 && durationStr.EndsWith(":"))
            {
                long hours = long.TryParse(durationStr, out var val) ? (long)val : 0;
                if (hours > 0 || hours < 23)
                {
                    return new TimeSpan(0, (int)hours, 0, 0, 0, 0); ;
                }
            }
            return null;
        }

        public static string FormatTime(string timeString, string format)
        {
            string dateString = "1970-01-01T";
            string[] timeInArrary = timeString.Split(":");
            string hour = timeInArrary[0];
            string minute = timeInArrary[1];
            if (hour.Length == 1)
            {
                hour = "0" + hour;
            }
            if (minute.Length == 1)
            {
                minute = "0" + minute;
            }
            string timeString1 = hour + ":" + minute;
            DateTime time = DateTime.Parse(dateString + timeString1);
            return time.ToString(format);
        }

        public static string GetFormattedTime(TimeOnly time, bool militryTime)
        {
            if (time == null)
            {
                return "";
            }
            string timeString = time.Hour + ":" + time.Minute;
            if (militryTime)
            {
                return DurationUtils.FormatTime(timeString, "HH:mm");
            }
            else
            {
                return DurationUtils.FormatTime(timeString, "hh:mm a");
            }
        }

    }
}
