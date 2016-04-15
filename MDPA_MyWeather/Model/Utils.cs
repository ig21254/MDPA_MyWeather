using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDPA_MyWeather.Model
{
    public class Utils
    {
        public static string FormatFullDateFromTicks(long date)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            time = time.AddSeconds(date).ToLocalTime();
            return time.ToString("dddd dd MMMM yyyy");
        }

        public static string GetTodayFullDate()
        {
            return DateTime.Now.ToString("dddd dd MMMM yyyy");
        }
    }
}
