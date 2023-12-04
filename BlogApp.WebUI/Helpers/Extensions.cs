using System.Text;
using System.Text.RegularExpressions;

namespace BlogApp.WebUI.Helpers
{
	public static class Extensions
	{
        public static string ToFormattedInt(this int value)
        {
            return string.Format("{0:n0}", value);
        }

        public static string ToFormattedDecimal(this decimal value)
        {
            return value.ToString("#,##0.00");
        }

        public static string StripHtml(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            return Regex.Replace(value, "<.*?>", String.Empty);
        }

        public static string SafeCrop(this string value,int length)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            if (value.Length<length)
            {
                return value;
            }
            return value.Substring(0,length)+"...";
        }
    }
}
