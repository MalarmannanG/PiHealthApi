using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiHealth.Web.Helper
{
    public static class Helper
    {

        public static string GenerateULIDPrefix(long ulId, string prefix)
        {
            return string.Concat(prefix, String.Format("{0:0000000}", ulId));
        }

        public static string GenerateReportPrefix(long ReportId, string prefix)
        {
            return string.Concat(prefix, String.Format("{0:0000000}", ReportId));
        }

        public static string CalculateAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            return String.Format("{0} Year(s) {1} Month(s) {2} Day(s)",
            Years, Months, Days, Hours, Seconds);
        }
        
        public static DateTime NumberToDate(double? years = 0, double? months = 0, double? days = 0)
        {
            DateTime dateObj = DateTime.Now;
            years = years ?? 0;
            months = months ?? 0;
            days = days ?? 0;

            var month = int.Parse(months.Value.ToString());
            var year = int.Parse(years.Value.ToString());
            var day = int.Parse(days.Value.ToString());

            dateObj = dateObj.AddDays(-day).AddMonths(-month).AddYears(-year);

            return dateObj;
        }
    }
}
