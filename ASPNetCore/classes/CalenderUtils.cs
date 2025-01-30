namespace classes
{
    public class CalenderUtils
    {
        public CalenderUtils()
        {
            
        }

        public static long GetMonthNumber(string month)
        {
            switch (month)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November": 
                    return 11;
                case "December":
                    return 12;
                default:
                    return 0;
            }
        }

        public static string GetMonthName(long month)
        {
            switch ((int)month)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default: return string.Empty;
            }
        }

        public static List<DateOnly> PrepareCalenderData(DateOnly date)
        {
            DateTime firstDay = new DateTime(date.Year, date.Month, 1,0,0,0,0,0);
            var weekday= (int)firstDay.DayOfWeek == 0 ? 6 : (int)firstDay.DayOfWeek - 1;
            DateOnly firstDayDate = new DateOnly(firstDay.Year, firstDay.Month, firstDay.Day);
            return GetListofDates(firstDayDate, -weekday, 421 - weekday);
        }

        public static List<DateOnly> GetListofDates(DateOnly firstDay, long startwith, long endsWidth)
        {
            List<DateOnly> list = new List<DateOnly>();
            for(long i = startwith; i < endsWidth; i++)
            {
                DateOnly date = firstDay.AddDays((int)i);
                list.Add(date);
            }
            return list;
        }

        public static List<long> GetYearsList(long year, bool forward)
        {
            List<long> list_of_years = new List<long>();
            list_of_years.Add(year);
            if(forward)
            {
                for(long i = 1; i < 12; i++)
                {
                    list_of_years.Add(year + i);
                }
            } 
            else
            {
                for (long i = 1;i < 12; i++)
                {
                    list_of_years.Insert(0, year - i);
                }
            }

            return list_of_years;
        }

        public static string GetFormateDate(DateOnly date, string format)
        {
            if(date == null)
            {
                return "";
            }
            DateOnly date1 = date != null ? date : DateOnly.FromDateTime(DateTime.Now);
            if(format != null)
            {
                string formatedDate = date1.ToString(format);
                if(formatedDate != null)
                {
                    return formatedDate;
                }
            } else
            {
                date1.ToString();
            }

            return date1.ToString();
        }

        public static string GetFormatedTimeFromDateTime(DateTime date, string format)
        {
            DateTime date1 = date != null ? date : DateTime.Now;
            if (format != null)
            {
                string formatedTime = date1.ToString(format);
                if (formatedTime != null)
                {
                    return formatedTime;
                } else
                {
                    date1.ToString();
                }
            }
            return date1.ToString();
        }

        public static List<DateOnly> GetWeekDayDates(DateOnly date)
        {
            var weekDates = new List<DateOnly>();
            var monday = date.AddDays(-(int)(date.DayOfWeek == DayOfWeek.Sunday ? 6 : date.DayOfWeek - DayOfWeek.Monday));
            for (int i = 0; i < 7; i++)
            {
                weekDates.Add(monday.AddDays(i));
            }
            return weekDates;
        }
    }
}
