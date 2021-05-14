using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.UTILITIES.DateTimeHandel
{
    public class DateTimeManagement
    {
        public static string GetMonthName(string MonthNameProperty)
        {
            try
            {
                string[] DateParts = MonthNameProperty.Split(new char[] { '/' });
                var MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateParts[0]));
                return MonthName;
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }
    }
}
