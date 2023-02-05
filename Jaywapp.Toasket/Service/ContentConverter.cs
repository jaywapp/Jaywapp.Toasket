using System;
using System.Linq;

namespace Jaywapp.Toasket.Service
{
    public static class ContentConverter
    {
        public static DateTime ConvertDateTime(string content, int year)
        {
            var splited = content.Split(new char[] { '.', ' ', '(', ')', ':', }, StringSplitOptions.RemoveEmptyEntries);

            var month = int.Parse(splited[0]);
            var day = int.Parse(splited[1]);
            var hour = int.Parse(splited[3]);
            var minute = int.Parse(splited[4]);

            return new DateTime(year, month, day, hour, minute, 0);
        }

        public static bool TryConvertDateTime(string content, int year, out DateTime date)
        {
            date = default;

            var splited = content.Split(new char[] { '.', ' ', '(', ')', ':', }, StringSplitOptions.RemoveEmptyEntries);

            if (splited.Count() != 5
                || !int.TryParse(splited[0], out int month)
                || !int.TryParse(splited[1], out int day)
                || !int.TryParse(splited[3], out int hour)
                || !int.TryParse(splited[4], out int minute))
                return false;

            date = new DateTime(year, month, day, hour, minute, 0);
            return true;
        }
    }
}
